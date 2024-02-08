using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLayoutGroup : MonoBehaviour
{
    public GameObject SuccessRePo; // 쌓일 오브젝트 프리팹
    public GameObject FailRePo;
    public int RePoNumber;
    //public float spacing = 1.0f; // 오브젝트 간격
    void Start()
    {
        RePoNumber = 0; ;
    }

    public void StackObjects(int maxRePo, bool Results)
    {
        // // 이전에 생성된 오브젝트가 있다면 삭제
        // foreach (Transform child in transform)
        // {
        //     Destroy(child.gameObject);
        // }

        // 오브젝트를 차곡차곡 쌓기
        Vector3 RePoPosition = new Vector3(0f, RePoNumber * SuccessRePo.transform.localScale.y, 0f);
        if (Results == true)
        {
            GameObject newObj = Instantiate(SuccessRePo);
            newObj.transform.SetParent(transform); // 부모 설정
            newObj.transform.localPosition = RePoPosition;
        }
        else
        {
            GameObject newObj = Instantiate(FailRePo);
            newObj.transform.SetParent(transform); // 부모 설정
            newObj.transform.localPosition = RePoPosition;
        }
        RePoNumber++;
    }
}
