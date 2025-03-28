using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public UIStage uistage;
    

    private void Awake()        // 싱글톤을 제네릭으로 구현 가능하다! 찾아보기
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
   



}
