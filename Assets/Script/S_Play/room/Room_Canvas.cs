using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Room_Canvas : MonoBehaviour
{
    public static Room_Canvas instance = null;

    public GameObject Work_Canvas;

    void Awake()
    {
        instance = this;
    }

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
