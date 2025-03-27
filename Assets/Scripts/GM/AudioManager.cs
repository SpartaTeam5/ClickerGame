using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public AudioMixer audioMixer; // AudioMixer�� ���� ��ü ���� ����
    public AudioSource bgmSource; // BGM ����� AudioSource
    public AudioClip titleBGM; // Ÿ��Ʋ ȭ��� BGM Ŭ��
    public AudioClip mainBGM; // ���� ȭ��� BGM Ŭ��

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // BGM ���� ���� �޼ҵ�: logarithmic scale ����
    public void SetBGMVolume(float volume)
    {
        // volume ���� 0~1 ������ �����ϸ�, AudioMixer������ dB������ ����
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    // ȿ����(SFX) ���� ���� �޼ҵ�
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    // ���� ���� �´� BGM ��� (Ÿ��Ʋ ȭ���̸� titleBGM, �ƴϸ� mainBGM)
    public void PlayBGM(bool isTitle)
    {
        bgmSource.clip = isTitle ? titleBGM : mainBGM;
        bgmSource.Play();
    }
}
