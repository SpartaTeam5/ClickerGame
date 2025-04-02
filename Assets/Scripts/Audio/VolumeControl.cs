using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider bgmSlider;   // BGM �����̴�
    public Slider sfxSlider;   // SFX �����̴�

    void Start()
    {

        //����� ���� �� �ҷ�����
        float savedBGMVolume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);

        // �����̴� �� ����
        bgmSlider.value = savedBGMVolume;
        sfxSlider.value = savedSFXVolume;

        // �ʱ� ���� ����: �����̴��� �⺻���� ���� ���� ������ ����
        bgmSlider.value = BGMManager.instance.bgmPlayer.volume;
        sfxSlider.value = BGMManager.instance.sfxPlayer.volume;

        // �����̴� �� ���� �� ȣ��� �޼��� ���
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    // ������� ���� ����
    public void OnBGMVolumeChanged(float value)
    {
        BGMManager.instance.SetBGMVolume(value);
    }

    // ȿ���� ���� ����
    public void OnSFXVolumeChanged(float value)
    {
        BGMManager.instance.SetSFXVolume(value);
    }
}
