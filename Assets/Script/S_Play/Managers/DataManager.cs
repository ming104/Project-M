using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using UnityEngine.UI;


#region DataSet
[Serializable]
public class MainCompanyData
{
    public int day;
    public int Money;
    public int ResearchPoint;
    public List<string> MonsterList;

    public List<string> EmployeeList;
}

[Serializable]
public class MonsterData
{
    public string id;
    public int Max_research_Level;
    public ProfileData profile;
    public ResearchLogData Research_log;
    public string OpenLevel;
}
[Serializable]
public class ProfileData
{
    public string code;
    public string imagePATH;
    public string MonsterName;
    public int riskLevel;
}

public class ResearchLogData
{
    public List<string> log;
}
#endregion DataSet

[Serializable]
public class DataManager : Singleton<DataManager>
{
    void Start()
    {
        //MainCompanyData();
    }

    #region DataSave_and_Create

    public void MaindataSave()
    {
        string filePath = "Assets/Resources/GameData/MainData.json";

        string jsonText = File.ReadAllText(filePath);

        MainCompanyData maindata = JsonUtility.FromJson<MainCompanyData>(jsonText);

        maindata.day = GameManager.Instance.nowday;
        maindata.Money = GameManager.Instance.nowMoney;
        maindata.ResearchPoint = GameManager.Instance.nowResearchPoint;

        maindata.MonsterList = GameManager.Instance.nowMonsterList;
        maindata.EmployeeList = GameManager.Instance.nowEmployeeList;

        string ChangeMainData = JsonUtility.ToJson(maindata, true);

        File.WriteAllText(filePath, ChangeMainData);
    }


    #endregion DataSave_and_Create

    #region DataLoad

    public MainCompanyData MainDataLoad()
    {
        string filePath = "Assets/Resources/GameData/MainData.json";

        string jsonText = File.ReadAllText(filePath);
        MainCompanyData MainData = JsonUtility.FromJson<MainCompanyData>(jsonText);

        return MainData;
    }

    public MonsterData MonsterDataLoad(string filename)
    {
        string filePath = "Assets/Resources/GameData/Monster/" + filename + ".json";

        // JSON 파일 읽어오기
        string jsonText = File.ReadAllText(filePath);

        // JSON 데이터를 객체로 변환
        MonsterData monsterData = JsonUtility.FromJson<MonsterData>(jsonText);

        // 리턴시켜서 다른곳에서도 사용이 가능하게 바꿈
        return monsterData;
    }
    #endregion DataLoad
}
