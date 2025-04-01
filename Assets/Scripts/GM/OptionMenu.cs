using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour
{
    // BGM 볼륨 조절 슬라이더
    public Slider bgmSlider;
    public Slider sfxSlider;// 효과음 볼륨 조절 슬라이더
    public Button closeBtn;//닫기 버튼
    public GameObject optionsMenu; //옵션 메뉴 창

    private void Start()
    {
        // 슬라이더 값 변경 시 AudioManager의 볼륨 조절 메소드 호출
        optionsMenu.SetActive(false);
        bgmSlider.onValueChanged.AddListener(BGMManager.instance.SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(BGMManager.instance.SetSFXVolume);
    }

    // 옵션 창에서 타이틀 화면으로 돌아가는 메소드
    public void GoToTitle()
    {
        // 씬 전환 (타이틀 씬의 이름이 "TitleScene"이라고 가정)
        SceneManager.LoadScene("TitleScene");
    }

    //볼륨 옵션 메뉴 열기
    public void OpenOption()
    {
        Debug.Log("오늘밤 주인공은 나야 나! 나야 나!");
        optionsMenu.SetActive(true);
    }

    //볼륨 옵션 메뉴 닫기
    public void CloseOption()
    {
        optionsMenu.SetActive(false);
    }
}
