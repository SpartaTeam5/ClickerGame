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

    public void onClickAtk() //���� �� ��ư ������ ���ݷ� ���� ����
    {
        weaponTable.baseAttack++;
        Debug.Log($"{weaponTable.baseAttack}");
    }

    public void onClickCritChance() //���� �� ��ư ������ ġ��Ÿ Ȯ�� ���� ����
    {
        weaponTable.critChance++;
    }
}
