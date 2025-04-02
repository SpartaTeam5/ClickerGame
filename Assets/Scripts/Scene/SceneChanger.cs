using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public float delayTime = 20f; // 전환 대기 시간 (초)
    public string nextSceneName = "TitleScene"; // 전환할 씬 이름

    //void Start()
    //{
    //    StartCoroutine(ChangeSceneAfterDelay());
    //}
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        int height = 900;
        int width = (int)(height * (9f / 16f));
        Screen.SetResolution(width, height, false);
    }
    public void SceneChange(string sceneName)   // 게임 시작 씬
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame(string sceneName)  // 게임 종료
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void EndSceneChanage()   // 엔딩
    {
        SceneManager.LoadScene("EndingScene");
        //StartCoroutine(ChangeSceneAfterDelay());
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 유니티 에디터에서는 플레이 모드 종료
#else
        Application.Quit(); // 빌드된 게임에서는 게임 종료
#endif
    }

    //IEnumerator ChangeSceneAfterDelay()
    //{
    //    Debug.Log("ChangeScene");
    //    yield return new WaitForSeconds(delayTime);
    //    SceneManager.LoadScene("TitleScene");
    //}
}
