using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    float cameraRotationX = 0;
    float cameraRotationY = 0;

    public Camera MainCamera;
    public Transform CameraAnchorPoint;//Target(CameraAnchorPoint)
    Vector2 viewInput;
    public Transform player;
    NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;
    ObjectNetwork objectNetwork;
    public Door door;

    private void Awake() {
        MainCamera = FindObjectOfType<Camera>();
        networkCharacterControllerPrototypeCustom = GetComponentInParent<NetworkCharacterControllerPrototypeCustom>();    
        objectNetwork = FindObjectOfType<ObjectNetwork>();
        door = FindObjectOfType<Door>();
    }


    

    void Start()
    {
        //if攝影機存在，則其父物件為空，分開控制
        if (MainCamera.enabled)
            MainCamera.transform.parent = null;
    }

    private void LateUpdate() {
         if (CameraAnchorPoint == null)
            return;

        if (!MainCamera.enabled)
            return;

        MainCamera.transform.position = CameraAnchorPoint.position;
        
        cameraRotationX += viewInput.y *Time.deltaTime* networkCharacterControllerPrototypeCustom.viewUpDownRotationSpeed;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90, 90);
        cameraRotationY += viewInput.x * Time.deltaTime *  networkCharacterControllerPrototypeCustom.rotationSpeed;
        
        transform.rotation = Quaternion.Euler(cameraRotationX, cameraRotationY, 0);

        //使相機之角度追隨玩家
    }

    private void Update() {
        //networkCharacterControllerPrototypeCustom.Rotate(cameraRotationY);
    }

    public void SetViewInputVector(Vector3 viewInput){
        this.viewInput = viewInput;
    }
}
