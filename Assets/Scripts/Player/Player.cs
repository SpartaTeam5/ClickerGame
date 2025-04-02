using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlayerStatTable statTable;

    public int critLevel = 0;
    public int autoLevel = 0; // 초기 자동공격 레벨을 0으로 설정
    public int goldLevel = 0;

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

    }

    public void UpdateUI()
    {
        CritData crit = statTable.crit[critLevel];
        critLevelText.text = "Lv" + crit.level.ToString();
        critDamageText.text = " X " + crit.critDamage.ToString();
        critCostText.text = crit.cost_c.ToString();
        if (GameManager.Instance.playerData.gold < statTable.crit[critLevel].cost_c) // 골드 부족하면 빨간색으로 표시
        {
            critCostText.color = Color.red;
        }
        else
        {
            critCostText.color = Color.black;
        }

        AutoAttackData auto = statTable.auto[autoLevel];
        autoLevelText.text = "Lv" + auto.level.ToString();
        autoCycleText.text = auto.autoAttackCycle.ToString() + "회/초";
        autoCostText.text = auto.cost_a.ToString();
        if (GameManager.Instance.playerData.gold < statTable.auto[autoLevel].cost_a) // 골드 부족하면 빨간색으로 표시
        {
            autoCostText.color = Color.red;
        }
        else
        {
            autoCostText.color = Color.black;
        }

        GoldData gold = statTable.gold[goldLevel];
        goldLevelText.text = "Lv" + gold.level.ToString();
        goldAmountText.text = gold.getGoldAmount.ToString() + "%";
        goldCostText.text = gold.cost_g.ToString();
        if (GameManager.Instance.playerData.gold < statTable.gold[goldLevel].cost_g) // 골드 부족하면 빨간색으로 표시
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
        if (GameManager.Instance.playerData.gold >= statTable.crit[critLevel].cost_c && critLevel < 99)
        {
            GameManager.Instance.playerData.gold -= statTable.crit[critLevel].cost_c;
            critLevel++;
            var crit = statTable.crit[critLevel];
            UpdateUI();
            GameManager.Instance.UpdateGoldUI();
            Debug.Log($"[치명타 레벨 업!] 현재 레벨: {crit.level}, 치명타 데미지: {crit.critDamage}");
        }
        else
        {
            return;
            Debug.Log("골드 부족");
        }
    }

    public void OnClickAuto() //레벨 업 버튼 누르면 자동공격 레벨 1씩 증가
    {
        if (GameManager.Instance.playerData.gold >= statTable.auto[autoLevel].cost_a && autoLevel < 99)
        {
            GameManager.Instance.playerData.gold -= statTable.auto[autoLevel].cost_a;
            autoLevel++;
            var auto = statTable.auto[autoLevel];
            UpdateUI();
            GameManager.Instance.UpdateGoldUI();
            clickAttack.ApplyStatsToClickAttack(); // 자동공격 주기 업데이트 및 재시작
            Debug.Log($"[자동공격 레벨 업!] 현재 레벨: {auto.level}, 초당 자동공격 횟수: {auto.autoAttackCycle}");
        }
        else
        {
            Debug.Log("골드 부족");
        }
    }

    public void OnClickGold() //레벨 업 버튼 누르면 골드 획득 레벨 1씩 증가
    {
        if (GameManager.Instance.playerData.gold >= statTable.gold[goldLevel].cost_g && goldLevel < 99)
        {
            GameManager.Instance.playerData.gold -= statTable.gold[goldLevel].cost_g;
            goldLevel++;
            var gold = statTable.gold[goldLevel];
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
}
