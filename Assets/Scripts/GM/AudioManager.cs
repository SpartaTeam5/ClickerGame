using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public AudioMixer audioMixer; // AudioMixer를 통해 전체 볼륨 제어
    public AudioSource bgmSource; // BGM 재생용 AudioSource
    public AudioClip titleBGM; // 타이틀 화면용 BGM 클립
    public AudioClip mainBGM; // 메인 화면용 BGM 클립

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // BGM 볼륨 조절 메소드: logarithmic scale 적용
    public void SetBGMVolume(float volume)
    {
        // volume 값은 0~1 범위를 가정하며, AudioMixer에서는 dB값으로 조절
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    // 효과음(SFX) 볼륨 조절 메소드
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    // 현재 씬에 맞는 BGM 재생 (타이틀 화면이면 titleBGM, 아니면 mainBGM)
    public void PlayBGM(bool isTitle)
    {
        bgmSource.clip = isTitle ? titleBGM : mainBGM;
        bgmSource.Play();
    }
}
