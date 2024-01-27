using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : Singleton<UI_Manager>
{
    public GameObject Work_Canvas;
    public GameObject InfoCanvas;
    public GameObject EmployeeListCanvas;
    public GameObject Employee_Info;

    [Header("Money_ReserchPoint")]
    public TextMeshProUGUI Money;
    public TextMeshProUGUI ReserchPoint;
    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

    [Header("InfoManager_Element")]
    public Image monsterImage;
    public TextMeshProUGUI monsterName;
    public TextMeshProUGUI code;
    public TextMeshProUGUI dangerLevel;
    public TextMeshProUGUI research_log;
    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    [Header("Pause")]
    public GameObject PauseMenu;
    public GameObject PauseBG;
    [Header("Setting")]
    public GameObject SettingPanel;
    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    [Header("Work_Canvas_Element")]
    public Image monsterWorkImage;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI CodeName;
    public TextMeshProUGUI RiskLevel;
    public TextMeshProUGUI OpenLevel;

    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

    [Header("EmployeeList_Element")]
    public GameObject Employee_Element;
    public Transform EmpLayOutGroup;

    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

    [Header("EmployeeSelected_Element")]
    public Image EmployeeImage;
    public TextMeshProUGUI empName;
    public TextMeshProUGUI empHp;
    public TextMeshProUGUI empMp;
    public TextMeshProUGUI empDepartment;
    public Slider EmployeeSelected_HpSlider;
    public Slider EmployeeSelected_MpSlider;
    public TextMeshProUGUI empDef;
    public TextMeshProUGUI empPower;
    public TextMeshProUGUI empintelligence;
    public TextMeshProUGUI empJustice;
    public TextMeshProUGUI empMovementSpeed;

    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

    [Header("else")]
    public Slider Enegy_Slider;
    public GameObject EndButton;
    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=




    private void Start()
    {
        EmployeeListCanvas.SetActive(false);
        InfoCanvasOff();
        WorkCanvasOff();
        EndButtonOff();
        PauseMenuOff();
        SettingOff();
        EmployeeSelectcancel();
        Money.text = "자금 : " + GameManager.Instance.nowMoney;
        ReserchPoint.text = "연구 포인트 : " + GameManager.Instance.nowResearchPoint;
    }

    public void WorkButtonClick(int workNum)
    {
        EmployeeListCanvasOn();
        Debug.Log("Select_Work : " + workNum);
    }

    public void InfoCanvasOn(MonsterData monsterData)
    {
        monsterImage.sprite = Resources.Load<Sprite>(monsterData.profile.imagePATH);
        monsterName.text = $"이름 : {monsterData.profile.MonsterName}";
        code.text = $"식별 코드 : {monsterData.profile.code}";
        dangerLevel.text = $"위험도 : {monsterData.profile.riskLevel}";
        research_log.text = "연구 일지\n";
        foreach (string relog in monsterData.Research_log.log)
        {
            research_log.text += "- " + relog + "\n";
        }
        InfoCanvas.SetActive(true);
    }

    public void InfoCanvasOff()
    {
        monsterImage.sprite = null;
        monsterName.text = null;
        code.text = null;
        dangerLevel.text = null;
        research_log.text = null;
        InfoCanvas.SetActive(false);
    }

    public void WorkCanvasOn(MonsterData monsterData)
    {
        monsterWorkImage.sprite = Resources.Load<Sprite>(monsterData.profile.imagePATH);
        Name.text = monsterData.profile.MonsterName;
        CodeName.text = monsterData.profile.code;
        RiskLevel.text = $"위험도 : {monsterData.profile.riskLevel}";
        OpenLevel.text = $"연구 정도 : {monsterData.OpenLevel}";


        EmployeeListCanvasOff();
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
        SettingOff();
        PauseMenu.SetActive(false);
    }

    public void EmployeeListCanvasOn()
    {
        // for (int i = 0; i < DataManager.Instance.MainDataLoad().Department[0].EmployeeList.Count && i < 5; i++) // 수정필요
        // {
        //     var newEmployee_Element = Instantiate(Employee_Element, EmpLayOutGroup);
        //     EmployeeListUI emUI = newEmployee_Element.GetComponent<EmployeeListUI>();
        //     emUI.Name.text = DataManager.Instance.EmployeeDataLoad(DataManager.Instance.MainDataLoad().EmployeeList[i]).name;
        //     emUI.Hp.text = DataManager.Instance.EmployeeDataLoad(DataManager.Instance.MainDataLoad().EmployeeList[i]).hp.ToString();
        //     emUI.def.text = DataManager.Instance.EmployeeDataLoad(DataManager.Instance.MainDataLoad().EmployeeList[i]).def.ToString();
        // }
        EmployeeListCanvas.SetActive(true);
    }

    public void EmployeeListCanvasOff()
    {
        EmployeeListCanvas.SetActive(false);
    }

    public void EmployeeSelected() //데미지 받으면 다시 호출하는 방식으로 해야할듯 그래야 체력이 동기화됨 + 힐 받을 때
    {
        var SelectedEmployeeData = Selection_Obj.Instance.SelectOBJ[0].GetComponent<Employee>();
        empName.text = SelectedEmployeeData._empName;
        empHp.text = $"체력 : {SelectedEmployeeData._empCurHp}/{SelectedEmployeeData._empMaxHp}";
        empMp.text = $"정신력 : {SelectedEmployeeData._empCurMp}/{SelectedEmployeeData._empMaxMp}";
        empDepartment.text = $"근무 부서 : {SelectedEmployeeData._empDepartment}";
        empDef.text = $"방어력 : {SelectedEmployeeData._empdef}";
        empPower.text = $"힘 : {SelectedEmployeeData._empPower}";
        empintelligence.text = $"지능 : {SelectedEmployeeData._empintelligence}";
        empJustice.text = $"정의 : {SelectedEmployeeData._empJustice}";
        empMovementSpeed.text = $"이동속도 : {SelectedEmployeeData._empMovementSpeed}";

        EmployeeSelected_HpSlider.maxValue = SelectedEmployeeData._empMaxHp;
        EmployeeSelected_HpSlider.value = SelectedEmployeeData._empCurHp;
        EmployeeSelected_MpSlider.maxValue = SelectedEmployeeData._empMaxMp;
        EmployeeSelected_MpSlider.value = SelectedEmployeeData._empCurMp;
        Employee_Info.SetActive(true);
    }
    public void EmployeeSelectcancel()
    {
        Employee_Info.SetActive(false);
    }

    public void SettingOn()
    {
        SettingPanel.SetActive(true);
    }
    public void SettingOff()
    {
        SettingPanel.SetActive(false);
    }
}
