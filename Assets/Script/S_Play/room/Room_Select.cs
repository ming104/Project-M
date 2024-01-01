using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Room_Select : MonoBehaviour
{
    public string MonName;
    public LayerMask clickableLayer;

    //string PATH = "GameData/monster/" + MonName + ".json";


    void Awake()
    {

    }

    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = Color.blue;
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    public string ReturnMonsterName()
    {
        return MonName;
    }
}
