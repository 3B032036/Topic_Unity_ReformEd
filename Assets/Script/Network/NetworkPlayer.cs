using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer Local { get; set; }
    
    [Networked] public int token{get; set;}

    public Transform playerModel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
            Utils.playerIsJoin = true;

            //設定本地玩家模型的圖層
            //Utils.SetRenderLayerInChildren(playerModel, LayerMask.NameToLayer("localplayermodel"));

            //禁用主相機
            //Camera.main.gameObject.SetActive(false);
            Debug.Log("生成本地玩家");
        }
        else
        {
            Utils.playerIsJoin = true;
            //如果我們不是本地玩家，停用相機
            Camera localCamera = GetComponentInChildren<Camera>();
            localCamera.enabled = false;

            //場景中只允許1個audiolistener，因此停用遠端玩家的audiolistener
            AudioListener audiolistener = GetComponentInChildren<AudioListener>();
            audiolistener.enabled = false;

            Debug.Log("生成遠程玩家");
        }
        transform.name = $"P_{Object.Id}";
    }


    public void PlayerLeft(PlayerRef player)
    {   
        print($"Huh?");

        
        if (player == Object.InputAuthority){
            print($"P_{Object.Id}刪除開始");
            //Runner.Despawn(Object);
        }
        
    }
}
