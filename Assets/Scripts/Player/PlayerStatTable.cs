using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerStatTable", menuName = "Scriptable/PlayerStatTable", order = 1)]
public class PlayerStatTable : ScriptableObject
{
    public CritData[] crit;
    public AutoAttackData[] autoAttack;
    public GoldData[] gold;

    public void GenerateData(int maxlevel)
    {
        crit = new CritData[maxlevel];
        autoAttack = new AutoAttackData[maxlevel];
        gold = new GoldData[maxlevel];

        for(int i = 0; i < maxlevel; i++)
        {
            int level = i + 1;

            crit[i] = new CritData
            {
                level = level,
                critDamage = 1f + (i * 0.5f)
            };

            autoAttack[i] = new AutoAttackData
            {
                level = level,
                AutoAttack = 1f + i
            };

            gold[i] = new GoldData
            {
                level = level,
                GetGold = 1f + i
            };
        }
    }
}



[System.Serializable]
public class CritData
{
    public int level;
    public float critDamage;
}

[System.Serializable]
public class AutoAttackData
{
    public int level;
    public float AutoAttack;
}

[System.Serializable]
public class GoldData
{
    public int level;
    public float GetGold;
}
