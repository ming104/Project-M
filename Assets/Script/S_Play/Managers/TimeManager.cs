using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager>
{
    public float[] TimeScaleList; // 배속 설정
    public Image[] Time_UI_Image;
    public int TimeScaleListIndex = 0; // 처음은 1배속이여야하니 0넣음 

    public bool TimeInteraction = true;

    void Start()
    {
        Time.timeScale = TimeScaleList[TimeScaleListIndex];
        Time_UI_Image[TimeScaleListIndex + 1].color = Color.black; // 얘는 0이 timescale이 0인데 그러면 그림에 맞지않음
    }
    void Update()
    {
        if (TimeInteraction == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TimeStop();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TimeScaleDown();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                TimeScaleUp();
            }
        }
    }

    public void TimeStop()
    {
        if (Time.timeScale == 0f) // 이미 멈춘상태라면 원래 속도로 돌려줌
        {
            Time_UI_Image[0].color = Color.white;
            Time_UI_Image[TimeScaleListIndex + 1].color = Color.black;
            Time.timeScale = TimeScaleList[TimeScaleListIndex];
        }
        else // 멈춤상태가 아니라면 멈춤
        {
            Time_UI_Image[0].color = Color.black;
            Time_UI_Image[TimeScaleListIndex + 1].color = Color.white;
            Time.timeScale = 0f;
        }

    }

    public void TimeScaleDown()
    {
        if (TimeScaleListIndex > 0) // 만약 0보다 크면 1배속 보다 크니까 
        {
            Time_UI_Image[0].color = Color.white;
            Time_UI_Image[TimeScaleListIndex + 1].color = Color.white; // 원래 자기 자신
            TimeScaleListIndex -= 1; // 감소시키고
            Time_UI_Image[TimeScaleListIndex + 1].color = Color.black;
            Time.timeScale = TimeScaleList[TimeScaleListIndex]; // 배속을 줄임
        }
    }
    public void TimeScaleUp()
    {
        if (TimeScaleListIndex < TimeScaleList.Length - 1) // 만약 3보다 작으면 3배속 이하니까
        {
            Time_UI_Image[0].color = Color.white;
            Time_UI_Image[TimeScaleListIndex + 1].color = Color.white;
            TimeScaleListIndex += 1; // 증가시키고
            Time_UI_Image[TimeScaleListIndex + 1].color = Color.black;
            Time.timeScale = TimeScaleList[TimeScaleListIndex]; // 배속을 늘림
        }
    }
}
