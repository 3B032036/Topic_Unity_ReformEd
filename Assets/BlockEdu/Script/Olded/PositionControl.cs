using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionControl : MonoBehaviour
{

    public static GameObject[] BlockArray;
    public static GameObject currentBlock;
    
    void Start()
    {
        
        BlockArray = GameObject.FindGameObjectsWithTag("Block_Multple");
        for (int i = 0; i < BlockArray.Length; i++)
        {
            Debug.Log(BlockArray[i].name); 
        }
        
    }

    void Update()
    {
        
    }
}
