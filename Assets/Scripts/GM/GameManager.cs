using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;// �̱��� �ν��Ͻ� ����
    public PlayerData playerData = new PlayerData();// �÷��̾� �����͸� �ν����Ϳ��� Ȯ�� �����ϵ��� public���� ����
    public Player player;

    [Header("UI Elements")]
    public TextMeshProUGUI goldText;//��� ǥ��
    public GameObject warningMessage;// ��� ���� �� ���
    public PlayerStatTable playerStatTable;

    [Header("Weapon")]
    public WeaponDataTable weaponDataTable; // ������ ���� ��������
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
        // �� ��ȯ �ÿ��� ����X
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

        // ���� �� ��� UI ������Ʈ
        UpdateGoldUI();

    }

    // ��� �߰� �� UI ������Ʈ
    public void AddGold(float amount)
    {
        playerData.AddGold(amount);
        UpdateGoldUI();
    }

    // ��� ��� ��, ��� ���� ���ο� ���� UI ó�� �Ǵ� ��� �޽��� ���
    public void TrySpendGold(float amount)
    {
        if (!playerData.SpendGold(amount))
        {
            // ��尡 ������ ��� ��� �޽��� ��� (�ڷ�ƾ ���)
            StartCoroutine(ShowWarning("��尡 �����մϴ�!"));
        }
        else
        {
            // ��� ���� �� UI ������Ʈ
            UpdateGoldUI();
        }
    }

    // ��� �޽����� ��� �����ִ� �ڷ�ƾ
    private IEnumerator ShowWarning(string message)
    {
        // ��� �޽��� Ȱ��ȭ �� �ؽ�Ʈ ����
        warningMessage.SetActive(true);
        warningMessage.GetComponent<Text>().text = message;
        // 2�ʰ� ���
        yield return new WaitForSeconds(2f);
        // ��� �޽��� ��Ȱ��ȭ
        warningMessage.SetActive(false);
    }

    // ��� UI ������Ʈ: ��� �ؽ�Ʈ�� �ֽ� �����ͷ� ����
    public void UpdateGoldUI()
    {
        //goldText.text = $"Gold: {playerData.gold:F1}";
        goldText.text = $"{playerData.gold:N0}";
    }

}
