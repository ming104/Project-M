using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using UnityEngine.UI;


#region DataSet
[Serializable]
public class MonsterEmpListClass
{
    public List<string> MonsterList;
    public List<string> EmployeeList;

}
[Serializable]
public class Floors
{
    public List<string> AuditDepartment;
    public List<string> AccountingDepartment;
    public List<MonsterEmpListClass> Department;
}

[Serializable]
public class MainCompanyData
{
    public int companyStability;
    public int day;
    public int Money;
    public int ResearchPoint;
    public List<Floors> Floor;
    public List<string> UnaffiliatedEmployee;
    public List<string> All_Monster;
}
[Serializable]
public class ResearchList
{
    public int FEAR; //0
    public int ANGER; //1
    public int DISGUST; //2
    public int SAD; //3
    public int HAPPY; //4
    public int SURPRISE; //5
}

// 몬스터 데이터 셋
[Serializable]
public class MonsterEquipment
{
    public string imagePATH;
    public string EquipName;
    public string equipEffect;
    public string equipSpecialEffect;
    public int type;
    public int maximumCount;
    public int currentCount;
    public int buyMoney;
    public int buyRP;
}

[Serializable]
public class MonsterData
{
    public string appearanceImagePath;
    public int Max_research_Level;
    public ProfileData profile;
    public ResearchLogData Research_log;
    public ResearchList Research_Preferences;
    public MonsterEquipment MonEquipment;
    public int OpenLevel;
}
[Serializable]
public class ProfileData
{
    public string code;
    public string imagePATH;
    public string MonsterName;
    public string MonsterBuy_Info;
    public int riskLevel;
    public int escapeCount;
    public string isEscape;
    public float researchMentalDamage;
}
[Serializable]
public class ResearchLogData
{
    public List<string> log;
}
//=-=-=-=-=-=-=-=-=-=-=-=-=-=

//직원 전용 데이터 셋
[Serializable]
public class Equipment
{
    public string weapon;
    public string armor;
}
[Serializable]
public class EmployeeData
{
    public string name;

    public int hp;
    public int mp;
    public int def;

    public int power;
    public int intelligence;
    public int justice;
    public int movementSpeed;
    public Equipment equipment;
}

// 무기 관련 데이터셋
[Serializable]
public class EquipmentType
{
    public List<string> weapon;
    public List<string> armor;
}
[Serializable]
public class EquipmentData
{
    public EquipmentType mountedEquipment;
    public EquipmentType unmountedEquipment;
}
#endregion DataSet

[Serializable]
public class DataManager : Singleton_DonDes<DataManager>
{
    void Start()
    {
        //MainCompanyData();
        //CreateEmployeeData();
        DataCreate();
    }
    
    void DataCreate()
    {
        if(!File.Exists(Path.Combine(Application.persistentDataPath, "MainData.json")))
        {
            MainDataReset();
        }
    }

    #region DataSave_and_Create

    public void MainDataReset()
    {
        //string mainDataFilePath = "Assets/Resources/GameData/MainData.json";
        string mainDataFilePath = Path.Combine(Application.persistentDataPath, "MainData.json");
         string mainjsonText;
        if (!File.Exists(mainDataFilePath))
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("GameData/MainData");
            mainjsonText = jsonFile.text; // 읽어오고
        }
        else
        {
             mainjsonText = File.ReadAllText(mainDataFilePath);
        }

        MainCompanyData maindata = JsonUtility.FromJson<MainCompanyData>(mainjsonText); // class객체로 변환
        
        maindata.companyStability = 0;
        maindata.day = 1;
        maindata.Money = 5000;
        maindata.ResearchPoint = 10;
        foreach (var floor in maindata.Floor)
        {
            if (floor.AuditDepartment != null)
                floor.AuditDepartment.Clear(); // AuditDepartment 내부 데이터 삭제

            if (floor.AccountingDepartment != null)
                floor.AccountingDepartment.Clear(); // AccountingDepartment 내부 데이터 삭제

            if (floor.Department != null)
            {
                floor.Department.Clear();
                MonsterEmpListClass monsterListAdd = new MonsterEmpListClass
                {
                    MonsterList = new List<string> { "Dummy" },
                    EmployeeList = new List<string>()
                };
                floor.Department.Add(monsterListAdd);
            }
        }
        
