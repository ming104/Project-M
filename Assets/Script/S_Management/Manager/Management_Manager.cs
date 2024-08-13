using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.EventSystems;

public class Management_Manager : MonoBehaviour
{
    [Header("Main_Canvas")]
    public TextMeshProUGUI Day;
    public TextMeshProUGUI Money;
    public TextMeshProUGUI ResearchPoint;
    public Button GameStartBtn;
    public Button FloorOpeningBtn;
    public TextMeshProUGUI currentFloor_Text;
    public int FloorNumber;
    /*
      1    
    2 0 4
      3
    */
    public int DepartmentNumber;


    [Header("Selected_Depart")]
    public TextMeshProUGUI Department;
    public List<Image> MonsterImage;
    public List<TextMeshProUGUI> MonsterNameText;
    public List<Image> EmployeeImage;
    public List<TextMeshProUGUI> EmployeeNameText;

    [Header("Selected_Depart_S")]
    public GameObject Selected_Depart_S;
    public TextMeshProUGUI Department_S;
    public List<Image> AuditEmployeeImage_S;
    public List<TextMeshProUGUI> AuditEmployeeNameText_S;
    public List<Image> AccountEmployeeImage_S;
    public List<TextMeshProUGUI> AccountEmployeeNameText_S;

    [Header("InfoCanvas")]
    public GameObject InfoCanvas;
    public Image monsterImage;
    public TextMeshProUGUI monsterName;
    public TextMeshProUGUI code;
    public TextMeshProUGUI dangerLevel;
    public TextMeshProUGUI research_log;
    [Header("Employ_Canvas")]
    public GameObject Employ_Canvas;
    [Header("UnaffiliatedEmployee_Panel")]
    public GameObject UnaffiliatedEmployee_Panel;
    public GameObject UnaffiliatedEmployee_Info;
    public Transform Prefab_UnaffiliatedEmployee_List;
    public GameObject Prefab_UnaffiliatedEmployee_Ele;
    public List<GameObject> Prefab_UnaffiliatedEmployee_Ele_List;
    public string SelectedName;
    [Header("UnaffiliatedEmployee_Panel_UI")]
    public TextMeshProUGUI UnaffiliatedEmployeeName;
    public TextMeshProUGUI UnaffiliatedEmployeeHp;
    public TextMeshProUGUI UnaffiliatedEmployeeMp;
    public TextMeshProUGUI UnaffiliatedEmployeeDef;
    public TextMeshProUGUI UnaffiliatedEmployeePower;
    public TextMeshProUGUI UnaffiliatedEmployeeIntelligence;
    public TextMeshProUGUI UnaffiliatedEmployeeJustice;
    public TextMeshProUGUI UnaffiliatedEmployeeMovementSpeed;
    [Header("EmpInfo_Selected")]
    public GameObject AffiliatedEmployee_Panel;
    public TextMeshProUGUI AffiliatedEmployeeName;
    public TextMeshProUGUI AffiliatedEmployeeHp;
    public TextMeshProUGUI AffiliatedEmployeeMp;
    public TextMeshProUGUI AffiliatedEmployeeDef;
    public TextMeshProUGUI AffiliatedEmployeePower;
    public TextMeshProUGUI AffiliatedEmployeeIntelligence;
    public TextMeshProUGUI AffiliatedEmployeeJustice;
    public TextMeshProUGUI AffiliatedEmployeeMovementSpeed;
    public GameObject Selected_GameObject;
    [Header("MonsterBuy_Element")]
    public GameObject MonsterBuy_Canvas;
    public List<GameObject> MonsterBuy_GO;
    [Header("Random_Duplicate_List")]
    public List<int> DuplicateList;
    [Header("Select_FD")]
    public GameObject Depart_Select;
    public GameObject Selected_Depart;
    public List<GameObject> Depart;
    public GameObject Depart_S;

    [Header("ResetImage")]
    public Sprite ResetImage;
    // Start is called before the first frame update
    void Start()
    {
        Day.text = $"Day : {DataManager.Instance.MainDataLoad().day}";
        Money.text = $"돈 : {DataManager.Instance.MainDataLoad().Money}";
        ResearchPoint.text = $"연구 포인트 : {DataManager.Instance.MainDataLoad().ResearchPoint}";
        DepartmentNumber = 0;
        FloorNumber = 0;
        currentFloor_Text.text = $"현재 층 : {FloorNumber}층";
        Depart_Select.SetActive(true);
        Selected_Depart.SetActive(false);
        Selected_Depart_S.SetActive(false);
        FloorInfo();
        GameStartBtn_check();
        FloorOpeningBtn_Check();
        UnaffiliatedEmployee_Panel_Off();
        AffiliatedEmployee_Panel_Off();
        MonsterBuy_Canvas_Off();
    }

