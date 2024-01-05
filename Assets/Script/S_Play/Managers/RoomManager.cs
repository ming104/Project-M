using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject Room;

    public Vector3[] RoomLocate; // 룸 지정 번호는 1 2 3 4 로 했을 때 왼쪽이 1 2 부서 3 4 형식

    public string[] Monsters;

    // Start is called before the first frame update
    void Awake()
    {
        Monsters = DataManager.Instance.MainData().MonsterList;
    }

    void Start()
    {
        for (int i = 0; i < Monsters.Length; i++)
        {
            Room.GetComponent<Room_Select_Manager>()._monName = Monsters[i];
            Instantiate(Room, RoomLocate[i], quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
