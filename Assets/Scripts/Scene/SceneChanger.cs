using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public float delayTime = 20f; // 전환 대기 시간 (초)
    public string nextSceneName; // 전환할 씬 이름

    //void Start()
    //{
    //    StartCoroutine(ChangeSceneAfterDelay());
    //}
    public void SceneChange(string sceneName)   // 게임 시작 씬
    {
        SceneManager.LoadScene("Testscenes");
    }

    public void ExitGame(string sceneName)  // 게임 종료
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void EndSceneChanage()   // 엔딩
    {
        SceneManager.LoadScene("EndingScene");
        StartCoroutine(ChangeSceneAfterDelay());
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene("TitleScene");
    }
}
