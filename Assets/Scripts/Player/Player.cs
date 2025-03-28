using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlayerStatTable statTable;

    public int critLevel = 1;
    public int autoLevel = 1;
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

    public int curgold;

    public void Start()
    {
        statTable.GenerateData(100);
        UpdateUI();

        
        
    }
   
    public void UpdateUI()
    {
        var crit = statTable.crit[critLevel - 1];
        critLevelText.text = "Lv" + crit.level.ToString();
        critDamageText.text = crit.critDamage.ToString() + "%";
        critCostText.text = crit.cost_c.ToString();
        if(curgold < statTable.crit[critLevel - 1].cost_c)
        {
            critCostText.color = Color.red;
        }

        var auto = statTable.auto[autoLevel - 1];
        autoLevelText.text = "Lv" + auto.level.ToString();
        autoCycleText.text = auto.autoAttackCycle.ToString();
        autoCostText.text = auto.cost_a.ToString();
        if (curgold < statTable.auto[autoLevel - 1].cost_a)
        {
            autoCostText.color = Color.red;
        }

        var gold = statTable.gold[goldLevel - 1];
        goldLevelText.text = "Lv" + gold.level.ToString();
        goldAmountText.text = gold.getGoldAmount.ToString() + "%";
        goldCostText.text = gold.cost_g.ToString();
        if (curgold < statTable.gold[goldLevel - 1].cost_g)
        {
            goldCostText.color = Color.red;
        }
    }

    public void OnClickCrit() //레벨 업 버튼 누르면 치명타 레벨 1씩 증가
    {
        if (curgold >= statTable.crit[critLevel - 1].cost_c)
        {
            curgold -= (critLevel * 10);
            critLevel++;
            var crit = statTable.crit[critLevel - 1];
            UpdateUI();
            Debug.Log($"[치명타 레벨 업!] 현재 레벨: {crit.level}, 치명타 데미지: {crit.critDamage}");
        }
        else
        {
            Debug.Log("골드 부족");
            
        }
    }

    public void OnClickAuto() //레벨 업 버튼 누르면 치명타 레벨 1씩 증가
    {
        if (curgold >= statTable.auto[autoLevel - 1].cost_a)
        {
            curgold -= (autoLevel * 10);
            autoLevel++;
            var auto = statTable.auto[autoLevel - 1];
            UpdateUI();
            Debug.Log($"[자동공격 레벨 업!] 현재 레벨: {auto.level}, 초당 자동공격 횟수: {auto.autoAttackCycle}");
        }
    }

    public void OnClickGold() //레벨 업 버튼 누르면 치명타 레벨 1씩 증가
    {
        if (curgold >= statTable.gold[goldLevel - 1].cost_g)
        {
            curgold -= (goldLevel * 10);
            goldLevel++;
            var gold = statTable.gold[goldLevel - 1];
            UpdateUI();
            Debug.Log($"[골드획득 레벨 업!] 현재 레벨: {gold.level}, 골드 획득량: {gold.getGoldAmount}");
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
}
