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
        RePoNumber = 0;
    }

    public void ChildDestory()
    {
        // // 이전에 생성된 오브젝트가 있다면 삭제
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
            RePoNumber = 0;
        }
    }

    public void StackObjects(int maxRePo, bool Results)
    {

        switch (maxRePo)
        {
            case 10:
                transform.localPosition = new Vector3(0, -0.46f, 0);
                SuccessRePo.transform.localScale = new Vector3(0.9f, 0.8f, 1);
                FailRePo.transform.localScale = new Vector3(0.9f, 0.8f, 1);
                break;
            case 20:
                transform.localPosition = new Vector3(0, -0.47f, 0);
                SuccessRePo.transform.localScale = new Vector3(0.9f, 0.4f, 1);
                FailRePo.transform.localScale = new Vector3(0.9f, 0.4f, 1);
                break;
            case 30:
                transform.localPosition = new Vector3(0, -0.48f, 0);
                SuccessRePo.transform.localScale = new Vector3(0.9f, 0.25f, 1);
                FailRePo.transform.localScale = new Vector3(0.9f, 0.25f, 1);
                break;
            case 40:
                transform.localPosition = new Vector3(0, -0.485f, 0);
                SuccessRePo.transform.localScale = new Vector3(0.9f, 0.2f, 1);
                FailRePo.transform.localScale = new Vector3(0.9f, 0.2f, 1);
                break;
            case 50:
                transform.localPosition = new Vector3(0, -0.4875f, 0);
                SuccessRePo.transform.localScale = new Vector3(0.9f, 0.15f, 1);
                FailRePo.transform.localScale = new Vector3(0.9f, 0.15f, 1);
                break;
        }

        // 오브젝트를 차곡차곡 쌓기
        Vector3 RePoPosition = new Vector3(0f, RePoNumber * SuccessRePo.transform.localScale.y, 0f);
        Debug.Log(RePoPosition);

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
