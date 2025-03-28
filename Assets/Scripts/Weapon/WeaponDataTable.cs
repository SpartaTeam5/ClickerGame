using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataTable", menuName = "Scriptable/WeaponDataTable", order = 1)]
public class WeaponDataTable : ScriptableObject
{
    public string weaponName; //무기 이름

    public int baseAttack;  //기본 공격력
    public int critChance;  //치명타 확률
    public int weaponLevel;  //무기 레벨

    public int atkIncrease; //강화 시 공격력 증가량
    public int critChanceIncrease; //강화 시 치명타 확률 증가량
    public int costAtk; //공격력 강화 비용
    public int costCritChance;//치명타 확률 강화 비용

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
    /// Editor에서 초기값 세팅하는 함수 (메뉴 버튼으로 실행 가능)
    /// </summary>
    [MenuItem("Tools/Initialize Weapon Data")]
    public void InitializeWeaponData()
    {
        WeaponDataTable monsterBall = AssetDatabase.LoadAssetAtPath<WeaponDataTable>("Assets/Prefabs/Weapon/PokeBall.asset");
        WeaponDataTable superBall = AssetDatabase.LoadAssetAtPath<WeaponDataTable>("Assets/Prefabs/Weapon/GreatBall.asset");
        WeaponDataTable ultraBall = AssetDatabase.LoadAssetAtPath<WeaponDataTable>("Assets/Prefabs/Weapon/UltraBall.asset");
        WeaponDataTable masterBall = AssetDatabase.LoadAssetAtPath<WeaponDataTable>("Assets/Prefabs/Weapon/MasterBall.asset");

        // 각 무기의 초기 값 설정
        monsterBall.weaponName = "몬스터볼";
        monsterBall.baseAttack = 5;
        monsterBall.weaponLevel = 1;
        monsterBall.critChance = 15;
        monsterBall.atkIncrease = 1;
        monsterBall.critChanceIncrease = 5;
        monsterBall.costAtk = 15;
        monsterBall.costCritChance = 15;

        superBall.weaponName = "슈퍼볼";
        superBall.baseAttack = 10;
        superBall.weaponLevel = 1;
        superBall.critChance = 20;
        superBall.atkIncrease = 2;
        superBall.critChanceIncrease = 5;
        superBall.costAtk = 15;
        superBall.costCritChance = 15;

        ultraBall.weaponName = "울트라볼";
        ultraBall.baseAttack = 15;
        ultraBall.weaponLevel = 1;
        ultraBall.critChance = 25;
        ultraBall.atkIncrease = 5;
        ultraBall.critChanceIncrease = 5;
        ultraBall.costAtk = 15;
        ultraBall.costCritChance = 15;

        masterBall.weaponName = "마스터볼";
        masterBall.baseAttack = 20;
        masterBall.weaponLevel = 1;
        masterBall.critChance = 30;
        masterBall.atkIncrease = 5;
        masterBall.critChanceIncrease = 5;
        masterBall.costAtk = 15;
        masterBall.costCritChance = 15;

        // 저장
        EditorUtility.SetDirty(monsterBall);
        EditorUtility.SetDirty(superBall);
        EditorUtility.SetDirty(ultraBall);
        EditorUtility.SetDirty(masterBall);
        AssetDatabase.SaveAssets();
    }


#endif


}




