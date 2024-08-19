using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public delegate void NavmeshDelegate(Vector3 destination);

public class Employee : MonoBehaviour
{
    public enum EmployeeFsm
    {
        Wait = 0,
        Moving = 1,
        Work = 2,
        Battle = 3,
        StatusEffect = 4
    }

    // Employee Info
    [SerializeField] private string employeeName;
    public string EmployeeName
    {
        get => employeeName;
        set => employeeName = value;
    }

    [SerializeField] private int employeeMaxHp;
    public int EmployeeMaxHp
    {
        get => employeeMaxHp;
        set => employeeMaxHp = value;
    }

    [SerializeField] private int employeeMaxMp;
    public int EmployeeMaxMp
    {
        get => employeeMaxMp;
        set => employeeMaxMp = value;
    }

    [SerializeField] private string employeeDepartment;
    public string EmployeeDepartment
    {
        get => employeeDepartment;
        set => employeeDepartment = value;
    }

    [SerializeField] private int employeeDef;
    public int EmployeeDef
    {
        get => employeeDef;
        set => employeeDef = value;
    }

    [SerializeField] private int employeePower;
    public int EmployeePower
    {
        get => employeePower;
        set => employeePower = value;
    }

    [SerializeField] private int employeeIntelligence;
    public int EmployeeIntelligence
    {
        get => employeeIntelligence;
        set => employeeIntelligence = value;
    }

    [SerializeField] private int employeeJustice;
    public int EmployeeJustice
    {
        get => employeeJustice;
        set => employeeJustice = value;
    }

    [SerializeField] private int employeeMovementSpeed;
    public int EmployeeMovementSpeed
    {
        get => employeeMovementSpeed;
        set => employeeMovementSpeed = value;
    }

    [SerializeField] private EmployeeFsm employeeCurrentStatus;
    public EmployeeFsm EmployeeCurrentStatus
    {
        get => employeeCurrentStatus;
        set => employeeCurrentStatus = value;
    }

    [SerializeField] private GameObject empGameObject;
    public GameObject EmpGameObject
    {
        get => empGameObject;
        set => empGameObject = value;
    }

    [SerializeField] private TMPro.TextMeshProUGUI nameText;
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider mpBar;

    private NavMeshAgent _agent;
    private Vector3 _destinationPos;

    public bool isResearchMoving;

    NavmeshDelegate _navmeshDelegate;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        nameText.text = employeeName;
        hpBar.maxValue = employeeMaxHp;
        mpBar.maxValue = employeeMaxMp;
        employeeCurrentStatus = EmployeeFsm.Wait;
        _navmeshDelegate = DestinationMoving;

        isResearchMoving = false;
    }

    public void DestinationMoving(Vector3 destination)
    {
        _agent.SetDestination(destination);
        _destinationPos = destination;
    }
    
    public void ResearchDestinationMoving(Vector3 destination)
    {
        _agent.SetDestination(destination);
        _destinationPos = destination;
        isResearchMoving = true;
    }

    private void Update()
    {
        HandleOffMeshLink();
        Research();
        // Update HP and MP bars
        hpBar.value = EmployeeManager.Instance.Employees[employeeName].CurrentHP;
        mpBar.value = EmployeeManager.Instance.Employees[employeeName].CurrentMP;

        // Handle FSM state
        switch (employeeCurrentStatus)
        {
            case EmployeeFsm.Wait:
                Waiting();
                break;
            case EmployeeFsm.Moving:
                Moving();
                break;
            case EmployeeFsm.Work:
                Working();
                break;
            case EmployeeFsm.Battle:
                Fighting();
                break;
            case EmployeeFsm.StatusEffect:
                StatusAbnormality();
                break;
        }
    }

    private void HandleOffMeshLink()
    {
        if (_agent.isOnOffMeshLink)
        {
            _agent.Warp(_agent.currentOffMeshLinkData.endPos);
            _agent.CompleteOffMeshLink();
            _agent.SetDestination(_destinationPos);
        }
        
        if (_agent.remainingDistance <= 0.5f && isResearchMoving)
        //if (_agent.velocity.sqrMagnitude <= 0.2f && _agent.remainingDistance >= 0.5f)
        {
            _agent.ResetPath();
            isResearchMoving = false;
            
            employeeCurrentStatus = EmployeeFsm.Wait;
        }
    }

    private void Research()
    {
        if (isResearchMoving == false)
        {
            
        }
    }

    private void Waiting() { }
    private void Moving() { }
    private void Working() { }
    private void Fighting() { }
    private void StatusAbnormality() { }
}
