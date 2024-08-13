using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeInfo_Management : MonoBehaviour
{
    [SerializeField] private string empName;
    public string _EmpName
    {
        set { empName = value; }
        get { return empName; }
    }
    [SerializeField] private int empDepart;
    public int _EmpDepart
    {
        set { empDepart = value; }
        get { return empDepart; }
    }
}
