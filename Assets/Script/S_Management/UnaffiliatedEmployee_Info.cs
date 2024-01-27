using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnaffiliatedEmployee_Info : MonoBehaviour
{
    [SerializeField] private string EmployeeName;
    public string _empName
    {
        set { EmployeeName = value; }
        get { return EmployeeName; }
    }

    [SerializeField] private int EmployeeHp;
    public int _empHp
    {
        set { EmployeeHp = value; }
        get { return EmployeeHp; }
    }

    [SerializeField] private int EmployeeMp;
    public int _empMp
    {
        set { EmployeeMp = value; }
        get { return EmployeeMp; }
    }

    [SerializeField] private int Employeedef;
    public int _empdef
    {
        set { Employeedef = value; }
        get { return Employeedef; }
    }

    [SerializeField] private int EmployeePower;
    public int _empPower
    {
        set { EmployeePower = value; }
        get { return EmployeePower; }
    }
    [SerializeField] private int Employeeintelligence;
    public int _empintelligence
    {
        set { Employeeintelligence = value; }
        get { return Employeeintelligence; }
    }
    [SerializeField] private int EmployeeJustice;
    public int _empJustice
    {
        set { EmployeeJustice = value; }
        get { return EmployeeJustice; }
    }
    [SerializeField] private int EmployeeMovementSpeed;
    public int _empMovementSpeed
    {
        set { EmployeeMovementSpeed = value; }
        get { return EmployeeMovementSpeed; }
    }
}
