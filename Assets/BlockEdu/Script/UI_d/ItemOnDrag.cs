using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    [SerializeField]
    private bool PuzzleChoosed;
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        //當他的父物件不是指定物件如canva 
        PuzzleChoosed = true;
        print($"PuzzleChoosed = {PuzzleChoosed}");
        if(this.GetComponent<CanvasGroup>())
        {
            this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        if(this.transform.parent.name != "Content")
        {
            int childpuzzlecount = this.transform.parent.transform.childCount - 1;
            if (childpuzzlecount > 0)
            {
                for (int i = 0; i <= childpuzzlecount; i++)
                {
                    if (this.transform.parent.transform.GetChild(i).TryGetComponent<ItemOnDrag>(out ItemOnDrag itemOnDrag)
                        && itemOnDrag.PuzzleChoosed == true)
                    {
                        for (int j = i + 1; j <= childpuzzlecount; j++)
                        {
                            this.transform.parent.transform.GetChild(i + 1).transform.parent = this.transform;
                        }
                        break;
                    }
                }
            }
        }
        while (this.transform.parent.name != "Content")
        {
            this.transform.parent = this.transform.parent.transform.parent;
        }
        this.transform.SetAsLastSibling();
        PuzzleChoosed = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        //this.GetComponent<RectTransform>().anchoredPosition += eventData.delta;
        transform.position = eventData.position;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        print("OnEndDrag");
        if(eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.tag == "Conditional Area"
                && this.tag == "Conditional Statement Puzzle")
            {
                SetthisGameObjectParent(eventData);
                SetChildPuzzleToParent();
            }
            else if (eventData.pointerCurrentRaycast.gameObject.tag == "Execute Area"
                && this.tag == "Execute Statement Puzzle")
            {
                SetthisGameObjectParent(eventData);
                SetChildPuzzleToParent();
            }
            else if (eventData.pointerCurrentRaycast.gameObject.tag == "Expression Area"
                && this.tag == "Expression Statement Puzzle")
            {
                SetthisGameObjectParent(eventData);
            }
        }else{print("OnEndDrag-eventData.pointerCurrentRaycast.gameObject == null");}
    }

    private void SetthisGameObjectParent(PointerEventData eventData)
    {
        this.transform.parent = eventData.pointerCurrentRaycast.gameObject.transform;
        this.transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
    }

    private void SetChildPuzzleToParent()
    {
        int childpuzzlecount = this.transform.childCount - 1;
        if (childpuzzlecount > 0)
        {
            for (int i = 0; i <= childpuzzlecount; i++)
            {
                if (this.transform.GetChild(i).TryGetComponent<ItemOnDrag>(out ItemOnDrag itemOnDrag))
                {
                    for (int j = i; j <= childpuzzlecount; j++)
                    {
                        this.transform.GetChild(i).transform.parent = this.transform.parent;
                    }
                    break;
                }
            }
        }
    }
}
