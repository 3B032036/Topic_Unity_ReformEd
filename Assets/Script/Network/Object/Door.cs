using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;


public class Door : NetworkBehaviour
{
    [SerializeField]
    //private float bulletSpeed = 5f;
    public bool DoorIsHited ;
    [Networked(OnChanged = nameof(OnDoorChanged))] public NetworkBool DoorState {get; set;}
    private void Awake() {
        
    }
    private void Start() {
        DoorState = false;
    }


    public override void FixedUpdateNetwork()
    {
        if (DoorIsHited == true ){
            DoorStateChanged();
        }
    }
    public void DoorStateChanged(){
        DoorState = !DoorState;
    }

    public static void OnDoorChanged(Changed<Door> changed){
        if (changed.Behaviour.DoorState == true)
            changed.Behaviour.transform.position += 5f * changed.Behaviour.transform.up;
            changed.Behaviour.DoorIsHited = false;

        if (changed.Behaviour.DoorState == false)
            changed.Behaviour.transform.position -= 5f * changed.Behaviour.transform.up;
            changed.Behaviour.DoorIsHited = false;
    }
}
