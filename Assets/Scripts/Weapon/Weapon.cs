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

    public Image monsterImage; // 몬스터 이미지

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
                weaponAtkText.text = "공격력 " + weaponTable.baseAttack.ToString();
                weaponCritText.text = "치명타 확률 " + weaponTable.critChance.ToString();
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
        // 현재 장착된 무기 찾아서 복사본 넘기기
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
        monsterImage.raycastTarget = false; // 강화창 활성시
        weaponEnhanceUI.SetActive(true);
    }

    public void onClickEnhance(GameObject weapon) //강화 버튼 누르면 레벨 증가
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

            Debug.Log($"[강화됨] {weaponTable.weaponName} → Lv.{weaponTable.weaponLevel}, 공격력: {weaponTable.baseAttack}, 치명타: {weaponTable.critChance}%");
        }

        if (GameManager.Instance.playerData.gold < weaponTable.costEnhance)
            Debug.Log("골드 부족");
        if(weaponTable.weaponLevel == weaponTable.weaponaMaxLevel)
        {
            Debug.Log("최대 레벨 달성");
        }

        UpdateUI();
        weaponEnhance.UpdateEnhanceUI();
        GameManager.Instance.UpdateGoldUI();

    }



    public void OnClickBack()
    {
        weaponEnhanceUI.SetActive(false);
        monsterImage.raycastTarget = true; // 비활성 시
    }


}


