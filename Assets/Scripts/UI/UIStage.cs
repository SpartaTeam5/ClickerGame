using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIStage : MonoBehaviour
{
    [Header("Stage Info")]
    public Image backGround;                // 배경 이미지
    public TextMeshProUGUI stageKillCount;  // 각 스테이지에서 죽은 적 카운트
    public TextMeshProUGUI stageName;       // 스테이지 이름

    public Monster monster; // 몬스터 관리 객체
    public SceneChanger sceneChanger;

    //임시용 인덱스 - > 게임 매니저에서 관리하기
    public int stageIndex = 0;
    public int waveIndex = 0;
    public int monsterIndex = 0;

    public List<StageData> stageDataList;   // 전체 스테이지 데이터 저장하기

    void Start()
    {
        LoadStage(stageIndex);      // 게임 시작 시 스테이지 불러오기
        UpdateUI();                 // UI 업데이트
    }

    public void UpdateUI()
    {
        StageData stageData = stageDataList[stageIndex];    // 현 스테이지 가져오기
        // 현재 몬스터 가져오기
        MonsterData monsterData = stageData.monsters[waveIndex].MonsterData[monsterIndex];  

        if (monsterIndex < stageData.monsters[waveIndex].MonsterData.Length)    // 전체 몬스터가 처리 시
        {
            monster.Init(monsterData);      // 몬스터 초기화

            // 새 몬스터로 ClickAttack의 참조 갱신 및 자동 공격 재시작
            ClickAttack clickAttack = FindObjectOfType<ClickAttack>();
            if (clickAttack != null)
            {
                clickAttack.monster = this.monster;
                clickAttack.RestartAutoAttack();
            }

            backGround.sprite = stageData.backGround;       // 배경 이미지 변경
            KillMonster();      // 현재 처치한 몬스터 수 UI 갱신
            stageName.text = $"{stageData.stageName} - {waveIndex + 1}";    // 스테이지 이름 및 웨이브 표시
        }
    }
    // 몬스터 처치 정보를 UI에 반영
    public void KillMonster()
    {
        StageData stageData = stageDataList[stageIndex];
        stageKillCount.text = $"{stageData.curKillCount} / {stageData.monsters[waveIndex].MonsterData.Length}";
    }
    // 몬스터 사망 시 호출되는 메서드
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
            Invoke("NextWave", 1f); // 모든 몬스터 처치 시 다음 웨이브 시작
        }
    }
    // 다음 웨이브로 이동하는 메서드
    private void NextWave()
    {
        waveIndex++;
        if (waveIndex < stageDataList[stageIndex].monsters.Length)
        {
            monsterIndex = 0;
            stageDataList[stageIndex].curKillCount = 0;
            UpdateUI();
        }
        else
        {
            NextStage();
        }
    }
    // 다음 스테이지로 이동하는 메서드
    private void NextStage()
    {
        stageIndex++;
        if (stageIndex < stageDataList.Count)
        {
            LoadStage(stageIndex);  // 새 스테이지 로드
            UpdateUI();
        }
        else
        {
            sceneChanger.EndSceneChanage();
        }
    }

    private void LoadStage(int stageIndex)
    {
        // 유효하지 않은 인덱스 예외 처리
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
