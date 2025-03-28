using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public UIStage uistage;
    

    private void Awake()        // �̱����� ���׸����� ���� �����ϴ�! ã�ƺ���
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
