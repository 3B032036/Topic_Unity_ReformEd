using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockUIHandler : MonoBehaviour
{
    /*-------以下內容已於6/2刪除------*/
    //public Button DelButton;//控制面板的刪除按鈕
    //public GameObject DestroyComfirmWindow;


    public BlockCtrlHandler blockCtrlHandler;
    
    private void Awake() {
        blockCtrlHandler = transform.GetComponent<BlockCtrlHandler>();

        /*-------以下內容已於6/2刪除------*/
        //DestroyComfirmWindow.SetActive(false);
        //DelButton = GameObject.Find("Delete").GetComponent<Button>();
        //DelButton.interactable = false;
    }







































    /*-------以下內容已於6/2刪除------*/
    /*
    public void Print_DestroyComfirmWindow(string Action){
        
        switch (Action){
            case "Show":
                DestroyComfirmWindow.SetActive(true);
                //DestroyComfirmWindow.GetComponentInChildren<Text>().text = $"你確定要刪除{blockCtrlHandler.lastSelectGameObject.name}嗎?";
                break;
            case "Del":
                Destroy(blockCtrlHandler.lastSelectGameObject);
                DestroyComfirmWindow.SetActive(false);
                DelButton.interactable = false;

                break;
            case "Cancel":
                DestroyComfirmWindow.SetActive(false);
                DelButton.interactable = false;
                break;
        }

    }
    */


    /*-------以下內容已於6/2刪除------*/
    /*
    public void CancelAllSelected(){
        //------------------------------------------------------------------------------------------------
        //觸發時機：當按下綁定此事件的按鈕，如取消選取按鈕，則執行此副程式
        //主要功能：將暫存在lastSelectGameObject中的物件做假重設
        //1.IsSelectedGameObject變數(物件判斷是否是本身被選到的變數)設為假
        //2.lastSelectGameObject設為空值(代表最後一個物件不存在，就讓其他針對物件做的動作抓值抓不到)
        //--------------------------------------------------------------------------------------------------
        if(blockCtrlHandler.lastSelectGameObject != null){
            blockCtrlHandler.lastSelectGameObject.GetComponent<UIOutlineController>().IsSelectedGameObject = false;
            blockCtrlHandler.lastSelectGameObject.GetComponent<UIOutlineController>().SetOutline(false);
            blockCtrlHandler.lastSelectGameObject = null;
            DelButton.interactable = false;//禁用與變灰刪除按鈕//
        }
    }
    */
}
