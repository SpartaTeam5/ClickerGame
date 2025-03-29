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
    public TextMeshProUGUI stageKillCount;  // �� ������������ ���� �� ī��Ʈ
    public TextMeshProUGUI stageName;       // �������� �̸�

    public Monster monster; // ���� ���� ��ü

    //�ӽÿ� �ε���
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
        StageData stageData = stageDataList[stageIndex];        // ��������

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

        // ���� ������ ������ �����ͼ� UI�� ������Ʈ
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
            Debug.Log("��� ���������� �Ϸ��߽��ϴ�.");
        }
    }

    private void LoadStage(int stageIndex)
    {
        if (stageDataList == null || stageIndex >= stageDataList.Count)
        {
            Debug.LogError($" ��ȿ���� ���� stageIndex ({stageIndex}). �����͸� Ȯ���ϼ���.");
            return;
        }

        StageData stageData = stageDataList[stageIndex];
        waveIndex = 0;
        monsterIndex = 0;
        stageData.curKillCount = 0;
    }
}
