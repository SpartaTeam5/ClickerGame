using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject settingUI;
    public void OnClickSetting()
    {
        settingUI.SetActive(true);
    }

    public void OnClickClose()
    {
        settingUI.SetActive(false);
    }
}
