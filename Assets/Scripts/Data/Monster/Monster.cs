using System;
using System.Collections;
using TMPro;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public TextMeshProUGUI monsterName;     // 몬스터 이름
    public Slider healthSlider;             // 몬스터 체력
    public Image monsterImage;              // 몬스터 이미지
    public Image pcImage;                   // PC 이미지

    public Animator animator;               // 몬스터 애니메이션

    private float curValue;
    private float maxValue;
    private float rewardGold; // 몬스터 처치 시 주는 골드

    private UIStage uiStage;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        uiStage = GetComponentInParent<UIStage>();
        pcImage.gameObject.SetActive(false);

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
        pcImage.gameObject.SetActive(false);
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void TakeDamage(float damage)
    {
        curValue -= damage;
        if (curValue < 0) curValue = 0;

        StartCoroutine(SmoothHealthBar(GetPercentage()));
        //healthSlider.value = GetPercentage();
        animator.SetTrigger("Attack");

        // 데미지 텍스트 표시
        DamageText damageText = DamageTextPool.Instance.GetFromPool();
        damageText.transform.position = Input.mousePosition;
        damageText.SetText(damage);

        if (curValue == 0)
        {
            Die();
            pcImage.gameObject.SetActive(true);

        }
    }

    private IEnumerator SmoothHealthBar(float targetValue)
    {
        float starValue = healthSlider.value;   // 현재 체력 비율
        float elapsedTime = 0f;
        float duration = 0.5f;  // 감소 속도 (0.5초)

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            healthSlider.value = Mathf.Lerp(starValue, targetValue, elapsedTime / duration);
            yield return null;  // 한 프레임 대기
        }
        healthSlider.value = targetValue;
    }

    private void Die()
    {
        monsterImage.raycastTarget = false;
        animator.SetBool("Die", true);

        GameManager.Instance.AddGold(rewardGold);

        uiStage.OnMonsterDeath();
        uiStage.KillMonster();
    }
}


