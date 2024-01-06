using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : Singleton<UI_Manager>
{
    public GameObject Work_Canvas;
    public GameObject InfoCanvas;
    public GameObject EmployeeListCanvas;

    [Header("Money_ReserchPoint")]
    public TextMeshProUGUI Money;
    public TextMeshProUGUI ReserchPoint;
    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

    [Header("InfoManager_Element")]
    public Image monsterImage;
    public TextMeshProUGUI monsterName;
    public TextMeshProUGUI code;
    public TextMeshProUGUI dangerLevel;
    public TextMeshProUGUI Research_log;
    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    [Header("Pause")]
    public GameObject PauseMenu;
    public GameObject PauseBG;
    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

    public Slider Enegy_Slider;
    public GameObject EndButton;


    private void Start()
    {
        EmployeeListCanvas.SetActive(false);
        InfoCanvasOff();
        WorkCanvasOff();
        EndButtonOff();
        PauseMenuOff();
        Money.text = "자금 : " + DataManager.Instance.MainData().Money.ToString();
        ReserchPoint.text = "연구 포인트 : " + DataManager.Instance.MainData().ResearchPoint.ToString();
    }

    public void WorkButtonClick(int workNum)
    {
        EmployeeListCanvas.SetActive(true);
        Debug.Log("Select_Work : " + workNum);
    }

    public void InfoCanvasOn(MonsterData monsterData)
    {
        monsterImage.sprite = Resources.Load<Sprite>(monsterData.profile.imagePATH);
        monsterName.text = "이름 : " + monsterData.profile.MonsterName;
        code.text = "식별 코드 : " + monsterData.profile.code;
        dangerLevel.text = "위험도 : " + monsterData.profile.riskLevel.ToString();
        Research_log.text = "연구 일지\n";
        foreach (string log in monsterData.Research_log.log)
        {
            Research_log.text += "- " + log + "\n";
        }
        InfoCanvas.SetActive(true);
    }

    public void InfoCanvasOff()
    {
        monsterImage.sprite = null;
        monsterName.text = null;
        code.text = null;
        dangerLevel.text = null;
        Research_log.text = null;
        InfoCanvas.SetActive(false);
    }

    public void WorkCanvasOn()
    {
        Work_Canvas.SetActive(true);
    }

    public void WorkCanvasOff()
    {
        Work_Canvas.SetActive(false);
    }

    public void EndButtonOn()
    {
        EndButton.SetActive(true);
    }
    public void EndButtonOff()
    {
        EndButton.SetActive(false);
    }

    public void PauseMenuOn()
    {
        Time.timeScale = 0;
        GameManager.Instance.AllInteractionOff();
        PauseMenu.SetActive(true);
    }

    public void PauseMenuOff()
    {
        Time.timeScale = TimeManager.Instance.TimeScaleList[TimeManager.Instance.TimeScaleListIndex];
        GameManager.Instance.AllInteractionOn();
        PauseMenu.SetActive(false);
    }
}
