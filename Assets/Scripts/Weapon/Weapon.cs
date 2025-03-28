using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public WeaponDataTable weaponTable;

    
    public void Start()
    {
        
    }

    public void onClickAtk() //레벨 업 버튼 누르면 공격력 레벨 증가
    {
        weaponTable.baseAttack++;
        Debug.Log($"{weaponTable.baseAttack}");
    }

    public void onClickCritChance() //레벨 업 버튼 누르면 치명타 확률 레벨 증가
    {
        weaponTable.critChance++;
    }
}
