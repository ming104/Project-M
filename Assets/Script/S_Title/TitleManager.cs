using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject Setting_Canvas;
    public GameObject Exit_Canvas;
    public GameObject HTP_Panel;
    public GameObject RealResetPanel;

    void Start()
    {
        Setting_Canvas.SetActive(false);
        Exit_Canvas.SetActive(false);
        RealResetPanelActive(false);
        HTP_PanelActive(false);
    }

    #region BtnOnOff
    public void OnGameStartBtn()
    {
        SceneManager.LoadScene("Management_Scene");
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

    public void RealResetPanelActive(bool isActive)
    {
        RealResetPanel.SetActive(isActive);
    }

    public void HTP_PanelActive(bool isActive)
    {
        HTP_Panel.SetActive(isActive);
    }
    #endregion BtnOnOff

    #region Btn_Func

    public void ResetAll()
    {
        DataManager.Instance.MainDataReset();
    }

    public void GameExit()
    {
        Application.Quit(0);
    }
    #endregion Btn_Func
}
