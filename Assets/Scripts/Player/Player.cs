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

    public void Start()
    {
        statTable.GenerateData(100);
        UpdateUI();

        //Check();
        
    }
    //public void Check() //�׽�Ʈ �ڵ�
    //{
        
    //    foreach (var crit in statTable.crit)
    //    {
    //        Debug.Log($"����: {crit.level}, ġ��Ÿ ������: {crit.critDamage}");
    //    }

        
    //    foreach (var atk in statTable.auto)
    //    {
    //        Debug.Log($"����: {atk.level}, �ʴ� ���� Ƚ��: {atk.autoAttack}");
    //    }

        
    //    foreach (var gold in statTable.gold)
    //    {
    //        Debug.Log($"����: {gold.level}, ��� ȹ�� ����: {gold.getGold}");
    //    }
    //}

    public void UpdateUI()
    {
        var crit = statTable.crit[critLevel - 1];
        critLevelText.text = crit.level.ToString();
        critDamageText.text = crit.critDamage.ToString() + "%";
        critCostText.text = crit.cost_c.ToString();
        var auto = statTable.auto[autoLevel - 1];
        autoLevelText.text = auto.level.ToString();
        autoCycleText.text = auto.autoAttackCycle.ToString();
        autoCostText.text = auto.cost_a.ToString();
        var gold = statTable.gold[goldLevel - 1];
        goldLevelText.text = gold.level.ToString();
        goldAmountText.text = gold.getGoldAmount.ToString() + "%";
        goldCostText.text = gold.cost_g.ToString();
    }

    public void OnClickCrit() //���� �� ��ư ������ ġ��Ÿ ���� 1�� ����
    {
        critLevel++;
        var crit = statTable.crit[critLevel - 1];
        UpdateUI();
        Debug.Log($"[ġ��Ÿ ���� ��!] ���� ����: {crit.level}, ġ��Ÿ ������: {crit.critDamage}");
    }

    public void OnClickAuto() //���� �� ��ư ������ ġ��Ÿ ���� 1�� ����
    {
        autoLevel++;
        var auto = statTable.auto[autoLevel - 1];
        UpdateUI();
        Debug.Log($"[�ڵ����� ���� ��!] ���� ����: {auto.level}, �ʴ� �ڵ����� Ƚ��: {auto.autoAttackCycle}");
    }

    public void OnClickGold() //���� �� ��ư ������ ġ��Ÿ ���� 1�� ����
    {
        goldLevel++;
        var gold = statTable.gold[goldLevel - 1];
        UpdateUI();
        Debug.Log($"[���ȹ�� ���� ��!] ���� ����: {gold.level}, ��� ȹ�淮: {gold.getGoldAmount}");
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
