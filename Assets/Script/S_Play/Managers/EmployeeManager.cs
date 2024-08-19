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
    public Dictionary<string, Employee> EmployeeDatas = new Dictionary<string, Employee>();

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

                empdata.EmployeeName = DataManager.Instance.MainDataLoad().Floor[f].AuditDepartment[i];
                empdata.EmployeeMaxHp = empdata_manager.hp;
                empdata.EmployeeMaxMp = empdata_manager.mp;
                empdata.EmployeeDepartment = $"감사부서";
                empdata.EmployeeDef = empdata_manager.def;
                empdata.EmployeePower = empdata_manager.power;
                empdata.EmployeeIntelligence = empdata_manager.intelligence;
                empdata.EmployeeJustice = empdata_manager.justice;
                empdata.EmployeeMovementSpeed = empdata_manager.movementSpeed;
                empdata.GetComponent<NavMeshAgent>().speed = empdata.EmployeeMovementSpeed/10;
                //empdata._empEmployee_CurrentStatus = EmployeeFSM.Wait;
                //empdata._empCurHp = empdata._empMaxHp;
                //empdata._empCurMp = empdata._empMaxMp;

                var emphmpdata = new Employee_Hp_Mp_Data
                {
                    MaxHP = empdata_manager.hp,
                    CurrentHP = empdata.EmployeeMaxHp,
                    MaxMP = empdata_manager.mp,
                    CurrentMP = empdata.EmployeeMaxMp,
                };
                Employees.Add(empdata.EmployeeName, emphmpdata);
                EmployeeDatas.Add(empdata.EmployeeName, empdata);
            }
            for (int i = 0; i < DataManager.Instance.MainDataLoad().Floor[f].AccountingDepartment.Count; i++)
            {
                var empl = Instantiate(Emp);
                var empdata = empl.GetComponent<Employee>();
                var empdata_manager = DataManager.Instance.EmployeeDataLoad(DataManager.Instance.MainDataLoad().Floor[f].AccountingDepartment[i]);
                empl.transform.position = Department_Emp[0].EmpLocate[0].transform.position;
                empl.GetComponent<NavMeshAgent>().updateRotation = false;
                empl.GetComponent<NavMeshAgent>().updateUpAxis = false;
                empdata.EmployeeName = DataManager.Instance.MainDataLoad().Floor[f].AccountingDepartment[i];
                empdata.EmployeeMaxHp = empdata_manager.hp;
                empdata.EmployeeMaxMp = empdata_manager.mp;
                empdata.EmployeeDepartment = $"회계부서";
                empdata.EmployeeDef = empdata_manager.def;
                empdata.EmployeePower = empdata_manager.power;
                empdata.EmployeeIntelligence = empdata_manager.intelligence;
                empdata.EmployeeJustice = empdata_manager.justice;
                empdata.EmployeeMovementSpeed = empdata_manager.movementSpeed;
                empdata.GetComponent<NavMeshAgent>().speed = empdata.EmployeeMovementSpeed/10;

                //empdata._emp_GameObject = empl;
                //empdata._empEmployee_CurrentStatus = EmployeeFSM.Wait;
                //empdata._empCurHp = empdata._empMaxHp;
                //empdata._empCurMp = empdata._empMaxMp;

                var emphmpdata = new Employee_Hp_Mp_Data
                {
                    MaxHP = empdata_manager.hp,
                    CurrentHP = empdata.EmployeeMaxHp,
                    MaxMP = empdata_manager.mp,
                    CurrentMP = empdata.EmployeeMaxMp,
                };
                Employees.Add(empdata.EmployeeName, emphmpdata);
                
                EmployeeDatas.Add(empdata.EmployeeName, empdata);
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

                    empdata.EmployeeName = empdata_manager.name;
                    empdata.EmployeeMaxHp = empdata_manager.hp;
                    empdata.EmployeeMaxMp = empdata_manager.mp;
                    empdata.EmployeeDepartment = $"관리부서_{i}";
                    empdata.EmployeeDef = empdata_manager.def;
                    empdata.EmployeePower = empdata_manager.power;
                    empdata.EmployeeIntelligence = empdata_manager.intelligence;
                    empdata.EmployeeJustice = empdata_manager.justice;
                    empdata.EmployeeMovementSpeed = empdata_manager.movementSpeed;
                    empdata.GetComponent<NavMeshAgent>().speed = empdata.EmployeeMovementSpeed/10;
                    //empdata._empEmployee_CurrentStatus = EmployeeFSM.Wait;
                    //empdata._empCurHp = empdata._empMaxHp;
                    //empdata._empCurMp = empdata._empMaxMp;

                    var emphmpdata = new Employee_Hp_Mp_Data
                    {
                        MaxHP = empdata_manager.hp,
                        CurrentHP = empdata.EmployeeMaxHp,
                        MaxMP = empdata_manager.mp,
                        CurrentMP = empdata.EmployeeMaxMp,
                    };
                    Employees.Add(empdata.EmployeeName, emphmpdata);
                    
                    EmployeeDatas.Add(empdata.EmployeeName, empdata);
                }
            }
        }
    }

    // Update is called once per frame
    // void Update()
    // {

    // }
}
