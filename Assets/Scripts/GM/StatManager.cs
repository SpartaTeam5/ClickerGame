using UnityEngine;

public class StatManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static StatManager Instance;

    private void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null) Instance = this;
    }

    // ���� ���ݷ�
    public float GetFinalDamage()
    {
        return GameManager.Instance.playerData.clickPower;
    }

    // ���� ġ��Ÿ Ȯ��
    public float GetCriticalChance()
    {
        return GameManager.Instance.playerData.criticalChance;
    }

    // ���� ġ��Ÿ ����
    public float GetCriticalDamage()
    {
        return GameManager.Instance.playerData.criticalMultiplier;
    }
}
