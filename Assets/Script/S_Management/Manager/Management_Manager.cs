using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Linq;

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
    public List<string> Monsters;
    // Start is called before the first frame update
    void Start()
    {
        Day.text = $"Day : {DataManager.Instance.MainDataLoad().day}";
        Money.text = $"Money : {DataManager.Instance.MainDataLoad().Money}";
        ResearchPoint.text = $"ResearchPoint : {DataManager.Instance.MainDataLoad().ResearchPoint}";
        Department.text = DepartmentNumber.ToString();

        for (int i = 0; i < DataManager.Instance.MainDataLoad().DepartmentMonster[0].Count; i++)
        {
            MonsterImage[i].sprite = Resources.Load<Sprite>(DataManager.Instance.MonsterDataLoad
                (DataManager.Instance.MainDataLoad().DepartmentMonster[0][i]).profile.imagePATH);
            MonsterNameText[i].text = DataManager.Instance.MonsterDataLoad
                (DataManager.Instance.MainDataLoad().DepartmentMonster[0][i]).profile.MonsterName;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
