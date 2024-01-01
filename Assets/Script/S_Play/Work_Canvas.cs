using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Work_Canvas : MonoBehaviour
{
    public GameObject EmployeeList;

    private void Start()
    {
        EmployeeList.SetActive(false);
    }

    public void WorkButtonClick(int workNum)
    {
        EmployeeList.SetActive(true);
        Debug.Log("Select_Work : " + workNum);
    }

}
