using System;
using System.Collections.Generic;
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
    public int monsterIndex = 0;

    public List<StageData> stageDataList;


    void Start()
    {
        LoadStage(stageIndex);
        UpdateUI();
    }

    void Update()
    {
        healthSlider.value = GetPercentage();
    }

    private void UpdateUI()
    {
        StageData stageData = stageDataList[stageIndex];

        MonsterData monsterData = stageData.monsters[waveIndex].MonsterData[monsterIndex];
        SetMonster(monsterData);

        stageKillCount.text = $"{stageData.curKillCount} / {stageData.monsters[waveIndex].MonsterData.Length}";
        stageName.text = $"{stageData.stageName} - {waveIndex + 1}";
        monsterName.text = monsterData.MonsterName;
        monsterImage.sprite = monsterData.sprite;
    }

    public void SetMonster(MonsterData monsterData)
    {
        maxValue = monsterData.maxhealth;
        curValue = monsterData.health;
    }

    private void OnValidate()
    {
        UpdateUI();
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void TakeDamage(float damage)
    {
        curValue -= damage;
        if (curValue < 0) curValue = 0;
        healthSlider.value = curValue;

        if (curValue == 0)
        {
            StageData stageData = stageDataList[stageIndex];
            stageData.curKillCount++;

            GameManager.Instance.AddGold(stageData.monsters[waveIndex].MonsterData[monsterIndex].money);
            UpdateUI();

            if (stageData.curKillCount >= stageData.monsters[waveIndex].MonsterData.Length)
                NextStage();
        }
    }

    private void NextStage()
    {
        stageIndex++;
        if (stageIndex < stageDataList.Count)
        {
            LoadStage(stageIndex);
            UpdateUI();
        }
        else
        {
            Debug.Log("모든 스테이지를 완료했습니다.");
        }
    }

    private void LoadStage(int stageIndex)
    {
        if (stageDataList == null || stageIndex >= stageDataList.Count)
        {
            Debug.LogError($" 유효하지 않은 stageIndex ({stageIndex}). 데이터를 확인하세요.");
            return;
        }

        StageData stageData = stageDataList[stageIndex];
        waveIndex = 0;
        monsterIndex = 0;
        stageData.curKillCount = 0;
    }
}
