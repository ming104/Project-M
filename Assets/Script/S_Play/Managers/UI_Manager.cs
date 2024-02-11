using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    private int Select_mon_Depart;
    public string pri_monname;
    public ObjectLayoutGroup SelectedRoom;

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
        Select_mon_Depart = 0;
        Money.text = "자금 : " + GameManager.Instance.nowMoney;
        ReserchPoint.text = "연구 포인트 : " + GameManager.Instance.nowResearchPoint;
    }

    public void WorkButtonClick(int workNum) // 작업 버튼 클릭
    {
        EmployeeListCanvasOn(Select_mon_Depart, workNum);
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

    public void WorkCanvasOn(MonsterData monsterData, int Mon_depart)
    {
        monsterWorkImage.sprite = Resources.Load<Sprite>(monsterData.profile.imagePATH);
        Name.text = monsterData.profile.MonsterName;
        CodeName.text = monsterData.profile.code;
        RiskLevel.text = $"위험도 : {monsterData.profile.riskLevel}";
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

    public void EmployeeListCanvasOn(int depart, int workButtonNumber)
    {
        foreach (Transform child in EmpLayOutGroup) // 자식 삭제 -> 초기화
        {
            Destroy(child.gameObject);
        }
        Depart_Text.text = $"관리부서_{depart}";
        for (int i = 0; i < DataManager.Instance.MainDataLoad().Department[depart].EmployeeList.Count && i < 5; i++)
        {
            var newEmployee_Element = Instantiate(Employee_Element, EmpLayOutGroup);
            EmployeeListUI emUI = newEmployee_Element.GetComponent<EmployeeListUI>();
            var empListdata = DataManager.Instance.EmployeeDataLoad(DataManager.Instance.MainDataLoad().Department[depart].EmployeeList[i]);
            var mondata = DataManager.Instance.MonsterDataLoad(pri_monname);
            emUI.Name.text = empListdata.name;
            emUI.HpSlider.maxValue = empListdata.hp;
            emUI.MpSlider.maxValue = empListdata.mp;
            emUI.HpSlider.value = EmployeeManager.Instance.Employees[empListdata.name].CurrentHP;
            emUI.MpSlider.value = EmployeeManager.Instance.Employees[empListdata.name].CurrentMP;
            emUI.HpSlider_Text.text = $"HP : {empListdata.hp}/{empListdata.hp}";
            emUI.MpSlider_Text.text = $"MP : {empListdata.mp}/{empListdata.mp}";

            switch (workButtonNumber)
            {
                case 1:
                    emUI.SuccessRate.text = $"{mondata.Research_Preferences.FEAR}%";
                    newEmployee_Element.GetComponent<Button>().onClick.AddListener(() => ResearchStart(mondata, mondata.Research_Preferences.FEAR));
                    break;
                case 2:
                    emUI.SuccessRate.text = $"{mondata.Research_Preferences.ANGER}%";
                    newEmployee_Element.GetComponent<Button>().onClick.AddListener(() => ResearchStart(mondata, mondata.Research_Preferences.ANGER));
                    break;
                case 3:
                    emUI.SuccessRate.text = $"{mondata.Research_Preferences.DISGUST}%";
                    newEmployee_Element.GetComponent<Button>().onClick.AddListener(() => ResearchStart(mondata, mondata.Research_Preferences.DISGUST));
                    break;
                case 4:
                    emUI.SuccessRate.text = $"{mondata.Research_Preferences.SAD}%";
                    newEmployee_Element.GetComponent<Button>().onClick.AddListener(() => ResearchStart(mondata, mondata.Research_Preferences.SAD));
                    break;
                case 5:
                    emUI.SuccessRate.text = $"{mondata.Research_Preferences.HAPPY}%";
                    newEmployee_Element.GetComponent<Button>().onClick.AddListener(() => ResearchStart(mondata, mondata.Research_Preferences.HAPPY));
                    break;
                case 6:
                    emUI.SuccessRate.text = $"{mondata.Research_Preferences.SURPRISE}%";
                    newEmployee_Element.GetComponent<Button>().onClick.AddListener(() => ResearchStart(mondata, mondata.Research_Preferences.SURPRISE));
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

    public void ResearchStart(MonsterData mondata, int persent)
    {
        int RePoCount = 0;
        //var empListUI = gameObject.GetComponent<EmployeeListUI>();
        switch (mondata.profile.riskLevel)
        {
            case 1:
                RePoCount = 10;
                StartCoroutine(probabilitytask(RePoCount, persent));
                break;
            case 2:
                RePoCount = 20;
                StartCoroutine(probabilitytask(RePoCount, persent));
                break;
            case 3:
                RePoCount = 30;
                StartCoroutine(probabilitytask(RePoCount, persent));
                break;
            case 4:
                RePoCount = 40;
                StartCoroutine(probabilitytask(RePoCount, persent));
                break;
            case 5:
                RePoCount = 50;
                StartCoroutine(probabilitytask(RePoCount, persent));
                break;
        }

    }
    private IEnumerator probabilitytask(int RePo, int persent)
    {
        int sum = 0;
        int nsum = 0;
        //Debug.Log(SelectedRoom.GetComponent<ObjectLayoutGroup>());
        SelectedRoom.GetComponent<ObjectLayoutGroup>().ChildDestory();
        for (int i = 0; i < RePo; i++)
        {
            var RanNum = Random.Range(0, 100);

            if (RanNum <= persent)
            {
                sum++;
                SelectedRoom.GetComponent<ObjectLayoutGroup>().StackObjects(RePo, true);
            }
            else
            {
                nsum++;
                SelectedRoom.GetComponent<ObjectLayoutGroup>().StackObjects(RePo, false);
            }
            yield return new WaitForSeconds(0.2f);
        }
        Debug.Log($"성공 : {sum}, 실패 : {nsum}");
    }

    public void EmployeeSelected() //데미지 받으면 다시 호출하는 방식으로 해야할듯 그래야 체력이 동기화됨 + 힐 받을 때
    {
        var SelectedEmployeeData = Selection_Obj.Instance.SelectOBJ[0].GetComponent<Employee>();
        empName.text = SelectedEmployeeData._empName;
        empHp.text = $"체력 : {EmployeeManager.Instance.Employees[SelectedEmployeeData._empName].CurrentHP}/{SelectedEmployeeData._empMaxHp}";
        empMp.text = $"정신력 : {EmployeeManager.Instance.Employees[SelectedEmployeeData._empName].CurrentMP}/{SelectedEmployeeData._empMaxMp}";
        empDepartment.text = $"근무 부서 : {SelectedEmployeeData._empDepartment}";
        empDef.text = $"방어력 : {SelectedEmployeeData._empdef}";
        empPower.text = $"힘 : {SelectedEmployeeData._empPower}";
        empintelligence.text = $"지능 : {SelectedEmployeeData._empintelligence}";
        empJustice.text = $"정의 : {SelectedEmployeeData._empJustice}";
        empMovementSpeed.text = $"이동속도 : {SelectedEmployeeData._empMovementSpeed}";

        EmployeeSelected_HpSlider.maxValue = SelectedEmployeeData._empMaxHp;
        EmployeeSelected_HpSlider.value = EmployeeManager.Instance.Employees[SelectedEmployeeData._empName].CurrentHP;
        EmployeeSelected_MpSlider.maxValue = SelectedEmployeeData._empMaxMp;
        EmployeeSelected_MpSlider.value = EmployeeManager.Instance.Employees[SelectedEmployeeData._empName].CurrentMP;
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
