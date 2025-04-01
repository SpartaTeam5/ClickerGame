using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public WeaponDataTable weaponTable;
    public WeaponEnhanceUI weaponEnhance;
    public Player player;
    //public PlayerData playerData;

    public Image monsterImage; // ���� �̹���

    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    public GameObject weaponEnhanceUI;

    public Button equipWeaponBtn1;
    public Button equipWeaponBtn2;
    public Button equipWeaponBtn3;
    public Button equipWeaponBtn4;
    public Button equipBtn;
    public Button enhanceBtn1;
    public Button enhanceBtn2;
    public Button enhanceBtn3;
    public Button enhanceBtn4;
    public Button backBtn;


    public TextMeshProUGUI weaponText;
    public TextMeshProUGUI weaponLevelText;
    public TextMeshProUGUI weaponAtkText;
    public TextMeshProUGUI weaponCritText;

   
    public void Start()
    {
        weaponTable.InitializeWeaponData();
        weapon1.GetComponent<WeaponData>().isEquipped = true;
        UpdateUI();
        weaponEnhance.UpdateEnhanceUI();
    }

    public void UpdateUI()
    {
        weapon1.SetActive(weapon1.GetComponent<WeaponData>().isEquipped);
        weapon2.SetActive(weapon2.GetComponent<WeaponData>().isEquipped);
        weapon3.SetActive(weapon3.GetComponent<WeaponData>().isEquipped);
        weapon4.SetActive(weapon4.GetComponent<WeaponData>().isEquipped);


        GameObject[] weapons = { weapon1, weapon2, weapon3, weapon4 };

        foreach (GameObject weapon in weapons)
        {
            WeaponData weaponData = weapon.GetComponent<WeaponData>();
            weapon.SetActive(weaponData.isEquipped);

            if (weaponData.isEquipped)
            {
                weaponTable = weaponData.weapondata;
                GameManager.Instance.weaponDataTable = weaponTable;
                weaponText.text = weaponTable.weaponName;
                weaponLevelText.text = "Lv " + weaponTable.weaponLevel.ToString();
                weaponAtkText.text = "���ݷ� " + weaponTable.baseAttack.ToString();
                weaponCritText.text = "ġ��Ÿ Ȯ�� " + weaponTable.critChance.ToString();
            }


        }
    }

    public void OnClickEquip(GameObject selectedWeapon)
    {
        GameObject[] weapons = { weapon1, weapon2, weapon3, weapon4 };

        foreach (GameObject weapon in weapons)
        {
            bool isSelected = (weapon == selectedWeapon);
            weapon.SetActive(isSelected);
            weapon.GetComponent<WeaponData>().isEquipped = isSelected;

        }

        UpdateUI();
    }

    public void OpenWeaponEnhance()
    {
        // ���� ������ ���� ã�Ƽ� ���纻 �ѱ��
        GameObject[] weapons = { weapon1, weapon2, weapon3, weapon4 };

        foreach (GameObject weapon in weapons)
        {
            WeaponData weaponData = weapon.GetComponent<WeaponData>();
            if (weaponData.isEquipped)
            {
                weaponEnhance.weaponTable = weaponData.weapondata;
                break;
            }
        }
        monsterImage.raycastTarget = false; // ��ȭâ Ȱ����
        weaponEnhanceUI.SetActive(true);
    }

    public void onClickEnhance(GameObject weapon) //��ȭ ��ư ������ ���� ����
    {

        WeaponData weaponData = weapon.GetComponent<WeaponData>();
        weaponTable = weaponData.weapondata;

        if (GameManager.Instance.playerData.gold >= weaponTable.costEnhance && weaponTable.weaponLevel < weaponTable.weaponaMaxLevel)
        {
            weaponTable.weaponLevel++;
            weaponTable.baseAttack += weaponTable.atkIncrease;
            weaponTable.critChance += weaponTable.critChanceIncrease;
            GameManager.Instance.playerData.gold -= weaponTable.costEnhance;
            weaponTable.costEnhance *= 2f;

            Debug.Log($"[��ȭ��] {weaponTable.weaponName} �� Lv.{weaponTable.weaponLevel}, ���ݷ�: {weaponTable.baseAttack}, ġ��Ÿ: {weaponTable.critChance}%");
        }

        if (GameManager.Instance.playerData.gold < weaponTable.costEnhance)
            Debug.Log("��� ����");
        if(weaponTable.weaponLevel == weaponTable.weaponaMaxLevel)
        {
            Debug.Log("�ִ� ���� �޼�");
        }

        UpdateUI();
        weaponEnhance.UpdateEnhanceUI();
        GameManager.Instance.UpdateGoldUI();

    }



    public void OnClickBack()
    {
        weaponEnhanceUI.SetActive(false);
        monsterImage.raycastTarget = true; // ��Ȱ�� ��
    }


}


