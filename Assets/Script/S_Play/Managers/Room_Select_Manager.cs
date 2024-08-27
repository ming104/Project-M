using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Room_Select_Manager : MonoBehaviour
{
    public TextMeshProUGUI textName;
    [SerializeField] private string monsterName;
    public string _monName
    {
        set { monsterName = value; }
        get { return monsterName; }
    }

    [SerializeField] private MonsterData roomMonsterData;
    public MonsterData RoomMonsterData
    {
        set { roomMonsterData = value; }
        get { return roomMonsterData; }
    }

    [SerializeField] private int Depart_locate;
    public int DepartLocate
    {
        set { Depart_locate = value; }
        get { return Depart_locate; }
    }

    [SerializeField] private Vector3 Room_locate;
    public Vector3 RoomPos
    {
        get { return Room_locate; }
    }

    public int escapeCount;
    
    //RoomStatus
    public GameObject roomStatusResearch;
   //public GameObject roomStatusResearching;

    public Sprite roomStatusResearchStart;
    public Sprite roomStatusResearching;
    //public Image roomStatusResearchSt;
    
    public bool isResearching;

    public Slider researchStatusSlider;

    // Start is called before the first frame update 22.5 7     26 4.8 
    void Start()
    {
        textName.text = _monName;
        Room_locate = new Vector3(transform.position.x + 3.5f, transform.position.y - 2.2f, transform.position.z);
        
        researchStatusSlider.minValue = 0;
        researchStatusSlider.maxValue = 100;
        researchStatusSlider.value = 50;
    }

    public void RoomStatusResearchActive(bool active)
    {
        roomStatusResearch.SetActive(active);
    }
    
    public void StartResearch(int index, Employee employee)
    {
        StartCoroutine(Probabilitytask(index, employee));
        isResearching = true;
    }
    
    private IEnumerator Probabilitytask(int index, Employee employee)
    {
        int sum = 0;
        int nsum = 0;
        int RePo = roomMonsterData.profile.riskLevel * 10; // riskLevel * 10 <- 감정 움직일 갯수
        int persent = 0;
        switch (index)
        {
            case 1:
                persent = roomMonsterData.Research_Preferences.FEAR;
                break;
            case 2: 
                persent = roomMonsterData.Research_Preferences.ANGER;
                break;
            case 3: 
                persent = roomMonsterData.Research_Preferences.DISGUST;
                break;
            case 4: 
                persent = roomMonsterData.Research_Preferences.SAD;
                break;
            case 5: 
                persent = roomMonsterData.Research_Preferences.HAPPY;
                break;
            case 6: 
                persent = roomMonsterData.Research_Preferences.SURPRISE;
                break;
        }
        
        for (int i = 0; i < RePo; i++) 
        {
            var RanNum = Random.Range(0, 100);

            if (RanNum <= persent)
            {
                sum++;
                ResearchStatus(RePo, true);
            }

            else
            {
                nsum++;
                ResearchStatus(RePo, false);
            }
            yield return new WaitForSeconds(0.6f);
        }
        ResetStatus();
        Debug.Log($"성공 : {sum}, 실패 : {nsum}");
        isResearching = false;
        employee.EmployeeCurrentStatus = Employee.EmployeeFsm.Wait;
        employee.ResetDestinationMoving();
        UI_Manager.Instance.IncreasedEnergy(sum);
        GameManager.Instance.sumResearchPoint += RePo / 10;
    }

    private void ResearchStatus(int maxRePo, bool Results)
    {
        int feeling = 0;
        switch (maxRePo)
        {
            case 10:
                feeling = 5;
                break;
            case 20:
                feeling = 4;
                break;
            case 30:
                feeling = 3;
                break;
            case 40:
                feeling = 2;
                break;
            case 50:
                feeling = 1;
                break;
        }
        
        if (Results == true)
        {
            researchStatusSlider.value += feeling;

        }
        else
        {
            researchStatusSlider.value -= feeling;
        }
    }

    private void ResetStatus()
    {
        StartCoroutine("ResetFeeling");
    }

    private IEnumerator ResetFeeling()
    {
        yield return new WaitForSeconds(5f);
        if (researchStatusSlider.value < 50)
        {
            while (researchStatusSlider.value < 50)
            {
                researchStatusSlider.value += 1;
                yield return new WaitForSeconds(0.1f);
            }
        }
        if (researchStatusSlider.value > 50)
        {
            while (researchStatusSlider.value > 50)
            {
                researchStatusSlider.value -= 1;
                //연구 포인트? 증가
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
