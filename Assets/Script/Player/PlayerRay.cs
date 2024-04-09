using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class PlayerRay : NetworkBehaviour {
    [SerializeField]
    private Transform Camera;
    [SerializeField]
    private float MaxUseDistance = 5f;

    

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void ChangeInteractiveStateRPC()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance))
        {
            if (hit.collider.TryGetComponent<DoorTrigger>(out DoorTrigger doortrigger))
            {
                doortrigger.opendoor();
            }
        }
    }


}

