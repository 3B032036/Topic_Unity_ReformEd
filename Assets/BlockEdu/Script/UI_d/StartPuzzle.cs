using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPuzzle : MonoBehaviour
{
    public GameObject ExecuteArea;
    public Text StaticText;
    
    private void Start() {
        //transform.tag = "Execute Statement Puzzle";
        FindObjectOfType<BlockCtrlHandler>().SetTagAllChildren(this.gameObject.transform);
    }

    private void OnTransformChildrenChanged() {
        print ("1");
    }
    

    private void Update() {
        
    }

    public void Execute()
    {
        int ExecuteChildCount = ExecuteArea.transform.childCount - 1;
        if(ExecuteChildCount < 0)
        {
            TextAppear("Start裡面沒有方塊");
            //GameObject.Find("StaticText");//???
            return;
        }
        else
        {
            for (int i = 0; i <= ExecuteChildCount; i++)
            {
                if (ExecuteArea.transform.GetChild(i).tag == "Execute Statement Puzzle")
                {
                    if (ExecuteArea.transform.GetChild(i).TryGetComponent<IfPuzzle>(out IfPuzzle ifpuzzle))
                    {
                        ifpuzzle.Execute();
                    }
                    else if((ExecuteArea.transform.GetChild(i).TryGetComponent<ForPuzzle>(out ForPuzzle forpuzzle)))
                    {
                        forpuzzle.Execute();
                    }
                    else if((ExecuteArea.transform.GetChild(i).TryGetComponent<VariablePuzzle>(out VariablePuzzle variablePuzzle)))
                    {
                        variablePuzzle.Execute();
                    }
                }
            }
        }
    }

    public void TextAppear(string text)
    {
        StaticText.text = (text);
    }
}
