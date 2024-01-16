using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    public GameObject Room;

    public Vector3[] RoomLocate; // 룸 지정 번호는 1 2 3 4 로 했을 때 왼쪽이 1 2 부서 3 4 형식

    public List<List<string>> Monsters;

    public void MainSet()
    {
        Debug.Log(DataManager.Instance.MainDataLoad().DepartmentMonster);
        Monsters = DataManager.Instance.MainDataLoad().DepartmentMonster; // 수정필요
        for (int i = 0; i < Monsters[0].Count; i++)
        {
            Room.GetComponent<Room_Select_Manager>()._monName = Monsters[0][i];
            Instantiate(Room, RoomLocate[i], quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
