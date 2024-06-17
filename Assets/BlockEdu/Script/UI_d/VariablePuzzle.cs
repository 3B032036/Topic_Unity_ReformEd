using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using UnityEngine.UI;

public class VariablePuzzle : MonoBehaviour
{   

    enum VariablePuzzle_Type{ Get, Set }
    [SerializeField] VariablePuzzle_Type variablePuzzle_Type;

    //之後會大改的變數積木

    //[SerializeField] private int Variablefixed;
    [SerializeField] private Text VarToText;//顯示物件上的文字
    [SerializeField] private GameObject ExpressionArea_1;
    [SerializeField] VariableManager variableManager;//需要自己拖曳
    [SerializeField] Dropdown variable_Dropdown;//需要自己拖曳
    [SerializeField] private int savedIndex; // 儲存目前已選擇的選項索引
    [SerializeField] Text print_var_text;


    void Start()
    {
        
        variableManager = GameObject.Find("VariableManager").GetComponent<VariableManager>();

        if (transform.name.Contains("Set") || transform.name.Contains("Get"))
        {
            transform.tag = "Execute Statement Puzzle";
        }
        else
        {
            transform.tag = "Expression Statement Puzzle";
        }
        FindObjectOfType<ItemOnDrag>().SetTagAllChildren(this.gameObject.transform);
        
        UpdateDropdownOption();
        SaveCurrentSelection();


    }

    void Update()
    {

    }

    // 儲存當前已選擇的選項
    public void SaveCurrentSelection()
    {
        savedIndex = variable_Dropdown.value;
    }

    // 恢復儲存的選項
    public void RestoreSavedSelection()
    {
        if(savedIndex < variable_Dropdown.options.Count)
        {
            variable_Dropdown.value = savedIndex;
            variable_Dropdown.RefreshShownValue();
        }
        else
        {
            Debug.LogWarning("儲存的索引超出新選項的範圍");
        }
    }

    public void UpdateDropdownOption()
    {
        if (variable_Dropdown != null)
        {
            //將VariableManager中之變數放進去dropdrop選項

            // 清空原有的選項
            variable_Dropdown.ClearOptions();

            // 取得所有的鍵
            Dictionary<string, int> variables = variableManager.GetVariables();
            List<string> options = new List<string>();

            // 遍歷所有變量，建立選項列表
            foreach (KeyValuePair<string, int> entry in variables)
            {
                string option = $"{entry.Key}";
                print($"{entry.Key}: {entry.Value}");
                options.Add(option);
            }
            //更新 Dropdown 選項
            variable_Dropdown.AddOptions(options);

        }
        RestoreSavedSelection();
    }

    public void Execute(){
        if (transform.tag != "Execute Statement Puzzle") return; //若不是執行方塊則禁止執行此區域
        
        if (ExpressionArea_1 != null && ExpressionArea_1.transform.childCount > 0)
        {   
            var expressionPuzzle = ExpressionArea_1.transform.GetChild(0).GetComponent<ExpressionPuzzle>();
            var variablePuzzle = ExpressionArea_1.transform.GetChild(0).GetComponent<VariablePuzzle>();
            var arrayPuzzle = ExpressionArea_1.transform.GetChild(0).GetComponent<ArrayPuzzle>();

            int var_temp = 0;
            if (expressionPuzzle != null)
            {
                var_temp = expressionPuzzle.Expression();
            }
            else if (variablePuzzle != null)
            {
                var_temp = variablePuzzle.Expression();
            }
            else if (arrayPuzzle != null)
            {
                var_temp = arrayPuzzle.Expression();
            } 
            else
            {
                var_temp = 0; // 如果都為 null，設置為 0
            }

            print($"var_temp={var_temp}");

            string variableName = variable_Dropdown.options[variable_Dropdown.value].text;// 取得選取的變數名
            variableManager.SetVariable(variableName, var_temp); // 呼叫 GetVariable 方法取得變數值
            print($"variableName{variableName}=var_temp{var_temp}");
            
        }
        else if (transform.name.Contains("Get"))
        {
            string variableName = variable_Dropdown.options[variable_Dropdown.value].text;// 取得選取的變數名
            int variableValue = variableManager.GetVariable(variableName); // 呼叫 GetVariable 方法取得變數值
            print($"選取的變數名稱: {variableName}, 變數值: {variableValue}");
            
            GameObject.Find("PrintPanel").transform.Find("IndexPanel").GetComponentInChildren<Text>().text += $" 變數 {variableName} 值為 {variableValue}\r\n";
        }
        else
        {
            Debug.Log("ExpressionArea_1錯誤或是空值");
        }

    }

    public int Expression()
    {   
        string variableName = variable_Dropdown.options[variable_Dropdown.value].text;// 取得選取的變數名
        int variableValue = variableManager.GetVariable(variableName); // 呼叫 GetVariable 方法取得變數值
        print($"選取的變數名稱: {variableName}, 變數值: {variableValue}");
        return variableValue;
    }

    public void ChooseType()
    {
        switch (variablePuzzle_Type)
        {
            case VariablePuzzle_Type.Get:
                print("123");
                break;
        }
    }

}
