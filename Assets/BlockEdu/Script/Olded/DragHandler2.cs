using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragHandler2 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // 宣告變數
    Transform parentTransform;
    public bool allowMovement = false;
    public bool absorbFlag = false;
    int mouseX, mouseY;
    
    void Start()
    {
        // 初始化父物件的 Transform 和一些變數
        parentTransform = transform.parent;
        tempValue = 25;
        itemWidth = 200;
        itemLength = 100;

        // 在 Start 方法中添加吸附功能
        AddAbsorption();
    }

    // 宣告向量變數
    public Vector3 direction;

    // 宣告浮點數變數
    float deltaX;
    float deltaY;

    // 屬性：物件的寬度和長度
    public float itemWidth { get; set; }
    public float itemLength { get; set; }
    // 暫存值
    float tempValue;

    // 方法：獲取物件中心點的位置
    public Vector3 getCenterPoint()
    {
        return new Vector3(transform.position.x + itemWidth / 2, transform.position.y - itemLength / 2);
    }

    void Update()
    {
        // 如果允許移動
        if (allowMovement == true)
        {
            // 計算滑鼠與物件中心點之間的偏移量
            deltaX = getCenterPoint().x - Input.mousePosition.x;
            deltaY = getCenterPoint().y - Input.mousePosition.y;

            // 如果 X 偏移量大於暫存值，則移動物件
            if (Mathf.Abs(deltaX) > tempValue)
            {
                absorbFlag = false;
                transform.position = transform.position - Vector3.Normalize(new Vector3(deltaX, 0)) * tempValue;
                absorbFlag = true;
            }

            // 如果 Y 偏移量大於暫存值，則移動物件
            if (Mathf.Abs(deltaY) > tempValue)
            {
                absorbFlag = false;
                transform.position = transform.position - Vector3.Normalize(new Vector3(0, deltaY)) * tempValue;
                absorbFlag = true;
            }

            // 計算方向向量
            direction = Vector3.Normalize(GetComponent<RectTransform>().localPosition - new Vector3());
            direction = new Vector3(direction.x, 0, direction.y);
        }
    }

    // 實現 IPointerDownHandler 介面的方法
    public void OnPointerDown(PointerEventData eventData)
    {
        // 當指針按下時，設置允許移動為真
        allowMovement = true;
        print($"OnPointerDown-allowMovement({allowMovement})");

    }

    // 實現 IPointerUpHandler 介面的方法
    public void OnPointerUp(PointerEventData eventData)
    {
        // 當指針釋放時，設置允許移動為假
        absorbFlag = true;
        allowMovement = false;
        print($"OnPointerUp-absorbFlag({absorbFlag})-allowMovement({allowMovement})");
    }

    // 添加吸附功能的方法
    public void AddAbsorption()
    {
        // 添加吸附功能的邏輯
    }

    // 移除吸附功能的方法
    public void DestroyAbsorption()
    {
        // 移除吸附功能的邏輯
    }
}
