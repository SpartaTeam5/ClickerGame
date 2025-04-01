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
    [SerializeField] float click_Damage;
    public float autoAttackInterval;
    public bool isAutoAttackEnabled = false; // 초기 자동공격 비활성화
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

            // 자동공격 레벨이 1 이상일 때만 자동공격 시작
            if (isAutoAttackEnabled)
            {
                StartCoroutine(AutoAttack());
            }
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

    public bool IsCriticalHit()
    {
        return Random.value < criticalPercent;
    }

    public void OnClickAttack(InputAction.CallbackContext context)
    {
        click_Damage = StatManager.Instance.GetFinalDamage();

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
        Debug.Log(lastAttCriticalCheck ? "치명타" : "일반 공격");

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
        // 모든 코루틴 정지 후 자동 공격 코루틴 재시작
        StopAllCoroutines();
        // 옵션 UI가 열려있지 않은 경우만 자동 공격 실행
        if (!isOptionUIOpen && isAutoAttackEnabled)
        {
            StartCoroutine(AutoAttack());
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
