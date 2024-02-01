using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : Singleton<MouseManager>
{
    public bool MouseInteractionOn = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && MouseInteractionOn == true && !EventSystem.current.IsPointerOverGameObject()) // 마우스 왼쪽 버튼 클릭 감지
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 클릭 위치를 2D 좌표로 변환
            RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero); // Raycast로 해당 위치에 오브젝트 감지

            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                // 아무런 오브젝트와 충돌하지 않았을 때의 처리                
                //Debug.Log("No object clicked.");
                UI_Manager.Instance.WorkCanvasOff();
                UI_Manager.Instance.EmployeeSelectcancel();
            }
        }
    }
}
