using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems; // ui ������ ���� �ʿ�
using UnityEngine.InputSystem;

public class ClickAttack : MonoBehaviour
{
    [SerializeField] float click_Damage = 1.0f;
    [SerializeField] float autoAttackInterval = 1.0f;
    [SerializeField] bool isAutoAttackEnabled = true;
    [SerializeField] Monster pokemonMonster;

    private Camera mainCamera;
    public InputAction _clickAtt;
    public bool isOptionUIOpen = false;
    

    private void Awake()
    {
        mainCamera = Camera.main; // ������� ī�޶� ��������
        _clickAtt = new InputAction(); // ��ǲ�׼� �ν��Ͻ� ����
    }

    private void OnEnable()
    {
        StartCoroutine(AutoAttack());
    }

    private void OnDisable()
    {
        StopCoroutine(AutoAttack());
    }

    public void OnClickAttack(InputAction.CallbackContext context)
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject() || isOptionUIOpen)
        { 
         // UI��� ������ Ŭ���Ǿ����� Ȯ���ϴ� �Լ�,EventSystem.current �� �����ϸ� UI Ŭ�� ���� ����
            Debug.Log("ui Ŭ�� ���� ���� �ȵ�");
            return;
        }
        Vector2 mousePosition = Mouse.current.position.ReadValue(); // Ŭ����ġ�� ��������
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition); // ȭ�� ��ǥ�� ������ǥ�� ��ȯ
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
        // 2D���� �� ��Ȯ�� Raycast �۵� (Physics2D.Raycast�� ������ �ʿ� ����)

        if (hit.collider != null )
        {
            Monster monster = hit.collider.GetComponent<Monster>();

            if (monster != null)
            {
                AttackMonster(click_Damage);
            }
        }

        if (isOptionUIOpen)
        {
            Debug.Log("�ɼ�ui�� ���������Ƿ� ���ݺҰ�");
            return ;
        }

        AttackMonster(click_Damage);
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

    public void AttackMonster(float Damage)
    {
        if (pokemonMonster != null)
        {
            // ������ ����
            Debug.Log($"���� {pokemonMonster.gameObject.name}�� ����!");
        }
    }
}
