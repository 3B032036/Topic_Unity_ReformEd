using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandRunner : MonoBehaviour
{
    //本腳本功能：執行中繼站，主要獲取並剖析陣列中代碼內容，並傳送到該執行的腳本執行

    /*----------------------
    邏輯->按下開始按鈕->執行開始按鈕函數->執行取右邊面板積木之文字->將其存取為陣列-
    -->從陣列內容依序使用方法判斷->判斷完後傳出去其他腳本執行
    ------------------------*/

    public string[] conditionalArray;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void WhenPlayClicked(){
        
    }
    
    private void ConditionalCheck(){
        // 逐個判斷陣列內容
        for (int i = 0; i < conditionalArray.Length; i++)
        {
            string currentElement = conditionalArray[i];

            // 判斷陣列元素是否符合特定條件
            if (currentElement == "如果")//if
            {
                // 進行相應操作
                Debug.Log("遇到 '如果'");
            }
            else if (currentElement == "否則")//else
            {
                
            }
           else if (currentElement == "否則如果")//else if
            {

            }
            else if (currentElement == "如果底")//if底
            {

            }
        }
    }
}
