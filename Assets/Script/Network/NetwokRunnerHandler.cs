using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion; //調用Fusion指令集
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Linq;

public class NetwokRunnerHandler : MonoBehaviour
{
    public NetworkRunner networkRunnerPrefab;//networkrunner預置件定義
    NetworkRunner networkRunner;




    private void Awake() {
        //宣告場景內networkrunner物件
        NetworkRunner networkRunnerInScene = FindObjectOfType<NetworkRunner>();

        //如果當下已經有一個networkrunner，就不用再多創建一個
        if (networkRunnerInScene != null)
            networkRunner = networkRunnerInScene;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (networkRunner == null){
            networkRunner = Instantiate(networkRunnerPrefab);
            networkRunner.name = "NetworkRunner";
            Debug.Log("NetworkRunner Instantiate done");
        
            if (SceneManager.GetActiveScene().name != "Start_meau"){
                var clientTask = InitializeNetworkRunner(networkRunner,GameMode.AutoHostOrClient,GameManager.instance.GetConnectionToken(),scenevalue.roomname,NetAddress.Any(),SceneManager.GetActiveScene().buildIndex,null);
            }
        }

    }

    /*
    public void StartHostMigration(HostMigrationToken hostMigrationToken){
        networkRunner = Instantiate(networkRunnerPrefab);
        networkRunner.name = "NetworkRunner - Migrated";
        Debug.Log("NetworkRunner - Migrated Instantiate done");

        var clientTask = InitializeNetworkRunnerHostMigration(networkRunner, hostMigrationToken);
        print("Host migration 已開始");
    }
    */

    // Update is called once per frame
    void Update()
    {
        
    }

    INetworkSceneManager GetSceneManager(NetworkRunner Runner){
        var sceneManager = Runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();
        if (sceneManager == null){
            sceneManager = Runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }
        return sceneManager;
    }
    protected virtual Task InitializeNetworkRunner(NetworkRunner runner,GameMode gameMode,byte[] connectionToken,string sessionName,NetAddress address, SceneRef scene, Action<NetworkRunner> initialized){
        var sceneManager = GetSceneManager(runner);

        if (sceneManager == null)
        {
            //Handle networked objects that already exits in the scene
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        networkRunner.ProvideInput = true; //告訴networkRunner要接受Input

        return runner.StartGame(new StartGameArgs{
            GameMode = gameMode, //指定模式HOST或是Client
            Address = address,//網路位置
            Scene =scene,//場景
            SessionName = sessionName,//房間
            CustomLobbyName = "OurLobbyID",//大廳
            Initialized = initialized,//
            SceneManager = sceneManager,//場景管理
            ConnectionToken = connectionToken,//連線之Token_主要確認玩家是否已經存在，避免重新生成與混亂
        });
    }

    /*
    protected virtual Task InitializeNetworkRunnerHostMigration(NetworkRunner runner,HostMigrationToken hostMigrationToken){
        var sceneManager = GetSceneManager(runner);

        if (sceneManager == null)
        {
            //處理場景中已經存在的網路對象
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        networkRunner.ProvideInput = true; //告訴networkRunner要接受Input

        return runner.StartGame(new StartGameArgs{
            SceneManager = sceneManager,
            HostMigrationToken = hostMigrationToken,//包含重新啟動Runner所需的所有信息
            HostMigrationResume = HostMigrationResume,//將呼叫該選項以恢復模擬
            ConnectionToken = GameManager.instance.GetConnectionToken()
        });
    }

    void HostMigrationResume(NetworkRunner runner){
        print("HostMigrationResume 開始");

        //從舊Host取得每個 Network Object的引用
        foreach (var resumeNetworkObject in runner.GetResumeSnapshotNetworkObjects()){
                //抓取所有玩家對象的 NetworkCharacterControllerPrototypeCustom
                print("HostMigrationResume player - enter foreach");
                
                if(resumeNetworkObject.TryGetBehaviour<NetworkCharacterControllerPrototypeCustom>(out var characterController)){
                    runner.Spawn(resumeNetworkObject, position : characterController.ReadPosition(), rotation : characterController.ReadRotation(), onBeforeSpawned : (runner, newNetworkObject) =>{
                        newNetworkObject.CopyStateFrom(resumeNetworkObject);

                        //將已連線的token對應到新的網路播放器
                        if (resumeNetworkObject.TryGetBehaviour<NetworkPlayer>(out var oldNetworkPlayer)){
                            //用於重新連接的儲存玩家token
                            FindObjectOfType<BasicSpawer>().SetConnectionTokenMapping(oldNetworkPlayer.token, newNetworkObject.GetComponent<NetworkPlayer>());
                        }
                    });
                    print("HostMigrationResume player");
                }
        }

    }
    */
    public void OnJoinLobby(){
        //呼叫時，傳遞給client的任務是:JoinLobby
        var clientTask = JoinLobby();
    }

    private async Task JoinLobby(){
        string lobbyID = "OurLobbyID";
        Debug.Log($"JoinLobby");
        var result  = await networkRunner.JoinSessionLobby(SessionLobby.Custom, lobbyID);//result = 加入大廳後抓取的參數(自訂,房間名稱)

        if (!result.Ok){ //如果result存在成功讀取：
            Debug.LogError($"尚未加入大廳{lobbyID}"); 
        }else{
            Debug.Log($"成功加入大廳{lobbyID}");
        }
    }

    //創建遊戲
    public void CreateGame(string sessionName, string sceneName){
        Debug.Log($"創建{sessionName} 場景{sceneName} 建置細節{SceneUtility.GetBuildIndexByScenePath($"scenes/{sceneName}")}");
        var clientTask = InitializeNetworkRunner(networkRunner,GameMode.Host,GameManager.instance.GetConnectionToken(),sessionName,NetAddress.Any(),SceneUtility.GetBuildIndexByScenePath($"Scenes/{sceneName}"),null);
    }

    //玩家加入遊戲
    public void JoinGame(SessionInfo sessionInfo){
        Debug.Log($"加入session{sessionInfo.Name}");
        var clientTask = InitializeNetworkRunner(networkRunner,GameMode.Client,GameManager.instance.GetConnectionToken(),sessionInfo.Name,NetAddress.Any(),SceneManager.GetActiveScene().buildIndex,null);
    }
}
