using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Setting : MonoBehaviour
{
    [Header("해상도 관련")]
    FullScreenMode screenMode;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenBtn;
    List<Resolution> resolutions = new List<Resolution>();
    private int resolutionNum;
    //public GameObject Settingpanel;
    //public Button SettingEndButton;

    void Start()
    {
        InitUI();
    }

    void InitUI()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate != 30)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }

        resolutionDropdown.options.Clear();

        int optionNum = 0;
        foreach (Resolution item in resolutions)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = item.width + "x" + item.height + " " + item.refreshRate + "hz";
            resolutionDropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
            {
                resolutionDropdown.value = optionNum;
            }
            optionNum++;
        }
        resolutionDropdown.RefreshShownValue();

        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
    }

    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void SettingOff()
    {
        Screen.SetResolution(resolutions[resolutionNum].width,
            resolutions[resolutionNum].height,
            screenMode);
        UI_Manager.Instance.SettingOff();
    }
}
