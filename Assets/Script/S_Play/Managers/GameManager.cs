using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int nowday;
    public int nowMoney;
    public int nowResearchPoint;
    public int CompanyOpenness; // 회사 개방도

    public int sumResearchPoint;
    
    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    [Header("DayReport")] 
    public GameObject dayReportPanel;

    public TextMeshProUGUI revenueReportMoneyText;
    public TextMeshProUGUI revenueReportMoneyTotalText;
    public TextMeshProUGUI revenueReportResearchPointText;
    public TextMeshProUGUI revenueReportResearchPointTotalText;

    public TextMeshProUGUI companyStability;

    public TextMeshProUGUI deathEmployeeCount;
    public TextMeshProUGUI escapedMonsterCount;
    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=


    // Start is called before the first frame update
    void Start()
    {
        sumResearchPoint = 0;
        
        dayReportPanel.SetActive(false);
        
        nowday = DataManager.Instance.MainDataLoad().day;
        nowMoney = DataManager.Instance.MainDataLoad().Money;
        nowResearchPoint = DataManager.Instance.MainDataLoad().ResearchPoint;
        RoomManager.Instance.MainSet();
        EmployeeManager.Instance.MainSet();
        UI_Manager.Instance.SettingEnergy(nowday);

        AllInteractionOn();
    }

    // Update is called once per frame
    void Update()
    {
        MoneyResearchPointTextUpdate();
        
        if (UI_Manager.Instance.Energy_Slider.value >= UI_Manager.Instance.Energy_Slider.maxValue)
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
    
    public void MoneyResearchPointTextUpdate()
    {
        UI_Manager.Instance.Money.text = $"자금 : {nowMoney}";
        UI_Manager.Instance.ReserchPoint.text = $"연구 포인트 : {nowResearchPoint}";
    }

    public void EndOfDay()
    {
        DayReportOn();
    }

    public void Revenue()
    {
        int defaultMoney = 0;
        int revMoney = 0;
        int accountMoney = 0;
        var mainData = DataManager.Instance.MainDataLoad();
        for (int i = 0; i < mainData.Floor.Count; i++)
        {
            for (int l = 0; l < mainData.Floor[i].Department.Count; l++)
            {
                defaultMoney += mainData.Floor[i].Department[l].MonsterList.Count;
            }
            for (int j = 0; j < mainData.Floor[i].AccountingDepartment.Count; j++)
            {
                var empdata = DataManager.Instance.EmployeeDataLoad(mainData.Floor[i].AccountingDepartment[j]);
                revMoney += empdata.intelligence;
            }
        }

        defaultMoney *= 100;
        accountMoney = (int)((revMoney / 5) * 0.01 * defaultMoney); // 기본수익에 직원 추가 수익 % 계산
        revenueReportMoneyText.text = $"현재 돈 : {nowMoney},\n기본 수익 : {defaultMoney},\n직원 추가 수익 : {accountMoney}";
        nowMoney += defaultMoney + accountMoney;
        revenueReportMoneyTotalText.text = $"자금 합계 : {nowMoney}";
    }

    public void ResearchPointRevenue()
    {
        revenueReportResearchPointText.text = $"현재 RP : {nowResearchPoint}RP,\n벌어들인 RP : {sumResearchPoint}RP";
        nowResearchPoint += sumResearchPoint;
        revenueReportResearchPointTotalText.text = $"RP 합계 : {nowResearchPoint}RP";
    }
    
    public void DayReportOn()
    {
        Time.timeScale = 0f;
        dayReportPanel.SetActive(true);
        Revenue();
        ResearchPointRevenue();
        // 사망직원수 
        // 탈출한 몬스터 수
    }

    public void Restart()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void GoNextDay()
    {
        DataManager.Instance.MaindataSave();
        SceneManager.LoadScene("Management_Scene");
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
