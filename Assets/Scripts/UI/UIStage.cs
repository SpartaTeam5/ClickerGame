using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIStage : MonoBehaviour
{
    [Header("Stage Info")]
    public Image backGround;                // ��� �̹���
    public TextMeshProUGUI stageKillCount;  // �� ������������ ���� �� ī��Ʈ
    public TextMeshProUGUI stageName;       // �������� �̸�

    public Monster monster; // ���� ���� ��ü
    public SceneChanger sceneChanger;

    //�ӽÿ� �ε��� - > ���� �Ŵ������� �����ϱ�
    public int stageIndex = 0;
    public int waveIndex = 0;
    public int monsterIndex = 0;

    public List<StageData> stageDataList;   // ��ü �������� ������ �����ϱ�

    void Start()
    {
        LoadStage(stageIndex);      // ���� ���� �� �������� �ҷ�����
        UpdateUI();                 // UI ������Ʈ
    }

    public void UpdateUI()
    {
        StageData stageData = stageDataList[stageIndex];    // �� �������� ��������
        // ���� ���� ��������
        MonsterData monsterData = stageData.monsters[waveIndex].MonsterData[monsterIndex];  

        if (monsterIndex < stageData.monsters[waveIndex].MonsterData.Length)    // ��ü ���Ͱ� ó�� ��
        {
            monster.Init(monsterData);      // ���� �ʱ�ȭ

            // �� ���ͷ� ClickAttack�� ���� ���� �� �ڵ� ���� �����
            ClickAttack clickAttack = FindObjectOfType<ClickAttack>();
            if (clickAttack != null)
            {
                clickAttack.monster = this.monster;
                clickAttack.RestartAutoAttack();
            }

            backGround.sprite = stageData.backGround;       // ��� �̹��� ����
            KillMonster();      // ���� óġ�� ���� �� UI ����
            stageName.text = $"{stageData.stageName} - {waveIndex + 1}";    // �������� �̸� �� ���̺� ǥ��
        }
    }
    // ���� óġ ������ UI�� �ݿ�
    public void KillMonster()
    {
        StageData stageData = stageDataList[stageIndex];
        stageKillCount.text = $"{stageData.curKillCount} / {stageData.monsters[waveIndex].MonsterData.Length}";
    }
    // ���� ��� �� ȣ��Ǵ� �޼���
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
            Invoke("NextWave", 1f); // ��� ���� óġ �� ���� ���̺� ����
        }
    }
    // ���� ���̺�� �̵��ϴ� �޼���
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
    // ���� ���������� �̵��ϴ� �޼���
    private void NextStage()
    {
        stageIndex++;
        if (stageIndex < stageDataList.Count)
        {
            LoadStage(stageIndex);  // �� �������� �ε�
            UpdateUI();
        }
        else
        {
            sceneChanger.EndSceneChanage();
        }
    }

    private void LoadStage(int stageIndex)
    {
        // ��ȿ���� ���� �ε��� ���� ó��
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
