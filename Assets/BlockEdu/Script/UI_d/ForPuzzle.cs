using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ForPuzzle : MonoBehaviour
{
    //for拼圖
    [SerializeField] private GameObject ExecuteArea;
    [SerializeField] private GameObject ExpressionArea_1;

    private void Start() {
        transform.tag = "Execute Statement Puzzle";
        FindObjectOfType<ItemOnDrag>().SetTagAllChildren(this.gameObject.transform);
    }

    public void Execute()
    {
        Debug.Log("已開始執行For");
        bool IsExecute = IsTrueExecute();
        if (IsExecute == true)
        {
            if (ExpressionArea_1.transform.childCount == 0)
            {
                ReturnErrorToStartPuzzle("For未執行");
                return;
            }

            var expressionPuzzle = ExpressionArea_1.transform.GetChild(0).GetComponent<ExpressionPuzzle>();
            var variablePuzzle = ExpressionArea_1.transform.GetChild(0).GetComponent<VariablePuzzle>();
            var arrayPuzzle = ExpressionArea_1.transform.GetChild(0).GetComponent<ArrayPuzzle>();

            int RepeatCounter = 0;
            if (expressionPuzzle != null)
            {
                RepeatCounter = expressionPuzzle.Expression();
            }
            else if (variablePuzzle != null)
            {
                RepeatCounter = variablePuzzle.Expression();
            }
            else if (arrayPuzzle != null)
            {
                RepeatCounter = arrayPuzzle.Expression();
            } 
            else
            {
                RepeatCounter = 0; // 如果都為 null，設置為 0
            }

            if (RepeatCounter > 0)
            {
                for (int i = 1; i <= RepeatCounter; i++)
                {   
                    Debug.Log("For開始執行" + i + "次");
                    int ExecuteChildCount = ExecuteArea.transform.childCount - 1;
                    if (ExecuteChildCount > -1)
                    {
                        for (int j = 0; j <= ExecuteChildCount; j++)
                        {
                            if (ExecuteArea.transform.GetChild(j).tag == "Execute Statement Puzzle")
                            {
                                if (ExecuteArea.transform.GetChild(j).TryGetComponent<IfPuzzle>(out IfPuzzle ifpuzzle_))
                                {
                                    ifpuzzle_.Execute();
                                }
                                else if ((ExecuteArea.transform.GetChild(j).TryGetComponent<ForPuzzle>(out ForPuzzle forpuzzle_)))
                                {
                                    forpuzzle_.Execute();
                                }
                                else if((ExecuteArea.transform.GetChild(j).TryGetComponent<VariablePuzzle>(out VariablePuzzle variablePuzzle_)))
                                {
                                    print("進入variablePuzzle_");
                                    variablePuzzle_.Execute();
                                }

                                /*
                                else if ((ExecuteArea.transform.GetChild(j).TryGetComponent<FreePuzzle>(out FreePuzzle freepuzzle)))
                                {
                                    //freepuzzle.Execute();
                                }
                                */
                            }
                        }
                    }
                }
            }
        }
    }

    IEnumerator DelayMethod(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        print("delay");
    }

    private bool IsTrueExecute()
    {

        return true;
    }


    private void ReturnErrorToStartPuzzle(string text)
    {
        GameObject FindStartBox = this.gameObject;
        while (!FindStartBox.transform.gameObject.TryGetComponent<StartPuzzle>(out StartPuzzle a))
        {
            FindStartBox = FindStartBox.transform.parent.transform.parent.gameObject;
        }
        FindStartBox.GetComponent<StartPuzzle>().TextAppear(text);
    }
}
