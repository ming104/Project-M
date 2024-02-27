using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum EmployeeFSM
{
    Wait = 0,
    moving = 1,
    Work = 2,
    battle = 3,
    status_effect = 4
}

public class Employee : MonoBehaviour
{
    private EmployeeFSM _StateEmp;

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

    [SerializeField] private EmployeeFSM Employee_CurrentStatus;
    public EmployeeFSM _empEmployee_CurrentStatus
    {
        set { Employee_CurrentStatus = value; }
        get { return Employee_CurrentStatus; }
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

    public Vector3 PingPongstartPosition;

    void Start()
    {
        nameText.text = EmployeeName;
        HPBar.maxValue = EmployeeMaxHp;
        MPBar.maxValue = EmployeeMaxMp;

        PingPongstartPosition = transform.position;
        _StateEmp = EmployeeFSM.Wait;
    }

    //Update is called once per frame
    void Update() // 매우 많은 수정이 필요할듯
    {
        HPBar.value = EmployeeManager.Instance.Employees[EmployeeName].CurrentHP;
        MPBar.value = EmployeeManager.Instance.Employees[EmployeeName].CurrentMP;
        switch (_StateEmp)
        {
            case EmployeeFSM.Wait:
                Waiting();
                break;
            case EmployeeFSM.moving:
                Moving();
                break;
            case EmployeeFSM.Work:
                Working();
                break;
            case EmployeeFSM.battle:
                Fighting();
                break;
            case EmployeeFSM.status_effect:
                StatusAbnormality();
                break;
        }
    }

    void Waiting()
    {
        float pingPongValue = Mathf.PingPong(Time.time * (_empMovementSpeed / 10), 5);

        // 오브젝트의 x 좌표를 업데이트합니다.
        transform.position = new Vector3(PingPongstartPosition.x + pingPongValue, PingPongstartPosition.y, PingPongstartPosition.z);
    }
    void Moving()
    {

    }
    void Working()
    {

    }
    void Fighting()
    {

    }
    void StatusAbnormality()
    {

    }
}
