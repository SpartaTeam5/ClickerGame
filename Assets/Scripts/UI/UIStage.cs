using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStage : MonoBehaviour
{
    [Header("Stage Info")]
    public TextMeshProUGUI stageName;       // 스테이지 이름
    public TextMeshProUGUI monsterName;     // 몬스터 이름
    public Slider healthSlider;             // 몬스터 체력
    public Image monsterImage;              // 몬스터 이미지


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
