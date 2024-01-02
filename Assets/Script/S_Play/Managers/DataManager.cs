using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


#region DataSet
[System.Serializable]
public class MonsterData
{
    public string id;
    public int Max_research_Level;
    public ProfileData profile;
    public ResearchLogData Research_log;
    public string OpenLevel;
}

[System.Serializable]
public class ProfileData
{
    public string code;
    public string imagePATH;
    public string MonsterName;
    public int riskLevel;
}

[System.Serializable]
public class ResearchLogData
{
    public string[] log;
}
#endregion DataSet

public class DataManager : Singleton<DataManager>
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DataLoad(string filename)
    {
        string filePath = "Assets/Resource/GameData/Monster/" + filename + ".json";

        // JSON 파일 읽어오기
        string jsonText = File.ReadAllText(filePath);

        // 읽어온 JSON 텍스트 확인
        Debug.Log("JSON Text: " + jsonText);

        // JSON 데이터를 객체로 변환
        MonsterData monsterData = JsonUtility.FromJson<MonsterData>(jsonText);

        // 변환된 객체 사용
        Debug.Log("ID: " + monsterData.id);
        Debug.Log("Max Research Level: " + monsterData.Max_research_Level);
        Debug.Log("Monster Name: " + monsterData.profile.MonsterName);
        foreach (string log in monsterData.Research_log.log)
        {
            Debug.Log("- " + log);
        }
        Debug.Log("Open Level: " + monsterData.OpenLevel);
    }
}
