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

    

    public void Start()
    {
        //statTable.GenerateData(100);
        statTable = GameManager.Instance.playerStatTable;
        statTable.GenerateData(100);
        UpdateUI();
                        
    }
   
    public void UpdateUI()
    {
        CritData crit = statTable.crit[critLevel - 1];
        critLevelText.text = "Lv" + crit.level.ToString();
        critDamageText.text = crit.critDamage.ToString() + "%";
        critCostText.text = crit.cost_c.ToString();
        if(GameManager.Instance.playerData.gold < statTable.crit[critLevel - 1].cost_c) // ��� �����ϸ� ���������� ǥ��
        {
            critCostText.color = Color.red; 
        }
        else
        {
            critCostText.color = Color.black;
        }

        AutoAttackData auto = statTable.auto[autoLevel - 1];
        autoLevelText.text = "Lv" + auto.level.ToString();
        autoCycleText.text = auto.autoAttackCycle.ToString() + "ȸ/��";
        autoCostText.text = auto.cost_a.ToString();
        if (GameManager.Instance.playerData.gold < statTable.auto[autoLevel - 1].cost_a) // ��� �����ϸ� ���������� ǥ��
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
        if (GameManager.Instance.playerData.gold < statTable.gold[goldLevel - 1].cost_g) // ��� �����ϸ� ���������� ǥ��
        {
            goldCostText.color = Color.red;
        }
        else
        {
            goldCostText.color= Color.black;
        }
    }

    public void OnClickCrit() //���� �� ��ư ������ ġ��Ÿ ���� 1�� ����
    {
        if (GameManager.Instance.playerData.gold >= statTable.crit[critLevel - 1].cost_c)
        {
            GameManager.Instance.playerData.gold -= (critLevel * 10);
            critLevel++;
            var crit = statTable.crit[critLevel - 1];
            UpdateUI();
            GameManager.Instance.UpdateGoldUI();
            Debug.Log($"[ġ��Ÿ ���� ��!] ���� ����: {crit.level}, ġ��Ÿ ������: {crit.critDamage}");
        }
        else
        {
            Debug.Log("��� ����");
            
        }
    }

    public void OnClickAuto() //���� �� ��ư ������ ġ��Ÿ ���� 1�� ����
    {
        if (GameManager.Instance.playerData.gold >= statTable.auto[autoLevel - 1].cost_a)
        {
            GameManager.Instance.playerData.gold -= (autoLevel * 10);
            autoLevel++;
            var auto = statTable.auto[autoLevel - 1];
            UpdateUI();
            GameManager.Instance.UpdateGoldUI();
            Debug.Log($"[�ڵ����� ���� ��!] ���� ����: {auto.level}, �ʴ� �ڵ����� Ƚ��: {auto.autoAttackCycle}");
        }
    }

    public void OnClickGold() //���� �� ��ư ������ ġ��Ÿ ���� 1�� ����
    {
        if (GameManager.Instance.playerData.gold >= statTable.gold[goldLevel - 1].cost_g)
        {
            GameManager.Instance.playerData.gold -= (goldLevel * 10);
            goldLevel++;
            var gold = statTable.gold[goldLevel - 1];
            UpdateUI();
            GameManager.Instance.UpdateGoldUI();
            Debug.Log($"[���ȹ�� ���� ��!] ���� ����: {gold.level}, ��� ȹ�淮: {gold.getGoldAmount}");
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
