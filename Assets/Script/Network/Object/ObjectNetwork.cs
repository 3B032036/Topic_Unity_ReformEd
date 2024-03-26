using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using UnityEngine.UIElements;
using System;

public class ObjectNetwork : NetworkBehaviour
{
    //public NetworkObject Cube;
    void Start()
    {
        
    }

    void Update()
    {
        
    }


    
    public override void FixedUpdateNetwork() {
        //Ray ray = LocalCamera.MainCamera.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        transform.position += 3* transform.forward * Runner.DeltaTime;

        if (GetInput(out NetworkInputData networkInputData)){
            if (networkInputData.IsPressE == true){
                /*
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.DrawLine(LocalCamera.MainCamera.transform.position, hit.transform.position, Color.red, 0.1f, true);
                    Debug.Log(hit.transform.name);

                    if (hit.transform.name == "Cube"){
                        ObjectCallTouched("Cube");
                        print("networkInputData.IsPressE-Cub位移");
                    }
                }
                */
                print("networkInputData.IsPressE-已接收");
            }
        } 
    }
    public void ObjectCallTouched(string obj){
        if (obj == "Cube"){
            Debug.Log("ObjectCallTouched->Cube");
            //Cube.transform.position *= 2;
        }
    }
    
}
