using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpressionPuzzle : MonoBehaviour
{
    //表達式拼圖
    [SerializeField] private int Number = 0;
    [SerializeField] private Text text;

    private void Start() {
        transform.tag = "Expression Statement Puzzle";
        FindObjectOfType<BlockCtrlHandler>().SetTagAllChildren(this.gameObject.transform);
    }

    private void Update()
    {
        //text.text = Number.ToString();
    }
    public int Expression()
    {
        return Number;
    }

}
