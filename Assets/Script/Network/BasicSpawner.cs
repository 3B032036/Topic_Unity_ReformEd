using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion; //調用Fusion指令集
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class BasicSpawer : MonoBehaviour, INetworkRunnerCallbacks
{
    //定義NetworkRunner

    [SerializeField]
    private NetworkPlayer playerPF;

    CharacterInputHandler characterInputHandler;//宣告玩家輸入

    SessionListHandler sessionListHandler;//宣告房間清單類別物件

    //Token相關
    Dictionary<int, NetworkPlayer> mapTokenIDWithNetworkPlayer;
    private Dictionary<PlayerRef, NetworkPlayer> playerList = new Dictionary<PlayerRef, NetworkPlayer>();

    private void Awake() {
        //房間清單類別物件->依照類別抓取
        sessionListHandler = FindObjectOfType<SessionListHandler>(true);
        
        mapTokenIDWithNetworkPlayer = new Dictionary<int, NetworkPlayer>();
    }

    private void Start() {
        
    }
    
    int GetPlayerToken(NetworkRunner runner, PlayerRef player){
        if (runner.LocalPlayer == player){
            //如果GameManager有抓到TOKEN，那就直接抓出來回傳
            return ConnectionTokenUtils.HashToken(GameManager.instance.GetConnectionToken());
        }else{
            //取得用戶端連接到此主機時儲存的連線令牌
            var token = runner.GetPlayerConnectionToken(player);
            
            //如果不是空值，那就先抓當前玩家的Token
            if(token != null)
                return ConnectionTokenUtils.HashToken(token);

            Debug.LogError($"GetPlayerToken傳回無效令牌");
            return 0;
        }
    }
    

    
    public void SetConnectionTokenMapping(int token, NetworkPlayer networkPlayer){
        mapTokenIDWithNetworkPlayer.Add(token, networkPlayer);
    }
    
    
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        //使用spawn取代原本的生成，使用方式相差無幾
        //Spawn(預置件, 位置, 轉向, 目標物件)
        if (runner.IsServer){
            //為玩家獲取Token
            int playerToken = GetPlayerToken(runner,player);
            
            Debug.Log($"OnPlayerJoined加入伺服器.連線Token:{playerToken}");

            //檢查伺服器是否已經記錄了Token，目的:避免已經存在TOKEN的再生出一個玩家
            if (mapTokenIDWithNetworkPlayer.TryGetValue(playerToken,out NetworkPlayer networkPlayer)){
                Debug.Log($"找到舊的連線token給Token{playerToken}.將控制權重新分配給該該玩家");
                
                networkPlayer.GetComponent<NetworkObject>().AssignInputAuthority(player);
                networkPlayer.Spawned();
            }else{
                
                //NetworkObject networkPlayerObject = runner.Spawn(playerPrefab, Utils.GetRandomSpawnPoint(), Quaternion.identity, player);
                NetworkPlayer spawnpedNetwokPlayer = runner.Spawn(playerPF, Utils.GetRandomSpawnPoint(), Quaternion.identity, player);
                
                //加入玩家到玩家清單內(儲存) 
                playerList.Add(player, spawnpedNetwokPlayer);

                //儲存玩家的Token
                spawnpedNetwokPlayer.token = playerToken;

                //儲存playerToken和衍生的網路玩家之間的映射
                mapTokenIDWithNetworkPlayer[playerToken] = spawnpedNetwokPlayer;
            }
        }
    }

    
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (!runner.IsServer) { return; }
        //當玩家離開
        if (playerList.TryGetValue(player, out NetworkPlayer networkPlayer)){
            print("TryGetValue");
            //runner.Despawn(networkPlayer); //玩家物件消除
            playerList.Remove(player); //將玩家從清單內刪除
            networkPlayer.PlayerLeft(player);
        }
    }


    public void OnInput(NetworkRunner runner, NetworkInput input) {
        //輸入項目
        //定義data為Network的輸入資料
        if (characterInputHandler == null && NetworkPlayer.Local != null)
            characterInputHandler = NetworkPlayer.Local.GetComponent<CharacterInputHandler>();
        
        if (characterInputHandler != null)
            input.Set(characterInputHandler.GetNetworkInput());
            print("GetNetworkInput");
    }



    
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { 
        
    }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) {
        //當sessionListUI狀態active時，List才會更新
        if(sessionListHandler == null)
            return;
        
        //如果房間數量為0
        if (sessionList.Count == 0){
            Debug.Log("尋找房間完畢，無任何房間");
            sessionListHandler.OnNoSessionsFound();//呼叫沒有房間函數
        }else{
            //如果房間數量不為0
            sessionListHandler.Clearlist();//呼叫清理清單函數

            //利用foreach來逐個更新房間清單
            foreach (SessionInfo sessionInfo in sessionList){
                sessionListHandler.AddToList(sessionInfo);//將各個房間(session)的資訊加入清單
                Debug.Log($"已找到{sessionInfo.Name}房間人數{sessionInfo.PlayerCount}");
            }
        }
    }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    
    public void OnSceneLoadStart(NetworkRunner runner) { }
    
    
    public /*async*/ void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) {
        /*
        print("OnHostMigration");
        await runner.Shutdown(shutdownReason: ShutdownReason.HostMigration);
        FindObjectOfType<NetwokRunnerHandler>().StartHostMigration(hostMigrationToken);
        */
    }
    
}
