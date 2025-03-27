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

    private MonsterData monsterData;
    private StageData   stageData;
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
        stageName.text = stageData.name;
        monsterName.text = monsterData.name;
        monsterImage.sprite = monsterData.sprite;
    }

    public void SetMonster(MonsterData monsterData)
    {
        maxValue = monsterData.maxhealth;
        curValue = monsterData.health;
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
