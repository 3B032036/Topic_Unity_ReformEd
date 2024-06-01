using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RWD_Handleer_new : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //邏輯
    //1.拖曳進入其他物件下時呼叫調整高度
    //2.判斷目前的方塊是哪一種

    float Min_width;
    float Min_height;

    public void RWDJudge(GameObject gameObject, string ChildChangeType, float totalHeight, float totalWidth)
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        if (transform.name.Contains("body") || transform.name.Contains("head"))
        {
            
        }
        else
        {
            Min_width = GetComponent<LayoutElement>().minWidth;
            Min_height = GetComponent<LayoutElement>().minHeight;
        }
        
        string _AdjustHeightOrWidth = null; // Height:高度   Width:寬度

        string _AdjustOption = null;//Minus:變小  HoldAdd:保持不變    Plus:變大

        float _TargetValue = 0f;

        switch (ChildChangeType){
            case "Plus":
                _AdjustOption = ChildChangeType;
                break;
            case "Minus":
                _AdjustOption = ChildChangeType;
                break;
        }

        switch (gameObject.tag){
            case "Expression Area"://寬度
                _AdjustHeightOrWidth = "Width";
                _TargetValue = totalWidth > Min_width ? Min_width + Math.Abs(Min_width - totalWidth) : Min_width;
                //if(_AdjustOption == "Minus"){_TargetValue = Min_width;}
                print($"_TargetValue->{_TargetValue} = Min_width{Min_width}、totalWidth{totalWidth}");
                break;
            case "Execute Area"://高度
                _AdjustHeightOrWidth = "Height";
                _TargetValue = Min_height + totalHeight;
                print($"_TargetValue->{_TargetValue} = Min_height{Min_height}+totalHeight{totalHeight}");

                break;
            case "Conditional Area"://寬度
                _AdjustHeightOrWidth = "Width";
                _TargetValue = totalWidth > Min_width ? Min_width + Math.Abs(Min_width - totalWidth) : Min_width;
                break;
        }

        AdjustSize(_AdjustHeightOrWidth, _AdjustOption, _TargetValue);

        
        
    }


    public void AdjustSize(string AdjustHeightOrWidth, string AdjustOption, float TargetValue)//調整大小(調整模式, 調整數值)
    {
        //AdjustHeightOrWidth
        /*----------------------------
           Height:高度   Width:寬度
        ------------------------------*/

        //AdjustOption
        /*---------------------------------------------
            Minus:變小  HoldAdd:保持不變    Plus:變大
        -----------------------------------------------*/

        
        string Adjust_All = $"{AdjustHeightOrWidth}, {AdjustOption}";


        
        switch (Adjust_All){
            case "Height, Minus":
                transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(transform.parent.GetComponent<RectTransform>().rect.width , TargetValue);
                print($"物件{transform.name}調整物件{transform.parent.name}高度");
                
                break;

            case "Height, Plus":
                print($"TargetValue->{TargetValue}");
                print($"物件{transform.name}調整物件{transform.parent.name}高度");
                print($"物件{transform.parent.name}舊高度->{transform.parent.GetComponent<RectTransform>().sizeDelta}");

                transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(transform.parent.GetComponent<RectTransform>().rect.width , TargetValue);
                print($"物件{transform.parent.name}新高度->{transform.parent.GetComponent<RectTransform>().sizeDelta}");
                
                break;  

            case "Width, Minus":
                transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(TargetValue, transform.parent.GetComponent<RectTransform>().rect.height);
                print($"物件{transform.name}調整物件{transform.parent.name}寬度");

                break;
            case "Width, Plus":
                transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(TargetValue, transform.parent.GetComponent<RectTransform>().rect.height);
                print($"物件{transform.name}調整物件{transform.parent.name}寬度");

                break;  
        }

        

    }
}