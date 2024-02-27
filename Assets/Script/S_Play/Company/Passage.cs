using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Passage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) // 클릭 시
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (Selection_Obj.Instance.isSelect == false)
            {
                return;
            }
            else
            {
                for (int i = 0; i < Selection_Obj.Instance.SelectOBJ.Count; i++)
                {
                    //Debug.Log(Selection_Obj.Instance.SelectOBJ[i].name);
                    Selection_Obj.Instance.SelectOBJ[i].GetComponent<Astar>().targetPos = new Vector2Int((int)transform.position.x, (int)Mathf.Floor(transform.position.y));
                    Selection_Obj.Instance.SelectOBJ[i].GetComponent<Astar>().PathFinding();
                }
                Selection_Obj.Instance.DeSelect_Obj();
            }
        }
        else
        {
            return;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) // 호버 시작
    {

    }

    public void OnPointerExit(PointerEventData eventData) // 호버 끝
    {

    }
}
