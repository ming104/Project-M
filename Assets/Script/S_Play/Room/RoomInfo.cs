using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshPro MonName_text;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MouseManager.Instance.MouseInteractionOn == true)
        {
            var monsterData = DataManager.Instance.MonsterDataLoad(GetComponentInParent<Room_Select_Manager>()._monfileName);

            UI_Manager.Instance.InfoCanvasOn(monsterData);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
        MonName_text.color = Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<SpriteRenderer>().color = Color.black;
        MonName_text.color = Color.white;
    }
}
