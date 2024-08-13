using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


public class Employee : MonoBehaviour
{
    public enum EmployeeFSM
    {
        Wait = 0,
        moving = 1,
        Work = 2,
        battle = 3,
        status_effect = 4
    }
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

    [SerializeField] private GameObject Emp_GO;
    public GameObject _emp_GameObject
    {
        set { Emp_GO = value; }
        get { return Emp_GO; }
    }

    [SerializeField] private TMPro.TextMeshProUGUI nameText;
    [SerializeField] private Slider HPBar;
    [SerializeField] private Slider MPBar;
    public NavMeshAgent agent;

    private Vector3 destinationPos;
    //Start is called before the first frame update
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        //agent.updateUpAxis = false;
    }

    //public Vector3 PingPongstartPosition;


    void Start()
    {
        nameText.text = EmployeeName;
        HPBar.maxValue = EmployeeMaxHp;
        MPBar.maxValue = EmployeeMaxMp;

        Employee_CurrentStatus = EmployeeFSM.Wait;
    }

    public void DestinationMoving(float x, float y, float z)
    {
        agent.SetDestination(new Vector3(x, y, z));
        destinationPos = new Vector3(x, y, z);
    }

    //Update is called once per frame
    void Update() // 매우 많은 수정이 필요할듯
    {
        if (agent.isOnOffMeshLink) //만약 agent가 offmeshLink를 만난다면?
        {
            agent.Warp(agent.currentOffMeshLinkData.endPos); // 바로 끝 점으로 워프를 함
            agent.CompleteOffMeshLink(); // offmesh가 끝났음을 알림

            agent.SetDestination(destinationPos); // 그리고 다시 원래 좌표로 지정해줌
        }
        
        HPBar.value = EmployeeManager.Instance.Employees[EmployeeName].CurrentHP;
        MPBar.value = EmployeeManager.Instance.Employees[EmployeeName].CurrentMP;
        if (agent.velocity.sqrMagnitude >= .2f && agent.remainingDistance <= 0.5f)
        {
            agent.ResetPath();
            Employee_CurrentStatus = EmployeeFSM.Wait;
        }
        switch (Employee_CurrentStatus)
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
