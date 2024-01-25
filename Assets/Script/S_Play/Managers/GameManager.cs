using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int nowday;
    public int nowMoney;
    public int nowResearchPoint;
    public int CompanyOpenness; // 회사 개방도


    // Start is called before the first frame update
    void Start()
    {
        nowday = DataManager.Instance.MainDataLoad().day;
        nowMoney = DataManager.Instance.MainDataLoad().Money;
        nowResearchPoint = DataManager.Instance.MainDataLoad().ResearchPoint;
        RoomManager.Instance.MainSet();
        EmployeeManager.Instance.MainSet();

        AllInteractionOn();
    }

    // Update is called once per frame
    void Update()
    {
        if (UI_Manager.Instance.Enegy_Slider.value >= 1)
        {
            UI_Manager.Instance.EndButtonOn();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && UI_Manager.Instance.PauseMenu.activeSelf == false)
        {
            AllInteractionOff();
            UI_Manager.Instance.PauseMenuOn();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && UI_Manager.Instance.PauseMenu.activeSelf == true)
        {
            AllInteractionOn();
            UI_Manager.Instance.PauseMenuOff();
        }
        // if (Input.GetKeyDown(KeyCode.L))
        // {
        //     DataManager.Instance.MaindataSave();
        // }
        // if (Input.GetKeyDown(KeyCode.M))
        // {
        //     DataManager.Instance.CreateEmployeeData(0);
        // }
    }

    public void AllInteractionOn() // 모든 인터렉션 켜기
    {
        TimeManager.Instance.TimeInteraction = true;
        MouseManager.Instance.MouseInteractionOn = true;
        Camera_Manager.Instance.CamInteractionOn = true;
        Selection_Obj.Instance.Select_Interaction = true;
    }

    public void AllInteractionOff() // 모든 인터렉션 끄기
    {
        TimeManager.Instance.TimeInteraction = false;
        MouseManager.Instance.MouseInteractionOn = false;
        Camera_Manager.Instance.CamInteractionOn = false;
        Selection_Obj.Instance.Select_Interaction = false;
    }
}
