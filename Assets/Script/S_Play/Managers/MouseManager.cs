using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MouseManager : Singleton<MouseManager>
{
    public bool MouseInteractionOn = true;

    void Update()
    {
        // Vector2 clickPosHover = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 클릭 위치를 2D 좌표로 변환
        // RaycastHit2D hit = Physics2D.Raycast(clickPosHover, Vector2.zero);
        
        
        
        if (Input.GetMouseButtonDown(1) && MouseInteractionOn == true) // 마우스 왼쪽 버튼 클릭 감지&& !EventSystem.current.IsPointerOverGameObject()
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 클릭 위치를 2D 좌표로 변환
            //RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero); // Raycast로 해당 위치에 오브젝트 감지
            
            if (Selection_Obj.Instance.isSelect == true)
            {
                for (int i = 0; i < Selection_Obj.Instance.SelectOBJ.Count; i++)
                {
                    var SelectEmp = Selection_Obj.Instance.SelectOBJ[i].GetComponent<Employee>();
                    SelectEmp.DestinationMoving(new Vector3( clickPos.x, clickPos.y, transform.position.z));
                    SelectEmp.EmployeeCurrentStatus = Employee.EmployeeFsm.Moving;
                }

                Selection_Obj.Instance.DeSelect_Obj();
            }


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
