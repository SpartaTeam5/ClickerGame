using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;
    public AudioSource bgmPlayer; //BGM
    public AudioSource sfxPlayer;   // ȿ����

    [Header("BGM Clips")]
    public AudioClip titleBGM;  // �⺻ BGM(Ÿ��Ʋ)
    public AudioClip battleBGM;   // ���� �� BGM

    private void Awake()
    {

        // �̱��� ���� ���� (�ߺ� ����)
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� �̵��ص� ����

            // ���ο� BGM ������Ʈ ����
            GameObject bgmObject = new GameObject("BGMPlayer");
            bgmPlayer = bgmObject.AddComponent<AudioSource>();

            GameObject sfxObject = new GameObject("SFXPlayer");
            sfxPlayer = sfxObject.AddComponent<AudioSource>();

            // AudioSource �⺻ ����
            bgmPlayer.loop = true;
            bgmPlayer.playOnAwake = false;
            bgmObject.transform.parent = transform;

            // ���ο� SFX ������Ʈ ����
            GameObject sfxObject = new GameObject("SFXPlayer");
            sfxPlayer = sfxObject.AddComponent<AudioSource>();
            sfxPlayer.loop = true;
            sfxPlayer.playOnAwake = false;
            sfxObject.transform.parent = transform;

            // �� ���� ���� ������ �߰�
            SceneManager.sceneLoaded += OnSceneLoaded;

            //PlayerPrefs���� ����� ���� �� �ҷ�����
            float savedBGMVolume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
            float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
            SetBGMVolume(savedBGMVolume);
            SetSFXVolume(savedSFXVolume);
        }
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    private void Start()
    {
        if (titleBGM != null)
        {
            PlayBGM(titleBGM);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �� �̸��� ���� BGM ����
        switch (scene.name)
        {
            case "TestScenes":
                PlayBGM(battleBGM);
                break;
            default:
                PlayBGM(titleBGM);
                break;
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmPlayer.clip == clip) return; // ������ BGM�̸� ���� �� ��

        bgmPlayer.Stop();
        bgmPlayer.clip = clip;
        bgmPlayer.Play();
    }

    public void StopBGM()
    {
        if (bgmPlayer.isPlaying)
        {
            bgmPlayer.Stop();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxPlayer.PlayOneShot(clip); // ȿ���� ���
    }

    public void SetBGMVolume(float volume)
    {
        bgmPlayer.volume = Mathf.Clamp01(volume); // 0 ~ 1 ���� ������ ���� ����
        PlayerPrefs.SetFloat("BGMVolume", volume);  //���� �� ����
    }

    public void SetSFXVolume(float volume)
    {
        sfxPlayer.volume = Mathf.Clamp01(volume); // 0 ~ 1 ���� ������ ���� ����
        PlayerPrefs.SetFloat("SFXVolume", volume);  //���� �� ����
    }
}
