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
        PauseMenu.SetActive(false);
    }

    public void EmployeeListCanvasOn()
    {
        for (int i = 0; i < GameManager.Instance.nowEmployeeList.Count; i++)
        {
            var newEmployee_Element = Instantiate(Employee_Element, EmpLayOutGroup);
            EmployeeListUI emUI = newEmployee_Element.GetComponent<EmployeeListUI>();
            emUI.Name.text = DataManager.Instance.EmployeeDataLoad(GameManager.Instance.nowEmployeeList[i]).name;
            emUI.Hp.text = DataManager.Instance.EmployeeDataLoad(GameManager.Instance.nowEmployeeList[i]).hp.ToString();
            emUI.def.text = DataManager.Instance.EmployeeDataLoad(GameManager.Instance.nowEmployeeList[i]).def.ToString();
        }
        EmployeeListCanvas.SetActive(true);
    }

    public void EmployeeListCanvasOff()
    {

    }
}
