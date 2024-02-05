using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class RoomList
{
    public List<Vector3> RoomLocate;
}

public class RoomManager : Singleton<RoomManager>
{
    public GameObject Room;

    public List<RoomList> Department_Room; // 룸 지정 번호는 1 2 3 4 로 했을 때 왼쪽이 1 2 부서 3 4 형식

    public void MainSet()
    {
        for (int Depart = 0; Depart < DataManager.Instance.MainDataLoad().Department.Count; Depart++)
        {
            for (int i = 0; i < DataManager.Instance.MainDataLoad().Department[Depart].MonsterList.Count; i++)
            {
                var MonRoom = Instantiate(Room);
                MonRoom.transform.position = Department_Room[Depart].RoomLocate[i];
                var monroom = MonRoom.GetComponent<Room_Select_Manager>();
                monroom._monName = DataManager.Instance.MonsterDataLoad(DataManager.Instance.MainDataLoad().Department[Depart].MonsterList[i]).profile.MonsterName;
                monroom._monfileName = DataManager.Instance.MainDataLoad().Department[Depart].MonsterList[i];
                monroom.DepartLocate = Depart;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
