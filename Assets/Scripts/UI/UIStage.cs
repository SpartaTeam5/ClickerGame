using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStage : MonoBehaviour
{
    [Header("Stage Info")]
    public TextMeshProUGUI stageKillCount;  // �� ������������ ���� �� ī��Ʈ
    public TextMeshProUGUI stageName;       // �������� �̸�
    public TextMeshProUGUI monsterName;     // ���� �̸�
    public Slider healthSlider;             // ���� ü��
    public Image monsterImage;              // ���� �̹���

    private float curValue;
    private float maxValue;

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
