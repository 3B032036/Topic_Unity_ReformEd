using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils{
 
    public static Vector3 GetRandomSpawnPoint(){
        return new Vector3(Random.Range(-20,20),4,Random.Range(-20,20));
    }

    public static void SetRenderLayerInChildren(Transform transform, int layerNumber){
        foreach (Transform trans in transform.GetComponentsInChildren<Transform>(true)){
            trans.gameObject.layer = layerNumber;
        }
    }
    
    public static bool playerIsJoin;
    public static string gamemode_;
}

