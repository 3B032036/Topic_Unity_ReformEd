using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerCol : MonoBehaviour
{
    RectTransform windowParent;//windowParent為正在拖曳的這個窗體本身
    RectTransform windowCollider;//windowCollider為被碰撞的物體
    public DragHandler dragTrue;//windowParent的拖曳腳本 原意是被移動者
    public DragHandler dragFalse;//windowCollider的拖曳腳本 原意是不被移動者

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake() {
        windowParent = this.transform.GetComponent<RectTransform>();
        dragTrue = windowParent.GetComponent<DragHandler>();
    }


    void OnTriggerStay2D(Collider2D other){
        //其中設定了吸附判斷變數用來控制窗體，只有在鬆開滑鼠的那一刻才會發生吸附效果。
        windowCollider = other.transform.GetComponent<RectTransform>();
        dragFalse = windowCollider.GetComponent<DragHandler>();

        if (other.gameObject.tag == "Block_Multple")//碰撞到的TAG 是只有下可以吸附的物件
        {

            Debug.Log("OnTriggerStay2D：" + other.gameObject.tag);//有傳值

            /*情况A，横放窗体碰横放窗体*/
            if (dragTrue.absorbFlag && !dragTrue.allowMove && windowParent.gameObject == PositionControl.currentBlock){
                Debug.Log("OnTriggerStay2D：" + this.transform.name + "-" + other.gameObject.name);

                transform.SetParent(other.transform, false);//不變

                Transform attachmentPoint = other.transform.Find("Bottom_Point");
                if (attachmentPoint != null)
                {
                    // 計算位置使其與attachmentPoint對齊
                    Vector3 newPosition = attachmentPoint.position;
                    transform.position = newPosition;
                }
                else
                {
                    // 計算位置使其與attachmentPoint對齊
                    Vector3 newPosition = other.transform.position;
                    transform.position = newPosition;
                }

                
            }
        }

    }

}
