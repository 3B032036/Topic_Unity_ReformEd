using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VariableManager : MonoBehaviour
{
    public Dictionary<string, int> variables = new Dictionary<string, int>();
    public BlockUIHandler blockUIHandler;
    private GameObject OptionPanel;
    private Text text;

    private void Awake() {
        blockUIHandler = FindObjectOfType<BlockUIHandler>();
        text = blockUIHandler.CreateVarPrompt;
    }
    
    private void Start() {
        AddVariable("A");
        blockUIHandler.UpdateAllDropdownManagers();
    }

    public void AddVariable(string name)
    {
        if (!variables.ContainsKey(name))
        {
            int value = 0;//設定初始值
            variables.Add(name, value);
            text.text = $"{name}已創建完畢";
        }
        else
        {
            text.text = $"{name}已經存在，請輸入其他名稱";
            Debug.Log($"{name}數值已經存在");
        }
    }


    public void RemoveVariable(string name)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
        }
        else
        {
            text.text = $"{name}不存在，請檢查名稱是否正確";
            Debug.Log($"{name}數值不存在");
        }
    }

    public void CancelOnClick(string name)
    {
        text.text = $"*請輸入變數名稱";

    }

    public Dictionary<string, int> GetVariables()
    {
        return variables;
    }

    public int GetVariable(string name)
    {
        if (variables.ContainsKey(name))
        {
            return variables[name];
        }
        return 0;
    }
    
    public void SetVariable(string name, int value)
    {
        if (variables.ContainsKey(name))
        {
            variables[name] = value;
        }
    }
    public void Execute() 
    {
        // 在這裡執行積木程式的邏輯
    }
}