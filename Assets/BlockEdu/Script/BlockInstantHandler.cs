/*-------以下內容已於6/2刪除------*/

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//引用Unity Editor
using UnityEditor;
using System.Linq;

public class BlockInstantHandler : MonoBehaviour
{

    // Panel_CodeWay 的引用需要在 Unity 面板中設置預設
    public GameObject Panel_CodeWay;

    // blockprefab 的引用需要在 Unity 面板中設置
    public GameObject blockprefab;

    [SerializeField]List<GameObject> blockObjects;
    [SerializeField]List<GameObject> codeObjects;

    [SerializeField]List<GameObject> nulltypeObjects;

    BlockCtrlHandler blockCtrlHandler;

    


    private void Awake() {
        //把叫做Panel_CodeWay的物件抓進來
        //Panel_CodeWay = GameObject.Find("Panel_CodeWay");

        //
        blockCtrlHandler = GetComponent<BlockCtrlHandler>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    

    // 按钮触发的方法，用于创建预设
    public void OnBlockBtnPress(string block_btn_name)
    {   
        //-----------------------------------------------------------------------------------------------
        //這邊有個主要的判斷方式->抓按鈕名子(block_btn_name)來判斷是要生成哪個Prefab
        //生成方式有兩個方法:
        //1.用方塊的名子直接去資料夾裡面抓名字一樣的Prefab來調用生成事件
        //2.建立一個清單，逐個把預置件拉進去
        //（值得思考的事情）思路很清晰，但1.是命名的問題，2.是prefab有動到就要重拉的問題，不過操作步驟其實差不多
        //--------------------------------------------------------------------------------------------------

        //撈取物件並指定物件類行為GameObject
        blockprefab = AssetDatabase.LoadAssetAtPath($"Assets/BlockEdu/Prefab/{block_btn_name}.prefab", typeof(GameObject)) as GameObject;
        if (blockprefab != null)Debug.Log("撈到" + blockprefab.name + "啦!!!");else Debug.Log("blockprefab不存在");

        if (blockCtrlHandler.lastSelectGameObject != null) {

            //調用判斷方法:主要判斷物件的類別，再傳回對應的值來做判斷，優化程式視讀

            GameObject newInstance;

            switch (blockCtrlHandler.lastSelectGameObject_BlockType()){
                case "Judge":

                    print("switch Judge");
                    if (blockCtrlHandler.lastSelectGameObject.transform.childCount <= 0){
                        // 在 Panel_Right 生成預置件
                        newInstance = Instantiate(blockprefab, blockCtrlHandler.lastSelectGameObject.transform);
                        newInstance.transform.localPosition = Vector3.zero;

                        if (blockCtrlHandler.lastSelectGameObject.GetComponent<UI_RWD_Handler>() == null)  print("UI_RWD_Handler") ; else print("ERROR");
                        if (newInstance == null)  print("UI_RWD_Handler") ; else print("ERROR");

                        //先紀錄lastSelectGameObject現在的寬度
                        //將新生成的物件的值加上去
                        //blockCtrlHandler.lastSelectGameObject.GetComponent<UI_RWD_Handler>().lastWidth += newInstance.GetComponent<RectTransform>().rect.width;
                        blockCtrlHandler.lastSelectGameObject.GetComponent<UI_RWD_Handler>().RWDJudge(newInstance);
                        //將他新的寬度存在另一個值裡面
                        
                    }
                    break;

                case "Container":
                    print("switch Container");

                    newInstance = Instantiate(blockprefab, blockCtrlHandler.lastSelectGameObject.transform);
                    newInstance.transform.localPosition = Vector3.zero;
                    blockCtrlHandler.lastSelectGameObject.GetComponent<UI_RWD_Handler>().RWDJudge(newInstance);
                    break;

                case "block":
                    print("switch block");

                    newInstance = Instantiate(blockprefab, Panel_CodeWay.transform);
                    newInstance.transform.localPosition = Vector3.zero;
                    break;

                default:
                    print("switch default");

                    newInstance = Instantiate(blockprefab, Panel_CodeWay.transform);
                    newInstance.transform.localPosition = Vector3.zero;
                    break;
            }

            
        }else{
            GameObject newInstance = Instantiate(blockprefab, Panel_CodeWay.transform);
            newInstance.transform.localPosition = Vector3.zero;
        }

        
    }

    public void WhenPlayClickTodo()
    {
        //------------------------------------------------------------------------------------------------
        //這邊主要的作用是，當按鈕按下並執行生成之後，要讓BlockManager可以創建一個清單，
        //並且把剛剛生成的物件給加進去，這樣方便之後判斷跟調用場上的內容。

        //24.05.07更新:
        //這邊的邏輯改變成「按下開始按鈕後才創建清單、將相對應物件判斷後放進去清單」，
        //這是為了簡化工作流程，不然只要使用者改一次積木程式順序，就需要再針對此項目調整
        //--------------------------------------------------------------------------------------------------
        
        // 獲取場景中所有遊戲物體
        //GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
        GameObject panel_codeWay = GameObject.Find("Panel_CodeWay");

        // 如果找到
        if (panel_codeWay)
        {
            //print
            Debug.Log("已找到Panel_CodeWay物件");

            // 建立清單來儲存所有有tag的物件
            blockObjects = new List<GameObject>();
            codeObjects = new List<GameObject>();
            nulltypeObjects = new List<GameObject>();

            // 檢查該物體的所有子物體，並且將帶有tag的添加到清單中
            GetChildrenWithTags(panel_codeWay.transform,"block", blockObjects);
            GetChildrenWithTags(panel_codeWay.transform,"code", codeObjects);
            GetChildrenWithTags(panel_codeWay.transform,"nulltype", nulltypeObjects);

        }    
    
    }

    // 一個遞迴的方法用於找到具有tag的子物體
    void GetChildrenWithTags(Transform parent, string ObjectTag, List<GameObject> listWithTags)
    {
        foreach (Transform child in parent)
        {
            // 如果子物件有tag
            if (child.tag == ObjectTag)
            {
                listWithTags.Add(child.gameObject);
                Debug.Log("找到tag的物件：" + child.gameObject.name);
            }else{
                Debug.Log($"{child.name}的標籤是{child.tag}，不是{ObjectTag}!!");
            }

            // 如果有子物體，繼續遞迴檢查
            if(child.childCount > 0)
                Debug.Log($"{child.name}還有子物件!");
                GetChildrenWithTags(child, ObjectTag, listWithTags);
        }
    }

    
    
}
*/