    public void DepartmentInfo(int _Depart)
    {
        DepartmentInfoReset();
        DepartmentNumber = _Depart;
        Department.text = $"현재 부서 : 관리부서-{DepartmentNumber}";
        Depart_Select.SetActive(false);
        Selected_Depart.SetActive(true);
        for (int i = 0; i < DataManager.Instance.MainDataLoad().Floor[FloorNumber].Department[DepartmentNumber].MonsterList.Count; i++)
        {
            MonsterImage[i].sprite = Resources.Load<Sprite>(DataManager.Instance.MonsterDataLoad
                (DataManager.Instance.MainDataLoad().Floor[FloorNumber].Department[DepartmentNumber].MonsterList[i]).profile.imagePATH);
            MonsterNameText[i].text = DataManager.Instance.MonsterDataLoad
                (DataManager.Instance.MainDataLoad().Floor[FloorNumber].Department[DepartmentNumber].MonsterList[i]).profile.MonsterName;
            MonsterImage[i].GetComponent<MonsterInfo_Management>()._monName =
                DataManager.Instance.MainDataLoad().Floor[FloorNumber].Department[DepartmentNumber].MonsterList[i];
        }
        for (int i = 0; i < DataManager.Instance.MainDataLoad().Floor[FloorNumber].Department[DepartmentNumber].EmployeeList.Count; i++)
        {
            EmployeeImage[i].sprite = null;
            EmployeeImage[i].GetComponent<EmployeeInfo_Management>()._EmpName = DataManager.Instance.MainDataLoad().Floor[FloorNumber].Department[DepartmentNumber].EmployeeList[i];
            EmployeeImage[i].GetComponent<EmployeeInfo_Management>()._EmpDepart = DepartmentNumber;
            EmployeeNameText[i].text = DataManager.Instance.MainDataLoad().Floor[FloorNumber].Department[DepartmentNumber].EmployeeList[i];
        }
        GameStartBtn_check();
    }

    public void DepartmentInfo_S()
    {
        DepartmentInfoReset_S();
        Department_S.text = $"현재 부서 : 총괄부서";
        DepartmentNumber = -1;
        Depart_Select.SetActive(false);
        Selected_Depart_S.SetActive(true);
        foreach (var emp in AuditEmployeeImage_S)
        {
            emp.GetComponent<EmployeeInfo_Management>()._EmpDepart = -2;
        }
        foreach (var emp in AccountEmployeeImage_S)
        {
            emp.GetComponent<EmployeeInfo_Management>()._EmpDepart = -1;
        }
        for (int i = 0; i < DataManager.Instance.MainDataLoad().Floor[FloorNumber].AuditDepartment.Count; i++)
        {
            AuditEmployeeImage_S[i].sprite = null;
            AuditEmployeeImage_S[i].GetComponent<EmployeeInfo_Management>()._EmpName = DataManager.Instance.MainDataLoad().Floor[FloorNumber].AuditDepartment[i];
            
            AuditEmployeeNameText_S[i].text = DataManager.Instance.MainDataLoad().Floor[FloorNumber].AuditDepartment[i];
        }
        
        for (int i = 0; i < DataManager.Instance.MainDataLoad().Floor[FloorNumber].AccountingDepartment.Count; i++)
        {
            AccountEmployeeImage_S[i].sprite = null;
            AccountEmployeeImage_S[i].GetComponent<EmployeeInfo_Management>()._EmpName = DataManager.Instance.MainDataLoad().Floor[FloorNumber].AccountingDepartment[i];
            AccountEmployeeImage_S[i].GetComponent<EmployeeInfo_Management>()._EmpDepart = -1;
            AccountEmployeeNameText_S[i].text = DataManager.Instance.MainDataLoad().Floor[FloorNumber].AccountingDepartment[i];
        }
        GameStartBtn_check();
    }

