using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject ExpressionArea_1;
    [SerializeField] private level2 _level2;

    void Start()
    {
        _level2 = FindObjectOfType<level2>();
        transform.tag = "Expression Statement Puzzle";
        FindObjectOfType<ItemOnDrag>().SetTagAllChildren(this.gameObject.transform);
    }

    void Update()
    {
        
    }

    public int Expression()
    {
        
        if (ExpressionArea_1 != null && ExpressionArea_1.transform.childCount > 0)
        {
            var expressionPuzzle = ExpressionArea_1.transform.GetChild(0).GetComponent<ExpressionPuzzle>();
            var variablePuzzle = ExpressionArea_1.transform.GetChild(0).GetComponent<VariablePuzzle>();
            var arrayPuzzle = ExpressionArea_1.transform.GetChild(0).GetComponent<ArrayPuzzle>();

            print($"expressionPuzzle{expressionPuzzle}存在");
            print($"variablePuzzle{variablePuzzle}存在");
            print($"arrayPuzzle{arrayPuzzle}存在");

            int var_temp = 0;
            if (expressionPuzzle != null)
            {
                print("expressionPuzzle != null");
                var_temp = expressionPuzzle.Expression();
            }
            else if (variablePuzzle != null)
            {
                print("variablePuzzle != null");
                var_temp = variablePuzzle.Expression();
            }
            else if (arrayPuzzle != null)
            {
                print("arrayPuzzle != null");
                var_temp = arrayPuzzle.Expression();
            } 
            else
            {
                var_temp = 0; // 如果都為 null，設置為 0
            }

            print($"var_temp={var_temp}");

            if (var_temp < 10)
            {
                int arrayIndexValue = _level2.GetArrarys(var_temp); // 呼叫 GetVariable 方法取得變數值
                print($"陣列索引: {var_temp}, 值: {arrayIndexValue}");
                return arrayIndexValue;
            }
            else
            {
                return -99999;
            }
        }
        else
        {
            return 0;
        }
    }

    
}
