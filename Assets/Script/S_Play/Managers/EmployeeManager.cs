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
        for (int i = 0; i < DataManager.Instance.MainDataLoad().Department.Count; i++)
        {
            Employees = DataManager.Instance.MainDataLoad().Department[i].EmployeeList;
            for (int j = 0; j < Employees.Count; j++)
            {
                var empl = Instantiate(Emp);
                var empdata = empl.GetComponent<Employee>();
                var empdata_manager = DataManager.Instance.EmployeeDataLoad(Employees[j]);
                empdata._empName = Employees[j];
                empdata._empMaxHp = empdata_manager.hp;
                empdata._empMaxMp = empdata_manager.mp;
                empdata._empDepartment = $"관리부서_{i}";
                empdata._empdef = empdata_manager.def;
                empdata._empPower = empdata_manager.power;
                empdata._empintelligence = empdata_manager.intelligence;
                empdata._empMovementSpeed = empdata_manager.movementSpeed;
                empdata._empCurHp = empdata._empMaxHp;
                empdata._empCurMp = empdata._empMaxMp;
                empl.transform.position = new Vector3(0, i * -20, 0);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
