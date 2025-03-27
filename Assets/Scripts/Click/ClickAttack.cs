using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems; // ui 감지를 위해 필요
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
        mainCamera = Camera.main; // 현재씬의 카메라 가져오기
        _clickAtt = new InputAction(); // 인풋액션 인스턴스 생성
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
         // UI요소 위에서 클릭되었는지 확인하는 함수,EventSystem.current 가 존재하면 UI 클릭 감지 가능
            Debug.Log("ui 클릭 공격 실행 안됨");
            return;
        }
        Vector2 mousePosition = Mouse.current.position.ReadValue(); // 클릭위치값 가져오기
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition); // 화면 좌표를 월드좌표로 변환
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
        // 2D에서 더 정확한 Raycast 작동 (Physics2D.Raycast는 방향이 필요 없다)

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
            Debug.Log("옵션ui가 열려있으므로 공격불가");
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
            // 데미지 받음
            Debug.Log($"몬스터 {pokemonMonster.gameObject.name}를 공격!");
        }
    }
}
