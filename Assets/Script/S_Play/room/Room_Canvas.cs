using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Room_Canvas : Singleton<Room_Canvas>
{
    public GameObject Work_Canvas;

    void Start()
    {
        OffCanvas();
    }
    public void OffCanvas()
    {
        Work_Canvas.SetActive(false);
    }

    public void OnCanvas()
    {
        Work_Canvas.SetActive(true);
    }
}
