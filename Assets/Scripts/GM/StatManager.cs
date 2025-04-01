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
        //return GameManager.Instance.playerData.clickPower;
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

    public float GetAutoAttackCycle()
    {
        int autoLevel = GameManager.Instance.player.autoLevel;
        if (autoLevel > 0)
        {
            return GameManager.Instance.playerStatTable.auto[autoLevel - 1].autoAttackCycle;
        }
        return 0;
    }

    public float GetGoldAmount()
    {
        return GameManager.Instance.playerStatTable.gold[GameManager.Instance.player.goldLevel - 1].getGoldAmount;
    }
}
