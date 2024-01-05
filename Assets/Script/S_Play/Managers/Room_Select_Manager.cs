using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room_Select_Manager : MonoBehaviour
{
    //public GameObject InfoCanvas;

    [SerializeField] private string monsterName;
    public string _monName
    {
        set { monsterName = value; }
        get { return monsterName; }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // private void OnMouseEnter()
    // {
    //     GetComponent<SpriteRenderer>().color = Color.yellow;
    // }

    // private void OnMouseExit()
    // {
    //     GetComponent<SpriteRenderer>().color = Color.black;
    // }
}
