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

    //private void Awake()
    //{

    //    // 싱글톤 패턴 적용 (중복 방지)
    //    if (instance == null)
    //    {
    //        instance = this;
    //        //(gameObject); // 씬 이동해도 유지

    //        // 새로운 BGM 오브젝트 생성
    //        GameObject bgmObject = new GameObject("BGMPlayer");
    //        bgmPlayer = bgmObject.AddComponent<AudioSource>();

    //        // 새로운 SFX 오브젝트 생성
    //        GameObject sfxObject = new GameObject("SFXPlayer");
    //        sfxPlayer = sfxObject.AddComponent<AudioSource>();

    //        // AudioSource 기본 설정
    //        bgmPlayer.loop = true;
    //        bgmPlayer.playOnAwake = false;
    //        bgmObject.transform.parent = transform;

    //        sfxPlayer.loop = true;
    //        sfxPlayer.playOnAwake = false;
    //        sfxObject.transform.parent = transform;

    //        // 씬 변경 감지 리스너 추가
    //        SceneManager.sceneLoaded += OnSceneLoaded;

    //        //PlayerPrefs에서 저장된 볼륨 값 불러오기
    //        float savedBGMVolume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
    //        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
    //        SetBGMVolume(savedBGMVolume);
    //        SetSFXVolume(savedSFXVolume);
    //    }

    //}
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // BGMManager 자체를 유지

            // bgmPlayer 초기화
            if (bgmPlayer == null)
            {
                GameObject bgmObject = new GameObject("BGMPlayer");
                DontDestroyOnLoad(bgmObject); // bgmPlayer 유지
                bgmPlayer = bgmObject.AddComponent<AudioSource>();
                bgmPlayer.loop = true;
                bgmPlayer.playOnAwake = false;
                bgmObject.transform.parent = transform;
            }

            // sfxPlayer 초기화
            if (sfxPlayer == null)
            {
                GameObject sfxObject = new GameObject("SFXPlayer");
                DontDestroyOnLoad(sfxObject); // sfxPlayer 유지
                sfxPlayer = sfxObject.AddComponent<AudioSource>();
                sfxObject.transform.parent = transform;
            }

            // 씬 로드 이벤트 등록
            SceneManager.sceneLoaded += OnSceneLoaded;

            // 볼륨 설정
            float savedBGMVolume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
            float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
            SetBGMVolume(savedBGMVolume);
            SetSFXVolume(savedSFXVolume);
        }
        else
        {
            Destroy(gameObject); // 중복된 BGMManager 제거
        }
    }

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "TestScenes":
                PlayBGM(battleBGM);
                break;
            default:
                PlayBGM(titleBGM);
                break;
        }

    }

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    // 씬 이름에 따라 BGM 변경
    //    switch (scene.name)
    //    {
    //        case "TestScenes":
    //            PlayBGM(battleBGM);
    //            break;
    //        default:
    //            PlayBGM(titleBGM);
    //            break;
    //    }
    //}

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // bgmPlayer 유효성 검사
        if (bgmPlayer == null)
        {
            Debug.LogWarning("bgmPlayer가 null 상태입니다. 다시 생성합니다.");
            GameObject bgmObject = new GameObject("BGMPlayer");
            DontDestroyOnLoad(bgmObject);
            bgmPlayer = bgmObject.AddComponent<AudioSource>();
            bgmPlayer.loop = true;
            bgmPlayer.playOnAwake = false;
            bgmObject.transform.parent = transform;
        }

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

