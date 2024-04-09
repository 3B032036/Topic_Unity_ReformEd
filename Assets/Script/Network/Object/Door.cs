using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;


public class Door : NetworkBehaviour
{
    CharacterMovementHandler characterMovementHandler;

    
    [Networked(OnChanged = nameof(OnDoorChanged))] public NetworkBool DoorState {get; set;}
    [SerializeField] public string netObj_ID;
    
    private void Awake() {

    }
    private void Start() {
        characterMovementHandler = FindObjectOfType<CharacterMovementHandler>();
    }


    public override void FixedUpdateNetwork()
    {
        
    }

    public void DoorStateChanged(){
        DoorState = !DoorState;
    }

    public static void OnDoorChanged(Changed<Door> changed){
        print( $"NetworkObject_{changed.Behaviour.netObj_ID}");
        if (changed.Behaviour.DoorState == true)
            changed.Behaviour.transform.position += 5f * changed.Behaviour.transform.up;

        if (changed.Behaviour.DoorState == false)
            changed.Behaviour.transform.position -= 5f * changed.Behaviour.transform.up;
    }
}
