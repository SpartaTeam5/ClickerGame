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
        return GameManager.Instance.weaponDataTable.baseAttack;
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

    // ġ��Ÿ ������ �ֱ�
    public float GetCritDamage()
    {
        return GameManager.Instance.playerStatTable.crit[GameManager.Instance.player.critLevel - 1].critDamage;
    }

    // �ڵ� ���� �ֱ�
    public float GetAutoAttackCycle()
    {
        return GameManager.Instance.playerStatTable.auto[GameManager.Instance.player.autoLevel -1].autoAttackCycle;
    }
    // ��� ȹ�淮 ��������
    public float GetGoldAmount()
    {
        return GameManager.Instance.playerStatTable.gold[GameManager.Instance.player.goldLevel - 1].getGoldAmount;
    }
}
