using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour
{
    // BGM ���� ���� �����̴�
    public Slider bgmSlider;
    // ȿ���� ���� ���� �����̴�
    public Slider sfxSlider;

    private void Start()
    {
        // �����̴� �� ���� �� AudioManager�� ���� ���� �޼ҵ� ȣ��
        bgmSlider.onValueChanged.AddListener(AudioManager.Instance.SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
    }

    // �ɼ� â���� Ÿ��Ʋ ȭ������ ���ư��� �޼ҵ�
    public void GoToTitle()
    {
        // �� ��ȯ (Ÿ��Ʋ ���� �̸��� "TitleScene"�̶�� ����)
        SceneManager.LoadScene("TitleScene");
        // Ÿ��Ʋ ȭ��� BGM ���
        AudioManager.Instance.PlayBGM(true);
    }
}
