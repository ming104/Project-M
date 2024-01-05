using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (UI_Manager.Instance.Enegy_Slider.value >= 1)
        {
            UI_Manager.Instance.EndButtonOn();
        }
    }
}
