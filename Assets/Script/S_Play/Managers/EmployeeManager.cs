using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

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
            empdata._empintelligence = DataManager.Instance.EmployeeDataLoad(Employees[i]).intelligence;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
