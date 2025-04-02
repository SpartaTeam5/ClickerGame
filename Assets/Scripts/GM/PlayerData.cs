using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inspector에서 값을 볼 수 있도록 하기 
[Serializable]
public class PlayerData
{

    public int stage; //스테이지 번호
    public float gold; //보유 중인 골드
    public int clickPower = 1; // 클릭 시 데미지 (업그레이드에 따라 증가)
    public float criticalChance = 0.1f;// 치명타 발생 확률 (0 ~ 1 사이의 값)
    public float criticalMultiplier = 2.0f;// 치명타 시 데미지 배율
    public float goldBonus = 1.0f; // 골드 획득 시 적용되는 보너스 (업그레이드 등으로 증가)


    // 골드추가(보너스 적용)
    public void AddGold(float amount)
    {
        gold += amount * goldBonus;
    }

    // 골드사용, 사용 성공 시 true 반환
    public bool SpendGold(float amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            return true;
        }
        return false;
    }
}
