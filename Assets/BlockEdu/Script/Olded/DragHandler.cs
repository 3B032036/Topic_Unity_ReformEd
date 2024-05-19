using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{//IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler為實作拖放動作API
    private RectTransform dragRectTransform;//定義Transform

    Transform parentTransform;
    public bool allowMove = false;//允許移動
    public bool absorbFlag = false;//到達定點
    

    private void Awake() {
        dragRectTransform = GetComponent<RectTransform>();
    }
    private void Start() {
        // 初始化父物件的 Transform
        parentTransform = transform.parent;
    }
    private void Update() {

    }


    public void OnBeginDrag(PointerEventData eventData){
        //canvasGroup.blocksRaycasts = false;//關閉可以指定的Raycast
        allowMove = true;
        absorbFlag = false;//不可吸附
        

        
        for (int i = 0; i < PositionControl.BlockArray.Length; i++)
        {
            if (this.transform.gameObject == PositionControl.BlockArray[i])
                PositionControl.currentBlock = PositionControl.BlockArray[i];
                print($"PositionControl.currentBlock={PositionControl.currentBlock}");
        }
        
    }

    public void OnDrag(PointerEventData eventData){
        dragRectTransform.anchoredPosition += eventData.delta;//處理即時加上Delta(事件資料)
        transform.SetAsLastSibling();//讓拖曳的物件始終在最上面顯示
        
    }

    public void OnEndDrag(PointerEventData eventData){
        //canvasGroup.blocksRaycasts = true;//開啟可以指定的Raycast
        allowMove = false;//已停止移動
        absorbFlag = true;//可以吸附

        // 檢查是否有父物件，並且父物件不是 Canvas，如果是，則解除父子關係
        //ReleaseParent(transform);
    }

    private void ReleaseParent(Transform currentTransform)
    {
        // 記錄物件在世界空間中的位置和旋轉
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        Transform parent = transform.parent;
        while (parent != null && !parent.CompareTag("Canvas"))
        {
            parent = parent.parent;
        }

        if (parent != null)
        {
            // 將物件設置為Canvas的子物件
            transform.SetParent(parent, false);

            // 重新設置位置和旋轉，保持在世界空間中的位置不變
            transform.position = position;
            transform.rotation = rotation;
        }
    }

}
