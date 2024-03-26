using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using TMPro;

public class SessionListHandler : MonoBehaviour
{
    public TMP_Text statusText;//定義狀態文字
    public GameObject sessionItemListPrefab; //清單預置件

    public VerticalLayoutGroup verticalLayoutGroup_A;//定義垂直塗層群組

    private void Awake() {
        Clearlist();
    }

    public void Clearlist(){
        foreach (Transform child in verticalLayoutGroup_A.transform){
            //在每次clearlist時，清空清單內垂直顯示的物件，避免重複顯示
            Destroy(child.gameObject);
        }
        statusText.gameObject.SetActive(false);//隱藏狀態顯示
    }

    public void AddToList(SessionInfo sessionInfo){//加入新的session於清單事件
        //以SessionInfoListUIItem類型宣告Session的addedSessionInfoListUIItem(加入清單物件)=(依據預置件生成的房間,垂直清單群組)的SessionInfoListUIItem
        SessionInfoListUIItem addedSessionInfoListUIItem = Instantiate(sessionItemListPrefab, verticalLayoutGroup_A.transform).GetComponent<SessionInfoListUIItem>();
        
        //欲加入session清單中的項目，設定其資訊為函數之input(sessionInfo)
        addedSessionInfoListUIItem.SetSessionInfomation(sessionInfo);

        //將新項目(房間)資訊加入session清單之後，並加入此session(房間)
        addedSessionInfoListUIItem.OnJoinsession += AddedSessionInfoListUIItem_OnJoinSession;
    }

    private void AddedSessionInfoListUIItem_OnJoinSession(SessionInfo sessionInfo){
        NetwokRunnerHandler netwokRunnerHandler = FindObjectOfType<NetwokRunnerHandler>();
        netwokRunnerHandler.JoinGame(sessionInfo);
        
        UIcontrler uicontrler = FindObjectOfType<UIcontrler>();
        uicontrler.OnJoiningServer();
    }

    public void OnNoSessionsFound(){
        Clearlist();
        //當沒有找到房間時
        statusText.text = "沒有找到任何房間><";
        statusText.gameObject.SetActive(true);
    }

    public void OnLookingForGameSessions(){
        Clearlist();
        //當有找到房間時
        statusText.text = "房間列表載入中...";
        statusText.gameObject.SetActive(true);
    }
}
