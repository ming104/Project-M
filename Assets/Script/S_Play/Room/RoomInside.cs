using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomInside : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<SpriteRenderer>().color = Color.blue;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && MouseManager.Instance.MouseInteractionOn == true && eventData.pointerDrag == false)
        {
            var monsterData = DataManager.Instance.MonsterDataLoad(GetComponentInParent<Room_Select_Manager>()._monfileName);
            UI_Manager.Instance.pri_monname = GetComponentInParent<Room_Select_Manager>()._monfileName;
            int monster_depart = GetComponentInParent<Room_Select_Manager>().DepartLocate;
            UI_Manager.Instance.WorkCanvasOn(monsterData, monster_depart); //UI 활성화하는 코드 실행

        }
    }
}

