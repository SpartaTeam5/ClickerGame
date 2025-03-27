using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour
{
    // BGM 볼륨 조절 슬라이더
    public Slider bgmSlider;
    // 효과음 볼륨 조절 슬라이더
    public Slider sfxSlider;

    private void Start()
    {
        // 슬라이더 값 변경 시 AudioManager의 볼륨 조절 메소드 호출
        bgmSlider.onValueChanged.AddListener(AudioManager.Instance.SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
    }

    // 옵션 창에서 타이틀 화면으로 돌아가는 메소드
    public void GoToTitle()
    {
        // 씬 전환 (타이틀 씬의 이름이 "TitleScene"이라고 가정)
        SceneManager.LoadScene("TitleScene");
        // 타이틀 화면용 BGM 재생
        AudioManager.Instance.PlayBGM(true);
    }
}
