using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MouseManager : Singleton<MouseManager>
{
    public bool MouseInteractionOn = true;
    public bool isAttack;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && isAttack == false && MouseInteractionOn && Selection_Obj.Instance.isSelect)
        {
            isAttack = true;
            UI_Manager.Instance.attackOnTextActive(true);
            Selection_Obj.Instance.Select_Interaction = false;
        }
        
        Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && MouseInteractionOn && isAttack) // 마우스 왼쪽 버튼 클릭 감지&& !EventSystem.current.IsPointerOverGameObject()
        {
            //Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 클릭 위치를 2D 좌표로 변환
            //RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero); // Raycast로 해당 위치에 오브젝트 감지

            if (Selection_Obj.Instance.isSelect == true)
            {
                for (int i = 0; i < Selection_Obj.Instance.SelectOBJ.Count; i++)
                {
                    var SelectEmp = Selection_Obj.Instance.SelectOBJ[i].GetComponent<Employee>();
                    SelectEmp.AttackDestinationMoving(new Vector3(clickPos.x, clickPos.y, transform.position.z));
                    SelectEmp.EmployeeCurrentStatus = Employee.EmployeeFsm.Moving;
                    UI_Manager.Instance.attackOnTextActive(false);
                    Selection_Obj.Instance.Select_Interaction = true;
                    isAttack = false;
                }

                Selection_Obj.Instance.DeSelect_Obj();
            }
        }

        if (Input.GetMouseButtonDown(1) && MouseInteractionOn == true) // 마우스 왼쪽 버튼 클릭 감지&& !EventSystem.current.IsPointerOverGameObject()
        {
            //Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 클릭 위치를 2D 좌표로 변환
            //RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero); // Raycast로 해당 위치에 오브젝트 감지
            //Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Selection_Obj.Instance.isSelect == true)
            {
                if (isAttack && MouseInteractionOn)
                {
                    isAttack = false;
                    Selection_Obj.Instance.Select_Interaction = true;
                    UI_Manager.Instance.attackOnTextActive(false);
                    return;
                }
                for (int i = 0; i < Selection_Obj.Instance.SelectOBJ.Count; i++)
                {
                    var SelectEmp = Selection_Obj.Instance.SelectOBJ[i].GetComponent<Employee>();
                    SelectEmp.DestinationMoving(new Vector3(clickPos.x, clickPos.y, transform.position.z));
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
