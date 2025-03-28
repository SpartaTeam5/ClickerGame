
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    private float moveSpeed = 1.5f;
    private float fadeDutation = 0.5f;

    public void SetText(float damage)
    {
        damageText.text = damage.ToString("F0");
        StartCoroutine(FadeOutAndReturn());
    }

    private IEnumerator FadeOutAndReturn()
    {
        Vector3 startPosition = transform.position;
        Color startColor = damageText.color;
        float elapsedTime = 0f; // ��� �ð�

        while (elapsedTime < fadeDutation)
        {
            transform.position = startPosition + new Vector3(0, moveSpeed * elapsedTime, 0);
            damageText.color = new Color(startColor.r, startColor.g, startColor.b, 1 - (elapsedTime / fadeDutation));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        damageText.color = startColor;  // ���� ����
        DamageTextPool.Instance.ReturnToPool(this);
    }
}
