using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForPuzzle : MonoBehaviour
{
    //for拼圖
    [SerializeField] private GameObject RepeatCounter;
    [SerializeField] private GameObject ExecuteArea;

    private void Start() {
        transform.tag = "Execute Statement Puzzle";
        FindObjectOfType<BlockCtrlHandler>().SetTagAllChildren(this.gameObject.transform);
    }

    public void Execute()
    {
        Debug.Log("已開始執行For");
        bool IsExecute = IsTrueExecute();
        if (IsExecute == true)
        {
            if (RepeatCounter.transform.childCount == 0)
            {
                ReturnErrorToStartPuzzle("For裡面並無子物件");
                return;
            }
            if (RepeatCounter.transform.GetChild(0).TryGetComponent<ExpressionPuzzle>(out ExpressionPuzzle expressionpuzzle))
            {
                for (int i = 1; i <= expressionpuzzle.Expression(); i++)
                {
                    Debug.Log("For開始執行" + i + "次");
                    int ExecuteChildCount = ExecuteArea.transform.childCount - 1;
                    if (ExecuteChildCount > 0)
                    {
                        for (int j = 0; j <= ExecuteChildCount; j++)
                        {
                            if (ExecuteArea.transform.GetChild(j).tag == "Execute Statement Puzzle")
                            {
                                if (ExecuteArea.transform.GetChild(j).TryGetComponent<IfPuzzle>(out IfPuzzle ifpuzzle))
                                {
                                    ifpuzzle.Execute();
                                }
                                else if ((ExecuteArea.transform.GetChild(j).TryGetComponent<ForPuzzle>(out ForPuzzle forpuzzle)))
                                {
                                    forpuzzle.Execute();
                                }
                                else if((ExecuteArea.transform.GetChild(j).TryGetComponent<VariablePuzzle>(out VariablePuzzle variablePuzzle)))
                                {
                                    variablePuzzle.Execute();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(5);
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
