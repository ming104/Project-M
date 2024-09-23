using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CurrentEquipWeapon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public string empName;
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //DataManager.Instance.EmployeeDataLoad(empName)
        ManagementManager.Instance.WeaponChangeMethod(DataManager.Instance.EmployeeDataLoad(empName));
    }
}
