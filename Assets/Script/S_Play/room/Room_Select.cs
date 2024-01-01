using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Room_Select : MonoBehaviour
{

    public LayerMask clickableLayer;


    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 감지
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 클릭 위치를 2D 좌표로 변환
            RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero); // Raycast로 해당 위치에 오브젝트 감지

            if (hit.collider != null) // 충돌체가 있을 경우
            {
                if (hit.collider.CompareTag("Room")) // Room 태그를 갖는 오브젝트를 클릭했다면
                {
                    Room_Canvas_Pool.instance.GetCanvas(); // UI 활성화하는 코드 실행
                }
            }

            else if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                // 아무런 오브젝트와 충돌하지 않았을 때의 처리                
                Debug.Log("No object clicked.");
                Room_Canvas_Pool.instance.ReturnCanvas();
            }
        }
    }

    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = Color.blue;
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
    }
}
