using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlayerStatTable statTable;
    //public PlayerData playerdata;

    public int critLevel = 1;
    public int autoLevel = 0; // 초기 자동공격 레벨을 0으로 설정
    public int goldLevel = 1;

    public Button critBtn;
    public Button autoBtn;
    public Button goldBtn;
    public Button enhanceBtn;
    public Button backBtn;

    public GameObject enhanceUI;

    public TextMeshProUGUI critLevelText;
    public TextMeshProUGUI critDamageText;
    public TextMeshProUGUI critCostText;
    public TextMeshProUGUI autoLevelText;
    public TextMeshProUGUI autoCycleText;
    public TextMeshProUGUI autoCostText;
    public TextMeshProUGUI goldLevelText;
    public TextMeshProUGUI goldAmountText;
    public TextMeshProUGUI goldCostText;

    public ClickAttack clickAttack; // ClickAttack 클래스의 인스턴스 추가

    public void Start()
    {
        statTable.GenerateData(100);
        UpdateUI();

        clickAttack = FindObjectOfType<ClickAttack>(); // ClickAttack 인스턴스 찾기
        //ApplyStatsToClickAttack(); // ClickAttack에 스탯 적용
    }

    public void UpdateUI()
    {
        CritData crit = statTable.crit[critLevel - 1];
        critLevelText.text = "Lv" + crit.level.ToString();
        critDamageText.text = crit.critDamage.ToString() + "%";
        critCostText.text = crit.cost_c.ToString();
        if (GameManager.Instance.playerData.gold < statTable.crit[critLevel - 1].cost_c) // 골드 부족하면 빨간색으로 표시
        {
            critCostText.color = Color.red;
        }
        else
        {
            critCostText.color = Color.black;
        }

        AutoAttackData auto = statTable.auto[autoLevel - 1];
        autoLevelText.text = "Lv" + auto.level.ToString();
        autoCycleText.text = auto.autoAttackCycle.ToString() + "회/초";
        autoCostText.text = auto.cost_a.ToString();
        if (GameManager.Instance.playerData.gold < statTable.auto[autoLevel - 1].cost_a) // 골드 부족하면 빨간색으로 표시
        {
            autoCostText.color = Color.red;
        }
        else
        {
            autoCostText.color = Color.black;
        }

        GoldData gold = statTable.gold[goldLevel - 1];
        goldLevelText.text = "Lv" + gold.level.ToString();
        goldAmountText.text = gold.getGoldAmount.ToString() + "%";
        goldCostText.text = gold.cost_g.ToString();
        if (GameManager.Instance.playerData.gold < statTable.gold[goldLevel - 1].cost_g) // 골드 부족하면 빨간색으로 표시
        {
            goldCostText.color = Color.red;
        }
        else
        {
            goldCostText.color = Color.black;
        }
    }

    public void OnClickCrit() //레벨 업 버튼 누르면 치명타 레벨 1씩 증가
    {
        if (GameManager.Instance.playerData.gold >= statTable.crit[critLevel - 1].cost_c)
        {
            GameManager.Instance.playerData.gold -= (critLevel * 10);
            critLevel++;
            var crit = statTable.crit[critLevel - 1];
            UpdateUI();
            GameManager.Instance.UpdateGoldUI();
            Debug.Log($"[치명타 레벨 업!] 현재 레벨: {crit.level}, 치명타 데미지: {crit.critDamage}");
        }
        else
        {
            Debug.Log("골드 부족");
        }
    }

    public void OnClickAuto() //레벨 업 버튼 누르면 자동공격 레벨 1씩 증가
    {
        if (GameManager.Instance.playerData.gold >= statTable.auto[autoLevel - 1].cost_a)
        {
            GameManager.Instance.playerData.gold -= (autoLevel * 10);
            autoLevel++;
            var auto = statTable.auto[autoLevel - 1];
            UpdateUI();
            GameManager.Instance.UpdateGoldUI();
            ApplyStatsToClickAttack(); // 자동공격 주기 업데이트 및 재시작
            Debug.Log($"[자동공격 레벨 업!] 현재 레벨: {auto.level}, 초당 자동공격 횟수: {auto.autoAttackCycle}");
        }
        else
        {
            Debug.Log("골드 부족");
        }
    }

    public void OnClickGold() //레벨 업 버튼 누르면 골드 획득 레벨 1씩 증가
    {
        if (GameManager.Instance.playerData.gold >= statTable.gold[goldLevel - 1].cost_g)
        {
            GameManager.Instance.playerData.gold -= (goldLevel * 10);
            goldLevel++;
            var gold = statTable.gold[goldLevel - 1];
            UpdateUI();
            GameManager.Instance.UpdateGoldUI();
            Debug.Log($"[골드획득 레벨 업!] 현재 레벨: {gold.level}, 골드 획득량: {gold.getGoldAmount}");
        }
        else
        {
            Debug.Log("골드 부족");
        }
    }

    public void OnClickEnhance()
    {
        enhanceUI.SetActive(true);
    }

    public void OnClickBack()
    {
        enhanceUI.SetActive(false);
    }

    private void ApplyStatsToClickAttack()
    {
        if (clickAttack != null)
        {
            if (autoLevel > 0) // 자동공격 레벨이 1 이상일 때만 자동공격 활성화
            {
                AutoAttackData auto = statTable.auto[autoLevel - 1];
                clickAttack.autoAttackInterval = 1.0f / auto.autoAttackCycle; // 자동공격 주기 설정
                clickAttack.isAutoAttackEnabled = true; // 자동공격 활성화
                clickAttack.RestartAutoAttack(); // 자동공격 재시작
            }
            else
            {
                clickAttack.isAutoAttackEnabled = false; // 자동공격 비활성화
            }

            CritData crit = statTable.crit[critLevel - 1];
            clickAttack.criticalPercent = crit.critChance; // 치명타 확률 설정
            clickAttack.criticalMultiplier = crit.critDamage / 100.0f; // 치명타 데미지 설정
        }
    }
}
