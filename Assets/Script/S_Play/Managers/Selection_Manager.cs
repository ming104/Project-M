using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selection_Manager : MonoBehaviour, IPointerClickHandler
{
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log(tag);
        }
    }
}
