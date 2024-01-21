using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Management_Manager : MonoBehaviour
{
    public TextMeshProUGUI Day;
    public TextMeshProUGUI Money;
    public TextMeshProUGUI ResearchPoint;
    public int DepartmentNumber;
    public TextMeshProUGUI Department;
    public List<Image> MonsterImage;
    public List<TextMeshProUGUI> MonsterNameText;
    public List<Image> EmployeeImage;
    public List<TextMeshProUGUI> EmployeeNameText;
    [Header("InfoCanvas")]
    public GameObject InfoCanvas;
    public Image monsterImage;
    public TextMeshProUGUI monsterName;
    public TextMeshProUGUI code;
    public TextMeshProUGUI dangerLevel;
    public TextMeshProUGUI research_log;
    [Header("Employ_Canvas")]
    public GameObject Employ_Canvas;


    [Header("ResetImage")]
    public Sprite ResetImage;
    // Start is called before the first frame update
    void Start()
    {
        Day.text = $"Day : {DataManager.Instance.MainDataLoad().day}";
        Money.text = $"돈 : {DataManager.Instance.MainDataLoad().Money}";
        ResearchPoint.text = $"연구 포인트 : {DataManager.Instance.MainDataLoad().ResearchPoint}";
        DepartmentNumber = 0;
        DepartmentInfo();
    }

    void DepartmentInfo()
    {
        DepartmentInfoReset();
        Department.text = $"현재 부서 : 관리부서_{DepartmentNumber}";

        for (int i = 0; i < DataManager.Instance.MainDataLoad().Department[DepartmentNumber].MonsterList.Count; i++)
        {
            MonsterImage[i].sprite = Resources.Load<Sprite>(DataManager.Instance.MonsterDataLoad
                (DataManager.Instance.MainDataLoad().Department[DepartmentNumber].MonsterList[i]).profile.imagePATH);
            MonsterNameText[i].text = DataManager.Instance.MonsterDataLoad
                (DataManager.Instance.MainDataLoad().Department[DepartmentNumber].MonsterList[i]).profile.MonsterName;
            MonsterImage[i].GetComponent<MonsterInfo_Management>()._monName =
                DataManager.Instance.MainDataLoad().Department[DepartmentNumber].MonsterList[i];
        }
        for (int i = 0; i < DataManager.Instance.MainDataLoad().Department[DepartmentNumber].EmployeeList.Count; i++)
        {
            EmployeeImage[i].sprite = null;
            EmployeeNameText[i].text = DataManager.Instance.MainDataLoad().Department[DepartmentNumber].EmployeeList[i];
        }
    }

    void DepartmentInfoReset()
    {
        for (int i = 0; i < MonsterImage.Count; i++)
        {
            MonsterImage[i].sprite = ResetImage;
            MonsterNameText[i].text = "없음";
        }
        for (int i = 0; i < EmployeeImage.Count; i++)
        {
            EmployeeImage[i].sprite = ResetImage;
            EmployeeNameText[i].text = "없음";
        }
    }

    // Update is called once per frame
    void Update()
    {
        DepartmentNumberSizeControll();
    }

    void DepartmentNumberSizeControll()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0 && DepartmentNumber > 0) // 올렸을 때 처리 -> Department감소
        {
            DepartmentNumber--;
            DepartmentInfo();
        }
        else if (wheelInput < 0 && DepartmentNumber < DataManager.Instance.MainDataLoad().Department.Count - 1) // 내렸을 때 처리 -> Department증가
        {
            DepartmentNumber++;
            DepartmentInfo();
        }
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

    public void InfoSelectButton(GameObject GB)
    {
        if (GB.GetComponent<MonsterInfo_Management>()._monName != string.Empty)
        {
            var monsterData = DataManager.Instance.MonsterDataLoad(GB.GetComponent<MonsterInfo_Management>()._monName);
            InfoCanvasOn(monsterData);
        }
        else
        {
            Debug.Log("비어있음");
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
}
