using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image weaponImage;
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI weaponPerformance;
    public string weaponNameString;
    public EmployeeData empData;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DataManager.Instance.EquipmentEquip(empData.name,weaponNameString, 0);
        ManagementManager.Instance.WeaponChangeMethod(empData);
        ManagementManager.Instance.AffiliatedEmployee_Panel_Reset(empData.name, 0);
    }
}
