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
            int level = i;

            crit[i] = new CritData
            {
                level = level,
                critDamage =  1f + (i * 0.5f),
                cost_c = 10 + (i * 10)
            };

            auto[i] = new AutoAttackData
            {
                level = level,
                autoAttackCycle = 0f + i,
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
    public int level; //ġ��Ÿ ����
    public float critDamage; //ġ��Ÿ ������
    public float critChance; // ġ��Ÿ Ȯ�� (0~1 ����)
    public int cost_c; //������ ���
}

[System.Serializable]
public class AutoAttackData
{
    public int level; //�ڵ����� ����
    public float autoAttackCycle; //�ڵ����� Ƚ��/��
    public int cost_a; //������ ���
}

[System.Serializable]
public class GoldData
{
    public int level; //���ȹ�� ����
    public float getGoldAmount; //��� ȹ�淮
    public int cost_g; //������ ���
}
