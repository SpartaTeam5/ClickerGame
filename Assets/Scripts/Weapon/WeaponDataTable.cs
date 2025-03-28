using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataTable", menuName = "Scriptable/WeaponDataTable", order = 1)]
public class WeaponDataTable : ScriptableObject
{
    public string weaponName; //���� �̸�

    public int baseAttack;  //�⺻ ���ݷ�
    public int critChance;  //ġ��Ÿ Ȯ��
    public int weaponLevel;  //���� ����

    public int atkIncrease; //��ȭ �� ���ݷ� ������
    public int critChanceIncrease; //��ȭ �� ġ��Ÿ Ȯ�� ������
    public int costAtk; //���ݷ� ��ȭ ���
    public int costCritChance;//ġ��Ÿ Ȯ�� ��ȭ ���

    

}

//public static class ScriptableObjectUtility
//{
//    public static T CreateRuntimeInstance<T>(T original) where T : ScriptableObject
//    {
//        return ScriptableObject.Instantiate(original);
//    }
//}


