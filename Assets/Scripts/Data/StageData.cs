using UnityEngine;

[System.Serializable]
public class StageMonster
{
    public MonsterData[] MonsterData;
}

[CreateAssetMenu(fileName = "Stage", menuName = "GameData/Stage")]
public class StageData : ScriptableObject
{
    [Header("Stage Info")]
    public string stageName;
    public StageMonster[] monsters;
}
