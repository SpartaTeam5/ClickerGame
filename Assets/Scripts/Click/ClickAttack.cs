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
    public float autoAttackInterval;
    public bool isAutoAttackEnabled = false; // �ʱ� �ڵ����� ��Ȱ��ȭ
    [SerializeField] public float criticalPercent;
    [SerializeField] public float criticalMultiplier;
    [SerializeField] MonsterData[] pokemonMonster;

    public PlayerInput playerInput;
    public bool isOptionUIOpen = false;
    public Monster monster;

    private InputAction _clickAction;
    private Particle particleEffect;
    private bool lastAttCriticalCheck;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        _clickAction = playerInput.actions["ClickAtt"];
    }

    private void Start()
    {
        particleEffect = FindObjectOfType<Particle>();
        monster = FindObjectOfType<Monster>();
        click_Damage = StatManager.Instance.GetFinalDamage();

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
                StartCoroutine(AutoAttack());
            }
        }
    }

    private void Update()
    {
        if (isOptionUIOpen) // �ɼ� â�� ������ �Է� ��Ȱ��ȭ �� ���� ����
        {
            _clickAction.performed -= OnClickAttack;
            _clickAction.Disable();

            StopCoroutine(AutoAttack());
        }
    }

    public bool IsCriticalHit()
    {
        return Random.value < criticalPercent;
    }

    public void OnClickAttack(InputAction.CallbackContext context)
    {
        click_Damage = StatManager.Instance.GetFinalDamage();

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

    IEnumerator AutoAttack()
    {
        while (true)
        {
            if (isAutoAttackEnabled && !isOptionUIOpen)
            {
                AttackMonster(click_Damage);
            }
            yield return new WaitForSeconds(autoAttackInterval);
        }
    }

    public void AttackMonster(float damage)
    {
        lastAttCriticalCheck = Random.value < criticalPercent;
        float finalDamage = lastAttCriticalCheck ? damage * criticalMultiplier : damage;
        Debug.Log(lastAttCriticalCheck ? "ġ��Ÿ" : "�Ϲ� ����");

        Monster monster = FindObjectOfType<Monster>();
        if (monster != null)
        {
            monster.TakeDamage(finalDamage);
        }

        Particle particleEffect = FindObjectOfType<Particle>();

        if (particleEffect != null)
        {
            particleEffect.PlayParticleSystem(lastAttCriticalCheck);
        }
    }

    public void RestartAutoAttack()
    {
        // ��� �ڷ�ƾ ���� �� �ڵ� ���� �ڷ�ƾ �����
        StopAllCoroutines();
        // �ɼ� UI�� �������� ���� ��츸 �ڵ� ���� ����
        if (!isOptionUIOpen && isAutoAttackEnabled)
        {
            StartCoroutine(AutoAttack());
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