        maindata.UnaffiliatedEmployee.Clear(); // 리스트 초기화
    
        string ChangeMainData = JsonUtility.ToJson(maindata, true); // class를 string으로 바꾸고

        File.WriteAllText(mainDataFilePath, ChangeMainData); // string 값을 파일로 저장
        // 아래는 장착 데이터
        
        //string EquipDataFilePath = "Assets/Resources/GameData/EquipmentData.json";
        string EquipDataFilePath = Path.Combine(Application.persistentDataPath, "EquipmentData.json");
         string EquipjsonText;
        if (!File.Exists(EquipDataFilePath))
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("GameData/EquipmentData");
            EquipjsonText = jsonFile.text; // 읽어오고
        }
        else
        {
             EquipjsonText = File.ReadAllText(EquipDataFilePath);
        }
        
        //string EquipjsonText = File.ReadAllText(EquipDataFilePath); // 읽어오고

        EquipmentData equipmentData = JsonUtility.FromJson<EquipmentData>(EquipjsonText); // class객체로 변환
        
        equipmentData.unmountedEquipment.weapon.Clear();
        equipmentData.unmountedEquipment.armor.Clear();
        equipmentData.mountedEquipment.weapon.Clear();
        equipmentData.mountedEquipment.armor.Clear();

        string ChangeEquipData = JsonUtility.ToJson(equipmentData, true); // class를 string으로 바꾸고

        File.WriteAllText(EquipDataFilePath, ChangeEquipData); // string 값을 파일로 저장
        // 아래는 직원 파일 삭제

        string employeeDataFilePath = Path.Combine(Application.persistentDataPath, "Employee");
        DirectoryInfo di = new DirectoryInfo(employeeDataFilePath);
        Debug.Log(employeeDataFilePath);
        if(!Directory.Exists(employeeDataFilePath))
        {
            //string 
            di.Create();
            employeeDataFilePath = "Assets/Resources/GameData/Employee";
        }
       

        if (Directory.Exists(employeeDataFilePath))
        {
            string[] files = Directory.GetFiles(employeeDataFilePath);
            foreach (var filePath in files)
            {
                try
                {
                  File.Delete(filePath);
                  Debug.Log($"파일 삭제 성공: {filePath}");
                }
                catch (Exception ex)
                {
                    Debug.Log($"파일 삭제 실패: {filePath} - 오류 메시지: {ex.Message}");
                }
            }
        }
        else
        {
            Console.WriteLine($"디렉토리가 존재하지 않습니다: {employeeDataFilePath}");
        } 
    }
    
    

    /// <summary>
    /// Day, Money, ReserchPoint 처럼 기본적인 것들만 저장함
    /// </summary>
    public void MaindataSave()
    {
        string mainDataFilePath = Path.Combine(Application.persistentDataPath, "MainData.json");
        string mainjsonText = File.ReadAllText(mainDataFilePath);
        
        //string filePath = "Assets/Resources/GameData/MainData.json";

        //string jsonText = File.ReadAllText(filePath); // 읽어오고

        MainCompanyData maindata = JsonUtility.FromJson<MainCompanyData>(mainjsonText); // class객체로 변환
        
        maindata.day = GameManager.Instance.nowday + 1;
        maindata.Money = GameManager.Instance.nowMoney;
        maindata.ResearchPoint = GameManager.Instance.nowResearchPoint;

        string ChangeMainData = JsonUtility.ToJson(maindata, true); // class를 string으로 바꾸고

        File.WriteAllText(mainDataFilePath, ChangeMainData); // string 값을 파일로 저장
    }
    public void MaindataSaveManagement()
    {
        string mainDataFilePath = Path.Combine(Application.persistentDataPath, "MainData.json");
        string mainjsonText = File.ReadAllText(mainDataFilePath);
        
        //string filePath = "Assets/Resources/GameData/MainData.json";

        //string jsonText = File.ReadAllText(filePath); // 읽어오고

        MainCompanyData maindata = JsonUtility.FromJson<MainCompanyData>(mainjsonText); // class객체로 변환

        maindata.Money = ManagementManager.Instance.currentMoney;
        maindata.ResearchPoint = ManagementManager.Instance.currentRP;

        string ChangeMainData = JsonUtility.ToJson(maindata, true); // class를 string으로 바꾸고

        File.WriteAllText(mainDataFilePath, ChangeMainData); // string 값을 파일로 저장
    }
    /// <summary>
    /// type는 0일때 몬스터, 1, 2, 3일때 직원임 다만 1은 직원 생성이고 2는 직원 부서 배치임 3은 부서에서 부서 이동할 때 잠깐 부서가 없을 때//
    /// department는 소속될 회사 부서를 말함 //
    /// name은 말 그대로 이름임, maindata에 올라갈 이름으로 꼭 파일이름과 같아야 함
    /// </summary>
    public void MaindataSave(int type, int floor, int department, string name) // 종류, 층, 소속, 이름으로 저장
    {
        string mainDataFilePath = Path.Combine(Application.persistentDataPath, "MainData.json");
        string mainjsonText = File.ReadAllText(mainDataFilePath);
        
        
        //string filePath = "Assets/Resources/GameData/MainData.json";

        //string jsonText = File.ReadAllText(filePath); // 읽어오고

        MainCompanyData maindata = JsonUtility.FromJson<MainCompanyData>(mainjsonText); // class객체로 변환
        switch (type)
        {
            case 0: // 몬스터
                if (maindata.Floor[maindata.Floor.Count - 1].Department[maindata.Floor[maindata.Floor.Count - 1].Department.Count - 1].MonsterList.Count == 4)
                {
                    MonsterEmpListClass monsterListAdd = new MonsterEmpListClass
                    {
                        MonsterList = new List<string> { name },
                        EmployeeList = new List<string>()
                    };
                    maindata.Floor[maindata.Floor.Count - 1].Department.Add(monsterListAdd);
                    ManagementManager.Instance.currentMoney -= 1000 + (maindata.Floor.Count * 400);
                }
                else
                {
                    maindata.Floor[maindata.Floor.Count - 1].Department[maindata.Floor[maindata.Floor.Count - 1].Department.Count - 1].MonsterList.Add(name);
                    ManagementManager.Instance.currentMoney -= 1000 + (maindata.Floor.Count * 400);
                }

                maindata.Money = ManagementManager.Instance.currentMoney;
                break;
            case 1: // 직원
                maindata.UnaffiliatedEmployee.Add(name);
                break;
            case 2: // 직원 부서 이동
                switch (department)
                {
                    case -2:
                        maindata.UnaffiliatedEmployee.Remove(name);
                        maindata.Floor[floor].AuditDepartment.Add(name);
                        break;
                    case -1:
                        maindata.UnaffiliatedEmployee.Remove(name);
                        maindata.Floor[floor].AccountingDepartment.Add(name);
                        break;
                    default:
                        maindata.UnaffiliatedEmployee.Remove(name);
                        maindata.Floor[floor].Department[department].EmployeeList.Add(name);
                        break;
                }
                break;
            case 3: // 직원 부서 이동할 때 뺄 때
                switch (department)
                {
                    case -2:
                        maindata.Floor[floor].AuditDepartment.Remove(name);
                        maindata.UnaffiliatedEmployee.Add(name);
                        break;
                    case -1:
                        maindata.Floor[floor].AccountingDepartment.Remove(name);
                        maindata.UnaffiliatedEmployee.Add(name);
                        break;
                    default:
                        maindata.Floor[floor].Department[department].EmployeeList.Remove(name);
                        maindata.UnaffiliatedEmployee.Add(name);
                        break;
                }
                break;
        }

        string ChangeMainData = JsonUtility.ToJson(maindata, true); // class를 string으로 바꾸고

        File.WriteAllText(mainDataFilePath, ChangeMainData); // string 값을 파일로 저장
    }
    public void AddFloor()
    {
        
        string mainDataFilePath = Path.Combine(Application.persistentDataPath, "MainData.json");
        string mainjsonText = File.ReadAllText(mainDataFilePath);
        
        //string filePath = "Assets/Resources/GameData/MainData.json";

        //string jsonText = File.ReadAllText(filePath); // 읽어오고

        MainCompanyData maindata = JsonUtility.FromJson<MainCompanyData>(mainjsonText); // class객체로 변환

        MonsterEmpListClass floorDepart = new MonsterEmpListClass
        {
            MonsterList = new List<string>(),
            EmployeeList = new List<string>()
        };

        Floors newfloors = new Floors
        {
            AuditDepartment = new List<string>(),
            AccountingDepartment = new List<string>(),
            Department = new List<MonsterEmpListClass>
            {
                floorDepart
            }

        };
        maindata.Floor.Add(newfloors);
        string ChangeMainData = JsonUtility.ToJson(maindata, true); // class를 string으로 바꾸고

        File.WriteAllText(mainDataFilePath, ChangeMainData); // string 값을 파일로 저장
    }
    public void MaindataSave_Employ(string name) // 종류, 소속, 이름으로 저장
    {
        string mainDataFilePath = Path.Combine(Application.persistentDataPath, "MainData.json");
        string mainjsonText = File.ReadAllText(mainDataFilePath);
        
        //string filePath = "Assets/Resources/GameData/MainData.json";

        //string jsonText = File.ReadAllText(filePath); // 읽어오고

        MainCompanyData maindata = JsonUtility.FromJson<MainCompanyData>(mainjsonText); // class객체로 변환

        maindata.UnaffiliatedEmployee.Add(name);
        ManagementManager.Instance.currentMoney -= (maindata.Floor.Count) * 100;

        string ChangeMainData = JsonUtility.ToJson(maindata, true); // class를 string으로 바꾸고

        File.WriteAllText(mainDataFilePath, ChangeMainData); // string 값을 파일로 저장
    }

    /// <summary>
    /// 직원의 데이터를 만드는 곳 저장은 되지 않음, 기본으로 이름, 부서, hp, mp, def, power, intelligence, movementSpeed가 정해지게 됨
    /// 각각 부서 별로 생성되는 직원이 다름 -> 0일 경우 기본 직원이 나오게 됨, 1일 경우 관리(연구)부서 직원, 2일 경우 감사부서 직원 3일 경우 회계부서 직원이 생성됨
    /// </summary>
    public EmployeeData CreateEmployeeData(int number) // 직원 생성
    {
        EmployeeData newEmployeeData = new EmployeeData();

        switch (number)
        {
            case 0: // 기본 능력치 직원
                newEmployeeData.name = $"계약직 {UnityEngine.Random.Range(0, 100)}호";

                newEmployeeData.hp = UnityEngine.Random.Range(30, 50);
                newEmployeeData.mp = UnityEngine.Random.Range(30, 50);
                newEmployeeData.def = UnityEngine.Random.Range(30, 50);

                newEmployeeData.power = UnityEngine.Random.Range(30, 50);
                newEmployeeData.intelligence = UnityEngine.Random.Range(30, 50);
                newEmployeeData.justice = UnityEngine.Random.Range(30, 50);
                newEmployeeData.movementSpeed = 50;
                break;

            case 1: // 관리(연구) 부서 직원
                newEmployeeData.name = $"계약직 {UnityEngine.Random.Range(0, 100)}호";

                newEmployeeData.hp = UnityEngine.Random.Range(30, 80);
                newEmployeeData.mp = UnityEngine.Random.Range(30, 80);
                newEmployeeData.def = UnityEngine.Random.Range(30, 80);

                newEmployeeData.power = UnityEngine.Random.Range(50, 80);
                newEmployeeData.intelligence = UnityEngine.Random.Range(10, 30);
                newEmployeeData.justice = UnityEngine.Random.Range(10, 40);
                newEmployeeData.movementSpeed = 50;
                break;

            case 2: // 감사 부서 직원
                newEmployeeData.name = $"계약직 {UnityEngine.Random.Range(0, 100)}호";

                newEmployeeData.hp = UnityEngine.Random.Range(30, 50);
                newEmployeeData.mp = UnityEngine.Random.Range(30, 50);
                newEmployeeData.def = UnityEngine.Random.Range(30, 50);

                newEmployeeData.power = UnityEngine.Random.Range(30, 50);
                newEmployeeData.intelligence = UnityEngine.Random.Range(30, 50);
                newEmployeeData.justice = UnityEngine.Random.Range(50, 80);
                newEmployeeData.movementSpeed = 50;
                break;

            case 3: // 회계 부서 직원
                newEmployeeData.name = $"계약직 {UnityEngine.Random.Range(0, 100)}호";

                newEmployeeData.hp = UnityEngine.Random.Range(30, 50);
                newEmployeeData.mp = UnityEngine.Random.Range(30, 50);
                newEmployeeData.def = UnityEngine.Random.Range(30, 50);

                newEmployeeData.power = UnityEngine.Random.Range(30, 50);
                newEmployeeData.intelligence = UnityEngine.Random.Range(50, 80);
                newEmployeeData.justice = UnityEngine.Random.Range(30, 50);
                newEmployeeData.movementSpeed = 50;
                break;
        }

        Equipment equipment = new Equipment();
        equipment.weapon = "삼단봉";
        equipment.armor = "격리복";
        newEmployeeData.equipment = equipment;

        return newEmployeeData;
        // string filename = newEmployeeData.name;
        // string filePath = $"Assets/Resources/GameData/Employee/{filename}.json";
        // string Employeejson = JsonUtility.ToJson(newEmployeeData, true);
        // File.WriteAllText(filePath, Employeejson);

    }
    
    public void MountedEquipmentCreate(string equipName, int type) 
    {
        string EquipDataFilePath = Path.Combine(Application.persistentDataPath, "EquipmentData.json");
        string EquipjsonText = File.ReadAllText(EquipDataFilePath);
        
        //string filePath = "Assets/Resources/GameData/EquipmentData.json";

        // JSON 파일 읽기
        //string jsonText = File.ReadAllText(filePath);

        // JSON 데이터를 EquipmentData 객체로 변환
        EquipmentData equipmentData = JsonUtility.FromJson<EquipmentData>(EquipjsonText);

        // 타입에 따라 무기 또는 방어구 추가
        switch (type)
        {
            case 0:
                equipmentData.mountedEquipment.weapon.Add(equipName);
                break;
            case 1:
                equipmentData.mountedEquipment.armor.Add(equipName);
                break;
        }

        // 변경된 데이터를 JSON 문자열로 변환
        string changeEquipmentData = JsonUtility.ToJson(equipmentData, true);

        // 수정된 JSON 데이터를 파일에 다시 저장
        File.WriteAllText(EquipDataFilePath, changeEquipmentData);
    }
    
    public void EquipmentEquip(string empName, string equipName, int type) // 주인없는놈 찾아줌 -> 장챡
    {
        string EquipmentFilePath = Path.Combine(Application.persistentDataPath, "EquipmentData.json");
        string EquipmentjsonText = File.ReadAllText(EquipmentFilePath);
        
        
        string employeeFilepath = Path.Combine(Application.persistentDataPath, $"Employee/{empName}.json");
        string employeejsonText;
        if (!File.Exists(employeeFilepath))
        {
            TextAsset jsonFile = Resources.Load<TextAsset>($"GameData/Employee/{empName}");
            employeejsonText = jsonFile.text; // 읽어오고
        }
        else
        {
            employeejsonText = File.ReadAllText(employeeFilepath);
        }
        
        //string employeeFilepath = $"Assets/Resources/GameData/Employee/{empName}.json";
        //string filePath = "Assets/Resources/GameData/EquipmentData.json";

        //string jsonText = File.ReadAllText(mainDataFilePath); // 읽어오고
        var empdata = EmployeeDataLoad(empName);

        EquipmentData equipmentData = JsonUtility.FromJson<EquipmentData>(EquipmentjsonText); // class객체로 변환
        switch (type)
        {
            case 0:
                equipmentData.mountedEquipment.weapon.Remove(empdata.equipment.weapon);
                equipmentData.unmountedEquipment.weapon.Add(empdata.equipment.weapon);
                
                equipmentData.unmountedEquipment.weapon.Remove(equipName);
                equipmentData.mountedEquipment.weapon.Add(equipName);
                
                empdata.equipment.weapon = equipName;
                break;
            
            case 1:
                equipmentData.mountedEquipment.armor.Remove(empdata.equipment.armor);
                equipmentData.unmountedEquipment.armor.Add(empdata.equipment.armor);
                
                equipmentData.unmountedEquipment.armor.Remove(equipName);
                equipmentData.mountedEquipment.armor.Add(equipName);
                
                empdata.equipment.armor = equipName;
                break;
        }
        string changeEmployeeData = JsonUtility.ToJson(empdata, true);
        string changeEquipmentData = JsonUtility.ToJson(equipmentData, true); // class를 string으로 바꾸고
        
        File.WriteAllText(employeeFilepath, changeEmployeeData);
        File.WriteAllText(EquipmentFilePath, changeEquipmentData); // string 값을 파일로 저장
    }

    public void EquipmentCreate(string equipName, int type) 
    {
        
        string EquipDataFilePath = Path.Combine(Application.persistentDataPath, "EquipmentData.json");
        string EquipjsonText = File.ReadAllText(EquipDataFilePath);
        
        
        //string filePath = "Assets/Resources/GameData/EquipmentData.json";

        // JSON 파일 읽기
        //string jsonText = File.ReadAllText(filePath);

        // JSON 데이터를 EquipmentData 객체로 변환
        EquipmentData equipmentData = JsonUtility.FromJson<EquipmentData>(EquipjsonText);

        // 타입에 따라 무기 또는 방어구 추가
        switch (type)
        {
            case 0:
                equipmentData.unmountedEquipment.weapon.Add(equipName);
                break;
            case 1:
                equipmentData.unmountedEquipment.armor.Add(equipName);
                break;
        }

        // 변경된 데이터를 JSON 문자열로 변환
        string changeEquipmentData = JsonUtility.ToJson(equipmentData, true);

        // 수정된 JSON 데이터를 파일에 다시 저장
        File.WriteAllText(EquipDataFilePath, changeEquipmentData);
    }

    public void EmployeeEquipChange(string empName, string equip, int type) // 주인 이름보고 끼워줌
    {
        string employeeFilepath = Path.Combine(Application.persistentDataPath, $"Employee/{empName}.json");
        string employeejsonText = File.ReadAllText(employeeFilepath);
        
        
        var equipmentData = EmployeeDataLoad(empName);
        //string filePath = $"Assets/Resources/GameData/Employee/{empName}.json";
        switch (type)
        {
            case 0:
                //EquipmentEquip(empName, 0);
                equipmentData.equipment.weapon = equip;
                break;
            case 1 :
                //EquipmentUnEquip(equip, 1);
                equipmentData.equipment.armor = equip;
                break;
        }
        string changeEquipmentData = JsonUtility.ToJson(equipmentData, true);
        File.WriteAllText(employeeFilepath, changeEquipmentData);
    }


    #endregion DataSave_and_Create

    #region DataLoad

    public MainCompanyData MainDataLoad()
    {
        string mainDataFilePath = Path.Combine(Application.persistentDataPath, "MainData.json");
        string mainjsonText = File.ReadAllText(mainDataFilePath);
        
        
        //string filePath = "Assets/Resources/GameData/MainData.json";

        //string jsonText = File.ReadAllText(filePath);
        MainCompanyData MainData = JsonUtility.FromJson<MainCompanyData>(mainjsonText);
        //Debug.Log(jsonText);

        return MainData;
    }

    public MonsterData MonsterDataLoad(string filename)
    {
        //string filePath = $"Assets/Resources/GameData/Monster/{filename}.json";
        TextAsset filePath = Resources.Load<TextAsset>($"GameData/Monster/{filename}");
        Debug.Log(filePath);
        // JSON 파일 읽어오기
        //string jsonText = File.ReadAllText(filePath.text);

        // JSON 데이터를 객체로 변환
        MonsterData monsterData = JsonUtility.FromJson<MonsterData>(filePath.text);

        //Debug.Log(jsonText);

        // 리턴시켜서 다른곳에서도 사용이 가능하게 바꿈
        return monsterData;
    }

    public EmployeeData EmployeeDataLoad(string filename)
    {
        string employeeFilepath = Path.Combine(Application.persistentDataPath, $"Employee/{filename}.json");
        string employeejsonText = File.ReadAllText(employeeFilepath);
        
        
        //string filePath = $"Assets/Resources/GameData/Employee/{filename}.json";

        //string jsonText = File.ReadAllText(filePath);

        EmployeeData employeeData = JsonUtility.FromJson<EmployeeData>(employeejsonText);

        return employeeData;
    }

    public int EquipmentCountLoad(string equipmentName, int type)
    {
        string EquipDataFilePath = Path.Combine(Application.persistentDataPath, "EquipmentData.json");
        string EquipjsonText = File.ReadAllText(EquipDataFilePath);
        
        
       // string filePath = "Assets/Resources/GameData/EquipmentData.json";

        //string jsonText = File.ReadAllText(filePath);

        int equipmentCount = 0;

        EquipmentData equipmentData = JsonUtility.FromJson<EquipmentData>(EquipjsonText);
        switch (type)
        {
            case 0:
                foreach (var weapons in equipmentData.unmountedEquipment.weapon)
                {
                    if (weapons == equipmentName)
                    {
                        equipmentCount++;
                    }
                }
                foreach (var armors in equipmentData.mountedEquipment.weapon)
                {
                    if (armors == equipmentName)
                    {
                        equipmentCount++;
                    }
                }

                break;
            
            case 1:
                foreach (var weapons in equipmentData.unmountedEquipment.armor)
                {
                    if (weapons == equipmentName)
                    {
                        equipmentCount++;
                    }
                }
                foreach (var armors in equipmentData.mountedEquipment.armor)
                {
                    if (armors == equipmentName)
                    {
                        equipmentCount++;
                    }
                }

                break;
            
        }
        Debug.Log(equipmentCount);
        return equipmentCount;
    }

    public EquipmentData EquipmentDataLoad()
    {
        string EquipDataFilePath = Path.Combine(Application.persistentDataPath, "EquipmentData.json");
        string EquipjsonText = File.ReadAllText(EquipDataFilePath);
        
        
        //string filePath = "Assets/Resources/GameData/EquipmentData.json";

        //string jsonText = File.ReadAllText(filePath);

        EquipmentData equipmentData = JsonUtility.FromJson<EquipmentData>(EquipjsonText);
        
        return equipmentData;
    }
    #endregion DataLoad
}
