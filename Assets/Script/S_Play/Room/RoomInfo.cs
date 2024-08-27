using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (MouseManager.Instance.MouseInteractionOn == true)
        {
            var monsterData = GetComponentInParent<Room_Select_Manager>().RoomMonsterData;

            UI_Manager.Instance.InfoCanvasOn(monsterData);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<SpriteRenderer>().color = new Color32(221,219,158, 255);
        GetComponentInParent<Room_Select_Manager>().textName.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponentInParent<Room_Select_Manager>().textName.color = Color.black;
    }
}
