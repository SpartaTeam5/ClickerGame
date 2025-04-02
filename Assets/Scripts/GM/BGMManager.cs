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

    private void Awake()
    {

        // 싱글톤 패턴 적용 (중복 방지)
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 이동해도 유지

            // 새로운 BGM 오브젝트 생성
            GameObject bgmObject = new GameObject("BGMPlayer");
            bgmPlayer = bgmObject.AddComponent<AudioSource>();

            // 새로운 SFX 오브젝트 생성
            GameObject sfxObject = new GameObject("SFXPlayer");
            sfxPlayer = sfxObject.AddComponent<AudioSource>();

            // AudioSource 기본 설정
            bgmPlayer.loop = true;
            bgmPlayer.playOnAwake = false;
            bgmObject.transform.parent = transform;

            sfxPlayer.loop = true;
            sfxPlayer.playOnAwake = false;
            sfxObject.transform.parent = transform;

            // 씬 변경 감지 리스너 추가
            SceneManager.sceneLoaded += OnSceneLoaded;

            //PlayerPrefs에서 저장된 볼륨 값 불러오기
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
        // 씬 이름에 따라 BGM 변경
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

