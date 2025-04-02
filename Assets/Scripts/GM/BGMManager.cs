using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;
    public AudioSource bgmPlayer; //BGM
    public AudioSource sfxPlayer;   // 효과음

    [Header("BGM Clips")]
    public AudioClip titleBGM;  // 기본 BGM(타이틀)
    public AudioClip battleBGM;   // 전투 씬 BGM
    public AudioClip enemyBGM;  // 엔딩 BGM

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            // bgmPlayer 초기화
            BGMInit();

            // sfxPlayer 초기화
            SFXInit();
            
            // 볼륨 설정
            Volumes();
        }
        else
        {
            Destroy(gameObject); // 중복된 BGMManager 제거
        }
    }

    private void Start()
    {
        PlayBGM();
    }

    private void Volumes()
    {
        float savedBGMVolume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        SetBGMVolume(savedBGMVolume);
        SetSFXVolume(savedSFXVolume);
    }

    private void PlayBGM()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "TestScenes":
                PlayBGM(battleBGM);
                break;
            case "EndingScene":
                PlayBGM(enemyBGM);
                break;
            default:
                PlayBGM(titleBGM);
                break;
        }
    }

    private void BGMInit()
    {
        // bgmPlayer 유효성 검사
        if (bgmPlayer == null)
        {
            Debug.LogWarning("bgmPlayer가 null 상태입니다. 다시 생성합니다.");
            GameObject bgmObject = new GameObject("BGMPlayer");
            bgmPlayer = bgmObject.AddComponent<AudioSource>();
            bgmPlayer.loop = true;
            bgmPlayer.playOnAwake = false;
            bgmObject.transform.SetParent(transform);
        }
    }

    private void SFXInit()
    {
        // sfxPlayer 초기화
        if (sfxPlayer == null)
        {
            GameObject sfxObject = new GameObject("SFXPlayer");
            sfxPlayer = sfxObject.AddComponent<AudioSource>();
            sfxObject.transform.parent = transform;
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmPlayer.clip == clip) return; // 동일한 BGM이면 변경 안 함

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

        if (clip == null)
        {
            Debug.LogWarning("PlaySFX: 재생할 AudioClip이 null입니다!");
            return;
        }

        if (sfxPlayer == null)
        {
            Debug.LogError("PlaySFX: AudioSource가 존재하지 않습니다!");
            return;
        }

        sfxPlayer.PlayOneShot(clip); // 효과음 재생
    }

    public void SetBGMVolume(float volume)
    {
        bgmPlayer.volume = Mathf.Clamp01(volume); // 0 ~ 1 사이 값으로 볼륨 설정
        PlayerPrefs.SetFloat("BGMVolume", volume);  //볼륨 값 저장
    }

    public void SetSFXVolume(float volume)
    {
        sfxPlayer.volume = Mathf.Clamp01(volume); // 0 ~ 1 사이 값으로 볼륨 설정
        PlayerPrefs.SetFloat("SFXVolume", volume);  //볼륨 값 저장
    }
}

