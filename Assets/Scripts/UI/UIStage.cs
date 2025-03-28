using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class UIStage : MonoBehaviour
{
    [Header("Stage Info")]
    public Image backGround;
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

    public Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        LoadStage(stageIndex);
        UpdateUI();
    }


    private void UpdateUI()
    {

        // 넘기기
        monsterImage.raycastTarget = true;
        animator.SetBool("Die", false);

        StageData stageData = stageDataList[stageIndex];        // 스테이지

        // 몬스터 넘기기
        MonsterData monsterData = stageData.monsters[waveIndex].MonsterData[monsterIndex];
        SetMonster(monsterData);

        healthSlider.value = GetPercentage();


        backGround.sprite = stageData.backGround;
        stageKillCount.text = $"{stageData.curKillCount} / {stageData.monsters[waveIndex].MonsterData.Length}";
        stageName.text = $"{stageData.stageName} - {waveIndex + 1}";
        
        // 여기
        monsterName.text = monsterData.MonsterName;
        monsterImage.sprite = monsterData.sprite;

    }
    // 너
    public void SetMonster(MonsterData monsterData)
    {
        maxValue = monsterData.maxhealth;
        curValue = monsterData.health;
    }
    // 너
    public float GetPercentage()
    {
        return curValue / maxValue;
    }
    // 너
    public void TakeDamage(float damage)
    {

        curValue -= damage;
        if (curValue < 0) curValue = 0;
        healthSlider.value = GetPercentage();
        animator.SetTrigger("Attack");


        DamageText damageText = DamageTextPool.Instance.GetFromPool();
        //damageText.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        damageText.transform.position = UnityEngine.Input.mousePosition;
        damageText.SetText(damage);

        if (curValue == 0)
        {
            monsterImage.raycastTarget = false;
            animator.SetBool("Die", true);

            StageData stageData = stageDataList[stageIndex];
            stageData.curKillCount++;
            monsterIndex++;

            //GameManager.Instance.AddGold(stageData.monsters[waveIndex].MonsterData[monsterIndex].money);
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
