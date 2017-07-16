using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Connecting : MonoBehaviour {
    List<PhotonPlayer> PlayerList; //儲存玩家(要照順序)
    

    PhotonPlayer player; //玩家
    string playerName; //玩家名稱
    string partyColor; //玩家政黨顏色
    string role; //玩家選擇角色
    public Image PlayerCharacterImg; //角色圖片
    
    //人物圖片
    public Sprite role1;
    public Sprite role2;
    public Sprite role3;
    public Sprite role4;

    // Use this for initialization
    void Start () {
        PlayerList.Add(PhotonNetwork.masterClient); //1
        PlayerList.Add(PhotonNetwork.masterClient.GetNext()); //2
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext()); //3
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext()); //4

        player = PhotonNetwork.player; //取得現在的player
        playerName = PhotonNetwork.playerName; //取得現在的player的暱稱
        partyColor = (string) PhotonNetwork.player.CustomProperties["PartyColor"];
        role = (string)PhotonNetwork.player.CustomProperties["Role"];

        if(role == "蔡中文")
        {
            PlayerCharacterImg.GetComponent<Image>().sprite = role1;
        }
        else if(role == "馬英八")
        {
            PlayerCharacterImg.GetComponent<Image>().sprite = role2;
        }
        else if(role == "蘇貞昌")
        {
            PlayerCharacterImg.GetComponent<Image>().sprite = role3;
        }
        else if(role == "陳橘")
        {
            PlayerCharacterImg.GetComponent<Image>().sprite = role4;
        }

        //Debug.Log("player:"+player+"\n");
        //Debug.Log("playerName:" + playerName+"\n");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
