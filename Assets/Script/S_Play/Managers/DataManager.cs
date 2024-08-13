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
public class MonsterData
{
    public string id;
    public int Max_research_Level;
    public ProfileData profile;
    public ResearchLogData Research_log;
    public ResearchList Research_Preferences;
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
}
[Serializable]
public class ResearchLogData
{
    public List<string> log;
}
//=-=-=-=-=-=-=-=-=-=-=-=-=-=

//직원 전용 데이터 셋
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
}

#endregion DataSet

[Serializable]
public class DataManager : Singleton_DonDes<DataManager>
{
    void Start()
    {
        //MainCompanyData();
        //CreateEmployeeData();
    }

    #region DataSave_and_Create

    /// <summary>
    /// Day, Money, ReserchPoint 처럼 기본적인 것들만 저장함
    /// </summary>
    public void MaindataSave()
    {
        string filePath = "Assets/Resources/GameData/MainData.json";

        string jsonText = File.ReadAllText(filePath); // 읽어오고

        MainCompanyData maindata = JsonUtility.FromJson<MainCompanyData>(jsonText); // class객체로 변환

        maindata.day = GameManager.Instance.nowday;
        maindata.Money = GameManager.Instance.nowMoney;
        maindata.ResearchPoint = GameManager.Instance.nowResearchPoint;

        string ChangeMainData = JsonUtility.ToJson(maindata, true); // class를 string으로 바꾸고

        File.WriteAllText(filePath, ChangeMainData); // string 값을 파일로 저장
    }
    /// <summary>
    /// type는 0일때 몬스터, 1, 2, 3일때 직원임 다만 1은 직원 생성이고 2는 직원 부서 배치임 3은 부서에서 부서 이동할 때 잠깐 부서가 없을 때//
    /// department는 소속될 회사 부서를 말함 //
    /// name은 말 그대로 이름임, maindata에 올라갈 이름으로 꼭 파일이름과 같아야 함
    /// </summary>
    public void MaindataSave(int type, int floor, int department, string name) // 종류, 층, 소속, 이름으로 저장
    {
        string filePath = "Assets/Resources/GameData/MainData.json";

        string jsonText = File.ReadAllText(filePath); // 읽어오고

        MainCompanyData maindata = JsonUtility.FromJson<MainCompanyData>(jsonText); // class객체로 변환
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
                }
                else
                {
                    maindata.Floor[maindata.Floor.Count - 1].Department[maindata.Floor[maindata.Floor.Count - 1].Department.Count - 1].MonsterList.Add(name);
                }
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

        File.WriteAllText(filePath, ChangeMainData); // string 값을 파일로 저장
    }
    public void AddFloor()
    {
        string filePath = "Assets/Resources/GameData/MainData.json";

        string jsonText = File.ReadAllText(filePath); // 읽어오고

        MainCompanyData maindata = JsonUtility.FromJson<MainCompanyData>(jsonText); // class객체로 변환

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

        File.WriteAllText(filePath, ChangeMainData); // string 값을 파일로 저장
    }
    public void MaindataSave_Employ(string name) // 종류, 소속, 이름으로 저장
    {
        string filePath = "Assets/Resources/GameData/MainData.json";

        string jsonText = File.ReadAllText(filePath); // 읽어오고

        MainCompanyData maindata = JsonUtility.FromJson<MainCompanyData>(jsonText); // class객체로 변환

        maindata.UnaffiliatedEmployee.Add(name);

        string ChangeMainData = JsonUtility.ToJson(maindata, true); // class를 string으로 바꾸고

        File.WriteAllText(filePath, ChangeMainData); // string 값을 파일로 저장
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

        return newEmployeeData;
        // string filename = newEmployeeData.name;
        // string filePath = $"Assets/Resources/GameData/Employee/{filename}.json";
        // string Employeejson = JsonUtility.ToJson(newEmployeeData, true);
        // File.WriteAllText(filePath, Employeejson);

    }



    #endregion DataSave_and_Create

    #region DataLoad

    public MainCompanyData MainDataLoad()
    {
        string filePath = "Assets/Resources/GameData/MainData.json";

        string jsonText = File.ReadAllText(filePath);
        MainCompanyData MainData = JsonUtility.FromJson<MainCompanyData>(jsonText);
        //Debug.Log(jsonText);

        return MainData;
    }

    public MonsterData MonsterDataLoad(string filename)
    {
        string filePath = $"Assets/Resources/GameData/Monster/{filename}.json";

        // JSON 파일 읽어오기
        string jsonText = File.ReadAllText(filePath);

        // JSON 데이터를 객체로 변환
        MonsterData monsterData = JsonUtility.FromJson<MonsterData>(jsonText);

        //Debug.Log(jsonText);

        // 리턴시켜서 다른곳에서도 사용이 가능하게 바꿈
        return monsterData;
    }

    public EmployeeData EmployeeDataLoad(string filename)
    {
        string filePath = $"Assets/Resources/GameData/Employee/{filename}.json";

        string jsonText = File.ReadAllText(filePath);

        EmployeeData employeeData = JsonUtility.FromJson<EmployeeData>(jsonText);

        return employeeData;
    }
    #endregion DataLoad
}