    public void GameStartBtn_check()
    {
        for (int f = 0; f < DataManager.Instance.MainDataLoad().Floor.Count; f++)
        {
            for (int i = 0; i < DataManager.Instance.MainDataLoad().Floor[f].Department.Count; i++)
            {
                if (DataManager.Instance.MainDataLoad().Floor[f].Department[i].EmployeeList.Count == 0
                || DataManager.Instance.MainDataLoad().Floor[f].AuditDepartment.Count == 0
                || DataManager.Instance.MainDataLoad().Floor[f].AccountingDepartment.Count == 0)
                {
                    GameStartBtn.interactable = false;
                    return;
                }
                else
                {
                    GameStartBtn.interactable = true;
                }
            }
        }
    }
    public void FloorOpeningBtn_Check()
    {
        var maindata = DataManager.Instance.MainDataLoad();
        if (maindata.Floor[maindata.Floor.Count - 1].Department.Count == 4
        && maindata.Floor[maindata.Floor.Count - 1].Department[maindata.Floor[maindata.Floor.Count - 1].Department.Count - 1].MonsterList.Count == 4)
        {
            FloorOpeningBtn.interactable = true;
        }
        else
        {
            FloorOpeningBtn.interactable = false;
        }
    }

    void DepartmentInfoReset()
    {
        for (int i = 0; i < MonsterImage.Count; i++)
        {
            MonsterImage[i].sprite = ResetImage;
            MonsterNameText[i].text = "없음";
            MonsterImage[i].GetComponent<MonsterInfo_Management>()._monName = string.Empty;
        }
        for (int i = 0; i < EmployeeImage.Count; i++)
        {
            EmployeeImage[i].sprite = ResetImage;
            EmployeeNameText[i].text = "없음";
            EmployeeImage[i].GetComponent<EmployeeInfo_Management>()._EmpName = string.Empty;
        }
    }
    void DepartmentInfoReset_S()
    {
        Department_S.text = string.Empty;
        for (int i = 0; i < AuditEmployeeImage_S.Count; i++)
        {
            AuditEmployeeImage_S[i].sprite = ResetImage;
            AuditEmployeeNameText_S[i].text = "없음";
            AuditEmployeeImage_S[i].GetComponent<EmployeeInfo_Management>()._EmpName = string.Empty;
        }
        for (int i = 0; i < AccountEmployeeImage_S.Count; i++)
        {
            AccountEmployeeImage_S[i].sprite = ResetImage;
            AccountEmployeeNameText_S[i].text = "없음";
            AccountEmployeeImage_S[i].GetComponent<EmployeeInfo_Management>()._EmpName = string.Empty;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            // 아무런 오브젝트와 충돌하지 않았을 때의 처리                
            //Debug.Log("No object clicked.");
            AffiliatedEmployee_Panel_Off();
        }
        FloorNumberSizeControll();
    }

    void FloorNumberSizeControll()
    {
        if (Depart_Select.activeSelf == true)
        {
            float wheelInput = Input.GetAxis("Mouse ScrollWheel");
            if (wheelInput > 0 && FloorNumber > 0) // 올렸을 때 처리 -> Floor감소
            {
                FloorNumber--;
                FloorInfo();
            }
            else if (wheelInput < 0 && FloorNumber < DataManager.Instance.MainDataLoad().Floor.Count - 1) // 내렸을 때 처리 -> Floor증가
            {
                FloorNumber++;
                FloorInfo();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                FloorInfo();
                Depart_Select.SetActive(true);
                Selected_Depart.SetActive(false);
                Selected_Depart_S.SetActive(false);
            }
        }
    }
    public void FloorInfoReset()
    {
        for (int i = 0; i < Depart.Count; i++)
        {
            Depart[i].GetComponent<Button>().interactable = false;
            ColorBlock BtnColor = Depart[i].GetComponent<Button>().colors;
            BtnColor.normalColor = Color.white;
            Depart[i].GetComponent<Button>().colors = BtnColor;
        }

        ColorBlock BtnColors = Depart_S.GetComponent<Button>().colors;
        BtnColors.normalColor = Color.white;
        Depart_S.GetComponent<Button>().colors = BtnColors;
    }

