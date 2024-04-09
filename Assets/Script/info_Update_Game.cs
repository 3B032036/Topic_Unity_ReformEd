using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using Fusion.Sockets;

public class info_Update_Game : MonoBehaviour
{
    //public static TextMeshProUGUI room_info;
    public static Text room_info;//房間資訊欲顯示物件
    public static Text door_info;

    public NetworkRunner networkRunner;
    Door door ;
    
    CharacterMovementHandler characterMovementHandler;
    
    void Start()
    {
        //抓物件下的Component：Text，以方便改變他要顯示的文字
        //room_info = GameObject.Find("room_index_print").GetComponent<Text>();
        
        door_info = GameObject.Find("room_index_print").GetComponent<Text>();
        networkRunner = FindObjectOfType<NetworkRunner>();
        door = FindObjectOfType<Door>();
        characterMovementHandler = FindObjectOfType<CharacterMovementHandler>();
    }

    void Update()
    {
        //將NetworkRunner的房間狀態回傳到room_info上，並更改其文字顯示
        //room_info.text = "房間名稱：" + networkRunner.SessionInfo.Name + "\n" +"玩家數量：" +networkRunner.SessionInfo.PlayerCount;
        if (Utils.playerIsJoin == true)
            door_info.text = $"DoorState = {door.DoorState} \n 遊戲方：{Utils.gamemode_}";
            print($"{Utils.gamemode_}");
    }
}
