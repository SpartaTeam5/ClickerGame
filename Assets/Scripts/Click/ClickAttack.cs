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
    [SerializeField] float click_Damage = 1.0f;
    [SerializeField] float autoAttackInterval = 1.0f;
    [SerializeField] bool isAutoAttackEnabled = true;
    [SerializeField] MonsterData[] pokemonMonster;

    private Camera mainCamera;
    public PlayerInput playerInput;
    private InputAction _clickAction;
    public bool isOptionUIOpen = false;

    public Monster monster;

    private void Awake()
    {
        mainCamera = Camera.main; // ������� ī�޶� ��������
        playerInput = GetComponent<PlayerInput>();
        _clickAction = playerInput.actions["ClickAtt"];
        
    }

    private void Start()
    {
        monster = FindObjectOfType<Monster>();
        if (monster == null)
        {
            Debug.LogError("Monster ������Ʈ�� ã�� �� �����ϴ�!");
            return;
        }

        Debug.Log($"[DEBUG] monster ��: {monster}");

        if (!isOptionUIOpen)
        {
            _clickAction.performed += OnClickAttack;
            _clickAction.Enable();

            StartCoroutine(AutoAttack()); // �ڵ� ���� ����
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


    public void OnClickAttack(InputAction.CallbackContext context)
    {
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
                Debug.Log("UI ���� Ŭ��! ����");
                AttackMonster(click_Damage);
                return;
            }

            Debug.Log("ui Ŭ�� ���� ���� �ȵ�");
            return;
        }
        Debug.Log("���͸� Ŭ������ ����");

    }

    IEnumerator AutoAttack()
    {
        while (true)
        {
            if(isAutoAttackEnabled && !isOptionUIOpen)
            {
                AttackMonster(click_Damage);
            }
            yield return new WaitForSeconds(autoAttackInterval);
        }
    }

    public void AttackMonster(float damage)
    {
        if (pokemonMonster != null)
        {
            monster.TakeDamage(damage);
            Debug.Log($"���͸� {damage}��ŭ ����! ���� ü��: {monster.GetPercentage() * 100}%");
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
