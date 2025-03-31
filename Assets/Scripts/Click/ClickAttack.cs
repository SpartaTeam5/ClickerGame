using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems; // ui 감지를 위해 필요
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
        mainCamera = Camera.main; // 현재씬의 카메라 가져오기
        playerInput = GetComponent<PlayerInput>();
        _clickAction = playerInput.actions["ClickAtt"];
        
    }

    private void Start()
    {
        monster = FindObjectOfType<Monster>();
        if (monster == null)
        {
            Debug.LogError("Monster 오브젝트를 찾을 수 없습니다!");
            return;
        }

        Debug.Log($"[DEBUG] monster 값: {monster}");

        if (!isOptionUIOpen)
        {
            _clickAction.performed += OnClickAttack;
            _clickAction.Enable();

            StartCoroutine(AutoAttack()); // 자동 공격 시작
        }
    }

    private void Update()
    {
        if (isOptionUIOpen) // 옵션 창이 열리면 입력 비활성화 및 공격 중지
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
            Debug.Log("옵션ui가 열려있음 공격불가");
            return;
        }

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        { 
         // UI요소 위에서 클릭되었는지 확인하는 함수,EventSystem.current 가 존재하면 UI 클릭 감지 가능
            
            GameObject clickMonterUI = GetClickMonsterUI(Mouse.current.position.ReadValue());
            if (clickMonterUI != null)
            {
                Debug.Log("UI 몬스터 클릭! 공격");
                AttackMonster(click_Damage);
                return;
            }

            Debug.Log("ui 클릭 공격 실행 안됨");
            return;
        }
        Debug.Log("몬스터를 클릭하지 않음");

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
            Debug.Log($"몬스터를 {damage}만큼 공격! 남은 체력: {monster.GetPercentage() * 100}%");
        }
    }

    private GameObject GetClickMonsterUI(Vector2 screenPosition)
    {
        EventSystem eventSystem = EventSystem.current ?? FindObjectOfType<EventSystem>();
        if (eventSystem == null) return null; // EventSystem이 없으면 null 반환
        // EventSystem을 찾고, 없으면 null을 반환

        PointerEventData eventData = new PointerEventData(eventSystem)
        {
        // PointerEventData 인스턴스를 생성하면서 화면 위치 설정
            position = screenPosition // 클릭된 화면 위치를 설정
        };

        List<RaycastResult> results = new List<RaycastResult>();
        // 레이캐스트 결과를 저장할 리스트

        GraphicRaycaster uiRaycaster = FindObjectOfType<GraphicRaycaster>();
        // 씬에서 GraphicRaycaster 컴포넌트를 찾음 (UI 레이캐스트 용도)

        if (uiRaycaster == null) return null;
        // 만약 GraphicRaycaster가 없다면, null을 반환 (UI가 없다는 의미)

        uiRaycaster.Raycast(eventData, results);
        // 레이캐스트를 실행하여 결과를 results 리스트에 저장

        foreach (var result in results)
        // 레이캐스트 결과를 반복하면서 "Monster" 태그가 붙은 GameObject를 찾음
        {
            if (result.gameObject.CompareTag("Monster"))
            {
                return result.gameObject;
            }
        }
        return null;
    }
}
