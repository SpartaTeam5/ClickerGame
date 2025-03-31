using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponEnhanceUI : MonoBehaviour
{
    public WeaponDataTable weaponTable;
    public Player player;

    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;

    

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
    public TextMeshProUGUI weapon3Enhanceext;
    public TextMeshProUGUI weapon4Enhancetext;

   
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

        weapon1Atktext.text = "���ݷ� " + w1.baseAttack.ToString();
        weapon2Atktext.text = "���ݷ� " + w2.baseAttack.ToString();
        weapon3Atktext.text = "���ݷ� " + w3.baseAttack.ToString();
        weapon4Atktext.text = "���ݷ� " + w4.baseAttack.ToString();

        weapon1Chancetext.text = "ġ��Ÿ Ȯ�� " + w1.critChance.ToString();
        weapon2Chancetext.text = "ġ��Ÿ Ȯ�� " + w2.critChance.ToString();
        weapon3Chancetext.text = "ġ��Ÿ Ȯ�� " + w3.critChance.ToString();
        weapon4Chancetext.text = "ġ��Ÿ Ȯ�� " + w4.critChance.ToString();

        weapon1Enhancetext.text = w1.costEnhance.ToString();
        if (player.curgold < w1.costEnhance) // ��� �����ϸ� ���������� ǥ��
        {
            weapon1Enhancetext.color = Color.red;
        }
        weapon2Enhancetext.text = w2.costEnhance.ToString();
        if (player.curgold < w2.costEnhance) // ��� �����ϸ� ���������� ǥ��
        {
            weapon1Enhancetext.color = Color.red;
        }
        weapon3Enhanceext.text = w3.costEnhance.ToString();
        if (player.curgold < w3.costEnhance) // ��� �����ϸ� ���������� ǥ��
        {
            weapon1Enhancetext.color = Color.red;
        }
        weapon4Enhancetext.text = w4.costEnhance.ToString();
        if (player.curgold < w4.costEnhance) // ��� �����ϸ� ���������� ǥ��
        {
            weapon1Enhancetext.color = Color.red;
        }


    }
}
