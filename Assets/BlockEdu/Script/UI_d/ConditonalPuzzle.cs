using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditonalPuzzle : MonoBehaviour
{
    //表達式拼圖
    [SerializeField] private bool SetTrueOrFalse;
    [SerializeField] private GameObject ExpressionArea_1;
    [SerializeField] private GameObject ExpressionArea_2;
    [SerializeField] private Dropdown dropdown;

    int var1,var2;
    string JudgeType;

    private void Start() {
        transform.tag = "Conditional Statement Puzzle";
        FindObjectOfType<ItemOnDrag>().SetTagAllChildren(this.gameObject.transform);
    }

    public void Execute()
    {
        
    }


    public bool Judge()
    {
        Debug.Log("Conditonal已執行");
        if (ExpressionArea_1 != null && ExpressionArea_1.transform.childCount > 0)
        {   
            var expressionPuzzle = ExpressionArea_1.transform.GetChild(0).GetComponent<ExpressionPuzzle>();
            var variablePuzzle = ExpressionArea_1.transform.GetChild(0).GetComponent<VariablePuzzle>();
            var arrayPuzzle = ExpressionArea_1.transform.GetChild(0).GetComponent<ArrayPuzzle>();

            int var1 = 0;
            if (expressionPuzzle != null)
            {
                var1 = expressionPuzzle.Expression();
            }
            else if (variablePuzzle != null)
            {
                var1 = variablePuzzle.Expression();
            }
            else if (arrayPuzzle != null)
            {
                var1 = arrayPuzzle.Expression();
            } 
            else
            {
                var1 = 0; // 如果都為 null，設置為 0
            }

        }
        else
        {
            print("ExpressionArea缺東西!");
        }

        if (ExpressionArea_2 != null && ExpressionArea_2.transform.childCount > 0)
        {   
            var expressionPuzzle = ExpressionArea_2.transform.GetChild(0).GetComponent<ExpressionPuzzle>();
            var variablePuzzle = ExpressionArea_2.transform.GetChild(0).GetComponent<VariablePuzzle>();
            var arrayPuzzle = ExpressionArea_2.transform.GetChild(0).GetComponent<ArrayPuzzle>();

            int var2 = 0;
            if (expressionPuzzle != null)
            {
                var2 = expressionPuzzle.Expression();
            }
            else if (variablePuzzle != null)
            {
                var2 = variablePuzzle.Expression();
            }
            else if (arrayPuzzle != null)
            {
                var2 = arrayPuzzle.Expression();
            } 
            else
            {
                var2 = 0; // 如果都為 null，設置為 0
            }
        }
        else
        {
            print("ExpressionArea缺東西!");
        }

        if (dropdown.value != -1)
        {   
            JudgeType = dropdown.options[dropdown.value].text;
        }
        else
        {
            print("dropdown無選中!");
        }

        
        switch (JudgeType)
        {
            case ">":
                SetTrueOrFalse = var1 > var2;
                print($"SetTrueOrFalse => var1{var1} > var2{var2} 為 {SetTrueOrFalse}");
                break;
            case "<":
                SetTrueOrFalse = var1 < var2;
                break;
            case "==":
                SetTrueOrFalse = var1 == var2;
                break;
            case ">=":
                SetTrueOrFalse = var1 >= var2;
                break;
            case "<=":
                SetTrueOrFalse = var1 <=var2;
                break;
            default:
                print("無效的 JudgeType");
                break;
        }
        print($"SetTrueOrFalse=>{SetTrueOrFalse}");
        return SetTrueOrFalse;
    }

    private void ReturnErrorToStartBox(string text)
    {
        GameObject FindStartBox = this.gameObject;
        while (!FindStartBox.transform.gameObject.TryGetComponent<StartPuzzle>(out StartPuzzle a))
        {
            FindStartBox = FindStartBox.transform.parent.transform.parent.gameObject;
        }
        FindStartBox.GetComponent<StartPuzzle>().TextAppear(text);
    }
}
