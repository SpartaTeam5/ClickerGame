using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems; // ui ������ ���� �ʿ�
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ClickAttack : MonoBehaviour
{
    [SerializeField] float click_Damage;
    private float autoAttackInterval = 1f;
    public float autaAttackCycle;
    public bool isAutoAttackEnabled = false; // �ʱ� �ڵ����� ��Ȱ��ȭ
    [SerializeField] public float criticalPercent;
    [SerializeField] public float criticalMultiplier;

    private WeaponDataTable weaponData; // ���� ������ ȣ��

    public PlayerInput playerInput;
    public bool isOptionUIOpen = false;
    public Monster monster;

    private InputAction _clickAction;
    private Particle particleEffect;
    private bool lastAttCriticalCheck;

    private Coroutine autoCoroutine;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        _clickAction = playerInput.actions["ClickAtt"];
    }

    private void Start()
    {
        particleEffect = FindObjectOfType<Particle>();
        monster = FindObjectOfType<Monster>();
        weaponData = GameManager.Instance.weaponDataTable;
        UpdateStat();

        if (monster == null)
        {
            return;
        }

        if (!isOptionUIOpen)
        {
            _clickAction.performed += OnClickAttack;
            _clickAction.Enable();

            // �ڵ����� ������ 1 �̻��� ���� �ڵ����� ����
            if (isAutoAttackEnabled)
            {
                autoCoroutine = StartCoroutine(AutoAttack());
            }
        }
    }

    private void Update()
    {
        if (isOptionUIOpen) // �ɼ� â�� ������ �Է� ��Ȱ��ȭ �� ���� ����
        {
            _clickAction.performed -= OnClickAttack;
            _clickAction.Disable();

            StopCoroutine(autoCoroutine);
        }
    }

    public void UpdateStat()
    {
        click_Damage = StatManager.Instance.GetFinalDamage();
        autaAttackCycle = StatManager.Instance.GetAutoDamage(); // �ʴ� ���� Ƚ���� ��ȯ
        criticalPercent = StatManager.Instance.GetCriticalChance();
        criticalMultiplier = StatManager.Instance.GetCriticalDamage();
    }

    public void OnClickAttack(InputAction.CallbackContext context)
    {
        UpdateStat();

        if (isOptionUIOpen)
        {
            Debug.Log("�ɼ�ui�� �������� ���ݺҰ�");
            return;
        }

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            // UI��� ������ Ŭ���Ǿ����� Ȯ���ϴ� �Լ�,EventSystem.current �� �����ϸ� UI Ŭ�� ���� ����
            GameObject clickMonterUI = GetClickMonsterUI(Mouse.current.position.ReadValue());
            if (clickMonterUI != null)
            {
                AttackMonster(click_Damage);
                return;
            }
            return;
        }
    }
    public void ApplyStatsToClickAttack()
    {
        int autoLevel = GameManager.Instance.player.autoLevel;
        int critLevel = GameManager.Instance.player.critLevel;
        if (autoLevel > 0) // �ڵ����� ������ 1 �̻��� ���� �ڵ����� Ȱ��ȭ
        {
            AutoAttackData auto = GameManager.Instance.playerStatTable.auto[autoLevel - 1];
            autaAttackCycle = auto.autoAttackCycle; // �ڵ����� �ֱ� ����
            isAutoAttackEnabled = true; // �ڵ����� Ȱ��ȭ
            RestartAutoAttack(); // �ڵ����� �����
        }
        else
        {
            isAutoAttackEnabled = false; // �ڵ����� ��Ȱ��ȭ
        }

        CritData crit = GameManager.Instance.playerStatTable.crit[critLevel - 1];
        criticalPercent = crit.critChance; // ġ��Ÿ Ȯ�� ����
        criticalMultiplier = crit.critDamage; // ġ��Ÿ ������ ����
    }

    public IEnumerator AutoAttack()    // �ڵ� ����
    {
        UpdateStat();
        while (true)
        {
            if (isAutoAttackEnabled && !isOptionUIOpen)
            {
                AttackMonster(click_Damage);
            }
            yield return new WaitForSeconds(autoAttackInterval / autaAttackCycle);
        }
    }

    public void AttackMonster(float damage)
    {
        if (monster.isDie == false)
        {
            UpdateStat();

            float critChance = weaponData.critChance / 100f; // ���� ġ��Ÿ Ȯ��
            lastAttCriticalCheck = Random.value < critChance; // ġ��Ÿ Ȯ�� ���

            float finalDamage = lastAttCriticalCheck ? damage * criticalMultiplier : damage;
            Debug.Log($"{weaponData.critChance}");
            Debug.Log(lastAttCriticalCheck ? "ġ��Ÿ" : "�Ϲ� ����");

            if (monster != null)
            {
                monster.TakeDamage(finalDamage);

                float clickGold = finalDamage * Random.value; // ���ݷ��� ���������� ��°�� ����
                GameManager.Instance.AddGold(clickGold);
            }

            Particle particleEffect = FindObjectOfType<Particle>();

            if (particleEffect != null)
            {
                particleEffect.PlayParticleSystem(lastAttCriticalCheck);
            }
        }
    }

    public void RestartAutoAttack()
    {
        // ��� �ڷ�ƾ ���� �� �ڵ� ���� �ڷ�ƾ �����
        if (autoCoroutine != null)
        {
            StopCoroutine(autoCoroutine);
        }
        // �ɼ� UI�� �������� ���� ��츸 �ڵ� ���� ����
        if (!isOptionUIOpen && isAutoAttackEnabled)
        {
            autoCoroutine = StartCoroutine(AutoAttack());
        }
    }

    
    private GameObject GetClickMonsterUI(Vector2 screenPosition)
    {
        EventSystem eventSystem = EventSystem.current ?? FindObjectOfType<EventSystem>();
        if (eventSystem == null) return null; // EventSystem�� ������ null ��ȯ
                                              // EventSystem�� ã��, ������ null�� ��ȯ

        PointerEventData eventData = new PointerEventData(eventSystem)
        {
            // PointerEventData �ν��Ͻ��� �����ϸ鼭 ȭ�� ��ġ ����
            position = screenPosition // Ŭ���� ȭ�� ��ġ�� ����
        };

        List<RaycastResult> results = new List<RaycastResult>();
        // ����ĳ��Ʈ ����� ������ ����Ʈ

        GraphicRaycaster uiRaycaster = FindObjectOfType<GraphicRaycaster>();
        // ������ GraphicRaycaster ������Ʈ�� ã�� (UI ����ĳ��Ʈ �뵵)

        if (uiRaycaster == null) return null;
        // ���� GraphicRaycaster�� ���ٸ�, null�� ��ȯ (UI�� ���ٴ� �ǹ�)

        uiRaycaster.Raycast(eventData, results);
        // ����ĳ��Ʈ�� �����Ͽ� ����� results ����Ʈ�� ����

        foreach (var result in results)
        // ����ĳ��Ʈ ����� �ݺ��ϸ鼭 "Monster" �±װ� ���� GameObject�� ã��
        {
            if (result.gameObject.CompareTag("Monster"))
            {
                return result.gameObject;
            }
        }
        return null;
    }
}
