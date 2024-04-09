using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject door;
    
    public void opendoor() {
        door.GetComponent<Door>().DoorStateChanged();
    }
}
