using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;// �̱��� �ν��Ͻ� ����
    public PlayerData playerData = new PlayerData();// �÷��̾� �����͸� �ν����Ϳ��� Ȯ�� �����ϵ��� public���� ����

    [Header("UI Elements")]
    public Text goldText;//��� ǥ��
    public GameObject warningMessage;// ��ȭ ���� �� ��� �޽��� UI (Text Ȥ�� Panel�� Text ������Ʈ�� ���ԵǾ� ���� �� ����)

    private void Awake()
    {
        // �̱��� ���� ����: �ϳ��� �ν��Ͻ��� �����ϵ��� ��
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // �� ��ȯ �ÿ��� �������� �ʵ��� ����
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
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
    private void UpdateGoldUI()
    {
        goldText.text = $"Gold: {playerData.gold:F1}";
    }
}
