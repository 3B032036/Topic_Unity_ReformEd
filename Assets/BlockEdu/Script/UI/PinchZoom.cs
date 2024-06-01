using UnityEngine;
using UnityEngine.EventSystems;

public class PinchZoom : MonoBehaviour
{
    public float zoomSpeed = 0.1f; // 縮放速度
    public float maxScale = 2f; // 最大放大尺寸
    private Vector3 initialScale; // 初始尺寸

    void Awake()
    {
        //initialScale = transform.localScale;
        initialScale = this.GetComponent<RectTransform>().localScale;

    }

    void Update()
    {
        // 如果ctrl按鍵被按下
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            float mouseScroll = Input.GetAxis("Mouse ScrollWheel");

            // 如果滑鼠向前滾動
            if (mouseScroll < 0)
            {
                ZoomIn();
            }
            // 如果滑鼠向後滾動
            else if (mouseScroll > 0)
            {
                ZoomOut();
            }
        }

        // 如果縮放後的尺寸小於初始尺寸，則將尺寸重置為初始尺寸
        if (this.GetComponent<RectTransform>().localScale.x < initialScale.x)
        {
            
            //transform.localScale = initialScale;
            this.GetComponent<RectTransform>().localScale = initialScale;
        }
    }

    void ZoomIn()
    {
        /*
        float newSize = transform.localScale.x * (1 - zoomSpeed);
        transform.localScale = new Vector3(newSize, newSize, newSize);
        */

        float newSize = GetComponent<RectTransform>().localScale.x * (1 - zoomSpeed);
        GetComponent<RectTransform>().localScale = new Vector3(newSize, newSize, newSize);
    }

    void ZoomOut()
    {
        /*
        float newSize = transform.localScale.x * (1 + zoomSpeed);
        //如果新增尺寸大於最大尺寸，則限制在最大尺寸
        newSize = newSize > maxScale ? maxScale : newSize;
        transform.localScale = new Vector3(newSize, newSize, newSize);
        */
        
        float newSize = GetComponent<RectTransform>().localScale.x * (1 + zoomSpeed);
        //如果新增尺寸大於最大尺寸，則限制在最大尺寸
        newSize = newSize > maxScale ? maxScale : newSize;
        GetComponent<RectTransform>().localScale = new Vector3(newSize, newSize, newSize);
    }
}