using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchStatusSlider : MonoBehaviour
{
    private Slider StatusSlider;
    // Start is called before the first frame update
    void Start()
    {
        StatusSlider = GetComponent<Slider>();
        StatusSlider.minValue = 0;
        StatusSlider.maxValue = 100;

        StatusSlider.value = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartResearch(int RePo, int persent)
    {
        StartCoroutine(Probabilitytask(RePo, persent));
    }

    private void Probabilitytasks(int RePo, int persent)
    {
        int sum = 0;
        int nsum = 0;
        
        for (int i = 0; i < RePo; i++)
        {
            var RanNum = Random.Range(0, 100);

            if (RanNum <= persent)
            {
                sum++;
                //SelectedRoom.GetComponent<ObjectLayoutGroup>().StackObjects(RePo, true);
                ResearchStatus(RePo, true);
            }

            else
            {
                nsum++;
                //SelectedRoom.GetComponent<ObjectLayoutGroup>().StackObjects(RePo, false);
                ResearchStatus(RePo, false);
            }
        }
    }
    
    private IEnumerator Probabilitytask(int RePo, int persent)
    {
        int sum = 0;
        int nsum = 0;
        //Debug.Log(SelectedRoom.GetComponent<ObjectLayoutGroup>());
        //SelectedRoom.GetComponent<ObjectLayoutGroup>().ChildDestory();
        for (int i = 0; i < RePo; i++)
        {
            var RanNum = Random.Range(0, 100);

            if (RanNum <= persent)
            {
                sum++;
                //SelectedRoom.GetComponent<ObjectLayoutGroup>().StackObjects(RePo, true);
                ResearchStatus(RePo, true);
            }

            else
            {
                nsum++;
                //SelectedRoom.GetComponent<ObjectLayoutGroup>().StackObjects(RePo, false);
                ResearchStatus(RePo, false);
            }
            yield return new WaitForSeconds(0.6f);
        }
        ResetStatus();
        Debug.Log($"성공 : {sum}, 실패 : {nsum}");
        UI_Manager.Instance.IncreasedEnergy(sum);
        GameManager.Instance.nowResearchPoint += RePo / 10;
    }

    private void ResearchStatus(int maxRePo, bool Results)
    {
        int feeling = 0;
        switch (maxRePo)
        {
            case 10:
                feeling = 5;
                break;
            case 20:
                feeling = 4;
                break;
            case 30:
                feeling = 3;
                break;
            case 40:
                feeling = 2;
                break;
            case 50:
                feeling = 1;
                break;
        }
        
        if (Results == true)
        {
            StatusSlider.value += feeling;

        }
        else
        {
            StatusSlider.value -= feeling;
        }
    }

    private void ResetStatus()
    {
        StartCoroutine("ResetFeeling");
    }

    private IEnumerator ResetFeeling()
    {
        yield return new WaitForSeconds(5f);
        if (StatusSlider.value < 50)
        {
            while (StatusSlider.value < 50)
            {
                StatusSlider.value += 1;
                yield return new WaitForSeconds(0.1f);
            }
        }
        if (StatusSlider.value > 50)
        {
            while (StatusSlider.value > 50)
            {
                StatusSlider.value -= 1;
                //연구 포인트? 증가
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
