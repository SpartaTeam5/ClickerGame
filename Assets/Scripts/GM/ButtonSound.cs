using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public Button targetButton; // ȿ������ ������ ��ư
    public AudioClip soundEffect; // ����� ȿ����

    void Start()
    {
        if (targetButton != null)
        {
            targetButton.onClick.AddListener(PlayButtonSound);
        }
    }

    void PlayButtonSound()
    {
        if (BGMManager.instance != null)
        {
            BGMManager.instance.PlaySFX(soundEffect);
        }
    }
}
