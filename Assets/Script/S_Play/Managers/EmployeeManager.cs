using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public enum EmployeeFSM
{
    Wait = 0,
    moving = 1,
    Work = 2,
    battle = 3,
    status_effect = 4
}

public class EmployeeManager : Singleton<EmployeeManager>
{
    public GameObject Emp;

    public List<string> Employees;

    public void MainSet()
    {
        Employees = DataManager.Instance.MainDataLoad().EmployeeList;
        for (int i = 0; i < Employees.Count; i++)
        {
            var empl = Instantiate(Emp, new Vector3(0, 0, 0), Quaternion.identity);
            var empdata = empl.GetComponent<Employee>();
            empdata._empName = Employees[i];
            empdata._empMaxHp = DataManager.Instance.EmployeeDataLoad(Employees[i]).hp;
            empdata._empMaxMp = DataManager.Instance.EmployeeDataLoad(Employees[i]).mp;
            empdata._empdef = DataManager.Instance.EmployeeDataLoad(Employees[i]).def;
            empdata._empPower = DataManager.Instance.EmployeeDataLoad(Employees[i]).power;
            empdata._empintelligence = DataManager.Instance.EmployeeDataLoad(Employees[i]).intelligence;
            empdata._empMovementSpeed = DataManager.Instance.EmployeeDataLoad(Employees[i]).movementSpeed;
            empdata._empCurHp = empdata._empMaxHp;
            empdata._empCurMp = empdata._empMaxMp;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
