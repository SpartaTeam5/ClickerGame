using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour
{
    // BGM ���� ���� �����̴�
    public Slider bgmSlider;
    public Slider sfxSlider;// ȿ���� ���� ���� �����̴�
    public Button closeBtn;//�ݱ� ��ư
    public GameObject optionsMenu; //�ɼ� �޴� â

    private void Start()
    {
        // �����̴� �� ���� �� AudioManager�� ���� ���� �޼ҵ� ȣ��
        optionsMenu.SetActive(false);
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

    //���� �ɼ� �޴� ����
    public void OpenOption()
    {
        optionsMenu.SetActive(true);
    }

    //���� �ɼ� �޴� �ݱ�
    public void CloseOption()
    {
        optionsMenu.SetActive(false);
    }
}
