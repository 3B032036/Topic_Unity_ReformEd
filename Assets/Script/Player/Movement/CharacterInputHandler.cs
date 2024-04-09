using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    public Vector2 moveInputVector = Vector2.zero;
    public Vector2 viewInputVector = Vector2.zero;
    //public PlayerContrler playerContrler;

    public FirstPersonCamera LocalCamera;
    bool isJumpButtonPressed = false;
    bool isUseButtonPressed = false;
    string hitObject;
    CharacterMovementHandler characterMovementHandler;
    bool escPress;
    Camera localCamera;
    //public Door door;



    private void Awake() {
        characterMovementHandler = GetComponent<CharacterMovementHandler>();
        LocalCamera = GetComponentInChildren<FirstPersonCamera>();
        localCamera = GetComponentInChildren<Camera>();
        //door = FindObjectOfType<Door>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //視角input
        viewInputVector.x = Input.GetAxis("Mouse X");
        viewInputVector.y = Input.GetAxis("Mouse Y") * -1;

        //視角更新
        LocalCamera.SetViewInputVector(viewInputVector);

        //移動input
        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump")){
            isJumpButtonPressed = true;
        }

        if (Input.GetKeyDown("e")){
            isUseButtonPressed = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape)){
            escPress = !escPress;
        }

        if (escPress == true){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }else{
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public NetworkInputData GetNetworkInput(){
        //獲取網路input
        NetworkInputData networkInputData = new NetworkInputData();//定義networkInputData為NetworkInputData
        
        //視角data
        networkInputData.aimForwardVector = LocalCamera.transform.forward;//viewInputVector為x軸向量值

        //移動data
        networkInputData.movementInput = moveInputVector;//networkInputData為移動向量值
        
        //跳躍與使用data
        networkInputData.IsJumping = isJumpButtonPressed;
        networkInputData.IsPressE = isUseButtonPressed;
        //networkInputData.CubeTouch = hitObject;

        isJumpButtonPressed = false;//恢復狀態 避免重新傳值
        isUseButtonPressed = false;//恢復狀態 避免重新傳值

        return networkInputData;//回傳
    }
}
