using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Selection_Obj : MonoBehaviour
{
    public static Selection_Obj instance = null;

    [Header("드래그 범위 설정")]
    public LayerMask unitLayerMask; // 유닛 레이어 마스크
    [SerializeField] private GameObject dragSquare; // 프리팹 범위 설정
    private GameObject square; // 담아둘 오브젝트
    private Vector3 startPos, nowPos, deltaPos; // 시작 좌표, 지금좌표, 중심점
    private float deltaX, deltaY; // 중심점을 구하기위한 좌표

    [Header("선택된 obj")]
    public List<GameObject> SelectOBJ = new List<GameObject>();

    public bool isSelect;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UnitSelect();
    }

    public void Select_Obj(GameObject obj)
    {
        SelectOBJ.Add(obj);
        isSelect = true;
    }

    public void DeSelect_Obj()
    {
        SelectOBJ.Clear();
        isSelect = false;
    }
    void UnitSelect()
    {
        if (Input.GetMouseButtonDown(0)) // 눌렀을 때 영역 그리기 시작
        {
            DeSelect_Obj();
            startPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));
            square = Instantiate(dragSquare, new Vector3(0, 0, 0), Quaternion.identity); // 사각형 소환
        }

        if (Input.GetMouseButton(0)) // 드래그 중
        {
            nowPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));
            deltaX = Mathf.Abs(nowPos.x - startPos.x); // 거리를 절댓값으로 바꿔 스케일을 정함
            deltaY = Mathf.Abs(nowPos.y - startPos.y); // 거리를 절댓값으로 바꿔 스케일을 정함
            deltaPos = startPos + (nowPos - startPos) / 2; // 중심점을 구하는 코드
            square.transform.position = deltaPos; // 시작점과 마우스의 사이에 거리중 중간에 포지션을 위치시킴
            square.transform.localScale = new Vector3(deltaX, deltaY, 0); // 스케일을 정함
        }

        if (Input.GetMouseButtonUp(0)) // 드래그가 끝나면 영역 사각형 삭제
        {
            Collider2D[] hitColliders = Physics2D.OverlapAreaAll(startPos, nowPos, unitLayerMask);
            // 콜라이더를 받아오고 OverlapAreaAll를 사용해서 startPos,nowPos 범위 안에있는 레이어를 받아오고 unitLayerMask 이게 직원을 가리키고 있어서 직원을 불러옴
            foreach (Collider2D collider in hitColliders) // foreach문으로 hitColliders를 받아오고 콜라이더로 넣어준뒤
            {
                GameObject unit = collider.gameObject; //유닛에다가 인식된 콜라이더의 게임오브젝트를 넣어줌
                Select_Obj(unit); // 배열에다가 넣어둠
            }
            Destroy(square); // 삭제
        }
    }

}
