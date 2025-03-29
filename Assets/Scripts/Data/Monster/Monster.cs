using System;
using TMPro;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public TextMeshProUGUI monsterName;     // ���� �̸�
    public Slider healthSlider;             // ���� ü��
    public Image monsterImage;              // ���� �̹���
    public Animator animator;               // ���� �ִϸ��̼�

    private float curValue;
    private float maxValue;
    private float rewardGold; // ���� óġ �� �ִ� ���

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Init(MonsterData monsterData)
    {

        monsterName.text = monsterData.MonsterName;
        healthSlider.value = monsterData.health;
        monsterImage.sprite = monsterData.sprite;

        maxValue = monsterData.maxhealth;
        curValue = monsterData.health;
        rewardGold = monsterData.money;

        healthSlider.value = GetPercentage();

        monsterImage.raycastTarget = true;
        animator.SetBool("Die", false);

    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void TakeDamage(float damage)
    {
        curValue -= damage;
        if (curValue < 0) curValue = 0;

        healthSlider.value = GetPercentage();
        animator.SetTrigger("Attack");

        // ������ �ؽ�Ʈ ǥ��
        DamageText damageText = DamageTextPool.Instance.GetFromPool();
        damageText.transform.position = Input.mousePosition;
        damageText.SetText(damage);

        if (curValue == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        monsterImage.raycastTarget = false;
        animator.SetBool("Die", true);
        //GameManager.Instance.AddGold(rewardGold);
        UIStage.Instance.OnMonsterDeath();
    }
}


