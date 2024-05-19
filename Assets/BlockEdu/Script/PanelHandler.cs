using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelHandler : MonoBehaviour, IDropHandler
{
    /*腳本作用*//*
    主要放置在panel身上，用以判斷是否要銷毀方塊
    邏輯:
        1.若在左側面板(Panel_ChoseWay)Drop，則銷毀手中方塊
        2.若在右側面板(Panel_CodeWay) Drop，則不進行任何作用，即不銷毀方塊
    */

    public GameObject itemDropped; // 存储被拖放到面板上的物件

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.name == "Panel_ChoseWay")
        {

            itemDropped = eventData.pointerDrag; // 保存被拖放到面板上的物件
            Destroy(itemDropped);
            /*
            if (eventData.pointerDrag != null)
            {

                Destroy(eventData.pointerDrag);
            }
            */
        }

    }

}
