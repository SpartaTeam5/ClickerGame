using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStage : MonoBehaviour
{
    [Header("Stage Info")]
    public TextMeshProUGUI stageName;       // �������� �̸�
    public TextMeshProUGUI monsterName;     // ���� �̸�
    public Slider healthSlider;             // ���� ü��
    public Image monsterImage;              // ���� �̹���


    private float curValue;
    private float maxValue;

    //�ӽÿ� �ε���
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
        stageName.text = stageData.stageName;
        monsterName.text = stageData.monsters[stageIndex].MonsterData[waveIndex].MonsterName; // ���° ���Ͱ� �ʿ����� �ε��� ���� �ʿ���
        monsterImage.sprite = stageData.monsters[stageIndex].MonsterData[waveIndex].sprite;   // ���° ���Ͱ� �ʿ����� �ε��� ���� �ʿ���
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
    }
}
