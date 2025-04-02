using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float elapsedTime = 0f; // ��� �ð�

    void Update()
    {
        elapsedTime += Time.deltaTime; // �����Ӹ��� �ð� ����
        int minutes = Mathf.FloorToInt(elapsedTime / 60); // ��
        int seconds = Mathf.FloorToInt(elapsedTime % 60); // ��

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
