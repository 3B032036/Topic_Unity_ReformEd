using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfPuzzle : MonoBehaviour
{
    //if拼圖
    [SerializeField] private GameObject ConditionalArea;
    [SerializeField] private GameObject ExecuteArea;

    private void Start() {
        transform.tag = "Execute Statement Puzzle";
        FindObjectOfType<BlockCtrlHandler>().SetTagAllChildren(this.gameObject.transform);
    }

    public void Execute()
    {
        Debug.Log("���b����If�{��");
        if(ConditionalArea.transform.childCount == 0)
        {
            ReturnErrorToStartBox("��if�S��P�_����");
            return;
        }
        if (ConditionalArea.transform.GetChild(0).TryGetComponent<ConditonalPuzzle>(out ConditonalPuzzle ConditonalPuzzle))
        {
            if(ConditonalPuzzle.Judge() == true)
            {
                TrueExecute(GetHowMuchChild());
            }
        }
    }

    public int GetHowMuchChild()
    {
        return ExecuteArea.transform.childCount - 1;
    }

    public void TrueExecute(int childcount)
    {
        if (childcount > 0)
        {
            for (int i = 0; i <= childcount; i++)
            {
                if (ExecuteArea.transform.GetChild(i).tag == "Execute Statement Puzzle")
                {
                    if (ExecuteArea.transform.GetChild(i).TryGetComponent<IfPuzzle>(out IfPuzzle ifpuzzle))
                    {
                        ifpuzzle.Execute();
                    }
                    else if ((ExecuteArea.transform.GetChild(i).TryGetComponent<ForPuzzle>(out ForPuzzle forpuzzle)))
                    {
                        forpuzzle.Execute();
                    }
                }
            }
        }
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
