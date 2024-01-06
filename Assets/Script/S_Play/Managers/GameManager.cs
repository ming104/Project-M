using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
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
