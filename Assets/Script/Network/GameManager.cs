using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //主要功能:

    //GameManager的靜態實例，以便其他腳本可以存取它
    public static GameManager instance = null;

    byte[] connectionToken = null;

    private void Awake() {
        if (instance == null){
            instance = this;
        }else if(instance != this){
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Check if token is valid,if not get a new one
        if (connectionToken == null){
            connectionToken = ConnectionTokenUtils.NewToken();
            print($"connectionToken生成完畢 {connectionToken}");
            //此呼叫ConnectionTokenUtils之NewToken函數
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetConnectionToken(byte[]connectionToken){
        this.connectionToken = connectionToken;
    }

    public byte[] GetConnectionToken(){
        return connectionToken;
    }



}
