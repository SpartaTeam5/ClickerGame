using UnityEngine;
using UnityEngine.UI;

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
    public int curKillCount;        // 현재 처리된 몬스터 수
    public Sprite backGround;           // 배경 이미지
    public StageMonster[] monsters;

}
