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
}
