using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpressionPuzzle : MonoBehaviour
{
    //表達式拼圖
    [SerializeField] private int Number;
    [SerializeField] public TMP_InputField inputField;
    [SerializeField] public bool IsOperationExpression;

    //運算式
    [SerializeField] private int CalcResult = 0;
    
    [SerializeField] private GameObject ExpressionArea_1;
    [SerializeField] private GameObject ExpressionArea_2;
    [SerializeField] private Dropdown dropdown;

    int var1,var2;
    string CalcType;


    private void Start() {
        transform.tag = "Expression Statement Puzzle";
        FindObjectOfType<ItemOnDrag>().SetTagAllChildren(this.gameObject.transform);
    }

    private void Update()
    {
        
    }
    
    //public void Execute(){
    //    print($"Expression()=>{Expression()}");
    //}

    public int Expression()
    {   
        if (!IsOperationExpression)
        {
            int number;
            if(int.TryParse(inputField.text, out number))//輸入之文字轉為數字
            {   
                Number = number;
            }
            Debug.Log($"Expression已傳回值:{Number}");
        }
        else
        {
            Number = calculate();
        }
        return Number;
    }


    public int calculate()
    {
        if (ExpressionArea_1 != null && ExpressionArea_1.transform.childCount > 0)
        {   
            var expressionPuzzle = ExpressionArea_1.transform.GetChild(0).GetComponent<ExpressionPuzzle>();
            var variablePuzzle = ExpressionArea_1.transform.GetChild(0).GetComponent<VariablePuzzle>();
            var arrayPuzzle = ExpressionArea_1.transform.GetChild(0).GetComponent<ArrayPuzzle>();

            
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
                var1 = -999; // 如果都為 null，設置為 0
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
                var2 = -999; // 如果都為 null，設置為 0
            }

        }
        else
        {
            print("ExpressionArea缺東西!");
        }

        if (dropdown.value != -1)
        {   
            CalcType = dropdown.options[dropdown.value].text;
        }
        else
        {
            print("dropdown無選中!");
        }

        switch (CalcType)
        {
            case "+":
                CalcResult = var1 + var2;
                print($"CalcResult{CalcResult} = var1{var1} + var2{var2}");
                break;
            case "-":
                CalcResult = var1 - var2;
                break;
            case "*":
                CalcResult = var1 * var2;
                break;

            case "/":
                if (var2 != 0)
                {
                    CalcResult = var1 / var2;
                }
                else
                {
                    Debug.LogError("除數不能為零");
                }
                break;

            case "%":
                if (var2 != 0)
                {
                    CalcResult = var1 % var2;
                }
                else
                {
                    Debug.LogError("除數不能為零");
                }
                break;

            default:
                Debug.LogError("無效的運算類型");
                break;
        }
        print($"CalcResult{CalcResult}");
        return CalcResult;
    }

}
