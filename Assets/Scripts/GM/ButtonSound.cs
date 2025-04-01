using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public Button targetButton; // 효과음을 적용할 버튼
    public AudioClip soundEffect; // 사용할 효과음

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
