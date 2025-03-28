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
    public int curKillCount;        // ���� ó���� ���� ��
    public Sprite backGround;           // ��� �̹���
    public StageMonster[] monsters;

}
