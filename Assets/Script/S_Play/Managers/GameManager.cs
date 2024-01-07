using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int nowday;
    public int nowMoney;
    public int nowResearchPoint;
    public List<string> nowMonsterList;

    public List<string> nowEmployeeList;


    // Start is called before the first frame update
    void Start()
    {
        nowday = DataManager.Instance.MainDataLoad().day;
        nowMoney = DataManager.Instance.MainDataLoad().Money;
        nowResearchPoint = DataManager.Instance.MainDataLoad().ResearchPoint;
        nowMonsterList = DataManager.Instance.MainDataLoad().MonsterList;
        nowEmployeeList = DataManager.Instance.MainDataLoad().EmployeeList;

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
        if (Input.GetKeyDown(KeyCode.L))
        {
            DataManager.Instance.MaindataSave();
        }
    }

    public void AllInteractionOn()
    {
        TimeManager.Instance.TimeInteraction = true;
        MouseManager.Instance.MouseInteractionOn = true;
        Camera_Manager.Instance.CamInteractionOn = true;
    }

    public void AllInteractionOff()
    {
        TimeManager.Instance.TimeInteraction = false;
        MouseManager.Instance.MouseInteractionOn = false;
        Camera_Manager.Instance.CamInteractionOn = false;
    }
}
