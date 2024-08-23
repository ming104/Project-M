using System.Collections.Generic;
using UnityEngine;

public class Camera_Manager : Singleton<Camera_Manager>
{
    [Header("카메라")]
    [SerializeField] private Camera Main_Camera; // 카메라

    [Header("WASD로 카메라 움직임 속도")]
    [SerializeField] private float CameraMovementSpeed = 7f; // WASD카메라 움직임
    private Vector3 MousePos; // 마우스 포지션 전역변수
    [Header("커서 모양")]
    public Texture2D defaultTexture;
    public Texture2D screenMoveTexture;
    public Texture2D screenMoveUp;
    public Texture2D screenMoveDown;
    public Texture2D screenMoveRight;
    public Texture2D screenMoveLeft;

    public bool CamInteractionOn = true;
    [Header("최대 x값, 최소 x값")]
    public float MaxPosX;
    public float MinPosX;
    
    public float edgeThickness = 10f; // 화면 가장자리에 가까워졌을 때의 거리
    
    private Dictionary<KeyCode, Vector3> cameraPositions = new Dictionary<KeyCode, Vector3>
    {
        { KeyCode.Alpha1, new Vector3(0, 0, -10) },
        { KeyCode.Alpha2, new Vector3(300, 0, -10) }
        // 필요한 만큼 추가하면 됨
    };
    
    void Start()
    {
        Main_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        
        // 커서를 화면 내로 제한
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true; // 커서를 보이게 함
    }

    // Update is called once per frame
    void Update()
    {
        if (CamInteractionOn == true)
        {
            CameraMove(); // 카메라 움직임
            CameraSize(); // 카메라 휠로 크기조절
            FloorChange();// 층 이동시 바로 이동시키기 워한 코드
        }
    }

    void FloorChange() // 나중에 질문---
    {
        if (Input.anyKeyDown)
        {
            foreach (var entry in cameraPositions)
            {
                if (Input.GetKeyDown(entry.Key))
                {
                    SetCameraPosition(entry.Value);
                    break; // 위치가 설정되면 반복 종료
                }
            }
        }

        void SetCameraPosition(Vector3 newPosition)
        {
            Main_Camera.transform.position = newPosition;
        }
    }

    void CameraMove()
    {
        //var asdf = Mathf.Clamp(Main_Camera.transform.position.x, MinPosX, MaxPosX);
        if (Input.GetMouseButtonDown(2))//Input.GetMouseButtonDown(1) || 
        {
            Cursor.SetCursor(screenMoveTexture, Vector2.zero, CursorMode.ForceSoftware);
            MousePos = Main_Camera.ScreenToWorldPoint(Input.mousePosition); // 마우스 포지션을 먼저 지정하고
        }
        if (Input.GetMouseButton(2))//Input.GetMouseButton(1) || 
        {
            Vector3 moved_Mouse = MousePos - Main_Camera.ScreenToWorldPoint(Input.mousePosition); // 움직인 좌표를 구함
            Main_Camera.transform.position += moved_Mouse; // 그것 만큼 더해서 이동시킴
        }
        if (Input.GetMouseButtonUp(2))//Input.GetMouseButtonUp(1) || 
        {
            Cursor.SetCursor(defaultTexture, Vector2.zero, CursorMode.ForceSoftware);
        }
        
     

    
        Vector3 mousePosition = Input.mousePosition;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // 화면의 가장자리에서 마우스 위치를 확인
        if (mousePosition.x < edgeThickness)
        {
            Main_Camera.transform.Translate(Vector2.left * CameraMovementSpeed * Time.unscaledDeltaTime);
            //Cursor.SetCursor(screenMoveLeft, Vector2.zero, CursorMode.ForceSoftware);
            //Debug.Log("left");
        }
        else if (mousePosition.x > screenWidth - edgeThickness)
        {
            Main_Camera.transform.Translate(Vector2.right * CameraMovementSpeed * Time.unscaledDeltaTime);
            //Cursor.SetCursor(screenMoveRight, Vector2.zero, CursorMode.ForceSoftware);
            //Debug.Log("right");
        }

        if (mousePosition.y < edgeThickness)
        {
            Main_Camera.transform.Translate(Vector2.down * CameraMovementSpeed * Time.unscaledDeltaTime);
            //Cursor.SetCursor(screenMoveDown, Vector2.zero, CursorMode.ForceSoftware);
            //Debug.Log("down");
        }
        else if (mousePosition.y > screenHeight - edgeThickness)
        {
            Main_Camera.transform.Translate(Vector2.up * CameraMovementSpeed * Time.unscaledDeltaTime);
            //Cursor.SetCursor(screenMoveUp, Vector2.zero, CursorMode.ForceSoftware);
            //Debug.Log("up");
        }
    

        // if (Input.GetKey(KeyCode.W))
        // {
        //     Main_Camera.transform.Translate(Vector2.up * CameraMovementSpeed * Time.unscaledDeltaTime);
        //     // 이동시키는 코드 시간이 멈췄을 때 움직이는것이 끊기면 안되니까 UnscaledDeltaTime을 사용함
        // }
        // if (Input.GetKey(KeyCode.A))
        // {
        //     Main_Camera.transform.Translate(Vector2.left * CameraMovementSpeed * Time.unscaledDeltaTime);
        //     // 이동시키는 코드 시간이 멈췄을 때 움직이는것이 끊기면 안되니까 UnscaledDeltaTime을 사용함
        // }
        // if (Input.GetKey(KeyCode.S))
        // {
        //     Main_Camera.transform.Translate(Vector2.down * CameraMovementSpeed * Time.unscaledDeltaTime);
        //     // 이동시키는 코드 시간이 멈췄을 때 움직이는것이 끊기면 안되니까 UnscaledDeltaTime을 사용함
        // }
        // if (Input.GetKey(KeyCode.D))
        // {
        //     Main_Camera.transform.Translate(Vector2.right * CameraMovementSpeed * Time.unscaledDeltaTime);
        //     // 이동시키는 코드 시간이 멈췄을 때 움직이는것이 끊기면 안되니까 UnscaledDeltaTime을 사용함
        // }

    }

    void CameraSize() //orthographicSize가 클수록 오브젝트가 작아짐
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0) // 올렸을 때 처리 -> 오브젝트 확대
        {
            if (Main_Camera.orthographicSize > 2)
            {
                Main_Camera.orthographicSize -= 1; // 확대를 해야하니 스케일을 줄여 보이는 범위를 줄임
            }
        }
        else if (wheelInput < 0) // 내렸을 때 처리 -> 오브젝트 축소
        {
            if (Main_Camera.orthographicSize < 40)
            {
                Main_Camera.orthographicSize += 1;// 축소를 해야하니 스케일을 늘려 보이는 범위를 늘림
            }
        }
    }
}
