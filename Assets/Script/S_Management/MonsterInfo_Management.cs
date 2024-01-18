using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo_Management : MonoBehaviour
{
    [SerializeField] private string monsterName;
    public string _monName
    {
        set { monsterName = value; }
        get { return monsterName; }
    }
}
