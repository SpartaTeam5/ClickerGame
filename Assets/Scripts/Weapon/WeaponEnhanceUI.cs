using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponEnhanceUI : MonoBehaviour
{
    public WeaponDataTable weaponTable;
    public PlayerData playerdata;

    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;

    public GameObject buy1;
    public GameObject buy2;
    public GameObject buy3;

    public Button buyBtn1;
    public Button buyBtn2;
    public Button buyBtn3;

    

    public TextMeshProUGUI weapon1leveltext;
    public TextMeshProUGUI weapon2leveltext;
    public TextMeshProUGUI weapon3leveltext;
    public TextMeshProUGUI weapon4leveltext;
    public TextMeshProUGUI weapon1Atktext;
    public TextMeshProUGUI weapon2Atktext;
    public TextMeshProUGUI weapon3Atktext;
    public TextMeshProUGUI weapon4Atktext;
    public TextMeshProUGUI weapon1Chancetext;
    public TextMeshProUGUI weapon2Chancetext;
    public TextMeshProUGUI weapon3Chancetext;
    public TextMeshProUGUI weapon4Chancetext;
    public TextMeshProUGUI weapon1Enhancetext;
    public TextMeshProUGUI weapon2Enhancetext;
    public TextMeshProUGUI weapon3Enhancetext;
    public TextMeshProUGUI weapon4Enhancetext;


    public void Start()
    {
        buy1.SetActive(true);
        buy2.SetActive(true);
        buy3.SetActive(true);
    }

    public void UpdateEnhanceUI()
    {
        WeaponDataTable w1 = weapon1.GetComponent<WeaponData>().weapondata;
        WeaponDataTable w2 = weapon2.GetComponent<WeaponData>().weapondata;
        WeaponDataTable w3 = weapon3.GetComponent<WeaponData>().weapondata;
        WeaponDataTable w4 = weapon4.GetComponent<WeaponData>().weapondata;


        weapon1leveltext.text = "Lv " + w1.weaponLevel.ToString();
        weapon2leveltext.text = "Lv " + w2.weaponLevel.ToString();
        weapon3leveltext.text = "Lv " + w3.weaponLevel.ToString();
        weapon4leveltext.text = "Lv " + w4.weaponLevel.ToString();

        weapon1Atktext.text = "공격력 " + w1.baseAttack.ToString();
        weapon2Atktext.text = "공격력 " + w2.baseAttack.ToString();
        weapon3Atktext.text = "공격력 " + w3.baseAttack.ToString();
        weapon4Atktext.text = "공격력 " + w4.baseAttack.ToString();

        weapon1Chancetext.text = "치명타 확률 " + w1.critChance.ToString();
        weapon2Chancetext.text = "치명타 확률 " + w2.critChance.ToString();
        weapon3Chancetext.text = "치명타 확률 " + w3.critChance.ToString();
        weapon4Chancetext.text = "치명타 확률 " + w4.critChance.ToString();

        weapon1Enhancetext.text = w1.costEnhance.ToString();
        if (playerdata.gold < w1.costEnhance) // 골드 부족하면 빨간색으로 표시
        {
            weapon1Enhancetext.color = Color.red;
        }
        weapon2Enhancetext.text = w2.costEnhance.ToString();
        if (playerdata.gold < w2.costEnhance) // 골드 부족하면 빨간색으로 표시
        {
            weapon2Enhancetext.color = Color.red;
        }
        weapon3Enhancetext.text = w3.costEnhance.ToString();
        if (playerdata.gold < w3.costEnhance) // 골드 부족하면 빨간색으로 표시
        {
            weapon3Enhancetext.color = Color.red;
        }
        weapon4Enhancetext.text = w4.costEnhance.ToString();
        if (playerdata.gold < w4.costEnhance) // 골드 부족하면 빨간색으로 표시
        {
            weapon4Enhancetext.color = Color.red;
        }

        
    }

    public void OnClickBuy1()
    {
        if(playerdata.gold >= 1000)
        {
            buy1.SetActive(false);
        }
    }

    public void OnClickBuy2()
    {
        if (playerdata.gold >= 2000)
        {
            buy2.SetActive(false);
        }
    }

    public void OnClickBuy3()
    {
        if (playerdata.gold >= 3000)
        {
            buy3.SetActive(false);
        }
    }
}
