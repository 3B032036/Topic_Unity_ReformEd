using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUp
{
    //主要作用:在遊戲開始時，將所有函數初始化

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    //區段功能：實例化的預置物件
    public static void InstantiatePrefabs(){
        Debug.Log("--Instantiating objects (實例化物件)--");

        GameObject[] prefabsToInstantiate = Resources.LoadAll<GameObject>("InstantiateOnLoad/");
        
        foreach (GameObject prefab in prefabsToInstantiate){
            Debug.Log($"Creating {prefab.name}");
            GameObject.Instantiate(prefab);
            Debug.Log($"Creating {prefab.name}完成");

        }
        Debug.Log("--Instantiating objects done--(實例化物件 完畢)");

    }
}
