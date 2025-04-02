using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float elapsedTime = 0f; // 경과 시간

    void Update()
    {
        elapsedTime += Time.deltaTime; // 프레임마다 시간 누적
        int minutes = Mathf.FloorToInt(elapsedTime / 60); // 분
        int seconds = Mathf.FloorToInt(elapsedTime % 60); // 초

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
