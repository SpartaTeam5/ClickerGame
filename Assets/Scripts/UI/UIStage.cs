using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStage : MonoBehaviour
{
    [Header("Stage Info")]
    public TextMeshProUGUI stageKillCount;  // 각 스테이지에서 죽은 적 카운트
    public TextMeshProUGUI stageName;       // 스테이지 이름
    public TextMeshProUGUI monsterName;     // 몬스터 이름
    public Slider healthSlider;             // 몬스터 체력
    public Image monsterImage;              // 몬스터 이미지

    private float curValue;
    private float maxValue;

    //임시용 인덱스
    public int stageIndex = 0;
    public int waveIndex = 0;

    public StageData   stageData;
    void Start()
    {
        UpdataUI();
    }
    void Update()
    {
        healthSlider.value = GetPercentage();
    }

    private void UpdataUI()
    {
        SetMonster(stageData.monsters[stageIndex].MonsterData[waveIndex]);
        stageKillCount.text = $"{stageData.curKillCount} / {stageData.monsters[stageIndex].MonsterData.Length}";
        stageName.text = $"{ stageData.stageName } - {stageIndex + 1}";
        monsterName.text = stageData.monsters[stageIndex].MonsterData[waveIndex].MonsterName; // 몇번째 몬스터가 필요하지 인덱스 값이 필요함
        monsterImage.sprite = stageData.monsters[stageIndex].MonsterData[waveIndex].sprite;   // 몇번째 몬스터가 필요하지 인덱스 값이 필요함
    }

    public void SetMonster(MonsterData monsterData)
    {
        maxValue = monsterData.maxhealth;
        curValue = monsterData.health;
    }

    private void OnValidate()
    {
        UpdataUI();
    }
    public float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void TakeDamage(float damage)
    {
        curValue -= damage;
        if(curValue < 0) curValue = 0;
        healthSlider.value = curValue;

        if (curValue == 0)
        {
            stageData.curKillCount++;
            // 플레이어에게 돈 추가
            GameManager.Instance.AddGold(stageData.monsters[stageIndex].MonsterData[waveIndex].money);
            UpdataUI();
        }
    }
}
