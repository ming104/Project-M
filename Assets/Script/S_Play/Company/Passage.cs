using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Passage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
                    NavMeshAgent Selection_Nav = Selection_Obj.Instance.SelectOBJ[i].GetComponent<NavMeshAgent>();
                    Selection_Nav.destination = transform.position;

                    // if (Selection_Nav.destination == transform.position)
                    // {
                    //     Selection_Nav.ResetPath(); // 이동했을 때 움직임 멈추는 코드
                    // }
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
