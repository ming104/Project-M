using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class RoomList
{
    public List<GameObject> RoomLocate;
}

public class RoomManager : Singleton<RoomManager>
{
    public GameObject Room;

    public List<RoomList> Department_Room; //1층마다 최대 16개의 격리시설 존재
    /*
    크리쳐 배치 순서
      9        10
          1 2
    12  4     7  15
    11  3     8  16
          6 5
      13       14
    // 이런 형태 1층마다 저렇게 생김
    */

    public void MainSet()
    {
        for (int f = 0; f < DataManager.Instance.MainDataLoad().Floor.Count; f++)
        {
            for (int Depart = 0; Depart < DataManager.Instance.MainDataLoad().Floor[f].Department.Count; Depart++)
            {
                for (int i = 0; i < DataManager.Instance.MainDataLoad().Floor[f].Department[Depart].MonsterList.Count; i++)
                {
                    var MonRoom = Instantiate(Room);
                    MonRoom.transform.position = Department_Room[Depart].RoomLocate[i].transform.position;
                    var monroom = MonRoom.GetComponent<Room_Select_Manager>();
                    monroom._monName = DataManager.Instance.MonsterDataLoad(DataManager.Instance.MainDataLoad().Floor[f].Department[Depart].MonsterList[i]).profile.MonsterName;
                    monroom._monfileName = DataManager.Instance.MainDataLoad().Floor[f].Department[Depart].MonsterList[i];
                    monroom.DepartLocate = Depart;

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
