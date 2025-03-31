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
    public int weaponaMaxLevel=25; //무기 최대 레벨

    public int atkIncrease; //강화 시 공격력 증가량
    public int critChanceIncrease; //강화 시 치명타 확률 증가량
    public float costEnhance; // 강화 비용
    
    public bool isInitialized = false; // 초기화 여부 체크용

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
        monsterBall.weaponaMaxLevel = 25;
        monsterBall.critChance = 10;
        monsterBall.atkIncrease = 1;
        monsterBall.critChanceIncrease = 3;
        monsterBall.costEnhance = 10f;
        

        superBall.weaponName = "슈퍼볼";
        superBall.baseAttack = 10;
        superBall.weaponLevel = 1;
        superBall.weaponaMaxLevel = 25;
        superBall.critChance = 15;
        superBall.atkIncrease = 2;
        superBall.critChanceIncrease = 3;
        superBall.costEnhance = 15f;
        

        ultraBall.weaponName = "울트라볼";
        ultraBall.baseAttack = 15;
        ultraBall.weaponLevel = 1;
        ultraBall.weaponaMaxLevel = 25;
        ultraBall.critChance = 23;
        ultraBall.atkIncrease = 5;
        ultraBall.critChanceIncrease = 3;
        ultraBall.costEnhance = 20f;
        

        masterBall.weaponName = "마스터볼";
        masterBall.baseAttack = 20;
        masterBall.weaponLevel = 1;
        masterBall.weaponaMaxLevel = 25;
        masterBall.critChance = 28;
        masterBall.atkIncrease = 5;
        masterBall.critChanceIncrease = 3;
        masterBall.costEnhance = 30f;
        

        // 저장
        EditorUtility.SetDirty(monsterBall);
        EditorUtility.SetDirty(superBall);
        EditorUtility.SetDirty(ultraBall);
        EditorUtility.SetDirty(masterBall);
        AssetDatabase.SaveAssets();
    }


#endif


}




