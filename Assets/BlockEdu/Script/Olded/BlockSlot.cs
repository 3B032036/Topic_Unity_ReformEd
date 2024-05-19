using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BlockSlot : MonoBehaviour, IDropHandler
{
    // 存儲被放到這個 Drop Zone 的物件的列表
    //[SerializeField]private List<GameObject> droppedObjects = new List<GameObject>();

    public RectTransform hole;
    public RectTransform parentC;
    private float lastWidth = 0f; // 新增這個實例變數來儲存上一次的 width 
    private float OrignalWidth = 0f; // 新增這個實例變數來儲存原本的 width 

    private void Start() {
        OrignalWidth = hole.rect.width;
    }

    public void OnDrop(PointerEventData eventData)
    {
        print("onDrop");
        DragBlock block = eventData.pointerDrag.GetComponent<DragBlock>();

        if (block != null)
        {
            lastWidth = hole.rect.width;
            print($"lastWidth={lastWidth}");
            print($"onDrop->{block.name}");
            block.transform.SetParent(transform.parent);

            // 將程式塊的位置設定為區域的中心
            block.transform.position = transform.position;

            // 調整孔的大小以適應新的物件
            AdjustSize(block.GetComponent<RectTransform>().rect.width);
        }
    }


    
    public void AdjustSize(float width)
    {
        Vector2 newSize = new Vector2(width, hole.sizeDelta.y);
        hole.sizeDelta = newSize;
        
        // 如果新的寬度大於上一次的寬度，將父物件C的寬度增加
        if (width > lastWidth)
        {
            print("width > lastWidth");
            parentC.sizeDelta = new Vector2(parentC.sizeDelta.x + (width - lastWidth), parentC.sizeDelta.y);
        }
        // 如果新的寬度小於上一次的寬度，將父物件C的寬度減少
        else if (width < lastWidth)
        {
            print("width < lastWidth");
            parentC.sizeDelta = new Vector2(parentC.sizeDelta.x - (lastWidth - width), parentC.sizeDelta.y);
        }
        lastWidth = width;// 不論是否改變了父物件的寬度，都更新 lastWidth 為這一次的寬度
    }
    
}
