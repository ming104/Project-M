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
    public int OpenCompany;
    public string[] MonsterList;

    public string[] Employee;
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
    public Image monsterImage;
    public TextMeshProUGUI monsterName;
    public TextMeshProUGUI code;
    public TextMeshProUGUI dangerLevel;
    public TextMeshProUGUI Research_log;

    void Start()
    {
        //MainCompanyData();
    }

    public string[] MainCompanyData()
    {
        string filePath = "Assets/Resources/GameData/MainData.json";

        string jsonText = File.ReadAllText(filePath);
        MainCompanyData MainData = JsonUtility.FromJson<MainCompanyData>(jsonText);
        Debug.Log("JSON Text: " + jsonText);
        // foreach (string MonList in MainData.MonsterList)
        // {
        //     return MonList;
        // }
        return MainData.MonsterList;
    }

    public void DataLoad(string filename)
    {
        monsterImage.sprite = Resources.Load<Sprite>(null);
        monsterName.text = null;
        code.text = null;
        dangerLevel.text = null;
        Research_log.text = null;

        string filePath = "Assets/Resources/GameData/Monster/" + filename + ".json";

        // JSON 파일 읽어오기
        string jsonText = File.ReadAllText(filePath);

        // 읽어온 JSON 텍스트 확인
        //Debug.Log("JSON Text: " + jsonText);

        // JSON 데이터를 객체로 변환
        MonsterData monsterData = JsonUtility.FromJson<MonsterData>(jsonText);

        // 변환된 객체 사용
        // Debug.Log("ID: " + monsterData.id);
        // Debug.Log("Max Research Level: " + monsterData.Max_research_Level);
        // Debug.Log("Monster Name: " + monsterData.profile.MonsterName);
        // foreach (string log in monsterData.Research_log.log)
        // {
        //     Debug.Log("- " + log);
        // }
        // Debug.Log("Open Level: " + monsterData.OpenLevel);

        monsterImage.sprite = Resources.Load<Sprite>(monsterData.profile.imagePATH);
        monsterName.text = "이름 : " + monsterData.profile.MonsterName;
        code.text = "식별 코드 : " + monsterData.profile.code;
        dangerLevel.text = "위험도 : " + monsterData.profile.riskLevel.ToString();
        foreach (string log in monsterData.Research_log.log)
        {
            Research_log.text += "- " + log + "\n";
        }
    }
}
