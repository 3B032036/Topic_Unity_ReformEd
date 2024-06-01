using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RWD_Handler : MonoBehaviour
{
    /*------------------------------------------------------------
    主要功能：
    調整附帶此腳本的物件之長寬(總稱自適應)
    --------------------------------------------------------------*/

    public RectTransform parent__rectTransform;
    public RectTransform nulltype_rectTransform;
    public float lastWidth = 0f; // 儲存上一次的 width 
    public float OrignalWidth = 0f; // 儲存原本的 width 

    public float lastHeight = 0f; // 儲存上一次的 Height 
    public float OrignalHeight = 0f; // 儲存原本的 Height 

    private int lastChildCount = 0;


    BlockCtrlHandler blockCtrlHandler;

    public Transform FindBlockParent() 
    {
        for (Transform t = transform; t != null; t = t.parent) 
        {
            if (t.name.Contains("block")) 
            {
                return t;
            }
        }
        return null;
    }


    private void Awake() {
        nulltype_rectTransform = transform.GetComponent<RectTransform>();
        //parent__rectTransform = transform.parent.GetComponent<RectTransform>();
        Transform blockParent = FindBlockParent();
        parent__rectTransform = blockParent.GetComponent<RectTransform>();
        blockCtrlHandler = FindObjectOfType<BlockCtrlHandler>();
        lastWidth = nulltype_rectTransform.sizeDelta.x;
    }
    
    private void Start() {
        OrignalWidth = nulltype_rectTransform.rect.width;
        OrignalHeight = nulltype_rectTransform.rect.height;
    }


    /*-----------------------------
        兩種nulltype情況
        1.調整寬度   2.調整高度
    ------------------------------*/
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
                
                break;
            case "Height, HoldAdd":

                break;
            case "Height, Plus":
                nulltype_rectTransform.sizeDelta = new Vector2(nulltype_rectTransform.rect.width , nulltype_rectTransform.rect.height + Mathf.Abs( TargetValue));
                print($"AdjustSize=>TargetValue{TargetValue}nulltype_rectTransform.rect.height.sizeDelta{nulltype_rectTransform.rect.height}");
                break;  

            case "Width, Minus":

                break;
            case "Width, HoldAdd":

                break;
            case "Width, Plus":
                nulltype_rectTransform.sizeDelta = new Vector2(nulltype_rectTransform.rect.width + Mathf.Abs(nulltype_rectTransform.rect.width - TargetValue), nulltype_rectTransform.rect.height);
                print($"AdjustSize=>TargetValue{TargetValue}nulltype_rectTransform.rect.height.sizeDelta{nulltype_rectTransform.rect.height}");
                break;  
        }

        lastWidth = TargetValue;
    }

    public void RWDJudge(GameObject newInstance){

        RectTransform rectTransform = newInstance.GetComponent<RectTransform>();
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;
        print($"newInstance=>width{width}，height{height}");
        
        string _AdjustHeightOrWidth = null; // Height:高度   Width:寬度

        string _AdjustOption = null;//Minus:變小  HoldAdd:保持不變    Plus:變大

        float _TargetValue = 0f;
        
        
        #region 子物件判斷=>_AdjustOption
        /*---------------------------------------------
         功能：執行子物件數量判斷，傳回_AdjustOption數值
        ----------------------------------------------*/

            // 子物件數量
            int childCount = transform.childCount;
            
            // 如果子物件數量不變，則保持
            if (childCount == lastChildCount)
            {
                _AdjustOption = "Hold";
                print($"{transform.name}=>_AdjustOption={_AdjustOption}");
            }
            
            // 如果子物件數量增加，放大物件
            else if (childCount > lastChildCount)
            {
                _AdjustOption = "Plus";
                
                print($"{transform.name}=>_AdjustOption={_AdjustOption}");
            }

            // 如果子物件數量減少，還原成前次大小
            else if (childCount < lastChildCount)
            {
                _AdjustOption = "Minus";
                print($"{transform.name}=>_AdjustOption={_AdjustOption}");
            }

            lastChildCount = childCount;// 更新前次子物件數量


        #endregion


        #region 選取物件類別判斷=>_AdjustHeightOrWidth
        /*---------------------------------------------
         功能：執行子物件類型判斷，傳回_AdjustHeightOrWidth數值
        ----------------------------------------------*/
            print($"blockCtrlHandler.lastSelectGameObject_BlockType()=>{blockCtrlHandler.lastSelectGameObject_BlockType()}");
            switch (blockCtrlHandler.lastSelectGameObject_BlockType()){
                case "Judge":
                    //變更寬度
                    _AdjustHeightOrWidth = "Width";
                    _TargetValue = width;
                    break;
                case "Container":
                    //變更高度
                    _AdjustHeightOrWidth = "Height";
                    _TargetValue = height;
                    break;
                case "block":
                    //nothing
                    break;
            }

            AdjustSize(_AdjustHeightOrWidth, _AdjustOption, _TargetValue);
        #endregion
    }
}
