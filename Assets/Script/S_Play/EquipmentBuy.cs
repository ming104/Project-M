using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentBuy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
        if (monsterData.MonEquipment.buyMoney <= GameManager.Instance.nowMoney
            && monsterData.MonEquipment.buyRP <= GameManager.Instance.nowResearchPoint
            && monsterData.MonEquipment.maximumCount >
            DataManager.Instance.EquipmentCountLoad(monsterData.MonEquipment.EquipName, monsterData.profile.riskLevel))
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
        if (monsterData.MonEquipment.buyMoney <= GameManager.Instance.nowMoney
            && monsterData.MonEquipment.buyRP <= GameManager.Instance.nowResearchPoint
            && monsterData.MonEquipment.maximumCount >
            DataManager.Instance.EquipmentCountLoad(monsterData.MonEquipment.EquipName, monsterData.profile.riskLevel))
        {
            DataManager.Instance.EquipmentCreate(monsterData.MonEquipment.EquipName, monsterData.profile.riskLevel);
            GameManager.Instance.nowMoney -= monsterData.MonEquipment.buyMoney;
            GameManager.Instance.nowResearchPoint -= monsterData.MonEquipment.buyRP;
        }
    }
}
