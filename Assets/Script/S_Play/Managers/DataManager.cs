using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using UnityEngine.UI;


#region DataSet
[Serializable]
public class MonsterListClass
{
    public List<string> MonsterList;
    public List<string> EmployeeList;
}
[Serializable]
public class MainCompanyData
{
    public int day;
    public int Money;
    public int ResearchPoint;
    public List<MonsterListClass> Department;
}

// 몬스터 데이터 셋

[Serializable]
public class MonsterData
{
    public string id;
    public int Max_research_Level;
    public ProfileData profile;
    public ResearchLogData Research_log;
    public int OpenLevel;
}
[Serializable]
public class ProfileData
{
    public string code;
    public string imagePATH;
    public string MonsterName;
    public int riskLevel;
}
[Serializable]
public class ResearchLogData
{
    public List<string> log;
}
//=-=-=-=-=-=-=-=-=-=-=-=-=-=

//직원 전용 데이터 셋(미완)
public class EmployeeData
{
    public string name;
    public int department;

    public int hp;
    public int mp;
    public int def;

    public int power;
    public int intelligence;
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

    public void MaindataSave()
    {
        string filePath = "Assets/Resources/GameData/MainData.json";

        string jsonText = File.ReadAllText(filePath);

        MainCompanyData maindata = JsonUtility.FromJson<MainCompanyData>(jsonText);

        maindata.day += 1;
        maindata.Money = GameManager.Instance.nowMoney;
        maindata.ResearchPoint = GameManager.Instance.nowResearchPoint;

        // maindata.Monsters[0].MonsterList = GameManager.Instance.nowMonsterList; // 수정 필요
        // maindata.EmployeeList = GameManager.Instance.nowEmployeeList;

        string ChangeMainData = JsonUtility.ToJson(maindata, true);

        File.WriteAllText(filePath, ChangeMainData);
    }

    public void CreateEmployeeData(int number)
    {

        EmployeeData newEmployeeData = new EmployeeData();

        switch (number)
        {
            case 0: // 기본 능력치 직원
                newEmployeeData.name = $"계약직 {UnityEngine.Random.Range(0, 100)}호";
                newEmployeeData.department = UnityEngine.Random.Range(10, 100);

                newEmployeeData.hp = UnityEngine.Random.Range(10, 80);
                newEmployeeData.mp = UnityEngine.Random.Range(10, 80);
                newEmployeeData.def = UnityEngine.Random.Range(10, 80);

                newEmployeeData.power = UnityEngine.Random.Range(0, 80);
                newEmployeeData.intelligence = UnityEngine.Random.Range(0, 80);
                newEmployeeData.movementSpeed = UnityEngine.Random.Range(50, 80);
                break;

            case 1: // 관리 부서 직원
                newEmployeeData.name = $"계약직 {UnityEngine.Random.Range(0, 100)}호";
                newEmployeeData.department = UnityEngine.Random.Range(0, 100);

                newEmployeeData.hp = UnityEngine.Random.Range(60, 80);
                newEmployeeData.mp = UnityEngine.Random.Range(60, 80);
                newEmployeeData.def = UnityEngine.Random.Range(40, 80);

                newEmployeeData.power = UnityEngine.Random.Range(70, 80);
                newEmployeeData.intelligence = UnityEngine.Random.Range(20, 50);
                newEmployeeData.movementSpeed = 50;
                break;

            case 2: // 감사 부서 직원
                newEmployeeData.name = $"계약직 {UnityEngine.Random.Range(0, 100)}호";
                newEmployeeData.department = UnityEngine.Random.Range(0, 100);

                newEmployeeData.hp = UnityEngine.Random.Range(30, 50);
                newEmployeeData.mp = UnityEngine.Random.Range(60, 80);
                newEmployeeData.def = UnityEngine.Random.Range(30, 50);

                newEmployeeData.power = UnityEngine.Random.Range(30, 50);
                newEmployeeData.intelligence = UnityEngine.Random.Range(60, 80);
                newEmployeeData.movementSpeed = 50;
                break;

            case 3: // 연구 부서 직원
                newEmployeeData.name = $"계약직 {UnityEngine.Random.Range(0, 100)}호";
                newEmployeeData.department = UnityEngine.Random.Range(0, 100);

                newEmployeeData.hp = UnityEngine.Random.Range(30, 50);
                newEmployeeData.mp = UnityEngine.Random.Range(60, 80);
                newEmployeeData.def = UnityEngine.Random.Range(30, 50);

                newEmployeeData.power = UnityEngine.Random.Range(20, 50);
                newEmployeeData.intelligence = UnityEngine.Random.Range(60, 80);
                newEmployeeData.movementSpeed = 50;
                break;

            case 4: // 회계 부서 직원
                newEmployeeData.name = $"계약직 {UnityEngine.Random.Range(0, 100)}호";
                newEmployeeData.department = UnityEngine.Random.Range(0, 100);

                newEmployeeData.hp = UnityEngine.Random.Range(30, 50);
                newEmployeeData.mp = UnityEngine.Random.Range(30, 50);
                newEmployeeData.def = UnityEngine.Random.Range(30, 50);

                newEmployeeData.power = UnityEngine.Random.Range(20, 50);
                newEmployeeData.intelligence = UnityEngine.Random.Range(60, 80);
                newEmployeeData.movementSpeed = 50;
                break;
        }

        string filename = newEmployeeData.name;
        string filePath = $"Assets/Resources/GameData/Employee/{filename}.json";
        string Employeejson = JsonUtility.ToJson(newEmployeeData, true);
        File.WriteAllText(filePath, Employeejson);

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
        string filePath = "Assets/Resources/GameData/Monster/" + filename + ".json";

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
        string filePath = "Assets/Resources/GameData/Employee/" + filename + ".json";

        string jsonText = File.ReadAllText(filePath);

        EmployeeData employeeData = JsonUtility.FromJson<EmployeeData>(jsonText);

        return employeeData;
    }
    #endregion DataLoad
}
