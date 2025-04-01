using UnityEngine;

public class StatManager : MonoBehaviour
{
    // 싱글턴 인스턴스
    public static StatManager Instance;
    private void Awake()
    {
        // 싱글턴 패턴 적용
        if (Instance == null) Instance = this;
    }

    // 최종 공격력
    public float GetFinalDamage()
    {
        //return GameManager.Instance.playerData.clickPower;
        return GameManager.Instance.weaponDataTable.baseAttack;
    }

    // 최종 치명타 확률
    public float GetCriticalChance()
    {
        int level = GameManager.Instance.playerData.stage; // 현재 레벨
        return GameManager.Instance.playerStatTable.crit[level - 1].critChance;
    }

    // 최종 치명타 배율
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
