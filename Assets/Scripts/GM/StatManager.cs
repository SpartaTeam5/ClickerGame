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
        int level = GameManager.Instance.playerData.stage; // ���� ����
        return GameManager.Instance.playerStatTable.crit[level - 1].critChance;
    }

    // ���� ġ��Ÿ ����
    public float GetCriticalDamage()
    {
        return GameManager.Instance.playerData.criticalMultiplier;
    }
}
