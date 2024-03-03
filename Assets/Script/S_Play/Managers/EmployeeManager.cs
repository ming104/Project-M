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

[Serializable]
public class EmployeeList
{
    public List<GameObject> EmpLocate;
}


public class EmployeeManager : Singleton<EmployeeManager>
{
    public GameObject Emp;
    public List<EmployeeList> Department_Emp;

    public Dictionary<string, Employee_Hp_Mp_Data> Employees = new Dictionary<string, Employee_Hp_Mp_Data>();

    public void MainSet()
    {
        for (int f = 0; f < DataManager.Instance.MainDataLoad().Floor.Count; f++)
        {
            for (int i = 0; i < DataManager.Instance.MainDataLoad().Floor[f].AuditDepartment.Count; i++)
            {
                var empl = Instantiate(Emp);
                var empdata = empl.GetComponent<Employee>();
                var empdata_manager = DataManager.Instance.EmployeeDataLoad(DataManager.Instance.MainDataLoad().Floor[f].AuditDepartment[i]);
                empl.transform.position = Department_Emp[0].EmpLocate[0].transform.position;
                empl.GetComponent<NavMeshAgent>().updateRotation = false;
                empl.GetComponent<NavMeshAgent>().updateUpAxis = false;

                empdata._empName = DataManager.Instance.MainDataLoad().Floor[f].AuditDepartment[i];
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
            for (int i = 0; i < DataManager.Instance.MainDataLoad().Floor[f].AccountingDepartment.Count; i++)
            {
                var empl = Instantiate(Emp);
                var empdata = empl.GetComponent<Employee>();
                var empdata_manager = DataManager.Instance.EmployeeDataLoad(DataManager.Instance.MainDataLoad().Floor[f].AccountingDepartment[i]);
                empl.transform.position = Department_Emp[0].EmpLocate[0].transform.position;
                empl.GetComponent<NavMeshAgent>().updateRotation = false;
                empl.GetComponent<NavMeshAgent>().updateUpAxis = false;
                empdata._empName = DataManager.Instance.MainDataLoad().Floor[f].AccountingDepartment[i];
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
            for (int i = 0; i < DataManager.Instance.MainDataLoad().Floor[f].Department.Count; i++)
            {
                for (int j = 0; j < DataManager.Instance.MainDataLoad().Floor[f].Department[i].EmployeeList.Count; j++)
                {
                    var empl = Instantiate(Emp);
                    var empdata = empl.GetComponent<Employee>();
                    var empdata_manager = DataManager.Instance.EmployeeDataLoad(DataManager.Instance.MainDataLoad().Floor[f].Department[i].EmployeeList[j]);
                    empl.transform.position = Department_Emp[f].EmpLocate[i + 1].transform.position;
                    empl.GetComponent<NavMeshAgent>().updateRotation = false;
                    empl.GetComponent<NavMeshAgent>().updateUpAxis = false;

                    empdata._empName = empdata_manager.name;
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
    }

    // Update is called once per frame
    // void Update()
    // {

    // }
}
