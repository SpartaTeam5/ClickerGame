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
        return GameManager.Instance.playerData.clickPower;
    }

    // 최종 치명타 확률
    public float GetCriticalChance()
    {
        return GameManager.Instance.playerData.criticalChance;
    }

    // 최종 치명타 배율
    public float GetCriticalDamage()
    {
        return GameManager.Instance.playerData.criticalMultiplier;
    }
}
