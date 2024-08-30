using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Employee : MonoBehaviour
{
    public enum EmployeeFsm
    {
        Wait = 0,
        Moving = 1,
        Work = 2,
        Battle = 3,
        StatusEffect = 4,
        Death = 5
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
    
    [SerializeField] private WeaponScriptableObject weapon;
    public WeaponScriptableObject Weapon
    {
        get => weapon;
        set => weapon = value;
    }
    
    [SerializeField] private ArmorScriptableObject armor;
    public ArmorScriptableObject Armor
    {
        get => armor;
        set => armor = value;
    }

    [SerializeField] private TMPro.TextMeshProUGUI nameText;
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider mpBar;

    private NavMeshAgent _agent;
    [SerializeField]
    private Vector3 _resetDestinationPos;
    private Vector3 _destinationPos;

    public CircleCollider2D attackRangeCollider;

    public bool isResearchMoving;
    public bool isAttackMoving;
    
    public int areaMask;
    public LayerMask monsterLayerMask; // 몬스터 레이어 마스크

    private Room_Select_Manager _currentResearchData;
    private int _roomIndex;
    

    //NavmeshDelegate _navmeshDelegate;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        nameText.text = employeeName;
        hpBar.maxValue = employeeMaxHp;
        mpBar.maxValue = employeeMaxMp;
        EmployeeCurrentStatus = EmployeeFsm.Wait;
        //_navmeshDelegate = DestinationMoving;

        isResearchMoving = false;
    }

    public void DestinationMoving(Vector3 destination)
    {
        Debug.Log(EmployeeCurrentStatus);
        _agent.isStopped = false;
        isResearchMoving = false;
        _currentResearchData = null;
        _agent.SetDestination(destination);
        _destinationPos = destination;
    }

    public void ResearchDestinationMoving(Vector3 destination, Room_Select_Manager roomData, int workindex)
    {
        Debug.Log(EmployeeCurrentStatus);
        if (EmployeeCurrentStatus != EmployeeFsm.Work)
        {
            EmployeeCurrentStatus = EmployeeFsm.Moving;
            //Debug.Log("실행");
            _agent.isStopped = false;
            _resetDestinationPos = transform.position;
            _agent.SetDestination(destination);
            _destinationPos = destination;
            isResearchMoving = true;
            _currentResearchData = roomData;
            _roomIndex = workindex;
        }
    }

    public void ResetDestinationMoving()
    {
        Debug.Log(EmployeeCurrentStatus);
        if (EmployeeCurrentStatus != EmployeeFsm.Work)
        {
            EmployeeCurrentStatus = EmployeeFsm.Moving;
            _agent.isStopped = false;
            isResearchMoving = false;
            _currentResearchData = null;
            _agent.SetDestination(_resetDestinationPos);
            _destinationPos = _resetDestinationPos;
        }
    }

    public void AttackDestinationMoving(Vector3 enemyPosition)
    {
        if (EmployeeCurrentStatus != EmployeeFsm.Work)
        {
            EmployeeCurrentStatus = EmployeeFsm.Moving;
            Debug.Log("attackMoving");

            _agent.isStopped = false;
            isResearchMoving = false;
            _currentResearchData = null;
            _agent.SetDestination(enemyPosition);
            _destinationPos = enemyPosition;
            isAttackMoving = true;
            // 무기 관련 코드 입력 되야함
        }
    }

    private void Update()
    {
        HandleOffMeshLink();
        Attack();
        CurrentArea();
        // Update HP and MP bars
        hpBar.value = EmployeeManager.Instance.Employees[employeeName].CurrentHP;
        mpBar.value = EmployeeManager.Instance.Employees[employeeName].CurrentMP;

        // Handle FSM state
        switch (EmployeeCurrentStatus)
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
            Debug.Log(areaMask);
            
            //_agent.SamplePathPosition();
        }
        
        
        
        //if (_agent.remainingDistance <= 0.5f && isResearchMoving)
        if (_agent.velocity.sqrMagnitude >= 0.2f && _agent.remainingDistance <= 0.5f)
        {
            _agent.ResetPath();
            _agent.isStopped = true;
            _agent.velocity = Vector3.zero;
            //isResearchMoving = false;
            employeeCurrentStatus = EmployeeFsm.Wait;
            
            if (isResearchMoving && areaMask == 8)
            {
                //Debug.Log("들어옴");
                _currentResearchData.StartResearch(_roomIndex , this);
                employeeCurrentStatus = EmployeeFsm.Work;
            }
        }
    }
    private void CurrentArea()
    {
        NavMeshHit hit;
       
        if (NavMesh.SamplePosition(_agent.transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            areaMask = hit.mask;
        }
    }

    private void Attack()
    {
        if (isAttackMoving)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, weapon.WeaponAttackRange, monsterLayerMask);

            foreach (Collider2D attackRangeCol in hitColliders)
            {
                _agent.isStopped = true;
                _agent.velocity = Vector2.zero;
                isAttackMoving = false;
                employeeCurrentStatus = EmployeeFsm.Battle;
                attackRangeCol.GetComponent<Enemy>().BeAttacked(); // 이거 히트스캔이면 이래야됨
                Debug.Log("Found Collider: " + attackRangeCol.name);
            }
        }
    }
    

    private void Waiting() { }
    private void Moving() { }
    private void Working() { }
    private void Fighting() { }
    private void StatusAbnormality() { }
}
