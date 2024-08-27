using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public delegate void ActionDelegate();


public class EmployeeListUI : MonoBehaviour
{
    public Image Employee_Image;
    public Slider HpSlider;
    public Slider MpSlider;
    public TextMeshProUGUI HpSlider_Text;
    public TextMeshProUGUI MpSlider_Text;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI SuccessRate;
    public TextMeshProUGUI ResearchTime;

    public GameObject ResearchPanel;

    void Update()
    {
        if (EmployeeManager.Instance.EmployeeDatas[Name.text].EmployeeCurrentStatus == Employee.EmployeeFsm.Work)
        {
            ResearchPanel.SetActive(true);
        }
        else
        {
            ResearchPanel.SetActive(false);
        }
    }
}
