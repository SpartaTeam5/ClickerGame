using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStatTable statTable;
    
      

    public void Start()
    {
        statTable.GenerateData(10);

        Check();
        
    }
    public void Check()
    {
        
        foreach (var crit in statTable.crit)
        {
            Debug.Log($"레벨: {crit.level}, 치명타 데미지: {crit.critDamage}");
        }

        
        foreach (var atk in statTable.autoAttack)
        {
            Debug.Log($"레벨: {atk.level}, 초당 공격 횟수: {atk.AutoAttack}");
        }

        
        foreach (var gold in statTable.gold)
        {
            Debug.Log($"레벨: {gold.level}, 골드 획득 배율: {gold.GetGold}");
        }
    }
}
