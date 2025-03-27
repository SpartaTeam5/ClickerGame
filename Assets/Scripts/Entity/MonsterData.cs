using UnityEngine;

public enum MonsterStat
{
    Health,
}

[System.Serializable]
public class MonsterStaData
{
    public MonsterStat stat;
    public float value;

    public MonsterStaData(MonsterStat stat, float value)
    {
        this.stat = stat;
        this.value = value;
    }
}

[CreateAssetMenu (fileName = "Monster", menuName = "Stat/Monster")]
public class MonsterData : ScriptableObject
{
    [Header("Info")]        // ���� ����
    public string _name;
    public Sprite sprite;

    [Header("StatData")]
    public MonsterStaData[] monsterStaDatas;

    // ���� ������ ������ 
    public MonsterData(string name, Sprite sprite, MonsterStaData[] monsterStaDatas)
    {
        this._name = name;
        this.sprite = sprite; 
        this.monsterStaDatas = monsterStaDatas;
    }
}
