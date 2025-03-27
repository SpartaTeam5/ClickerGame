using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {  get; private set; }

    public UIStage uistage;
    

    private void Awake()
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
