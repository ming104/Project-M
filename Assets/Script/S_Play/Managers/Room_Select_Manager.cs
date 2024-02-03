using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class Room_Select_Manager : MonoBehaviour
{
    public TextMeshPro textName;

    [SerializeField] private string monsterName;
    public string _monName
    {
        set { monsterName = value; }
        get { return monsterName; }
    }

    [SerializeField] private int Depart_locate;
    public int DepartLocate
    {
        set { Depart_locate = value; }
        get { return Depart_locate; }
    }

    // Start is called before the first frame update
    void Start()
    {
        textName.text = _monName;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
