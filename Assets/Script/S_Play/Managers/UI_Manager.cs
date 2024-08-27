using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
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
    public TextMeshProUGUI possibilityOfEscape;
    public TextMeshProUGUI mentalDamage;
    public TextMeshProUGUI monsterCode;
    public TextMeshProUGUI riskLevel;
    public TextMeshProUGUI research_log;
    public Transform logContent;
    public GameObject logTextBoxPrefab;
    public TextMeshProUGUI feelingBad;
    public TextMeshProUGUI feelingDefault;
    public TextMeshProUGUI feelingGood;

    public TextMeshProUGUI equipmentName;
    public TextMeshProUGUI equipmentType;
    public TextMeshProUGUI equipEffect;
    public TextMeshProUGUI equipSpacialEffect;
    public TextMeshProUGUI equipCost;
    
    public TextMeshProUGUI monsterResearchFear;
    public TextMeshProUGUI monsterResearchAnger;
    public TextMeshProUGUI monsterResearchDisgust;
    public TextMeshProUGUI monsterResearchSad;
    public TextMeshProUGUI monsterResearchHappy;
    public TextMeshProUGUI monsterResearchSurprise;
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
    public TextMeshProUGUI workRiskLevel;
    public TextMeshProUGUI OpenLevel;

    public int Select_mon_Depart;
    public int currnentFloor;
    public MonsterData monsterData;
    public Room_Select_Manager roomSelectManager;
    public Vector3 roomPos;

    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

    [Header("EmployeeList_Element")]
    public TextMeshProUGUI Depart_Text;
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

    [Header("else")]
    public Slider Energy_Slider;
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
        Select_mon_Depart = 0;
        Money.text = "자금 : " + GameManager.Instance.nowMoney;
        ReserchPoint.text = "연구 포인트 : " + GameManager.Instance.nowResearchPoint;
    }

    public void SettingEnergy(int day)
    {
        Energy_Slider.maxValue = (day * day) * 10;
        Energy_Slider.value = 0;
    }

    public void IncreasedEnergy(int increase)
    {
        Energy_Slider.value += increase;
    }

    public void WorkButtonClick(int workNum) // 작업 버튼 클릭
    {
        EmployeeListCanvasOn(currnentFloor, Select_mon_Depart, workNum);
        Debug.Log("Select_Work : " + workNum);
    }

    public void InfoCanvasOn(MonsterData monsterData) // 수정 필요
    {
        monsterImage.sprite = Resources.Load<Sprite>(monsterData.profile.imagePATH);
        monsterName.text = $"이름 : {monsterData.profile.MonsterName}";
        possibilityOfEscape.text = $"탈출 여부 : {monsterData.profile.isEscape}";
        mentalDamage.text = $"연구시 정신피해 정도 : {monsterData.profile.researchMentalDamage}";
        monsterCode.text = $"식별 코드 : {monsterData.profile.code}";
        riskLevel.text = $"위험도 : {monsterData.profile.riskLevel}";
        research_log.text = "연구 기록";
        foreach (string relog in monsterData.Research_log.log)
        {
            var logTextBox = Instantiate(logTextBoxPrefab);
            logTextBox.GetComponent<MonsterResultLog>().resultText.text = relog;
            logTextBox.transform.SetParent(logContent);
        }

        switch (monsterData.profile.riskLevel)
        {
            case 1:
                feelingBad.text = "나쁨 : 0 ~ 2";
                feelingDefault.text = "보통 : 3 ~ 6";
                feelingGood.text = "좋음 : 7 ~ 10";
                break;
            case 2:
                feelingBad.text = "나쁨 : 0 ~ 6";
                feelingDefault.text = "보통 : 7 ~ 13";
                feelingGood.text = "좋음 : 14 ~ 20";
                break;
            case 3:
                feelingBad.text = "나쁨 : 0 ~ 9";
                feelingDefault.text = "보통 : 10 ~ 20";
                feelingGood.text = "좋음 : 21 ~ 30";
                break;
            case 4:
                feelingBad.text = "나쁨 : 0 ~ 12";
                feelingDefault.text = "보통 : 13 ~ 26";
                feelingGood.text = "좋음 : 27 ~ 40";
                break;
            case 5:
                feelingBad.text = "나쁨 : 0 ~ 16";
                feelingDefault.text = "보통 : 17 ~ 33";
                feelingGood.text = "좋음 : 33 ~ 50";
                break;
        }

        equipmentName.text = $"{monsterData.MonEquipment.EquipName}";

        switch (monsterData.MonEquipment.Type)
        {
            case 0:
                equipmentType.text = "무기";
                break;
            case 1:
                equipmentType.text = "방어구";
                break;
        }

        equipEffect.text = $"효과 : {monsterData.MonEquipment.equipEffect}";
        equipSpacialEffect.text = $"{monsterData.MonEquipment.equipSpecialEffect}";
        equipCost.text = "가격 : ???";

        monsterResearchFear.text = $"{monsterData.Research_Preferences.FEAR}%";
        monsterResearchAnger.text = $"{monsterData.Research_Preferences.ANGER}%";
        monsterResearchDisgust.text = $"{monsterData.Research_Preferences.DISGUST}%";
        monsterResearchSad.text = $"{monsterData.Research_Preferences.SAD}%";
        monsterResearchHappy.text = $"{monsterData.Research_Preferences.HAPPY}%";
        monsterResearchSurprise.text = $"{monsterData.Research_Preferences.SURPRISE}%";
        
        GameManager.Instance.AllInteractionOff();
        InfoCanvas.SetActive(true);
    }

    public void InfoCanvasOff()
    {
        // monsterImage.sprite = null;
        // monsterName.text = null;
        // monsterCode.text = null;
        // workRiskLevel.text = null;
        // research_log.text = null;
        InfoCanvas.SetActive(false);
        GameManager.Instance.AllInteractionOn();
        foreach (Transform child in logContent)
        {
            Destroy(child.gameObject) ;
        }
    }

    public void WorkCanvasOn(MonsterData monsterData, int Mon_depart)
    {
        monsterWorkImage.sprite = Resources.Load<Sprite>(monsterData.profile.imagePATH);
        Name.text = monsterData.profile.MonsterName;
        CodeName.text = monsterData.profile.code;
        workRiskLevel.text = $"위험도 : {monsterData.profile.riskLevel}";
        OpenLevel.text = $"연구 정도 : {monsterData.OpenLevel}";
        Select_mon_Depart = Mon_depart;


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

    public void EmployeeListCanvasOn(int floor, int depart, int workButtonNumber)
    {
        foreach (Transform child in EmpLayOutGroup) // 자식 삭제 -> 초기화
        {
            Destroy(child.gameObject);
        }
        Depart_Text.text = $"관리부서_{depart}";
        for (int i = 0; i < DataManager.Instance.MainDataLoad().Floor[floor].Department[depart].EmployeeList.Count && i < 5; i++)
        {
            var newEmployee_Element = Instantiate(Employee_Element, EmpLayOutGroup);
            EmployeeListUI emUI = newEmployee_Element.GetComponent<EmployeeListUI>();
            var empListdata = DataManager.Instance.EmployeeDataLoad(DataManager.Instance.MainDataLoad().Floor[floor].Department[depart].EmployeeList[i]);
            //var employeeData = EmployeeManager.Instance.EmployeeDatas[empListdata.name];
            emUI.Name.text = empListdata.name;
            emUI.HpSlider.maxValue = empListdata.hp;
            emUI.MpSlider.maxValue = empListdata.mp;
            emUI.HpSlider.value = EmployeeManager.Instance.Employees[empListdata.name].CurrentHP;
            emUI.MpSlider.value = EmployeeManager.Instance.Employees[empListdata.name].CurrentMP;
            emUI.HpSlider_Text.text = $"HP : {empListdata.hp}/{empListdata.hp}";
            emUI.MpSlider_Text.text = $"MP : {empListdata.mp}/{empListdata.mp}";
            // if (employeeData.EmployeeCurrentStatus == Employee.EmployeeFsm.Work)
            // {
            //     emUI.ResearchPanel.SetActive(true);
            // }
            
            switch (workButtonNumber)
            {
                case 1:    
                    emUI.SuccessRate.text = $"{monsterData.Research_Preferences.FEAR}%";
                    newEmployee_Element.GetComponent<Button>().onClick.AddListener(() => ResearchStart(empListdata, roomSelectManager, 1)); //fear
                    break;
                case 2:
                    emUI.SuccessRate.text = $"{monsterData.Research_Preferences.ANGER}%";
                    newEmployee_Element.GetComponent<Button>().onClick.AddListener(() => ResearchStart(empListdata, roomSelectManager, 2)); //anger
                    break;
                case 3:
                    emUI.SuccessRate.text = $"{monsterData.Research_Preferences.DISGUST}%";
                    newEmployee_Element.GetComponent<Button>().onClick.AddListener(() => ResearchStart(empListdata, roomSelectManager, 3)); //disgust
                    break;
                case 4:
                    emUI.SuccessRate.text = $"{monsterData.Research_Preferences.SAD}%";
                    newEmployee_Element.GetComponent<Button>().onClick.AddListener(() => ResearchStart(empListdata, roomSelectManager, 4)); //sad
                    break;
                case 5:
                    emUI.SuccessRate.text = $"{monsterData.Research_Preferences.HAPPY}%";
                    newEmployee_Element.GetComponent<Button>().onClick.AddListener(() => ResearchStart(empListdata, roomSelectManager, 5)); // happy
                    break; 
                case 6:
                    emUI.SuccessRate.text = $"{monsterData.Research_Preferences.SURPRISE}%";
                    newEmployee_Element.GetComponent<Button>().onClick.AddListener(() => ResearchStart(empListdata, roomSelectManager, 6)); //surprise
                    break;
            }
            //emUI.ResearchTime.text = data.def.ToString(); // 수정필요
        }
        EmployeeListCanvas.SetActive(true);
    }

    public void EmployeeListCanvasOff()
    {
        EmployeeListCanvas.SetActive(false);
    }

    public void ResearchStart(EmployeeData empdata,Room_Select_Manager roomData, int workIndex)
    {
        //int RePoCount;
        //var empListUI = gameObject.GetComponent<EmployeeListUI>();
        
        //여기서 작업 실행 해야됨
        if (EmployeeManager.Instance.EmployeeDatas.ContainsKey(empdata.name))
        {
            EmployeeManager.Instance.EmployeeDatas[empdata.name].ResearchDestinationMoving(roomPos, roomData, workIndex); // 이동
                        
                // switch (mondata.profile.riskLevel)
                // {
                //     case 1:
                //         RePoCount = 10;
                //         researchStatusSlider.StartResearch(RePoCount, persent);
                //         break;
                //     case 2:
                //         RePoCount = 20;
                //         researchStatusSlider.StartResearch(RePoCount, persent);
                //         break;
                //     case 3:
                //         RePoCount = 30;
                //         researchStatusSlider.StartResearch(RePoCount, persent);
                //         break;
                //     case 4:
                //         RePoCount = 40;
                //         researchStatusSlider.StartResearch(RePoCount, persent);
                //         break;
                //     case 5:
                //         RePoCount = 50;
                //         researchStatusSlider.StartResearch(RePoCount, persent);
                //         break;
                // }
            Work_Canvas.SetActive(false);
        }
    }
    

    public void EmployeeSelected() //데미지 받으면 다시 호출하는 방식으로 해야할듯 그래야 체력이 동기화됨 + 힐 받을 때
    {
        var SelectedEmployeeData = Selection_Obj.Instance.SelectOBJ[0].GetComponent<Employee>();
        empName.text = SelectedEmployeeData.EmployeeName;
        empHp.text = $"체력 : {EmployeeManager.Instance.Employees[SelectedEmployeeData.EmployeeName].CurrentHP}/{SelectedEmployeeData.EmployeeMaxHp}";
        empMp.text = $"정신력 : {EmployeeManager.Instance.Employees[SelectedEmployeeData.EmployeeName].CurrentMP}/{SelectedEmployeeData.EmployeeMaxMp}";
        empDepartment.text = $"근무 부서 : {SelectedEmployeeData.EmployeeDepartment}";
        empDef.text = $"방어력 : {SelectedEmployeeData.EmployeeDef}";
        empPower.text = $"힘 : {SelectedEmployeeData.EmployeePower}";
        empintelligence.text = $"지능 : {SelectedEmployeeData.EmployeeIntelligence}";
        empJustice.text = $"정의 : {SelectedEmployeeData.EmployeeJustice}";
        empMovementSpeed.text = $"이동속도 : {SelectedEmployeeData.EmployeeMovementSpeed}";

        EmployeeSelected_HpSlider.maxValue = SelectedEmployeeData.EmployeeMaxHp;
        EmployeeSelected_HpSlider.value = EmployeeManager.Instance.Employees[SelectedEmployeeData.EmployeeName].CurrentHP;
        EmployeeSelected_MpSlider.maxValue = SelectedEmployeeData.EmployeeMaxMp;
        EmployeeSelected_MpSlider.value = EmployeeManager.Instance.Employees[SelectedEmployeeData.EmployeeName].CurrentMP;
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

    public void GotoMenu()
    {
        SceneManager.LoadScene("GameTitle");
    }
    public void SaveAndExit()
    {
        Application.Quit();
    }


}
