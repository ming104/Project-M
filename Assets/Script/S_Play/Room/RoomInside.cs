using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomInside : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInParent<Room_Select_Manager>().RoomStatusResearchActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInParent<Room_Select_Manager>().RoomStatusResearchActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && MouseManager.Instance.MouseInteractionOn == true && eventData.pointerDrag == false)
        {
            var monsterData = GetComponentInParent<Room_Select_Manager>().RoomMonsterData;
            UI_Manager.Instance.monsterData = GetComponentInParent<Room_Select_Manager>().RoomMonsterData;
            int monster_depart = GetComponentInParent<Room_Select_Manager>().DepartLocate;
            UI_Manager.Instance.roomSelectManager = GetComponentInParent<Room_Select_Manager>();
            UI_Manager.Instance.WorkCanvasOn(monsterData, monster_depart); //UI 활성화하는 코드 실행
            UI_Manager.Instance.roomPos = GetComponentInParent<Room_Select_Manager>().RoomPos;
        }
    }
}

