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
    public int weaponaMaxLevel=25; //���� �ִ� ����

    public int atkIncrease; //��ȭ �� ���ݷ� ������
    public int critChanceIncrease; //��ȭ �� ġ��Ÿ Ȯ�� ������
    public float costEnhance; // ��ȭ ���
    
    public bool isInitialized = false; // �ʱ�ȭ ���� üũ��

    public void WeaponData(string weaponname, int baseattack, int critchance, int weaponlevel, int maxlevel, int atkincrease, int critchanceincrease, float costenhance)
    {
        weaponName = weaponname;
        baseAttack = baseattack;
        critChance = critchance;
        weaponLevel = weaponlevel;
        weaponaMaxLevel = maxlevel;
        atkIncrease = atkincrease;
        critChanceIncrease = critchanceincrease;
        costEnhance = costenhance;
        
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
        monsterBall.weaponaMaxLevel = 25;
        monsterBall.critChance = 10;
        monsterBall.atkIncrease = 1;
        monsterBall.critChanceIncrease = 3;
        monsterBall.costEnhance = 10f;
        

        superBall.weaponName = "���ۺ�";
        superBall.baseAttack = 10;
        superBall.weaponLevel = 1;
        superBall.weaponaMaxLevel = 25;
        superBall.critChance = 15;
        superBall.atkIncrease = 2;
        superBall.critChanceIncrease = 3;
        superBall.costEnhance = 15f;
        

        ultraBall.weaponName = "��Ʈ��";
        ultraBall.baseAttack = 15;
        ultraBall.weaponLevel = 1;
        ultraBall.weaponaMaxLevel = 25;
        ultraBall.critChance = 23;
        ultraBall.atkIncrease = 5;
        ultraBall.critChanceIncrease = 3;
        ultraBall.costEnhance = 20f;
        

        masterBall.weaponName = "�����ͺ�";
        masterBall.baseAttack = 20;
        masterBall.weaponLevel = 1;
        masterBall.weaponaMaxLevel = 25;
        masterBall.critChance = 28;
        masterBall.atkIncrease = 5;
        masterBall.critChanceIncrease = 3;
        masterBall.costEnhance = 30f;
        

        // ����
        EditorUtility.SetDirty(monsterBall);
        EditorUtility.SetDirty(superBall);
        EditorUtility.SetDirty(ultraBall);
        EditorUtility.SetDirty(masterBall);
        AssetDatabase.SaveAssets();
    }


#endif


}




