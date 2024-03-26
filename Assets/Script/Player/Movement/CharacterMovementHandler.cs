using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharacterMovementHandler : NetworkBehaviour
{

    Vector2 viewInput;

    //Rotation
    float cameraRotationX = 0;

    //Other components
    NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;
    Camera localCamera;
    FirstPersonCamera LocalCamera;
    public ObjectNetwork objectNetwork;
    public Door door;
    RaycastHit hit;
    private void Awake()
    {
        networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
        localCamera = GetComponentInChildren<Camera>();
        //LocalCamera = GetComponentInChildren<FirstPersonCamera>();
        objectNetwork = FindObjectOfType<ObjectNetwork>();
        door = FindObjectOfType<Door>();
        door.DoorState = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraRotationX += viewInput.y * Time.deltaTime * networkCharacterControllerPrototypeCustom.viewUpDownRotationSpeed;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90, 90);

        localCamera.transform.localRotation = Quaternion.Euler(cameraRotationX, 0, 0);
    }

    public override void FixedUpdateNetwork()
    {
        //Get the input from the network
        if (GetInput(out NetworkInputData networkInputData))
        {
            transform.forward = networkInputData.aimForwardVector;//接受input後處理本地玩家轉向

            //取消X軸上的旋轉，因為我們不希望角色傾斜
            Quaternion rotation = transform.rotation;
            rotation.eulerAngles = new Vector3(0,rotation.eulerAngles.y,rotation.eulerAngles.z);
            transform.rotation = rotation;

            Vector3 moveDirection = transform.forward * networkInputData.movementInput.y  + transform.right * networkInputData.movementInput.x;
            //定義moveVector為x軸向量加上y軸向量
            moveDirection.Normalize();//將輸入近來的參數做normalized

            networkCharacterControllerPrototypeCustom.Move(moveDirection);
            
            if (networkInputData.IsJumping == true){
                //呼叫networkCharacterController預設跳躍事件
                Debug.Log("JUMP");
                networkCharacterControllerPrototypeCustom.Jump();
            }

            //Ray ray = localCamera.ScreenPointToRay(Input.mousePosition);
            

            //if (networkInputData.IsPressE == true){
                //door.DoorIsHited = true;
                //Debug.Log("DoorIsHited");
                /*
                if (Physics.Raycast(ray, out hit)){
                    Debug.DrawRay(ray.origin, ray.direction);
                    Debug.DrawRay(ray.origin, ray.origin + ray.direction * 100);
                    
                    //Debug.DrawLine(localCamera.transform.position, hit.transform.position, Color.red, 0.1f, true);
                    Debug.Log(hit.transform.name);
                    door.DoorIsHited = Physics.Raycast(ray, out hit);
                    //door.HitObjectName = hit.transform.name;
                }
                */
                //Ray ray = localCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                
                if (networkInputData.IsPressE == true){
                    if (Physics.Raycast(localCamera.transform.position,localCamera.transform.forward, out hit, 100f)){
                        Debug.DrawLine(localCamera.transform.position, hit.transform.position, Color.red, 10f, true);
                        //Debug.Log(hit.point);
                        Debug.Log(hit.transform.name);
                        //door.DoorStateChanged();
                        door.DoorIsHited = !door.DoorIsHited;
                        //door.HitObjectName = hit.transform.name;
                    }
                }
            //}

            
            
        }
    }
}
