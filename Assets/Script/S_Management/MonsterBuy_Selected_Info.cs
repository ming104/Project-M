using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterBuy_Selected_Info : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject MonsterInfo_GO;
    public TMPro.TextMeshProUGUI MonsterInfo_Text;
    [SerializeField] private string MonsterBuy_Code;
    public string _MonCode
    {
        set { MonsterBuy_Code = value; }
        get { return MonsterBuy_Code; }
    }
    [SerializeField] private string MonsterBuy_Name;
    public string _MonName
    {
        set { MonsterBuy_Name = value; }
        get { return MonsterBuy_Name; }
    }
    [SerializeField] private string MonsterBuy_Info;
    public string _MonInfo
    {
        set { MonsterBuy_Info = value; }
        get { return MonsterBuy_Info; }
    }

    // private void Start()
    // {
    //     MonsterInfo_GO.SetActive(false);
    // }
    public void OnPointerEnter(PointerEventData eventData)
    {
        MonsterInfo_GO.SetActive(true);
        MonsterInfo_Text.text = $"\"{MonsterBuy_Info}\"";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MonsterInfo_GO.SetActive(false);
    }
    // public void OnMouseDown()
    // {

    // }
}
