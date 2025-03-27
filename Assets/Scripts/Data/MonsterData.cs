using UnityEngine;

//public enum MonsterStats
//{
//    Health,
//}

//[System.Serializable]
//public class MonsterStatData
//{
//    public MonsterStats stat;
//    public float value;

//    public MonsterStatData(MonsterStats stat, float value)
//    {
//        this.stat = stat;
//        this.value = value;
//    }
//}

[CreateAssetMenu (fileName = "Monster", menuName = "GameData/Monster")]
public class MonsterData : ScriptableObject
{
    [Header("Info")]        // ���� ����
    public string MonsterName;
    public float health;
    public float maxhealth;
    public Sprite sprite;

    //[Header("StatData")]
    //public MonsterStatData[] monsterStatDatas;

    // ���� ������ ������ 
    public MonsterData(string name, float health, float maxhealth, Sprite sprite)
    {
        MonsterName = name;
        this.health = health;
        this.maxhealth = maxhealth;

        this.sprite = sprite; 
        //this.monsterStatDatas = monsterStatDatas;
    }
}
