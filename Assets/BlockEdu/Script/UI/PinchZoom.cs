using UnityEngine;
using UnityEngine.EventSystems;

public class PinchZoom : MonoBehaviour
{
    public float zoomSpeed = 0.1f;  
    private Vector3 initialScale;

    void Awake()
    {
        initialScale = transform.localScale;
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

        // 如果縮放後的尺寸小於原本的尺寸，則將尺寸重置為原本的尺寸
        if (transform.localScale.x < initialScale.x)
        {
            transform.localScale = initialScale;
        }
    }

    void ZoomIn()
    {
        float newSize = transform.localScale.x * (1 - zoomSpeed);
        transform.localScale = new Vector3(newSize, newSize, newSize);
    }

    void ZoomOut()
    {
        float newSize = transform.localScale.x * (1 + zoomSpeed);
        transform.localScale = new Vector3(newSize, newSize, newSize);
    }
}