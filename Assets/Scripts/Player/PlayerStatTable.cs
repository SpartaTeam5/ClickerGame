using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerStatTable", menuName = "Scriptable/PlayerStatTable", order = 1)]
public class PlayerStatTable : ScriptableObject
{
    public CritData[] crit; //치명타 데이터
    public AutoAttackData[] auto; //자동 공격 데이터
    public GoldData[] gold; //골드 획득 데이터

    public void GenerateData(int maxlevel)
    {
        //maxlevel 만큼 데이터 배열 생성
        crit = new CritData[maxlevel];
        auto = new AutoAttackData[maxlevel];
        gold = new GoldData[maxlevel];

        for(int i = 0; i < maxlevel; i++)
        {
            int level = i + 1;

            crit[i] = new CritData
            {
                level = level,
                critDamage = 50f + (i * 50f),
                critChance = Mathf.Clamp(0.05f + (i * 0.01f), 0f, 0.5f), // 최대 50% 제한
                cost_c = 10 + (i * 10)
            };

            auto[i] = new AutoAttackData
            {
                level = level,
                autoAttackCycle = 1f + i,
                cost_a = 10 + (i * 10)
            };

            gold[i] = new GoldData
            {
                level = level,
                getGoldAmount = 100f + (i * 100f),
                cost_g = 10 + (i * 10)
            };
        }
    }
}



[System.Serializable]
public class CritData
{
    public int level; //치명타 레벨
    public float critDamage; //치명타 데미지
    public float critChance; // 치명타 확률 (0~1 범위)
    public int cost_c; //레벨업 비용
}

[System.Serializable]
public class AutoAttackData
{
    public int level; //자동공격 레벨
    public float autoAttackCycle; //자동공격 횟수/초
    public int cost_a; //레벨업 비용
}

[System.Serializable]
public class GoldData
{
    public int level; //골드획득 레벨
    public float getGoldAmount; //골드 획득량
    public int cost_g; //레벨업 비용
}
