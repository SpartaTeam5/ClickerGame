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

    public Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        LoadStage(stageIndex);
        UpdateUI();
    }


    private void UpdateUI()
    {

        // �ѱ��
        monsterImage.raycastTarget = true;
        animator.SetBool("Die", false);

        StageData stageData = stageDataList[stageIndex];        // ��������

        // ���� �ѱ��
        MonsterData monsterData = stageData.monsters[waveIndex].MonsterData[monsterIndex];
        SetMonster(monsterData);

        healthSlider.value = GetPercentage();


        backGround.sprite = stageData.backGround;
        stageKillCount.text = $"{stageData.curKillCount} / {stageData.monsters[waveIndex].MonsterData.Length}";
        stageName.text = $"{stageData.stageName} - {waveIndex + 1}";
        
        // ����
        monsterName.text = monsterData.MonsterName;
        monsterImage.sprite = monsterData.sprite;

    }
    // ��
    public void SetMonster(MonsterData monsterData)
    {
        maxValue = monsterData.maxhealth;
        curValue = monsterData.health;
    }
    // ��
    public float GetPercentage()
    {
        return curValue / maxValue;
    }
    // ��
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
