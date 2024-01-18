using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    public GameObject Room;

    public Vector3[] RoomLocate; // 룸 지정 번호는 1 2 3 4 로 했을 때 왼쪽이 1 2 부서 3 4 형식

    public List<List<string>> Monsters = new List<List<string>>();

    public void MainSet()
    {
        //Debug.Log(DataManager.Instance.MainDataLoad().Department[0].MonsterList[0]);
        //Monsters = DataManager.Instance.MainDataLoad().Department[0].MonsterList.Count; // 수정필요
        for (int i = 0; i < DataManager.Instance.MainDataLoad().Department[0].MonsterList.Count; i++)
        {
            Room.GetComponent<Room_Select_Manager>()._monName = DataManager.Instance.MainDataLoad().Department[0].MonsterList[i];
            Instantiate(Room, RoomLocate[i], quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
