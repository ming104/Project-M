using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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

    [SerializeField] private string EmployeeDepartment;
    public string _empDepartment
    {
        set { EmployeeDepartment = value; }
        get { return EmployeeDepartment; }
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

    [SerializeField] private TMPro.TextMeshProUGUI nameText;
    [SerializeField] private Slider HPBar;
    [SerializeField] private Slider MPBar;
    // Start is called before the first frame update
    // private void Awake()
    // {
    //     NavMeshAgent agent = GetComponent<NavMeshAgent>();
    //     agent.updateRotation = false;
    //     agent.updateUpAxis = false;
    // }

    void Start()
    {
        nameText.text = EmployeeName;
        HPBar.maxValue = EmployeeMaxHp;
        MPBar.maxValue = EmployeeMaxMp;
    }

    //Update is called once per frame
    void Update()
    {
        HPBar.value = EmployeeManager.Instance.Employees[EmployeeName].CurrentHP;
        MPBar.value = EmployeeManager.Instance.Employees[EmployeeName].CurrentMP;
    }
}
