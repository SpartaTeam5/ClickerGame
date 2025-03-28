using System.Collections;
using System.Collections.Generic;
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

    

}

//public static class ScriptableObjectUtility
//{
//    public static T CreateRuntimeInstance<T>(T original) where T : ScriptableObject
//    {
//        return ScriptableObject.Instantiate(original);
//    }
//}


