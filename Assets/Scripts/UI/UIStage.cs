using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class UIStage : MonoBehaviour
{
    //public static UIStage Instance;

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


    void Start()
    {
        LoadStage(stageIndex);
        UpdateUI();
    }

    public void UpdateUI()
    {
        StageData stageData = stageDataList[stageIndex];        // ��������

        MonsterData monsterData = stageData.monsters[waveIndex].MonsterData[monsterIndex];

        if (monsterIndex < stageData.monsters[waveIndex].MonsterData.Length)
        {
            monster.Init(monsterData);

            backGround.sprite = stageData.backGround;
            KillMonster();
            stageName.text = $"{stageData.stageName} - {waveIndex + 1}";
        }
    }
    public void KillMonster()
    {
        StageData stageData = stageDataList[stageIndex];
        stageKillCount.text = $"{stageData.curKillCount} / {stageData.monsters[waveIndex].MonsterData.Length}";
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
            stageDataList[stageIndex].curKillCount = 0;
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
