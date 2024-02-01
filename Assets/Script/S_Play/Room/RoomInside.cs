using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomInside : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && MouseManager.Instance.MouseInteractionOn == true)
        {
            var monsterData = DataManager.Instance.MonsterDataLoad(GetComponentInParent<Room_Select_Manager>()._monName);
            UI_Manager.Instance.WorkCanvasOn(monsterData); //UI 활성화하는 코드 실행

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<SpriteRenderer>().color = Color.blue;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<SpriteRenderer>().color = Color.green;
    }
}

