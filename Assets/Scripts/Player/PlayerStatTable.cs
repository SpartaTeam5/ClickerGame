using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerStatTable", menuName = "Scriptable/PlayerStatTable", order = 1)]
public class PlayerStatTable : ScriptableObject
{
    public CritData[] crit; //ġ��Ÿ ������
    public AutoAttackData[] auto; //�ڵ� ���� ������
    public GoldData[] gold; //��� ȹ�� ������

    public void GenerateData(int maxlevel)
    {
        //maxlevel ��ŭ ������ �迭 ����
        crit = new CritData[maxlevel];
        auto = new AutoAttackData[maxlevel];
        gold = new GoldData[maxlevel];

        for(int i = 0; i < maxlevel; i++)
        {
            int level = i + 1;

            crit[i] = new CritData
            {
                level = level,
                critDamage = 50f + (i * 50f)
            };

            auto[i] = new AutoAttackData
            {
                level = level,
                autoAttackCycle = 1f + i
            };

            gold[i] = new GoldData
            {
                level = level,
                getGoldAmount = 100f + (i * 100f)
            };
        }
    }
}



[System.Serializable]
public class CritData
{
    public int level; //ġ��Ÿ ����
    public float critDamage; //ġ��Ÿ ������
}

[System.Serializable]
public class AutoAttackData
{
    public int level; //�ڵ����� ����
    public float autoAttackCycle; //�ڵ����� Ƚ��/��
}

[System.Serializable]
public class GoldData
{
    public int level; //���ȹ�� ����
    public float getGoldAmount; //��� ȹ�淮
}
