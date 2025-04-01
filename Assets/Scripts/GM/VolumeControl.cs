using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider bgmSlider;   // BGM 슬라이더
    public Slider sfxSlider;   // SFX 슬라이더

    void Start()
    {

        //저장된 볼륨 값 불러오기
        float savedBGMVolume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);

        // 슬라이더 값 설정
        bgmSlider.value = savedBGMVolume;
        sfxSlider.value = savedSFXVolume;

        // 초기 볼륨 설정: 슬라이더의 기본값을 현재 볼륨 값으로 설정
        bgmSlider.value = BGMManager.instance.bgmPlayer.volume;
        sfxSlider.value = BGMManager.instance.sfxPlayer.volume;

        // 슬라이더 값 변경 시 호출될 메서드 등록
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    // 배경음악 볼륨 변경
    public void OnBGMVolumeChanged(float value)
    {
        BGMManager.instance.SetBGMVolume(value);
    }

    // 효과음 볼륨 변경
    public void OnSFXVolumeChanged(float value)
    {
        BGMManager.instance.SetSFXVolume(value);
    }
}
