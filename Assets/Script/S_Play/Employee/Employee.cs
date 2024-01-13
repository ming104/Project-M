using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Employee : MonoBehaviour
{
    [SerializeField] private string EmployeeName;
    public string _empName
    {
        set { EmployeeName = value; }
        get { return EmployeeName; }
    }

    [SerializeField] private int EmployeeMaxHp;
    public int _empMaxHp
    {
        set { EmployeeMaxHp = value; }
        get { return EmployeeMaxHp; }
    }

    [SerializeField] private int EmployeeMaxMp;
    public int _empMaxMp
    {
        set { EmployeeMaxMp = value; }
        get { return EmployeeMaxMp; }
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
    [SerializeField] private int EmployeeMovementSpeed;
    public int _empMovementSpeed
    {
        set { EmployeeMovementSpeed = value; }
        get { return EmployeeMovementSpeed; }
    }

    [SerializeField] private int CurHP;
    public int _empCurHp
    {
        set { CurHP = value; }
        get { return CurHP; }
    }

    [SerializeField] private int CurMP;
    public int _empCurMp
    {
        set { CurMP = value; }
        get { return CurMP; }
    }

    [SerializeField] private TMPro.TextMeshProUGUI nameText;
    [SerializeField] private Slider HPBar;
    [SerializeField] private Slider MPBar;
    // Start is called before the first frame update
    void Start()
    {
        nameText.text = EmployeeName;
        HPBar.maxValue = EmployeeMaxHp;
        MPBar.maxValue = EmployeeMaxMp;
    }

    // Update is called once per frame
    void Update()
    {
        HPBar.value = CurHP;
        MPBar.value = CurMP;
    }
}
