using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

[Serializable]
public class Employee_Element_EmployManager
{
    public GameObject Employ;
    public TextMeshProUGUI Hp;
    public TextMeshProUGUI Mp;
    public TextMeshProUGUI Def;
    public TextMeshProUGUI Power;
    public TextMeshProUGUI Intelligence;
    public TextMeshProUGUI MovementSpeed;
}
public class Employ_Manager : Singleton<Employ_Manager>
{
    public List<Employee_Element_EmployManager> Employ;
    public int employeecategory;
    public int Selected_Number;
    public EmployeeData[] empdata;
    // Start is called before the first frame update
    public void Employ_Setting()
    {
        for (int i = 0; i < Employ.Count; i++)
        {
            var empinfo = DataManager.Instance.CreateEmployeeData(employeecategory);
            Employ[i].Hp.text = $"체력 : {empinfo.hp}";
            Employ[i].Mp.text = $"정신력 : {empinfo.mp}";
            Employ[i].Def.text = $"방어력 : {empinfo.def}";
            Employ[i].Power.text = $"힘 : {empinfo.power}";
            Employ[i].Intelligence.text = $"지능 : {empinfo.intelligence}";
            Employ[i].MovementSpeed.text = $"이동속도 : {empinfo.movementSpeed}";
            empdata[i] = empinfo;
        }
    }

    public void Selected_Employee()
    {
        string filename = empdata[Selected_Number].name;
        string filePath = $"Assets/Resources/GameData/Employee/{filename}.json";
        string Employeejson = JsonUtility.ToJson(empdata[Selected_Number], true);
        File.WriteAllText(filePath, Employeejson);
        Debug.Log(filename);
        DataManager.Instance.MaindataSave(1, 1, filename);
    }

    public void SelectedEmp(int num)
    {
        Selected_Number = num;
    }
}
