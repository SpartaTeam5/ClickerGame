using System;
using System.Collections;
using TMPro;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public TextMeshProUGUI monsterName;     // ���� �̸�
    public Slider healthSlider;             // ���� ü��
    public Image monsterImage;              // ���� �̹���
    public Image pcImage;                   // PC �̹���

    public Animator animator;               // ���� �ִϸ��̼�

    private float curValue;
    private float maxValue;
    private float rewardGold; // ���� óġ �� �ִ� ���

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

        // ������ �ؽ�Ʈ ǥ��
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
        float starValue = healthSlider.value;   // ���� ü�� ����
        float elapsedTime = 0f;
        float duration = 0.5f;  // ���� �ӵ� (0.5��)

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            healthSlider.value = Mathf.Lerp(starValue, targetValue, elapsedTime / duration);
            yield return null;  // �� ������ ���
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


