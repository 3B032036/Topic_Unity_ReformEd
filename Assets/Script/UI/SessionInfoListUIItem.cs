using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using TMPro;
using System;

public class SessionInfoListUIItem : MonoBehaviour
{
    /*------變數宣告區(Start)------*/
    public TMP_Text sessionNameText; //Session(房間)名稱
    public TMP_Text playerCountText; //Session(房間)玩家人數
    public Button joinButton; //加入按鈕
    public SessionInfo sessionInfo; //定義SessionInfo(房間資訊)
    /*------變數宣告區(End)------*/

    /*------Event宣告區(Start)------*/
    public event Action<SessionInfo> OnJoinsession; //加入房間事件
    /*------Event宣告區(End)------*/

    public void SetSessionInfomation(SessionInfo sessionInfo){
        this.sessionInfo = sessionInfo; //sessionInfo_A的內容=此函數input的sessionInfo
        
        sessionNameText.text = sessionInfo.Name; //房間名稱=sessionInfo中抓到的房間名稱
        playerCountText.text = $"{sessionInfo.PlayerCount.ToString()} / {sessionInfo.MaxPlayers.ToString()}"; //顯示玩家人數多寡，如：5/10(目前人數/最大人數)

        bool isJoinButtonActive = true; //是否讓加入按鈕啟用（布林值）

        if (sessionInfo.PlayerCount >= sessionInfo.MaxPlayers){
            //若抓到的目前玩家人數大於等於最大人數，則加入按鈕不啟用（布林值）
            isJoinButtonActive = false;
        }
        joinButton.gameObject.SetActive(isJoinButtonActive); //使加入按鈕依布林值控制
    }

    public void OnClick(){//點擊事件
        //當玩家加入session時呼叫session的資訊(sessionInfo_A)
        OnJoinsession?.Invoke(sessionInfo);
    }
    
}
