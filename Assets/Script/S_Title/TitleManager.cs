using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject Setting_Canvas;
    public GameObject Exit_Canvas;

    void Start()
    {
        Setting_Canvas.SetActive(false);
        Exit_Canvas.SetActive(false);
    }

    #region BtnOnOff
    public void OnGameStartBtn()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void SettingBtn_On()
    {
        Setting_Canvas.SetActive(true);
    }
    public void SettingBtn_Off()
    {
        Setting_Canvas.SetActive(false);
    }

    public void ExitBtn_On()
    {
        Exit_Canvas.SetActive(true);
    }
    public void ExitBtn_Off()
    {
        Exit_Canvas.SetActive(false);
    }
    #endregion BtnOnOff

    #region Btn_Func
    public void GameExit()
    {
        Application.Quit(0);
    }
    #endregion Btn_Func
}
