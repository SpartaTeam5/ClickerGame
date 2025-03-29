using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class UIStage : MonoBehaviour
{
    public static UIStage Instance;

    [Header("Stage Info")]
    public Image backGround;
    public TextMeshProUGUI stageKillCount;  // 각 스테이지에서 죽은 적 카운트
    public TextMeshProUGUI stageName;       // 스테이지 이름

    public Monster monster; // 몬스터 관리 객체

    //임시용 인덱스
    public int stageIndex = 0;
    public int waveIndex = 0;
    public int monsterIndex = 0;

    public List<StageData> stageDataList;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LoadStage(stageIndex);
        UpdateUI();
    }

    public void UpdateUI()
    {
        StageData stageData = stageDataList[stageIndex];        // 스테이지

        if (monsterIndex < stageData.monsters[waveIndex].MonsterData.Length)
        {
            MonsterData monsterData = stageData.monsters[waveIndex].MonsterData[monsterIndex];
            monster.Init(monsterData);

            backGround.sprite = stageData.backGround;
            //if(stageData.curKillCount == stageData.monsters[waveIndex].MonsterData.Length)
            //    stageKillCount.text = $"{stageData.curKillCount} / {stageData.monsters[waveIndex].MonsterData.Length}";
            //else
                stageKillCount.text = $"{stageData.curKillCount} / {stageData.monsters[waveIndex].MonsterData.Length}";
            stageName.text = $"{stageData.stageName} - {waveIndex + 1}";
        }
    }
   
    public void OnMonsterDeath()
    {
        StageData stageData = stageDataList[stageIndex];
        stageData.curKillCount++;
        monsterIndex++;

        // 다음 몬스터의 정보를 가져와서 UI를 업데이트
        if (monsterIndex < stageData.monsters[waveIndex].MonsterData.Length)
        {
            Invoke("UpdateUI", 1f);
        }
        else
        {
            Invoke("NextWave", 1f);
        }
    }

    private void NextWave()
    {
        waveIndex++;
        if (waveIndex < stageDataList[stageIndex].monsters.Length)
        {
            monsterIndex = 0;
            UpdateUI();
        }
        else
        {
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
