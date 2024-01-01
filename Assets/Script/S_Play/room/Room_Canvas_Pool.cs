using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Room_Canvas_Pool : MonoBehaviour
{
    public static Room_Canvas_Pool instance = null;

    public GameObject Canvas_Prefab;

    private GameObject Summoned_Canvas;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartPool();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartPool()
    {
        Summoned_Canvas = Instantiate(Canvas_Prefab);
        Summoned_Canvas.transform.SetParent(gameObject.transform);
        Summoned_Canvas.SetActive(false);
    }

    public void GetCanvas()
    {
        //Debug.Log("캔버스 켜짐");
        Summoned_Canvas.transform.SetParent(null);
        Summoned_Canvas.SetActive(true);
    }

    public void ReturnCanvas()
    {
        Summoned_Canvas.transform.SetParent(gameObject.transform);
        Summoned_Canvas.SetActive(false);
    }
}