    public void FloorInfo()
    {
        FloorInfoReset();
        currentFloor_Text.text = $"현재 층 : {FloorNumber}층";

        var mainData = DataManager.Instance.MainDataLoad();

        if (mainData != null && FloorNumber < mainData.Floor.Count)
        {
            for (int i = 0; i < mainData.Floor[FloorNumber].Department.Count; i++) // 여기를 있는 갯수만큼 세아리고 나머지를 인터랙트 꺼야함 수정필요!
            {
                Depart[i].GetComponent<Button>().interactable = true;

                if (DataManager.Instance.MainDataLoad().Floor[FloorNumber].Department[i].MonsterList.Count != 0 && DataManager.Instance.MainDataLoad().Floor[FloorNumber].Department[i].EmployeeList.Count == 0)
                {
                    ColorBlock BtnColor = Depart[i].GetComponent<Button>().colors;
                    BtnColor.normalColor = Color.red;
                    Depart[i].GetComponent<Button>().colors = BtnColor;
                    //Depart[i].GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
        }
        if(mainData.Floor[FloorNumber].AccountingDepartment.Count == 0
           || mainData.Floor[FloorNumber].AuditDepartment.Count == 0)
        {
            ColorBlock BtnColor = Depart_S.GetComponent<Button>().colors;
            BtnColor.normalColor = Color.red;
            Depart_S.GetComponent<Button>().colors = BtnColor;
        }
        GameStartBtn_check();
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

    public void InfoSelectButton(GameObject GO)
    {
        if (GO.GetComponent<MonsterInfo_Management>()._monName != string.Empty)
        {
            var monsterData = DataManager.Instance.MonsterDataLoad(GO.GetComponent<MonsterInfo_Management>()._monName);
            InfoCanvasOn(monsterData);
        }
        else
        {
            Debug.Log("비어있음");
            return;
        }
    }

    public void Employ_Canvas_ON()
    {
        Employ_Manager.Instance.Employ_Setting();
        Employ_Canvas.SetActive(true);
    }
    public void Employ_Canvas_OFF()
    {
        Employ_Canvas.SetActive(false);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void UnaffiliatedEmployee_Panel_On()
    {
        UnaffiliatedEmployee_Info_Off();
        for (int i = 0; i < DataManager.Instance.MainDataLoad().UnaffiliatedEmployee.Count; i++)
        {
            var UnaffiliatedEmployee = Instantiate(Prefab_UnaffiliatedEmployee_Ele, Prefab_UnaffiliatedEmployee_List);
            var UnaffEmpData = DataManager.Instance.EmployeeDataLoad(DataManager.Instance.MainDataLoad().UnaffiliatedEmployee[i]);
            var GetComUnEmpInfo = UnaffiliatedEmployee.GetComponent<UnaffiliatedEmployee_Info>();
            GetComUnEmpInfo._empName = UnaffEmpData.name;
            GetComUnEmpInfo._empHp = UnaffEmpData.hp;
            GetComUnEmpInfo._empMp = UnaffEmpData.mp;
            GetComUnEmpInfo._empdef = UnaffEmpData.def;
            GetComUnEmpInfo._empPower = UnaffEmpData.power;
            GetComUnEmpInfo._empintelligence = UnaffEmpData.intelligence;
            GetComUnEmpInfo._empJustice = UnaffEmpData.justice;
            GetComUnEmpInfo._empMovementSpeed = UnaffEmpData.movementSpeed;
            Prefab_UnaffiliatedEmployee_Ele_List.Add(UnaffiliatedEmployee);
            UnaffiliatedEmployee.GetComponent<Button>().onClick.AddListener(() => ShowUnaffiliatedEmployeeData(UnaffiliatedEmployee));
        }
        UnaffiliatedEmployee_Panel.SetActive(true);
    }
    public void UnaffiliatedEmployee_Panel_Off()
    {
        foreach (Transform child in Prefab_UnaffiliatedEmployee_List)
        {
            Destroy(child.gameObject);
        }
        UnaffiliatedEmployee_Panel.SetActive(false);
    }

    public void EmployeeInfoSelectButton(GameObject GO)
    {
        if (GO.GetComponent<EmployeeInfo_Management>()._EmpName != string.Empty)
        {
            var UnaffiliatedEmployeeData = DataManager.Instance.EmployeeDataLoad(GO.GetComponent<EmployeeInfo_Management>()._EmpName);
            DepartmentNumber = GO.GetComponent<EmployeeInfo_Management>()._EmpDepart;
            AffiliatedEmployee_Panel_On(UnaffiliatedEmployeeData);
        }
        else
        {
            DepartmentNumber = GO.GetComponent<EmployeeInfo_Management>()._EmpDepart;
            UnaffiliatedEmployee_Panel_On();
        }
    }

    public void ShowUnaffiliatedEmployeeData(GameObject GO)
    {
        var UnEmp_Info = GO.GetComponent<UnaffiliatedEmployee_Info>();
        UnaffiliatedEmployeeName.text = $"이름 : {UnEmp_Info._empName}";
        UnaffiliatedEmployeeHp.text = $"체력 : {UnEmp_Info._empHp}";
        UnaffiliatedEmployeeMp.text = $"정신력 : {UnEmp_Info._empMp}";
        UnaffiliatedEmployeeDef.text = $"방어력 : {UnEmp_Info._empdef}";
        UnaffiliatedEmployeePower.text = $"힘 : {UnEmp_Info._empPower}";
        UnaffiliatedEmployeeIntelligence.text = $"지능 : {UnEmp_Info._empintelligence}";
        UnaffiliatedEmployeeJustice.text = $"정의 : {UnEmp_Info._empJustice}";
        UnaffiliatedEmployeeMovementSpeed.text = $"이동속도 : {UnEmp_Info._empMovementSpeed}";
        SelectedName = UnEmp_Info._empName;
        UnaffiliatedEmployee_Info_On();
    }

    public void UnaffiliatedEmployee_Info_On()
    {
        UnaffiliatedEmployee_Info.SetActive(true);
    }
    public void UnaffiliatedEmployee_Info_Off()
    {
        SelectedName = string.Empty;
        UnaffiliatedEmployee_Info.SetActive(false);
    }

    public void InsertUnaffiliatedEmp()
    {
        if (SelectedName != string.Empty)
        {
            DataManager.Instance.MaindataSave(2, FloorNumber, DepartmentNumber, SelectedName);
            if (DepartmentNumber >= 0)
            {
                DepartmentInfo(DepartmentNumber);
            }
            else
            {
                DepartmentInfo_S();
            }
            UnaffiliatedEmployee_Panel_Off();
        }
        else
        {
            Debug.Log("비어있음");
        }
    }

    public void ConvertUnaffiliatedEmp()
    {
        if (SelectedName != string.Empty)
        {
            DataManager.Instance.MaindataSave(3, FloorNumber, DepartmentNumber, SelectedName);
            AffiliatedEmployee_Panel_Off();
            if (DepartmentNumber >= 0)
            {
                DepartmentInfo(DepartmentNumber); // 리셋 느낌
            }
            else
            {
                DepartmentInfo_S();
            }
        }
        else
        {
            Debug.Log("비어있음");
        }
    }
    public void AffiliatedEmployee_Panel_On(EmployeeData empdata)
    {
        AffiliatedEmployeeName.text = $"이름 : {empdata.name}";
        AffiliatedEmployeeHp.text = $"체력 : {empdata.hp}";
        AffiliatedEmployeeMp.text = $"정신력 : {empdata.mp}";
        AffiliatedEmployeeDef.text = $"방어력 : {empdata.def}";
        AffiliatedEmployeePower.text = $"힘 : {empdata.power}";
        AffiliatedEmployeeIntelligence.text = $"지능 : {empdata.intelligence}";
        AffiliatedEmployeeJustice.text = $"정의 : {empdata.justice}";
        AffiliatedEmployeeMovementSpeed.text = $"이동속도 : {empdata.movementSpeed}";
        SelectedName = empdata.name;
        AffiliatedEmployee_Panel.SetActive(true);
    }
    public void AffiliatedEmployee_Panel_Off()
    {
        AffiliatedEmployee_Panel.SetActive(false);
    }
    public int CreateUnDuplicateRandom(int min, int max)
    {
        int number = 0;
        int currentNumber = 0;

        while (number < max)
        {
            currentNumber = UnityEngine.Random.Range(min, max);
            if (!DuplicateList.Contains(currentNumber))
            {
                DuplicateList.Add(currentNumber);
                break;
            }
            else
            {
                number++;
            }
        }
        return currentNumber;
    }
    public void MonsterBuy_Canvas_Btn_On()
    {
        for (int i = 0; i < MonsterBuy_GO.Count; i++)
        {
            var rand_number = CreateUnDuplicateRandom(0, DataManager.Instance.MainDataLoad().All_Monster.Count);
            var MonsterInfo = MonsterBuy_GO[i].GetComponent<MonsterBuy_Selected_Info>();
            var MonsterDataInfo = DataManager.Instance.MonsterDataLoad(DataManager.Instance.MainDataLoad().All_Monster[rand_number]);
            MonsterInfo._MonCode = MonsterDataInfo.profile.code;
            MonsterInfo._MonName = MonsterDataInfo.profile.MonsterName;
            MonsterInfo._MonInfo = MonsterDataInfo.profile.MonsterBuy_Info;

        }
        MonsterBuy_Canvas.SetActive(true);
    }
    public void MonsterBuy_Canvas_Btn_Off(int num)
    {
        string RandomMonster = DataManager.Instance.MainDataLoad().All_Monster[DuplicateList[num]];
        DataManager.Instance.MaindataSave(0, FloorNumber, DepartmentNumber, RandomMonster);
        Debug.Log(RandomMonster);
        DuplicateList.Clear();
        FloorInfo();
        MonsterBuy_Canvas_Off();
        FloorOpeningBtn_Check();
    }
    public void MonsterBuy_Canvas_Off()
    {
        MonsterBuy_Canvas.SetActive(false);
    }

    public void AddFloor()
    {
        DataManager.Instance.AddFloor();
        FloorOpeningBtn_Check();
    }

}