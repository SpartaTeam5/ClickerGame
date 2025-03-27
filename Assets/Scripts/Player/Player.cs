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
            Debug.Log($"����: {crit.level}, ġ��Ÿ ������: {crit.critDamage}");
        }

        
        foreach (var atk in statTable.autoAttack)
        {
            Debug.Log($"����: {atk.level}, �ʴ� ���� Ƚ��: {atk.AutoAttack}");
        }

        
        foreach (var gold in statTable.gold)
        {
            Debug.Log($"����: {gold.level}, ��� ȹ�� ����: {gold.GetGold}");
        }
    }
}
