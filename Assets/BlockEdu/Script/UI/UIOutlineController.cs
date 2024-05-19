using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIOutlineController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler 
{   
    /*
    
    
    
    */


    public bool IsSelectedGameObject = false;//可否被選擇的關鍵角色

    public Image targetImage; // 要添加描邊效果的目標 UI 圖像
    
    BlockCtrlHandler blockCtrlHandler;//BlockManager下的組件code：BlockCtrlHandler

    private void Awake() {
        //將BlockManager下的組件code：BlockCtrlHandler抓進來
        blockCtrlHandler = GameObject.Find("BlockManager").GetComponent<BlockCtrlHandler>();
    }

    void Start()
    {
        SetOutline(false); // 開始時禁用描邊效果
    }

    void Update() // 在每一幀中監視滑鼠點擊
    {
        
    }
    
    // 當滑鼠移入時，將描邊效果改為綠色並啟用
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!IsSelectedGameObject){SetOutline(true, Color.green);}
    }

    // 當滑鼠移出時，如果物件不是最後一次被點擊的物件，則將描邊效果關閉
    public void OnPointerExit(PointerEventData eventData)
    {
        if(!IsSelectedGameObject){ResetOutline();}
            
    }

    public void OnPointerClick(PointerEventData eventData)
    {   
        print("OnPointerClick--IsSelectedGameObject = true");
        IsSelectedGameObject = true;
        blockCtrlHandler.BlockSelectJudge(this.gameObject);
        //SetOutline(true, Color.blue);
    }



    // 重置描邊效果（主要用於切換描邊效果的物件時）
    public void ResetOutline()
    {
        SetOutline(false);
    }

    // SetOutline方法用於開啟或關閉描邊效果，並設定描邊顏色
    public void SetOutline(bool isEnabled, Color color = default(Color))
    {
        Outline outline = targetImage.GetComponent<Outline>(); // 從 targetImage 物件中獲取描邊效果組件
        
        if (outline == null) // 如果沒有找到描邊組件
        {
            outline = targetImage.gameObject.AddComponent<Outline>(); // 在 targetImage 上添加描邊效果組件
        }

        outline.enabled = isEnabled; // 根據 isEnabled 參數來開啟或關閉描邊效果

        if(isEnabled)
        {
            outline.effectColor = color; // 如果描邊效果為啟用狀態，將描邊顏色設定為 color 參數色
        }
    }

    
}