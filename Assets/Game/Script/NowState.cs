﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NowState : MonoBehaviour { //控制連線及背景component
    //計算錢
    public int money = 100; //錢
    public int time = 120; //時間(秒)
    public Text moneyText; //顯示錢的資訊
    public Text timeText; //顯示時間
    int blueMoney; //藍黨總錢
    int greenMoney; //綠黨總錢
    //int[] playerMoney; //player的錢
    public Hashtable hash; //宣告HashTable變數
    public int playerMoney; //宣告要新增的變數名稱:
    public Text postText; //告示牌

    //炸彈
    public GameObject bombObj;

    //連線
    List<PhotonPlayer> PlayerList; //儲存玩家(要照順序)
    public GameObject portalLeft; //左portal
    public GameObject portalUp; //上portal
    public GameObject portalRight; //右portal
    public byte playerCode; //此player的code，用以判斷要不要接收訊息

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
        //取得玩家list(同樣順序)
        PlayerList.Add(PhotonNetwork.masterClient); //1
        PlayerList.Add(PhotonNetwork.masterClient.GetNext()); //2
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext()); //3
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext()); //4

        hash = new Hashtable();
        playerMoney = money;
        hash.Add("Money", playerMoney); //把變數存進剛剛宣告的hash裡
        PhotonNetwork.player.SetCustomProperties(hash);


        player = PhotonNetwork.player; //取得現在的player
        playerName = PhotonNetwork.playerName; //取得現在的player的暱稱
        partyColor = (string)PhotonNetwork.player.CustomProperties["PartyColor"]; //政黨顏色
        role = (string)PhotonNetwork.player.CustomProperties["Role"]; //政黨角色
        
        //根據角色換角色圖片
        if (role == "蔡中文")
        {
            PlayerCharacterImg.GetComponent<Image>().sprite = role1;
        }
        else if (role == "馬英八")
        {
            PlayerCharacterImg.GetComponent<Image>().sprite = role2;
        }
        else if (role == "蘇貞昌")
        {
            PlayerCharacterImg.GetComponent<Image>().sprite = role3;
        }
        else if (role == "陳橘")
        {
            PlayerCharacterImg.GetComponent<Image>().sprite = role4;
        }

        //判斷是哪個player
        if(player == PlayerList[0]) //Player1
        {
            playerCode = (byte) 0;
            portalLeft.GetComponent<SendBall>().setTarget(PlayerList[1],1); //Player2放左
            portalUp.GetComponent<SendBall>().setTarget(PlayerList[2], 2); //Player3放上
            portalRight.GetComponent<SendBall>().setTarget(PlayerList[3], 3); //Player4放右
        }
        else if(player == PlayerList[1]) //Player2
        {
            playerCode = (byte) 1;
            portalLeft.GetComponent<SendBall>().setTarget(PlayerList[2], 2); //Player3放左
            portalUp.GetComponent<SendBall>().setTarget(PlayerList[3], 3); //Player4放上
            portalRight.GetComponent<SendBall>().setTarget(PlayerList[0], 0); //Player1放右
        }
        else if(player == PlayerList[2]) //Player3
        {
            playerCode = (byte) 2;
            portalLeft.GetComponent<SendBall>().setTarget(PlayerList[3], 3); //Player4放左
            portalUp.GetComponent<SendBall>().setTarget(PlayerList[0], 0); //Player1放上
            portalLeft.GetComponent<SendBall>().setTarget(PlayerList[1], 1); //Player2放右
        }
        else if(player == PlayerList[3]) //Player4
        {
            playerCode = (byte) 3;
            portalLeft.GetComponent<SendBall>().setTarget(PlayerList[0], 0); //Player1放左
            portalUp.GetComponent<SendBall>().setTarget(PlayerList[1], 1); //Player2放上
            portalRight.GetComponent<SendBall>().setTarget(PlayerList[2], 2); //Player3放右
        }

        


        if (PhotonNetwork.isMasterClient) //若是Master Client，遊戲開始
        {
            InvokeRepeating("timeCountDown", 1, 1); //每隔一秒執行一次 
        }

        //UI
        moneyText.text = "金錢 x" + money;
        bombObj.SetActive(false); //一開始炸彈不顯示
                                  
    }
	
	// Update is called once per frame
	void Update () {

        //先將每個玩家的錢取出來
        /*for(int i =0; i< PlayerList.Count; i++)
        {
            if(player == PlayerList[i])
            {
                playerMoney[i] = money;
            }
        }*/

       

        


        if (time == 0) //如果時間歸零，就停止減少
        {
            CancelInvoke("timeCountDown");
            timeText.text = "Game Over";
            Time.timeScale = 0f; //時間暫停
            
        }
    }

    void OnTriggerEnter2D(Collider2D collision) //進洞
    {
        if (collision.gameObject.CompareTag("黑球"))
        { //黑球
            money -= 10;
            moneyText.text = "金錢 x" + money;
            /*
            //依據政黨顏色，來減少總額
            if (partyColor == "green") 
            {
                greenMoney -= 10;
            }
            else if(partyColor == "blue")
            {
                blueMoney -= 10;
            }*/
        }
        if(collision.gameObject.CompareTag("金球"))
        { //金球
            money += 10;
            moneyText.text = "金錢 x" + money;
            /*
            //依據政黨顏色，來減少總額
            if (partyColor == "green")
            {
                greenMoney += 10;
            }
            else if (partyColor == "blue")
            {
                blueMoney += 10;
            }*/
        }
        if (collision.gameObject.CompareTag("炸彈"))
        { //炸彈，扣50%的錢
            if(money > 0)
            {
                money = money / 2;
                moneyText.text = "金錢 x" + money;
            }
        }

        //每次變動，將hash裡的金錢更新
        PhotonNetwork.player.SetCustomProperties(hash);

        //每次金錢變動時，來檢查金錢總額
        greenMoney = 0; //初始化
        blueMoney = 0;

        //判斷是什麼陣營，將錢加總
        for (int i = 0; i < PlayerList.Count; i++)
        {
            if ((string)PhotonNetwork.playerList[i].CustomProperties["PartyColor"] == "green") //如果是綠的
            {
                greenMoney += (int)PhotonNetwork.playerList[i].CustomProperties["Money"];
            }
            else if ((string)PhotonNetwork.playerList[i].CustomProperties["PartyColor"] == "blue") //藍的
            {
                blueMoney += (int)PhotonNetwork.playerList[i].CustomProperties["Money"];
            }
        }

        //判斷目前藍綠陣營，政黨輪替
        if (greenMoney - blueMoney > 30)
        {
            postText.text = "綠黨執政";
        }
        else if (blueMoney - greenMoney > 30)
        {
            postText.text = "藍黨執政";
        }

    }

    void timeCountDown() //時間倒數，每次減一秒
    {
        timeText.text = "" + time;
        time--;
    }
}
