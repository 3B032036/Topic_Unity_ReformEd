using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragBlock : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private RectTransform rectTransform; // 拖曳物件的 RectTransform
    private CanvasGroup canvasGroup;
    
    // 靜態變量，用於存儲被拖曳的物件
    public static GameObject draggingItem;

    public GameObject blockPrefab;   // 已经创建的prefab的引用
    public Canvas canvas; // Canvas


    private void Awake()
    {
        // 獲取拖曳物件的 RectTransform
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        canvas = GetComponentInParent<Canvas>();

    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Nothing here for now
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Instantiate a new block only when there isn't already one
        if(draggingItem == null)
        {
            // 在鼠标当前位置创建新的prefab实例
            draggingItem = Instantiate(blockPrefab, 
                                        eventData.position, 
                                        Quaternion.identity,
                                        canvas.transform);

            // 保持复制的对象在鼠标下方
            draggingItem.transform.SetAsLastSibling();
        }
        else
        {
            // 移动已存在的block至鼠标当前位置
            draggingItem.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("OnEndDrag");

        // 釋放被拖曳的物件
        draggingItem = null;

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // 釋放父物件的 Transform
        //ReleaseParent(transform);
    }

    private void ReleaseParent(Transform currentTransform)
    {
        // 記錄物件在世界空間中的位置和旋轉
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        // 查找並解除父子關係，直到找到 Canvas 為止
        Transform parent = transform.parent;
        while (parent != null && !parent.CompareTag("Canvas"))
        {
            parent = parent.parent;

        }

        if (parent != null)
        {
            // 將物件設置為 Canvas 的子物件
            transform.SetParent(parent, false);

            // 重新設置位置和旋轉，保持在世界空間中的位置不變
            transform.position = position;
            transform.rotation = rotation;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {

    }
}