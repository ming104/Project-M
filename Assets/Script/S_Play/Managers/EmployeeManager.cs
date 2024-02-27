using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class Employee_Hp_Mp_Data
{
    public int MaxHP;
    public int CurrentHP;
    public int MaxMP;
    public int CurrentMP;
}

public class EmployeeManager : Singleton<EmployeeManager>
{
    public GameObject Emp;

    public Dictionary<string, Employee_Hp_Mp_Data> Employees = new Dictionary<string, Employee_Hp_Mp_Data>();

    public void MainSet()
    {
        for (int i = 0; i < DataManager.Instance.MainDataLoad().elseDepart[0].AuditDepartment.Count; i++)
        {
            var empl = Instantiate(Emp);
            var empdata = empl.GetComponent<Employee>();
            var empdata_manager = DataManager.Instance.EmployeeDataLoad(DataManager.Instance.MainDataLoad().elseDepart[0].AuditDepartment[i]);
            empl.transform.position = new Vector3(-20, 17, 0);

            empdata._empName = DataManager.Instance.MainDataLoad().elseDepart[0].AuditDepartment[i];
            empdata._empMaxHp = empdata_manager.hp;
            empdata._empMaxMp = empdata_manager.mp;
            empdata._empDepartment = $"감사부서";
            empdata._empdef = empdata_manager.def;
            empdata._empPower = empdata_manager.power;
            empdata._empintelligence = empdata_manager.intelligence;
            empdata._empJustice = empdata_manager.justice;
            empdata._empMovementSpeed = empdata_manager.movementSpeed;
            //empdata._empEmployee_CurrentStatus = EmployeeFSM.Wait;
            //empdata._empCurHp = empdata._empMaxHp;
            //empdata._empCurMp = empdata._empMaxMp;

            var emphmpdata = new Employee_Hp_Mp_Data
            {
                MaxHP = empdata_manager.hp,
                CurrentHP = empdata._empMaxHp,
                MaxMP = empdata_manager.mp,
                CurrentMP = empdata._empMaxMp,
            };
            Employees.Add(empdata._empName, emphmpdata);
        }
        for (int i = 0; i < DataManager.Instance.MainDataLoad().elseDepart[0].AccountingDepartment.Count; i++)
        {
            var empl = Instantiate(Emp);
            var empdata = empl.GetComponent<Employee>();
            var empdata_manager = DataManager.Instance.EmployeeDataLoad(DataManager.Instance.MainDataLoad().elseDepart[0].AccountingDepartment[i]);
            empl.transform.position = new Vector3(20, 17, 0);
            empdata._empName = DataManager.Instance.MainDataLoad().elseDepart[0].AccountingDepartment[i];
            empdata._empMaxHp = empdata_manager.hp;
            empdata._empMaxMp = empdata_manager.mp;
            empdata._empDepartment = $"회계부서";
            empdata._empdef = empdata_manager.def;
            empdata._empPower = empdata_manager.power;
            empdata._empintelligence = empdata_manager.intelligence;
            empdata._empJustice = empdata_manager.justice;
            empdata._empMovementSpeed = empdata_manager.movementSpeed;
            //empdata._empEmployee_CurrentStatus = EmployeeFSM.Wait;
            //empdata._empCurHp = empdata._empMaxHp;
            //empdata._empCurMp = empdata._empMaxMp;

            var emphmpdata = new Employee_Hp_Mp_Data
            {
                MaxHP = empdata_manager.hp,
                CurrentHP = empdata._empMaxHp,
                MaxMP = empdata_manager.mp,
                CurrentMP = empdata._empMaxMp,
            };
            Employees.Add(empdata._empName, emphmpdata);
        }
        for (int i = 0; i < DataManager.Instance.MainDataLoad().Department.Count; i++)
        {
            for (int j = 0; j < DataManager.Instance.MainDataLoad().Department[i].EmployeeList.Count; j++)
            {
                var empl = Instantiate(Emp);
                var empdata = empl.GetComponent<Employee>();
                var empdata_manager = DataManager.Instance.EmployeeDataLoad(DataManager.Instance.MainDataLoad().Department[i].EmployeeList[j]);
                empl.transform.position = new Vector3(0, i * -20 - 3, 0);

                empdata._empName = DataManager.Instance.MainDataLoad().Department[i].EmployeeList[j];
                empdata._empMaxHp = empdata_manager.hp;
                empdata._empMaxMp = empdata_manager.mp;
                empdata._empDepartment = $"관리부서_{i}";
                empdata._empdef = empdata_manager.def;
                empdata._empPower = empdata_manager.power;
                empdata._empintelligence = empdata_manager.intelligence;
                empdata._empJustice = empdata_manager.justice;
                empdata._empMovementSpeed = empdata_manager.movementSpeed;
                //empdata._empEmployee_CurrentStatus = EmployeeFSM.Wait;
                //empdata._empCurHp = empdata._empMaxHp;
                //empdata._empCurMp = empdata._empMaxMp;

                var emphmpdata = new Employee_Hp_Mp_Data
                {
                    MaxHP = empdata_manager.hp,
                    CurrentHP = empdata._empMaxHp,
                    MaxMP = empdata_manager.mp,
                    CurrentMP = empdata._empMaxMp,
                };
                Employees.Add(empdata._empName, emphmpdata);
            }
        }

    }

    // Update is called once per frame
    // void Update()
    // {

    // }
}
