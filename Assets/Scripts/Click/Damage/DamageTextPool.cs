using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextPool : MonoBehaviour
{
    public static DamageTextPool Instance { get; private set; }

    [SerializeField] private DamageText damageTextPrefab;
    private Queue<DamageText> pool = new Queue<DamageText>();

    public Transform parents;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
            return Instantiate(damageTextPrefab, parents);
        }
    }

    public void ReturnToPool(DamageText damageText)
    {
        damageText.gameObject.SetActive(false);
        pool.Enqueue(damageText);
    }
}
