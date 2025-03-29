using System;
using TMPro;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public TextMeshProUGUI monsterName;     // 몬스터 이름
    public Slider healthSlider;             // 몬스터 체력
    public Image monsterImage;              // 몬스터 이미지
    public Animator animator;               // 몬스터 애니메이션

    private float curValue;
    private float maxValue;
    private float rewardGold; // 몬스터 처치 시 주는 골드

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

        // 데미지 텍스트 표시
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


