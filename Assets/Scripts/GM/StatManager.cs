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

    // 치명타 데미지 주기
    public float GetCritDamage()
    {
        return GameManager.Instance.playerStatTable.crit[GameManager.Instance.player.critLevel - 1].critDamage;
    }

    // 자동 공격 주기
    //public float g
}
