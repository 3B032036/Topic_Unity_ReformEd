using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditonalPuzzle : MonoBehaviour
{
    //表達式拼圖
    [SerializeField] private bool SetTrueOrFalse;

    private void Start() {
        transform.tag = "Conditional Statement Puzzle";
        FindObjectOfType<BlockCtrlHandler>().SetTagAllChildren(this.gameObject.transform);
    }
    public bool Judge()
    {
        
        return SetTrueOrFalse;
    }
}
