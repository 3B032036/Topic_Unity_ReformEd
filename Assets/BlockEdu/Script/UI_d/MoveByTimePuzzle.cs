using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByTimePuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.tag = "Execute Statement Puzzle";
        FindObjectOfType<BlockCtrlHandler>().SetTagAllChildren(this.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
