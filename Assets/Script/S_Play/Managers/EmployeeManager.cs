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
        Employees = GameManager.Instance.nowEmployeeList;
        for (int i = 0; i < Employees.Count; i++)
        {
            var empl = Instantiate(Emp, new Vector3(0, -2.5f, 0), Quaternion.identity);
            var empdata = empl.GetComponent<Employee>();
            empdata._empName = Employees[i];
            empdata._empHp = DataManager.Instance.EmployeeDataLoad(Employees[i]).hp;
            empdata._empdef = DataManager.Instance.EmployeeDataLoad(Employees[i]).def;
            empdata._empPower = DataManager.Instance.EmployeeDataLoad(Employees[i]).power;
            empdata._empintelligence = DataManager.Instance.EmployeeDataLoad(Employees[i]).intelligence;
            empdata._empMovementSpeed = DataManager.Instance.EmployeeDataLoad(Employees[i]).movementSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
