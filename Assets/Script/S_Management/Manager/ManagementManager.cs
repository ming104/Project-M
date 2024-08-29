using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ManagementManager : Singleton<ManagementManager>
{
    [Header("Main_Canvas")]
    public TextMeshProUGUI mainDay;
    public TextMeshProUGUI mainMoney;
    public TextMeshProUGUI mainResearchPoint;
    public Button gameStartBtn;
    public Button floorOpeningBtn;
    public TextMeshProUGUI currentFloorText;
    public int floorNumber;
    /*
      1    
    2 0 4
      3
    */
    public int departmentNumber;


    [Header("Selected_Depart")]
    public TextMeshProUGUI department;
    public List<Image> monsterImageList;
    public List<TextMeshProUGUI> monsterNameText;
    public List<Image> employeeImage;
    public List<TextMeshProUGUI> employeeNameText;

    [Header("Selected_Depart_General")]
    public GameObject selectedDepartGeneral;
    public TextMeshProUGUI departmentGeneral;
    public List<Image> auditEmployeeImageGeneral;
    public List<TextMeshProUGUI> auditEmployeeNameTextGeneral;
    public List<Image> accountEmployeeImageGeneral;
    public List<TextMeshProUGUI> accountEmployeeNameTextGeneral;
    public TextMeshProUGUI dayGeneral;
    public TextMeshProUGUI moneyGeneral;
    public TextMeshProUGUI researchPointGeneral;

    [Header("InfoCanvas")] 
    public GameObject infoCanvas;
    public Image monsterImage;
    public TextMeshProUGUI monsterName;
    public TextMeshProUGUI possibilityOfEscape;
    public TextMeshProUGUI mentalDamage;
    public TextMeshProUGUI monsterCode;
    public TextMeshProUGUI riskLevel;
    public TextMeshProUGUI researchLog;
    public Transform logContent;
    public GameObject logTextBoxPrefab;
    public TextMeshProUGUI feelingBad;
    public TextMeshProUGUI feelingDefault;
    public TextMeshProUGUI feelingGood;

    public Image equipmentImage;
    public TextMeshProUGUI equipmentName;
    public TextMeshProUGUI equipmentType;
    public TextMeshProUGUI equipEffect;
    public TextMeshProUGUI equipSpacialEffect;
    public TextMeshProUGUI equipCost;
    public GameObject equipBuyPostit;
    
    public TextMeshProUGUI monsterResearchFear;
    public TextMeshProUGUI monsterResearchAnger;
    public TextMeshProUGUI monsterResearchDisgust;
    public TextMeshProUGUI monsterResearchSad;
    public TextMeshProUGUI monsterResearchHappy;
    public TextMeshProUGUI monsterResearchSurprise;
    
    [Header("Employ_Canvas")]
    public GameObject employCanvas;
    
    [Header("UnaffiliatedEmployee_Panel")]
    public GameObject unaffiliatedEmployeePanel;
    public GameObject unaffiliatedEmployeeInfo;
    public Transform prefabUnaffiliatedEmployeeList;
    public GameObject prefabUnaffiliatedEmployeeElement;
    public List<GameObject> prefabUnaffiliatedEmployee;
    public string selectedName;
    
    [Header("UnaffiliatedEmployee_Panel_UI")]
    public TextMeshProUGUI unaffiliatedEmployeeName;
    public TextMeshProUGUI unaffiliatedEmployeeHp;
    public TextMeshProUGUI unaffiliatedEmployeeMp;
    public TextMeshProUGUI unaffiliatedEmployeeDef;
    public TextMeshProUGUI unaffiliatedEmployeePower;
    public TextMeshProUGUI unaffiliatedEmployeeIntelligence;
    public TextMeshProUGUI unaffiliatedEmployeeJustice;
    public TextMeshProUGUI unaffiliatedEmployeeMovementSpeed;
    
    [Header("EmpInfo_Selected")]
    public GameObject affiliatedEmployeePanel;
    public TextMeshProUGUI affiliatedEmployeeName;
    public TextMeshProUGUI affiliatedEmployeeHp;
    public TextMeshProUGUI affiliatedEmployeeMp;
    public TextMeshProUGUI affiliatedEmployeeDef;
    public TextMeshProUGUI affiliatedEmployeePower;
    public TextMeshProUGUI affiliatedEmployeeIntelligence;
    public TextMeshProUGUI affiliatedEmployeeJustice;
    public TextMeshProUGUI affiliatedEmployeeMovementSpeed;
    public GameObject selectedGameObject;
    
    [Header("MonsterBuy_Element")]
    public GameObject monsterBuyCanvas;
    public List<GameObject> monsterBuyGameObjects;
    
    [Header("Random_Duplicate_List")]
    public List<int> duplicateList;
    
    [Header("Select_FD")]
    public GameObject departSelect;
    public GameObject selectedDepart;
    public List<GameObject> depart;
    public GameObject departGeneral;

    [Header("ResetImage")]
    public Sprite resetImage;

    public int currentMoney;
    public int currentRP;
    // Start is called before the first frame update
    void Start()
    {
        mainDay.text = $"Day : {DataManager.Instance.MainDataLoad().day}";
        dayGeneral.text = $"Day : {DataManager.Instance.MainDataLoad().day}";
        currentMoney = DataManager.Instance.MainDataLoad().Money;
        currentRP = DataManager.Instance.MainDataLoad().ResearchPoint;
        
        //mainResearchPoint.text = $"연구 포인트 : {currentRP}";
        departmentNumber = 0;
        floorNumber = 0;
        currentFloorText.text = $"현재 층 : {floorNumber + 1}층";
        departSelect.SetActive(true);
        selectedDepart.SetActive(false);
        selectedDepartGeneral.SetActive(false);
        FloorInfo();
        GameStartBtn_check();
        FloorOpeningBtn_Check();
        UnaffiliatedEmployee_Panel_Off();
        AffiliatedEmployee_Panel_Off();
        MonsterBuy_Canvas_Off();
    }

    // Update is called once per frame
    void Update()
    {
        mainMoney.text = $"돈 : {currentMoney}";
        mainResearchPoint.text = $"연구 포인트 : {currentRP}";
        moneyGeneral.text = $"돈 : {currentMoney}";
        researchPointGeneral.text = $"연구 포인트 : {currentRP}";
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            // 아무런 오브젝트와 충돌하지 않았을 때의 처리                
            //Debug.Log("No object clicked.");
            AffiliatedEmployee_Panel_Off();
        }
        FloorNumberSizeControll();
    }

    public void DepartmentInfo(int _Depart)
    {
        DepartmentInfoReset();
        departmentNumber = _Depart;
        department.text = $"현재 부서 : 관리부서-{departmentNumber}";
        departSelect.SetActive(false);
        selectedDepart.SetActive(true);
        for (int i = 0; i < DataManager.Instance.MainDataLoad().Floor[floorNumber].Department[departmentNumber].MonsterList.Count; i++)
        {
            monsterImageList[i].sprite = Resources.Load<Sprite>(DataManager.Instance.MonsterDataLoad
                (DataManager.Instance.MainDataLoad().Floor[floorNumber].Department[departmentNumber].MonsterList[i]).profile.imagePATH);
            monsterNameText[i].text = DataManager.Instance.MonsterDataLoad
                (DataManager.Instance.MainDataLoad().Floor[floorNumber].Department[departmentNumber].MonsterList[i]).profile.MonsterName;
            monsterImageList[i].GetComponent<MonsterInfo_Management>()._monName =
                DataManager.Instance.MainDataLoad().Floor[floorNumber].Department[departmentNumber].MonsterList[i];
        }
        for (int i = 0; i < DataManager.Instance.MainDataLoad().Floor[floorNumber].Department[departmentNumber].EmployeeList.Count; i++)
        {
            employeeImage[i].sprite = null;
            employeeImage[i].GetComponent<EmployeeInfo_Management>()._EmpName = DataManager.Instance.MainDataLoad().Floor[floorNumber].Department[departmentNumber].EmployeeList[i];
            employeeImage[i].GetComponent<EmployeeInfo_Management>()._EmpDepart = departmentNumber;
            employeeNameText[i].text = DataManager.Instance.MainDataLoad().Floor[floorNumber].Department[departmentNumber].EmployeeList[i];
        }
        GameStartBtn_check();
    }

    public void DepartmentInfo_S()
    {
        DepartmentInfoReset_S();
        departmentGeneral.text = $"현재 부서 : 총괄부서";
        departmentNumber = -1;
        departSelect.SetActive(false);
        selectedDepartGeneral.SetActive(true);
        foreach (var emp in auditEmployeeImageGeneral)
        {
            emp.GetComponent<EmployeeInfo_Management>()._EmpDepart = -2;
        }
        foreach (var emp in accountEmployeeImageGeneral)
        {
            emp.GetComponent<EmployeeInfo_Management>()._EmpDepart = -1;
        }
        for (int i = 0; i < DataManager.Instance.MainDataLoad().Floor[floorNumber].AuditDepartment.Count; i++)
        {
            auditEmployeeImageGeneral[i].sprite = null;
            auditEmployeeImageGeneral[i].GetComponent<EmployeeInfo_Management>()._EmpName = DataManager.Instance.MainDataLoad().Floor[floorNumber].AuditDepartment[i];
            
            auditEmployeeNameTextGeneral[i].text = DataManager.Instance.MainDataLoad().Floor[floorNumber].AuditDepartment[i];
        }
        
        for (int i = 0; i < DataManager.Instance.MainDataLoad().Floor[floorNumber].AccountingDepartment.Count; i++)
        {
            accountEmployeeImageGeneral[i].sprite = null;
            accountEmployeeImageGeneral[i].GetComponent<EmployeeInfo_Management>()._EmpName = DataManager.Instance.MainDataLoad().Floor[floorNumber].AccountingDepartment[i];
            accountEmployeeImageGeneral[i].GetComponent<EmployeeInfo_Management>()._EmpDepart = -1;
            accountEmployeeNameTextGeneral[i].text = DataManager.Instance.MainDataLoad().Floor[floorNumber].AccountingDepartment[i];
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
                    gameStartBtn.interactable = false;
                    return;
                }
                else
                {
                    gameStartBtn.interactable = true;
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
            floorOpeningBtn.interactable = true;
        }
        else
        {
            floorOpeningBtn.interactable = false;
        }
    }

    void DepartmentInfoReset()
    {
        for (int i = 0; i < monsterImageList.Count; i++)
        {
            monsterImageList[i].sprite = resetImage;
            monsterNameText[i].text = "없음";
            monsterImageList[i].GetComponent<MonsterInfo_Management>()._monName = string.Empty;
        }
        for (int i = 0; i < employeeImage.Count; i++)
        {
            employeeImage[i].sprite = resetImage;
            employeeNameText[i].text = "없음";
            employeeImage[i].GetComponent<EmployeeInfo_Management>()._EmpName = string.Empty;
        }
    }
    void DepartmentInfoReset_S()
    {
        departmentGeneral.text = string.Empty;
        for (int i = 0; i < auditEmployeeImageGeneral.Count; i++)
        {
            auditEmployeeImageGeneral[i].sprite = resetImage;
            auditEmployeeNameTextGeneral[i].text = "없음";
            auditEmployeeImageGeneral[i].GetComponent<EmployeeInfo_Management>()._EmpName = string.Empty;
        }
        for (int i = 0; i < accountEmployeeImageGeneral.Count; i++)
        {
            accountEmployeeImageGeneral[i].sprite = resetImage;
            accountEmployeeNameTextGeneral[i].text = "없음";
            accountEmployeeImageGeneral[i].GetComponent<EmployeeInfo_Management>()._EmpName = string.Empty;
        }
    }

    void FloorNumberSizeControll()
    {
        if (departSelect.activeSelf == true)
        {
            float wheelInput = Input.GetAxis("Mouse ScrollWheel");
            if (wheelInput > 0 && floorNumber > 0) // 올렸을 때 처리 -> Floor감소
            {
                floorNumber--;
                FloorInfo();
            }
            else if (wheelInput < 0 && floorNumber < DataManager.Instance.MainDataLoad().Floor.Count - 1) // 내렸을 때 처리 -> Floor증가
            {
                floorNumber++;
                FloorInfo();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                FloorInfo();
                departSelect.SetActive(true);
                selectedDepart.SetActive(false);
                selectedDepartGeneral.SetActive(false);
            }
        }
    }
    public void FloorInfoReset()
    {
        for (int i = 0; i < depart.Count; i++)
        {
            depart[i].GetComponent<Button>().interactable = false;
            ColorBlock BtnColor = depart[i].GetComponent<Button>().colors;
            BtnColor.normalColor = Color.white;
            depart[i].GetComponent<Button>().colors = BtnColor;
        }

        ColorBlock BtnColors = departGeneral.GetComponent<Button>().colors;
        BtnColors.normalColor = Color.white;
        departGeneral.GetComponent<Button>().colors = BtnColors;
    }

    public void FloorInfo()
    {
        FloorInfoReset();
        currentFloorText.text = $"현재 층 : {floorNumber}층";

        var mainData = DataManager.Instance.MainDataLoad();

        if (mainData != null && floorNumber < mainData.Floor.Count)
        {
            for (int i = 0; i < mainData.Floor[floorNumber].Department.Count; i++) // 여기를 있는 갯수만큼 세아리고 나머지를 인터랙트 꺼야함 수정필요!
            {
                depart[i].GetComponent<Button>().interactable = true;

                if (DataManager.Instance.MainDataLoad().Floor[floorNumber].Department[i].MonsterList.Count != 0 && DataManager.Instance.MainDataLoad().Floor[floorNumber].Department[i].EmployeeList.Count == 0)
                {
                    ColorBlock BtnColor = depart[i].GetComponent<Button>().colors;
                    BtnColor.normalColor = Color.red;
                    depart[i].GetComponent<Button>().colors = BtnColor;
                    //Depart[i].GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
        }
        if(mainData.Floor[floorNumber].AccountingDepartment.Count == 0
           || mainData.Floor[floorNumber].AuditDepartment.Count == 0)
        {
            ColorBlock BtnColor = departGeneral.GetComponent<Button>().colors;
            BtnColor.normalColor = Color.red;
            departGeneral.GetComponent<Button>().colors = BtnColor;
        }
        GameStartBtn_check();
    }

    public void InfoCanvasOn(MonsterData monsterData) // 수정 필요
    {
        monsterImage.sprite = Resources.Load<Sprite>(monsterData.profile.imagePATH);
        monsterName.text = $"이름 : {monsterData.profile.MonsterName}";
        possibilityOfEscape.text = $"탈출 여부 : {monsterData.profile.isEscape}";
        mentalDamage.text = $"연구시 정신피해 정도 : {monsterData.profile.researchMentalDamage}";
        monsterCode.text = $"{monsterData.profile.code}";
        riskLevel.text = $"위험도 : {monsterData.profile.riskLevel}";
        researchLog.text = "연구 기록";
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

        equipmentImage.sprite = Resources.Load<Sprite>(monsterData.MonEquipment.imagePATH);
        equipmentName.text = $"{monsterData.MonEquipment.EquipName}";

        switch (monsterData.MonEquipment.type)
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
        equipCost.text = $"가격 : 자금 : {monsterData.MonEquipment.buyMoney}, RP : {monsterData.MonEquipment.buyRP}";

        equipBuyPostit.GetComponent<EquipmentBuyManagement>().monsterData = monsterData;

        monsterResearchFear.text = $"{monsterData.Research_Preferences.FEAR}%";
        monsterResearchAnger.text = $"{monsterData.Research_Preferences.ANGER}%";
        monsterResearchDisgust.text = $"{monsterData.Research_Preferences.DISGUST}%";
        monsterResearchSad.text = $"{monsterData.Research_Preferences.SAD}%";
        monsterResearchHappy.text = $"{monsterData.Research_Preferences.HAPPY}%";
        monsterResearchSurprise.text = $"{monsterData.Research_Preferences.SURPRISE}%";
        
        infoCanvas.SetActive(true);
    }

    public void InfoCanvasOff()
    {
        // monsterImage.sprite = null;
        // monsterName.text = null;
        // code.text = null;
        // dangerLevel.text = null;
        // researchLog.text = null;
        infoCanvas.SetActive(false);
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
        employCanvas.SetActive(true);
    }
    public void Employ_Canvas_OFF()
    {
        employCanvas.SetActive(false);
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
            var UnaffiliatedEmployee = Instantiate(prefabUnaffiliatedEmployeeElement, prefabUnaffiliatedEmployeeList);
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
            prefabUnaffiliatedEmployee.Add(UnaffiliatedEmployee);
            UnaffiliatedEmployee.GetComponent<Button>().onClick.AddListener(() => ShowUnaffiliatedEmployeeData(UnaffiliatedEmployee));
        }
        unaffiliatedEmployeePanel.SetActive(true);
    }
    public void UnaffiliatedEmployee_Panel_Off()
    {
        foreach (Transform child in prefabUnaffiliatedEmployeeList)
        {
            Destroy(child.gameObject);
        }
        unaffiliatedEmployeePanel.SetActive(false);
    }

    public void EmployeeInfoSelectButton(GameObject GO)
    {
        if (GO.GetComponent<EmployeeInfo_Management>()._EmpName != string.Empty)
        {
            var UnaffiliatedEmployeeData = DataManager.Instance.EmployeeDataLoad(GO.GetComponent<EmployeeInfo_Management>()._EmpName);
            departmentNumber = GO.GetComponent<EmployeeInfo_Management>()._EmpDepart;
            AffiliatedEmployee_Panel_On(UnaffiliatedEmployeeData);
        }
        else
        {
            departmentNumber = GO.GetComponent<EmployeeInfo_Management>()._EmpDepart;
            UnaffiliatedEmployee_Panel_On();
        }
    }

    public void ShowUnaffiliatedEmployeeData(GameObject GO)
    {
        var UnEmp_Info = GO.GetComponent<UnaffiliatedEmployee_Info>();
        unaffiliatedEmployeeName.text = $"이름 : {UnEmp_Info._empName}";
        unaffiliatedEmployeeHp.text = $"체력 : {UnEmp_Info._empHp}";
        unaffiliatedEmployeeMp.text = $"정신력 : {UnEmp_Info._empMp}";
        unaffiliatedEmployeeDef.text = $"방어력 : {UnEmp_Info._empdef}";
        unaffiliatedEmployeePower.text = $"힘 : {UnEmp_Info._empPower}";
        unaffiliatedEmployeeIntelligence.text = $"지능 : {UnEmp_Info._empintelligence}";
        unaffiliatedEmployeeJustice.text = $"정의 : {UnEmp_Info._empJustice}";
        unaffiliatedEmployeeMovementSpeed.text = $"이동속도 : {UnEmp_Info._empMovementSpeed}";
        selectedName = UnEmp_Info._empName;
        UnaffiliatedEmployee_Info_On();
    }

    public void UnaffiliatedEmployee_Info_On()
    {
        unaffiliatedEmployeeInfo.SetActive(true);
    }
    public void UnaffiliatedEmployee_Info_Off()
    {
        selectedName = string.Empty;
        unaffiliatedEmployeeInfo.SetActive(false);
    }

    public void InsertUnaffiliatedEmp()
    {
        if (selectedName != string.Empty)
        {
            DataManager.Instance.MaindataSave(2, floorNumber, departmentNumber, selectedName);
            if (departmentNumber >= 0)
            {
                DepartmentInfo(departmentNumber);
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
        if (selectedName != string.Empty)
        {
            DataManager.Instance.MaindataSave(3, floorNumber, departmentNumber, selectedName);
            AffiliatedEmployee_Panel_Off();
            if (departmentNumber >= 0)
            {
                DepartmentInfo(departmentNumber); // 리셋 느낌
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
        affiliatedEmployeeName.text = $"이름 : {empdata.name}";
        affiliatedEmployeeHp.text = $"체력 : {empdata.hp}";
        affiliatedEmployeeMp.text = $"정신력 : {empdata.mp}";
        affiliatedEmployeeDef.text = $"방어력 : {empdata.def}";
        affiliatedEmployeePower.text = $"힘 : {empdata.power}";
        affiliatedEmployeeIntelligence.text = $"지능 : {empdata.intelligence}";
        affiliatedEmployeeJustice.text = $"정의 : {empdata.justice}";
        affiliatedEmployeeMovementSpeed.text = $"이동속도 : {empdata.movementSpeed}";
        selectedName = empdata.name;
        affiliatedEmployeePanel.SetActive(true);
    }
    public void AffiliatedEmployee_Panel_Off()
    {
        affiliatedEmployeePanel.SetActive(false);
    }
    public int CreateUnDuplicateRandom(int min, int max)
    {
        int number = 0;
        int currentNumber = 0;

        while (number < max)
        {
            currentNumber = UnityEngine.Random.Range(min, max);
            if (!duplicateList.Contains(currentNumber))
            {
                duplicateList.Add(currentNumber);
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
        for (int i = 0; i < monsterBuyGameObjects.Count; i++)
        {
            var rand_number = CreateUnDuplicateRandom(0, DataManager.Instance.MainDataLoad().All_Monster.Count);
            var MonsterInfo = monsterBuyGameObjects[i].GetComponent<MonsterBuy_Selected_Info>();
            var MonsterDataInfo = DataManager.Instance.MonsterDataLoad(DataManager.Instance.MainDataLoad().All_Monster[rand_number]);
            MonsterInfo._MonCode = MonsterDataInfo.profile.code;
            MonsterInfo._MonName = MonsterDataInfo.profile.MonsterName;
            MonsterInfo._MonInfo = MonsterDataInfo.profile.MonsterBuy_Info;

        }
        monsterBuyCanvas.SetActive(true);
    }
    public void MonsterBuy_Canvas_Btn_Off(int num)
    {
        string RandomMonster = DataManager.Instance.MainDataLoad().All_Monster[duplicateList[num]];
        DataManager.Instance.MaindataSave(0, floorNumber, departmentNumber, RandomMonster);
        Debug.Log(RandomMonster);
        duplicateList.Clear();
        FloorInfo();
        MonsterBuy_Canvas_Off();
        FloorOpeningBtn_Check();
    }
    public void MonsterBuy_Canvas_Off()
    {
        monsterBuyCanvas.SetActive(false);
    }

    public void AddFloor()
    {
        DataManager.Instance.AddFloor();
        FloorOpeningBtn_Check();
    }

}