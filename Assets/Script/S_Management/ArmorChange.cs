using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArmorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image armorImage;
    public TextMeshProUGUI armorName;
    public TextMeshProUGUI armorPerformance;
    public string armorNameString;
    public EmployeeData empData;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DataManager.Instance.EquipmentEquip(empData.name,armorNameString, 1);
        ManagementManager.Instance.ArmorChangeMethod(empData);
        ManagementManager.Instance.AffiliatedEmployee_Panel_Reset(empData.name, 1);
    }
}
