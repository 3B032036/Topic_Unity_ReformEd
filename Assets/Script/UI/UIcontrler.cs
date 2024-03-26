using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIcontrler : MonoBehaviour
{
    //UI控制-宣告遊戲物件
    [Header("未登入畫面物件")]
    public GameObject login_meau_ob;//登入物件
    public GameObject signup_meau_ob;//註冊物件

    public GameObject start_meau_ob;//遊戲開始介面物件
    
    [Header("登入後介面")]
    public GameObject create_room_meau_ob;//創建遊戲介面
    public GameObject join_room_meau_ob;//加入遊戲介面

    public GameObject start_room_meau_login_session_ob;//開始遊戲介面
    public TMP_InputField create_room_input_ob;//創建房間名稱物件
    public TMP_InputField join_room_input_ob;//加入房間名稱物件




    void Start()
    {
        /*---------設定選單初始數值"登入與註冊選單視窗隱藏"--------*/
        start_meau_ob.SetActive(true);
        login_meau_ob.SetActive(false);
        signup_meau_ob.SetActive(false);
        /*---------設定選單初始數值"登入後切換為房間介面"--------*/
        start_room_meau_login_session_ob.SetActive(false);
        create_room_meau_ob.SetActive(false);
        join_room_meau_ob.SetActive(false);
        /*-----------------------------------------------------*/
    }

    #region 視窗顯示
    //未登入介面-總選單
    public void start_meau_print(){
        start_meau_ob.SetActive(true);
        login_meau_ob.SetActive(false);
        signup_meau_ob.SetActive(false);
        create_room_meau_ob.SetActive(false);
        start_room_meau_login_session_ob.SetActive(false);
        create_room_meau_ob.SetActive(false);
        join_room_meau_ob.SetActive(false);
    }
    //未登入介面-註冊選單
    public void signup_meau_print(){
        start_meau_ob.SetActive(false);
        login_meau_ob.SetActive(false);
        signup_meau_ob.SetActive(true);
        create_room_meau_ob.SetActive(false);
        start_room_meau_login_session_ob.SetActive(false);
        create_room_meau_ob.SetActive(false);
        join_room_meau_ob.SetActive(false);
    }
    //未登入介面-登入選單
    public void login_meau_meau_print(){
        start_meau_ob.SetActive(false);
        login_meau_ob.SetActive(true);
        signup_meau_ob.SetActive(false);
        create_room_meau_ob.SetActive(false);
        start_room_meau_login_session_ob.SetActive(false);
        create_room_meau_ob.SetActive(false);
        join_room_meau_ob.SetActive(false);
    }

    //已登入介面-總選單
    public void start_room_meau_login_session_print(){
        if (GameObject.Find("Toggle").GetComponent<Toggle>().isOn){
            start_meau_ob.SetActive(false);
            login_meau_ob.SetActive(false);
            signup_meau_ob.SetActive(false);
            create_room_meau_ob.SetActive(false);
            start_room_meau_login_session_ob.SetActive(true);
            create_room_meau_ob.SetActive(false);
            join_room_meau_ob.SetActive(false);
        }else{
            start_meau_print();
        }
    }
    //已登入介面-創建房間
    public void create_room_meau_print(){
        start_meau_ob.SetActive(false);
        login_meau_ob.SetActive(false);
        signup_meau_ob.SetActive(false);
        create_room_meau_ob.SetActive(false);
        start_room_meau_login_session_ob.SetActive(false);
        create_room_meau_ob.SetActive(true);
        join_room_meau_ob.SetActive(false);
    }
    //已登入介面-加入房間
    
    public void join_room_meau_print(){
        start_meau_ob.SetActive(false);
        login_meau_ob.SetActive(false);
        signup_meau_ob.SetActive(false);
        create_room_meau_ob.SetActive(false);
        start_room_meau_login_session_ob.SetActive(false);
        create_room_meau_ob.SetActive(false);
        join_room_meau_ob.SetActive(true);
        OnFindGameClicked();
    }

    public void create_room_with_text(){
        if (string.IsNullOrWhiteSpace(create_room_input_ob.text)){
            Debug.Log("輸入房間名稱錯誤或存在空格");
        }else{
            NetwokRunnerHandler netwokRunnerHandler = FindObjectOfType<NetwokRunnerHandler>();
            netwokRunnerHandler.CreateGame(create_room_input_ob.text, "MainGame");
        }
    }
    
    #endregion

    //當按下進入大廳按鈕
    public void OnFindGameClicked(){
        //BasicSpawer networkRunnerHandler = FindObjectOfType<BasicSpawer>();
        NetwokRunnerHandler networkRunnerHandler = FindObjectOfType<NetwokRunnerHandler>();
        networkRunnerHandler.OnJoinLobby();
        FindObjectOfType<SessionListHandler>(true).OnLookingForGameSessions();
    }

    public void OnJoiningServer(){
        
    }

    void Update()
    {
        
    }
}
