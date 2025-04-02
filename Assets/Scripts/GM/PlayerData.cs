using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inspector���� ���� �� �� �ֵ��� �ϱ� 
[Serializable]
public class PlayerData
{

    public int stage; //�������� ��ȣ
    public float gold; //���� ���� ���
    public int clickPower = 1; // Ŭ�� �� ������ (���׷��̵忡 ���� ����)
    public float criticalChance = 0.1f;// ġ��Ÿ �߻� Ȯ�� (0 ~ 1 ������ ��)
    public float criticalMultiplier = 2.0f;// ġ��Ÿ �� ������ ����
    public float goldBonus = 1.0f; // ��� ȹ�� �� ����Ǵ� ���ʽ� (���׷��̵� ������ ����)


    // ����߰�(���ʽ� ����)
    public void AddGold(float amount)
    {
        gold += amount * goldBonus;
    }

    // �����, ��� ���� �� true ��ȯ
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
