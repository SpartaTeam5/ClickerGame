using UnityEngine;

public class StatManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static StatManager Instance;
    private void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null) Instance = this;
    }

    // ���� ���ݷ�
    public float GetFinalDamage()
    {
        return GameManager.Instance.weaponDataTable.baseAttack;
    }

    public float GetAutoDamage()
    {
        return GameManager.Instance.playerStatTable.auto[GameManager.Instance.player.autoLevel].autoAttackCycle;
    }

    // ���� ġ��Ÿ Ȯ��
    public float GetCriticalChance()
    {
        return GameManager.Instance.weaponDataTable.critChance;
    }

    // ���� ġ��Ÿ ����
    public float GetCriticalDamage()
    {

        return GameManager.Instance.playerStatTable.crit[GameManager.Instance.player.critLevel].critDamage;
    }

    public float GetGoldAmount()
    {
        return GameManager.Instance.playerStatTable.gold[GameManager.Instance.player.goldLevel].getGoldAmount;
    }
}
