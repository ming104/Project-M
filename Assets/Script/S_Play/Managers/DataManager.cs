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
    public string[] MonsterList;

    public string[] EmployeeList;
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

[Serializable]
public class ResearchLogData
{
    public string[] log;
}
#endregion DataSet

public class DataManager : Singleton<DataManager>
{
    void Start()
    {
        //MainCompanyData();
    }

    public MainCompanyData MainData()
    {
        string filePath = "Assets/Resources/GameData/MainData.json";

        string jsonText = File.ReadAllText(filePath);
        MainCompanyData MainData = JsonUtility.FromJson<MainCompanyData>(jsonText);
        //Debug.Log("JSON Text: " + jsonText);
        // foreach (string MonList in MainData.MonsterList)
        // {
        //     return MonList;
        // }
        return MainData;
    }

    public MonsterData MonsterDataLoad(string filename)
    {
        string filePath = "Assets/Resources/GameData/Monster/" + filename + ".json";

        // JSON 파일 읽어오기
        string jsonText = File.ReadAllText(filePath);

        // 읽어온 JSON 텍스트 확인
        //Debug.Log("JSON Text: " + jsonText);

        // JSON 데이터를 객체로 변환
        MonsterData monsterData = JsonUtility.FromJson<MonsterData>(jsonText);

        // 리턴시켜서 다른곳에서도 사용이 가능하게 바꿈
        return monsterData;
    }
}
