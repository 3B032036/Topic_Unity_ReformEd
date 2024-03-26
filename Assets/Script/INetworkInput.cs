using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;

//參考資料：https://doc.photonengine.com/zh-tw/fusion/current/tutorials/host-mode-basics/2-setting-up-a-scene
public enum InputButtons{
    JUMP,
    OpenDoor
}

public struct NetworkInputData : INetworkInput
{
    public Vector2 movementInput; //定義vector3之接受移動資料的輸入
    //public float rotationInput;//定義旋轉之角度輸入
    public Vector3 aimForwardVector;//定義鏡頭指向之向量
    public NetworkButtons buttons; //定義使用fusion自有的線上Button
    public bool IsJumping;
    public bool IsPressE;

    //public int hitObjectID;//互動物件
    //public string CubeTouch;
}
