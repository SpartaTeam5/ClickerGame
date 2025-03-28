using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextPool : MonoBehaviour
{
    public static DamageTextPool Instance {  get; private set; }
    [SerializeField] private DamageText damageTextPrefab;
    private Queue<DamageText> pool = new Queue<DamageText>();

    private void Awake()
    {
        if(Instance == null) { Instance = this; }
        else Destroy(gameObject);       // ? �̰� ���� �� ��� ���ϴ� �� �ƴѰ�?
    }

    public DamageText GetFromPool()
    {
        if (pool.Count > 0)
        {
            DamageText damageText = pool.Dequeue();
            damageText.gameObject.SetActive(true);
            return damageText;
        }
        else
        {
            return Instantiate(damageTextPrefab);  // Ǯ�� ������� ���� ����
        }
    }
    public void ReturnToPool(DamageText damageText)
    {
        damageText.gameObject.SetActive(false);
        pool.Enqueue(damageText);
    }

}
