using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;// 싱글턴 인스턴스 접근
    public PlayerData playerData = new PlayerData();// 플레이어 데이터를 인스펙터에서 확인 가능하도록 public으로 선언
    public Player player;

    [Header("UI Elements")]
    public TextMeshProUGUI goldText;//골드 표시
    public GameObject warningMessage;// 골드 부족 시 경고
    public PlayerStatTable playerStatTable;

    [Header("Weapon")]
    public WeaponDataTable weaponDataTable; // 데이터 정보 가져오기
    public Weapon weapon;
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // 씬 전환 시에도 삭제X
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameObject[] weapons = { weapon1, weapon2, weapon3, weapon4 };
        foreach (GameObject weapon in weapons)
        {
            WeaponData weaponData = weapon.GetComponent<WeaponData>();
            if (weaponData != null && weaponData.isEquipped)
            {
                weaponDataTable = weaponData.weapondata;
                break;
            }
        }

        // 시작 시 골드 UI 업데이트
        UpdateGoldUI();

    }

    // 골드 추가 후 UI 업데이트
    public void AddGold(float amount)
    {
        playerData.AddGold(amount);
        UpdateGoldUI();
    }

    // 골드 사용 시, 사용 성공 여부에 따라 UI 처리 또는 경고 메시지 출력
    public void TrySpendGold(float amount)
    {
        if (!playerData.SpendGold(amount))
        {
            // 골드가 부족할 경우 경고 메시지 출력 (코루틴 사용)
            StartCoroutine(ShowWarning("골드가 부족합니다!"));
        }
        else
        {
            // 사용 성공 시 UI 업데이트
            UpdateGoldUI();
        }
    }

    // 경고 메시지를 잠시 보여주는 코루틴
    private IEnumerator ShowWarning(string message)
    {
        // 경고 메시지 활성화 및 텍스트 설정
        warningMessage.SetActive(true);
        warningMessage.GetComponent<Text>().text = message;
        // 2초간 대기
        yield return new WaitForSeconds(2f);
        // 경고 메시지 비활성화
        warningMessage.SetActive(false);
    }

    // 골드 UI 업데이트: 골드 텍스트를 최신 데이터로 갱신
    public void UpdateGoldUI()
    {
        //goldText.text = $"Gold: {playerData.gold:F1}";
        goldText.text = $"{playerData.gold:N0}";
    }

}
