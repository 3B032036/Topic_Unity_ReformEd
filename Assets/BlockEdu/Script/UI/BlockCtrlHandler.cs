using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockCtrlHandler : MonoBehaviour
{
    public GameObject lastSelectGameObject;


    /*-------以下內容已於6/2刪除------*/
    //public UI_RWD_Handler ui_rwd_handler;


    private void Awake() {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTagAllChildren(Transform parent) 
    {
        if (parent.childCount > 0) 
        {
            foreach (Transform child in parent) 
            {
                if (child.name.Contains("ConditionalArea"))
                {
                    child.tag = "Conditional Area";
                }
                else if (child.name.Contains("ExecuteArea"))
                {
                    child.tag = "Execute Area";
                }
                else if (child.name.Contains("ExpressionArea"))
                {
                    child.tag = "Expression Area";
                }
                SetTagAllChildren(child);
            }
        }
    }










    /*-------以下內容已於6/2刪除------*/
    /*
    public void BlockSelectJudge(GameObject SelectGameObject)
    {   
        //調用傳進來的GameObject值，裡面的UIOutlineController，直接設定他的外框顏色，表示選定
        SelectGameObject.GetComponent<UIOutlineController>().SetOutline(true, Color.blue);
        print($"lastClicked傳到BlockCtrlHandler了!!他是{SelectGameObject}!!!");


        //-----------------------------------------------------------------------------------------
        //這邊的邏輯是:
        //1.如果最後一個選擇的物件是空的，那就將lastSelectGameObject物件賦予傳進來的物件SelectGameObject
        //2.如果不是空的，就檢視新傳進來的物件，是否跟上一個lastSelectGameObject物件一樣，不一樣就執行動作
        //  a)把IsSelectedGameObject設為假   b)把舊的物件上的邊框關掉  c)將lastSelectGameObject賦予新值
        //-------------------------------------------------------------------------------------------
        if(lastSelectGameObject == null){
            lastSelectGameObject = SelectGameObject;
        }else if(lastSelectGameObject != null){
            if(SelectGameObject != lastSelectGameObject){
                if(SelectGameObject.gameObject.tag != "UI"){
                    print("BlockSelectJudge--IsSelectedGameObject = false");
                    print($"{lastSelectGameObject.name}不等於{SelectGameObject.name}");
                    lastSelectGameObject.GetComponent<UIOutlineController>().IsSelectedGameObject = false;
                    lastSelectGameObject.GetComponent<UIOutlineController>().SetOutline(false);
                    lastSelectGameObject = SelectGameObject;
                }
            }
        }

        
        if (lastSelectGameObject != null && lastSelectGameObject.name.Contains("block")){
            transform.GetComponent<BlockUIHandler>().DelButton.interactable = true;//禁用與變灰刪除按鈕
        }else{
            transform.GetComponent<BlockUIHandler>().DelButton.interactable = false;//禁用與變灰刪除按鈕
        }
    }



    public void SelectedG_O_Ctrl(string UpOrDown){
        
        if(lastSelectGameObject != null && lastSelectGameObject.gameObject.tag == "block"){
            int currentIndex = lastSelectGameObject.transform.GetSiblingIndex();
            
            if(UpOrDown == "UP"){
                int newIndex = Mathf.Min(currentIndex - 1, lastSelectGameObject.transform.parent.childCount - 1);
                lastSelectGameObject.transform.SetSiblingIndex(newIndex);
            }else if (UpOrDown == "Down"){
                int newIndex = Mathf.Min(currentIndex + 1, lastSelectGameObject.transform.parent.childCount - 1);
                lastSelectGameObject.transform.SetSiblingIndex(newIndex); 
            }
        }
        
    }

    public string lastSelectGameObject_BlockType(){
        //------------------------------------------------------------
        //主要功能:
        //判斷使用者最後一個選取的物件的類別、名子，並傳回相對應的類別名稱，
        //以便上面節省代碼過多、可讀性低的問題。
        //--------------------------------------------------------------

        if(lastSelectGameObject.tag == "nulltype" && lastSelectGameObject.name.Contains("Judge")){
            //只能放入一個方塊的類型
            Debug.Log("BlockType = 'Judge'");
            return "Judge";

        }else if (lastSelectGameObject.tag == "nulltype" && lastSelectGameObject.name.Contains("Container")){
            //可以放入多個方塊的類型
            print("BlockType = 'Container'");
            return "Container";

        }else if(lastSelectGameObject.tag == "block"){
            print("BlockType = 'block'");
            return "block";

        }else{
            print("default");
            return default; 
        }
        
    }
    */
}
