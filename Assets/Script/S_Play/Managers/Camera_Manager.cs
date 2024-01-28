using UnityEngine;

public class Camera_Manager : Singleton<Camera_Manager>
{
    [Header("카메라")]
    [SerializeField] private Camera Main_Camera; // 카메라

    [Header("WASD로 카메라 움직임 속도")]
    [SerializeField] private float CameraMovementSpeed = 7f; // WASD카메라 움직임
    private Vector3 MousePos; // 마우스 포지션 전역변수
    [Header("커서 모양")]
    public Texture2D CursorTexture;

    public bool CamInteractionOn = true;
    [Header("최대 x값, 최소 x값")]
    public float MaxPosX;
    public float MinPosX;
    void Start()
    {
        Main_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CamInteractionOn == true)
        {
            CameraMove(); // 카메라 움직임
            CameraSize(); // 카메라 휠로 크기조절
        }
    }

    void CameraMove()
    {
        //var asdf = Mathf.Clamp(Main_Camera.transform.position.x, MinPosX, MaxPosX);
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            //Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.ForceSoftware);
            MousePos = Main_Camera.ScreenToWorldPoint(Input.mousePosition); // 마우스 포지션을 먼저 지정하고
        }
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            Vector3 moved_Mouse = MousePos - Main_Camera.ScreenToWorldPoint(Input.mousePosition); // 움직인 좌표를 구함
            Main_Camera.transform.position += moved_Mouse; // 그것 만큼 더해서 이동시킴
        }
        if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
        {
            //Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        }

        if (Input.GetKey(KeyCode.W))
        {
            Main_Camera.transform.Translate(Vector2.up * CameraMovementSpeed * Time.unscaledDeltaTime);
            // 이동시키는 코드 시간이 멈췄을 때 움직이는것이 끊기면 안되니까 UnscaledDeltaTime을 사용함
        }
        if (Input.GetKey(KeyCode.A))
        {
            Main_Camera.transform.Translate(Vector2.left * CameraMovementSpeed * Time.unscaledDeltaTime);
            // 이동시키는 코드 시간이 멈췄을 때 움직이는것이 끊기면 안되니까 UnscaledDeltaTime을 사용함
        }
        if (Input.GetKey(KeyCode.S))
        {
            Main_Camera.transform.Translate(Vector2.down * CameraMovementSpeed * Time.unscaledDeltaTime);
            // 이동시키는 코드 시간이 멈췄을 때 움직이는것이 끊기면 안되니까 UnscaledDeltaTime을 사용함
        }
        if (Input.GetKey(KeyCode.D))
        {
            Main_Camera.transform.Translate(Vector2.right * CameraMovementSpeed * Time.unscaledDeltaTime);
            // 이동시키는 코드 시간이 멈췄을 때 움직이는것이 끊기면 안되니까 UnscaledDeltaTime을 사용함
        }

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
