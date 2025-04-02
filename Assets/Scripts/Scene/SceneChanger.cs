using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public float delayTime = 20f; // 전환 대기 시간 (초)
    public string nextSceneName; // 전환할 씬 이름

    void Start()
    {
        StartCoroutine(ChangeSceneAfterDelay());
    }
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene("Testscenes");
    }

    public void ExitGame(string sceneName)
    {
        SceneManager.LoadScene("TitleScene");
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene("TitleScene");
    }
}
