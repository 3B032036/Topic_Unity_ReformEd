using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariablePuzzle : MonoBehaviour
{   
    //之後會大改的變數積木

    //[SerializeField] private int Variablefixed;
    [SerializeField] private Text VarToText;//顯示物件上的文字
    [SerializeField] private GameObject ExpressionArea_1;
    [SerializeField] VariableHandler variableHandler;//需要自己拖曳

    //[SerializeField] private Dropdown dropdown; //之後在新增功能

    void Start()
    {
        variableHandler = GameObject.Find("VariableManager").GetComponent<VariableHandler>();

        if (transform.name.Contains("Set") || transform.name.Contains("Get"))
        {
            transform.tag = "Execute Statement Puzzle";
        }
        else
        {
            transform.tag = "Expression Statement Puzzle";
        }
        FindObjectOfType<BlockCtrlHandler>().SetTagAllChildren(this.gameObject.transform);
    }

    void Update()
    {
        
    }

    public void Execute(){
        if (transform.tag != "Execute Statement Puzzle") return; //若不是執行方塊則禁止執行此區域
        
        if (ExpressionArea_1 != null && ExpressionArea_1.transform.childCount > 0)
        {   
            variableHandler.var1 = ExpressionArea_1.GetComponentInChildren<ExpressionPuzzle>().Expression();
            print($"顯示變數var1值為{variableHandler.var1}");
            
        }
        else if (transform.name.Contains("Get"))
        {
            print($"目前var1值為{variableHandler.var1}");
        }
        else
        {
            Debug.Log("ExpressionArea_1錯誤或是空值");
        }

    }

    public int Expression()
    {
        print($"目前var值為{variableHandler.var1}");
        return variableHandler.var1;//set
    }

    /*
    private Dictionary<string, int> variables = new Dictionary<string, int>();

    // 創建變數
    public void CreateVariable(string name, int initialValue = 0)
    {
        if (!variables.ContainsKey(name))
        {
            variables[name] = initialValue;
            Debug.Log($"變數 {name} 已創建，初始值為 {initialValue}");
        }
        else
        {
            Debug.LogError($"變數 {name} 已存在");
        }
    }

    // 修改變數
    public void SetVariable(string name, int value)
    {
        if (variables.ContainsKey(name))
        {
            variables[name] = value;
            Debug.Log($"變數 {name} 的值已設置為 {value}");
        }
        else
        {
            Debug.LogError($"變數 {name} 不存在");
        }
    }

    // 獲取變數值
    public int GetVariable(string name)
    {
        if (variables.ContainsKey(name))
        {
            return variables[name];
        }
        else
        {
            Debug.LogError($"變數 {name} 不存在");
            return 0;
        }
    }

    // 刪除變數
    public void DeleteVariable(string name)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            Debug.Log($"變數 {name} 已刪除");
        }
        else
        {
            Debug.LogError($"變數 {name} 不存在");
        }
    }
    */

}
