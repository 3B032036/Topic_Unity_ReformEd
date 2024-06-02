using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectChildrenChange : MonoBehaviour
{
     //儲存上一次的子物件數量
    private int lastChildCount;
    string ChildChangeType;

    private void OnTransformChildrenChanged() {
        OnChildrenChanged();
    }

    
    private void Start()
    {
        //初始化上一次的子物件數量
        lastChildCount = transform.childCount;
    }



    public void OnChildrenChanged()
    {
        print($"物件{transform.name}執行OnChildrenChanged()");
        int currentChildCount = transform.childCount;
        if (currentChildCount > lastChildCount)
        {
            Debug.Log($"物件{transform.name}子物件增加");
            ChildChangeType = "Plus";
        }
        else if (currentChildCount < lastChildCount)
        {
            Debug.Log($"物件{transform.name}子物件減少");
            ChildChangeType = "Minus";
        }
        else
        {
            ChildChangeType = "Plus";
        }

        float totalHeight = 0f;  // 用於儲存所有子物件的總高度
        float totalWidth = 0f;  // 用於儲存所有子物件的總高度
        foreach(Transform child in transform){
            print($"物件{transform.name}加前高度+{totalHeight}");

            totalHeight += child.GetComponent<RectTransform>().rect.height;
            totalWidth += child.GetComponent<RectTransform>().rect.width;

            print($"物件{transform.name}加{child.name}高度{child.GetComponent<RectTransform>().rect.height}={totalHeight}");
            print($"物件{transform.name}加後高度+{totalHeight}");

        }
        print($"物件{transform.name}獲取高度{totalHeight}、寬度{totalWidth}");
        
        GetComponent<UI_RWD_Handleer_new>().RWDJudge(this.gameObject, ChildChangeType, totalHeight, totalWidth);
        lastChildCount = currentChildCount;
        StartCoroutine(CallParentOnChildrenChanged());
    }

    private IEnumerator CallParentOnChildrenChanged()
    {
        yield return null; // 等待一幀，確保子物件的變化已經完全處理完畢
        //Debug.LogError("CallParentOnChildrenChanged");
        Transform parent = transform.parent;
        while (parent != null && parent.name != "Content")
        {
            if (parent.name.ToLower().Contains("area"))
            {
                DetectChildrenChange detectChildrenChange = parent.GetComponent<DetectChildrenChange>();
                if (detectChildrenChange != null)
                {
                    print($"物件{transform.name}呼叫物件{parent.name}執行OnChildrenChanged()");
                    detectChildrenChange.OnChildrenChanged();
                }
                break;
            }
            else
            {
                parent = parent.parent;
                if (parent != null)
                {
                    print($"目前{parent.name}不包含area，向上尋找");
                }
            }
        }
    }
}
