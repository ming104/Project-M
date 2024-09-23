using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleHTP : MonoBehaviour
{
    private int _index = 0;
    public List<GameObject> HTPPanel;

    public void ButtonRight()
    {
        if (_index < HTPPanel.Count-1)
        {
            _index++;
        }
    }
    public void ButtonLeft()
    {
        if (0 < _index)
        {
            _index--;
        }
    }

    public void Update()
    {
        foreach (var Panel in HTPPanel)
        {
            Panel.SetActive(false);
        }
        HTPPanel[_index].SetActive(true);
    }
}
