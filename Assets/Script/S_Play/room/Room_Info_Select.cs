using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Info_Select : Singleton<Room_Info_Select>
{
    public GameObject InfoCanvas;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InfoCanvasON()
    {
        DataManager.Instance.DataLoad("Dummy");
        InfoCanvas.SetActive(true);
    }

    public void InfoCanvasOFF()
    {
        InfoCanvas.SetActive(false);
    }

    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
    }
}
