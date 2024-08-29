using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentBuyManagement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public MonsterData monsterData;
    public GameObject buyPossiblePanel;
    public GameObject buyImpossiblePanel;

    void Start()
    {
        buyPossiblePanel.SetActive(false);
        buyImpossiblePanel.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        var mainData = DataManager.Instance.MainDataLoad();
        if (monsterData.MonEquipment.buyMoney <= ManagementManager.Instance.currentMoney
            && monsterData.MonEquipment.buyRP <= ManagementManager.Instance.currentRP
            && monsterData.MonEquipment.maximumCount >
            DataManager.Instance.EquipmentCountLoad(monsterData.MonEquipment.EquipName,  monsterData.MonEquipment.type))
        {
            buyPossiblePanel.SetActive(true);
        }
        else
        {
            buyImpossiblePanel.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buyPossiblePanel.SetActive(false);
        buyImpossiblePanel.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var mainData = DataManager.Instance.MainDataLoad();
        if (monsterData.MonEquipment.buyMoney <= ManagementManager.Instance.currentMoney
            && monsterData.MonEquipment.buyRP <= ManagementManager.Instance.currentRP
            && monsterData.MonEquipment.maximumCount >
            DataManager.Instance.EquipmentCountLoad(monsterData.MonEquipment.EquipName,  monsterData.MonEquipment.type))
        {
            DataManager.Instance.EquipmentCreate(monsterData.MonEquipment.EquipName, monsterData.MonEquipment.type);
            ManagementManager.Instance.currentMoney -= monsterData.MonEquipment.buyMoney;
            ManagementManager.Instance.currentRP -= monsterData.MonEquipment.buyRP;
            DataManager.Instance.MaindataSaveManagement();
        }
    }
}
