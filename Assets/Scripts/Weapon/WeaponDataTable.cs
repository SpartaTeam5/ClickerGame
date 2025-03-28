using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    public void WeaponData(string weaponname, int baseattack, int critchance, int weaponlevel, int atkincrease, int critchanceincrease, int costatk, int costcritchance)
    {
        weaponName = weaponname;
        baseAttack = baseattack;
        critChance = critchance;
        weaponLevel = weaponlevel;
        atkIncrease = atkincrease;
        critChanceIncrease = critchanceincrease;
        costAtk = costatk;
        costCritChance = costcritchance;
    }
    
#if UNITY_EDITOR
    /// <summary>
    /// Editor���� �ʱⰪ �����ϴ� �Լ� (�޴� ��ư���� ���� ����)
    /// </summary>
    [MenuItem("Tools/Initialize Weapon Data")]
    public void InitializeWeaponData()
    {
        WeaponDataTable monsterBall = AssetDatabase.LoadAssetAtPath<WeaponDataTable>("Assets/Prefabs/Weapon/PokeBall.asset");
        WeaponDataTable superBall = AssetDatabase.LoadAssetAtPath<WeaponDataTable>("Assets/Prefabs/Weapon/GreatBall.asset");
        WeaponDataTable ultraBall = AssetDatabase.LoadAssetAtPath<WeaponDataTable>("Assets/Prefabs/Weapon/UltraBall.asset");
        WeaponDataTable masterBall = AssetDatabase.LoadAssetAtPath<WeaponDataTable>("Assets/Prefabs/Weapon/MasterBall.asset");

        // �� ������ �ʱ� �� ����
        monsterBall.weaponName = "���ͺ�";
        monsterBall.baseAttack = 5;
        monsterBall.weaponLevel = 1;
        monsterBall.critChance = 15;
        monsterBall.atkIncrease = 1;
        monsterBall.critChanceIncrease = 5;
        monsterBall.costAtk = 15;
        monsterBall.costCritChance = 15;

        superBall.weaponName = "���ۺ�";
        superBall.baseAttack = 10;
        superBall.weaponLevel = 1;
        superBall.critChance = 20;
        superBall.atkIncrease = 2;
        superBall.critChanceIncrease = 5;
        superBall.costAtk = 15;
        superBall.costCritChance = 15;

        ultraBall.weaponName = "��Ʈ��";
        ultraBall.baseAttack = 15;
        ultraBall.weaponLevel = 1;
        ultraBall.critChance = 25;
        ultraBall.atkIncrease = 5;
        ultraBall.critChanceIncrease = 5;
        ultraBall.costAtk = 15;
        ultraBall.costCritChance = 15;

        masterBall.weaponName = "�����ͺ�";
        masterBall.baseAttack = 20;
        masterBall.weaponLevel = 1;
        masterBall.critChance = 30;
        masterBall.atkIncrease = 5;
        masterBall.critChanceIncrease = 5;
        masterBall.costAtk = 15;
        masterBall.costCritChance = 15;

        // ����
        EditorUtility.SetDirty(monsterBall);
        EditorUtility.SetDirty(superBall);
        EditorUtility.SetDirty(ultraBall);
        EditorUtility.SetDirty(masterBall);
        AssetDatabase.SaveAssets();
    }


#endif


}




