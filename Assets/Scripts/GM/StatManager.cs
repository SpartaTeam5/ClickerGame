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

    // ���� ġ��Ÿ Ȯ��
    public float GetCriticalChance()
    {
        return GameManager.Instance.weaponDataTable.critChance;
    }

    // ���� ġ��Ÿ ����
    public float GetCriticalDamage()
    {

        return GameManager.Instance.weaponDataTable.baseAttack *
            GameManager.Instance.playerStatTable.crit[GameManager.Instance.player.critLevel - 1].critDamage;
    }

    public float GetGoldAmount()
    {
        return GameManager.Instance.playerStatTable.gold[GameManager.Instance.player.goldLevel - 1].getGoldAmount;
    }
}
