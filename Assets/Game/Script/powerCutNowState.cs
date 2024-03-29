﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class powerCutNowState : Photon.PunBehaviour { //控制連線及背景component
    //ReadyGo
    public GameObject ReadyGoPanel;
    public GameObject ready;
    public GameObject go;

    //同步
    int okCount = 0;
    int okLevel = 1; //1:一開始，2:結束

    //計算錢
    public int money = 100; //錢
    public int time = 120; //時間(秒)
    public Text moneyText; //顯示錢的資訊
    public Text timeText; //顯示時間
    int blueMoney; //藍黨總錢
    int greenMoney; //綠黨總錢
    int[] playerMoney = new int[4]; //player的錢

    //執政
    public GameObject winLogo; //中選logo
    public int winPlayerIndex = -1; //贏家，預設是-1
    public GameObject[] allArray; //全部球+道具
    public Hashtable hash; //宣告HashTable變數:

    //炸彈
    public GameObject bombObj;
    public GameObject forbiddenObj; //禁止的logo

    //報紙
    public GameObject paperMenu; //報紙選單
    public GameObject paper; //報紙
    public Image[] paperPersonImage; //報紙上的人物
    public Button[] paperPersonMenu; //人物陣列

    //連線
    List<PhotonPlayer> PlayerList; //儲存玩家(要照順序)
    public GameObject portalLeft; //左portal
    public GameObject portalUp; //上portal
    public GameObject portalRight; //右portal
    byte playerCode; //此player的code，用以判斷要不要接收訊息
    PhotonView photonView;

    PhotonPlayer player; //玩家
    string playerName; //玩家名稱
    string partyColor = "green"; //玩家政黨顏色(預設:綠)
    string role; //玩家選擇角色
    string[] ballColor =  {"綠球","紫球","紅球","黃球" };
    int partyColorNum; //根據玩家陣營，判斷是哪種球
    public Image PlayerCharacterImg; //角色圖片

    //人物圖片
    public Sprite role1;
    public Sprite role2;
    public Sprite role3;
    public Sprite role4;

    public Sprite[] SneerPersonImg; //人物奸笑
    public Sprite[] AngryPersonImg; //人物憤怒
    public Sprite[] cryPersonImg; //人物哭泣

    //Edge、Portal
    public GameObject edge1_green;
    public GameObject edge2_green;
    public GameObject edge1_purple;
    public GameObject edge2_purple;
    public GameObject edge1_red;
    public GameObject edge2_red;
    public GameObject edge1_yellow;
    public GameObject edge2_yellow;
    public GameObject portalLeft_green;
    public GameObject portalUp_green;
    public GameObject portalRight_green;
    public GameObject portalLeft_purple;
    public GameObject portalUp_purple;
    public GameObject portalRight_purple;
    public GameObject portalLeft_red;
    public GameObject portalUp_red;
    public GameObject portalRight_red;
    public GameObject portalLeft_yellow;
    public GameObject portalUp_yellow;
    public GameObject portalRight_yellow;

    //佈告欄
    public GameObject[] partyColorNoticeBoard; //存取各政黨的布告欄(綠、紫、紅、黃)
    public GameObject[] partyColorGoodText; //存取各政黨好的布告欄文字(綠、紫、紅、黃)
    public GameObject[] partyColorBadText; //存取各政黨壞的布告欄文字(綠、紫、紅、黃)

    public GameObject badNoticeBoard; //顯示黑訊息
    public GameObject microphoneNoticeBoard; //顯示麥克風訊息
    public Text badNoticeBoardText; //黑訊息文字
    public Text microphoneNoticeBoardText; //麥克風
    public Text paperNoticeBoardText; //報紙訊息文字
    string badMessage; //黑訊息
    string[] microphoneMessage = { "政府發布最新能源政策，\n獲民眾支持", "中油公布最新調查進度", "台電宣布全台恢復供電", "巨路發表照常營運聲明" }; //麥克風訊息
    string paperMessage;//報紙訊息

    //音樂
    public GameObject bgm; //遊戲bgm
    public GameObject goldBallMusic; //金球音樂
    public GameObject blackBallMusic; //黑球音樂
    public GameObject itemMusic; //道具音樂
    public GameObject bombMusic; //炸彈音樂
    public GameObject readyMusic; //ready音樂
    public GameObject goMusic; //go音樂

    //停電效果
    public GameObject powerCutPanel; //造成停電效果

    // Use this for initialization
    void Start() {

        winPlayerIndex = -1;

        //獲得所有的球+道具
        allArray = GameObject.Find("左Portal(真)").GetComponent<SendBall>().allArray;

        //UI
        moneyText.text = money + "(百萬)";

        //取得玩家list(同樣順序)
        PlayerList = new List<PhotonPlayer>();
        PlayerList.Add(PhotonNetwork.masterClient); //1
        PlayerList.Add(PhotonNetwork.masterClient.GetNext()); //2
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext()); //3
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext().GetNext()); //4

        photonView = PhotonView.Get(this);

        //錢初始化
        for (int i = 0; i < playerMoney.Length; i++)
        {
            playerMoney[i] = money;
        }

        player = PhotonNetwork.player; //取得現在的player
        playerName = PhotonNetwork.playerName; //取得現在的player的暱稱
        partyColor = (string)PhotonNetwork.player.CustomProperties["PartyColor"]; //政黨顏色
        role = (string)PhotonNetwork.player.CustomProperties["Role"]; //政黨角色

        //根據角色換角色圖片
        setRoleImg(PlayerCharacterImg, role);
        //判斷是哪個政黨，以利之後判斷是否為同顏色的球
        decideWhichBallColor();

        //根據政黨顏色換配置(Edge)
        if (partyColor == "green") //綠黨
        {
            edge1_green.SetActive(true);
            edge2_green.SetActive(true);
        }
        else if (partyColor == "purple") //紫:承包商(巨路)
        {
            edge1_purple.SetActive(true);
            edge2_purple.SetActive(true);
        }
        else if(partyColor == "red") //紅:中油
        {
            edge1_red.SetActive(true);
            edge2_red.SetActive(true);
        }
        else if(partyColor == "yellow")//黃:台電
        {
            edge1_yellow.SetActive(true);
            edge2_yellow.SetActive(true);
        }

        //判斷是哪個player
        if (player == PlayerList[0]) //Player1
        {
            playerCode = (byte)0; //setTarget，1.傳送的目標，2.目標code，3.玩家自己的playerCode
            portalLeft.GetComponent<SendBall>().setTarget(PlayerList[1], 1, playerCode, 0); //Player2放左
            portalUp.GetComponent<SendBall>().setTarget(PlayerList[2], 2, playerCode, 1); //Player3放上
            portalRight.GetComponent<SendBall>().setTarget(PlayerList[3], 3, playerCode, 2); //Player4放右
            setRoleImg(paperPersonMenu[0].GetComponent<Image>(), (string)PlayerList[1].CustomProperties["Role"]); //Paper第一個人物圖片
            setRoleImg(paperPersonMenu[1].GetComponent<Image>(), (string)PlayerList[2].CustomProperties["Role"]); //Paper第二個人物圖片
            setRoleImg(paperPersonMenu[2].GetComponent<Image>(), (string)PlayerList[3].CustomProperties["Role"]); //Paper第三個人物圖片
        }
        else if (player == PlayerList[1]) //Player2
        {
            playerCode = (byte)1;
            portalLeft.GetComponent<SendBall>().setTarget(PlayerList[2], 2, playerCode, 0); //Player3放左
            portalUp.GetComponent<SendBall>().setTarget(PlayerList[3], 3, playerCode, 1); //Player4放上
            portalRight.GetComponent<SendBall>().setTarget(PlayerList[0], 0, playerCode, 2); //Player1放右
            setRoleImg(paperPersonMenu[0].GetComponent<Image>(), (string)PlayerList[2].CustomProperties["Role"]); //Paper第一個人物圖片
            setRoleImg(paperPersonMenu[1].GetComponent<Image>(), (string)PlayerList[3].CustomProperties["Role"]); //Paper第二個人物圖片
            setRoleImg(paperPersonMenu[2].GetComponent<Image>(), (string)PlayerList[0].CustomProperties["Role"]); //Paper第三個人物圖片
        }
        else if (player == PlayerList[2]) //Player3
        {
            playerCode = (byte)2;
            portalLeft.GetComponent<SendBall>().setTarget(PlayerList[3], 3, playerCode, 0); //Player4放左
            portalUp.GetComponent<SendBall>().setTarget(PlayerList[0], 0, playerCode, 1); //Player1放上
            portalRight.GetComponent<SendBall>().setTarget(PlayerList[1], 1, playerCode, 2); //Player2放右
            setRoleImg(paperPersonMenu[0].GetComponent<Image>(), (string)PlayerList[3].CustomProperties["Role"]); //Paper第一個人物圖片
            setRoleImg(paperPersonMenu[1].GetComponent<Image>(), (string)PlayerList[0].CustomProperties["Role"]); //Paper第二個人物圖片
            setRoleImg(paperPersonMenu[2].GetComponent<Image>(), (string)PlayerList[1].CustomProperties["Role"]); //Paper第三個人物圖片
        }
        else if (player == PlayerList[3]) //Player4
        {
            playerCode = (byte)3;
            portalLeft.GetComponent<SendBall>().setTarget(PlayerList[0], 0, playerCode, 0); //Player1放左
            portalUp.GetComponent<SendBall>().setTarget(PlayerList[1], 1, playerCode, 1); //Player2放上
            portalRight.GetComponent<SendBall>().setTarget(PlayerList[2], 2, playerCode, 2); //Player3放右
            setRoleImg(paperPersonMenu[0].GetComponent<Image>(), (string)PlayerList[0].CustomProperties["Role"]); //Paper第一個人物圖片
            setRoleImg(paperPersonMenu[1].GetComponent<Image>(), (string)PlayerList[1].CustomProperties["Role"]); //Paper第二個人物圖片
            setRoleImg(paperPersonMenu[2].GetComponent<Image>(), (string)PlayerList[2].CustomProperties["Role"]); //Paper第三個人物圖片
        }

        //根據政黨顏色換配置(Edge)
        //左
        if ((string)portalLeft.GetComponent<SendBall>().targetPlayer.CustomProperties["PartyColor"] == "green")
        {
            portalLeft_green.SetActive(true);
        }
        else if ((string)portalLeft.GetComponent<SendBall>().targetPlayer.CustomProperties["PartyColor"] == "purple")
        {
            portalLeft_purple.SetActive(true);
        }
        else if ((string)portalLeft.GetComponent<SendBall>().targetPlayer.CustomProperties["PartyColor"] == "red")
        {
            portalLeft_red.SetActive(true);
        }
        else if ((string)portalLeft.GetComponent<SendBall>().targetPlayer.CustomProperties["PartyColor"] == "yellow")
        {
            portalLeft_yellow.SetActive(true);
        }
        //上
        if ((string)portalUp.GetComponent<SendBall>().targetPlayer.CustomProperties["PartyColor"] == "green")
        {
            portalUp_green.SetActive(true);
        }
        else if ((string)portalUp.GetComponent<SendBall>().targetPlayer.CustomProperties["PartyColor"] == "purple")
        {
            portalUp_purple.SetActive(true);
        }
        else if ((string)portalUp.GetComponent<SendBall>().targetPlayer.CustomProperties["PartyColor"] == "red")
        {
            portalUp_red.SetActive(true);
        }
        else if ((string)portalUp.GetComponent<SendBall>().targetPlayer.CustomProperties["PartyColor"] == "yellow")
        {
            portalUp_yellow.SetActive(true);
        }
        //右
        if ((string)portalRight.GetComponent<SendBall>().targetPlayer.CustomProperties["PartyColor"] == "green")
        {
            portalRight_green.SetActive(true);
        }
        else if ((string)portalRight.GetComponent<SendBall>().targetPlayer.CustomProperties["PartyColor"] == "purple")
        {
            portalRight_purple.SetActive(true);
        }
        else if ((string)portalRight.GetComponent<SendBall>().targetPlayer.CustomProperties["PartyColor"] == "red")
        {
            portalRight_red.SetActive(true);
        }
        else if ((string)portalRight.GetComponent<SendBall>().targetPlayer.CustomProperties["PartyColor"] == "yellow")
        {
            portalRight_yellow.SetActive(true);
        }

        //設定portal顯示人物圖
        if (player == PlayerList[0]) //Player1
        {
            decideWhichPortal(PlayerList[1], 0); //顯示portal人物圖片
            decideWhichPortal(PlayerList[2], 1); //顯示portal人物圖片
            decideWhichPortal(PlayerList[3], 2); //顯示portal人物圖片
        }
        else if (player == PlayerList[1]) //Player2
        {
            decideWhichPortal(PlayerList[2], 0); //顯示portal人物圖片
            decideWhichPortal(PlayerList[3], 1); //顯示portal人物圖片
            decideWhichPortal(PlayerList[0], 2); //顯示portal人物圖片
        }
        else if (player == PlayerList[2]) //Player3
        {
            decideWhichPortal(PlayerList[3], 0); //顯示portal人物圖片
            decideWhichPortal(PlayerList[0], 1); //顯示portal人物圖片
            decideWhichPortal(PlayerList[1], 2); //顯示portal人物圖片
        }
        else if (player == PlayerList[3]) //Player4
        {
            decideWhichPortal(PlayerList[0], 0); //顯示portal人物圖片
            decideWhichPortal(PlayerList[1], 1); //顯示portal人物圖片
            decideWhichPortal(PlayerList[2], 2); //顯示portal人物圖片
        }

        //把paper人物圖片儲存
        for (int i = 0; i < paperPersonMenu.Length; i++)
        {
            paperPersonImage[i] = paperPersonMenu[i].GetComponent<Image>();
        }

        //讓遊戲時間一致
        photonView.RPC("sendOk", PhotonTargets.MasterClient,1); //只傳給masterClient
       
    }

    // Update is called once per frame
    void Update() {
        if (PhotonNetwork.isMasterClient) //masterClient才可以
        {
            if(okCount == 4) //如果大家都準好就可以一起開始
            {
                if(okLevel == 1) //一開始畫面
                {
                    photonView.RPC("sendReady", PhotonTargets.All);
                }
                else if(okLevel == 2) //結束畫面
                {
                    PhotonNetwork.LoadLevel("WinOrLose"); //load到結束畫面
                }
                okCount = 0;
            }
        }
    }

    #region 同步
    [PunRPC]
    void sendOk(int level) //已ok
    {
        okCount++;
        okLevel = level;
    }

    [PunRPC]
    void sendReady() //可以開始了
    {
        Invoke("openReady", 2);
    }

    #endregion

    void decideWhichPortal(PhotonPlayer decidePlayer,int portalPos) //要顯示的圖片，哪個Portal
    {
        if(portalPos == 0)
        {
            //左邊
            if (portalLeft_green.activeSelf == true) //綠色
            {
                portalLeft_green.GetComponent<showPortalPersonImg_PowerCut>().setPortalPersonImg((string)decidePlayer.CustomProperties["Role"]);
            }
            else if (portalLeft_purple.activeSelf == true) //紫色
            {
                portalLeft_purple.GetComponent<showPortalPersonImg_PowerCut>().setPortalPersonImg((string)decidePlayer.CustomProperties["Role"]);
            }
            else if (portalLeft_red.activeSelf == true) //紅色
            {
                portalLeft_red.GetComponent<showPortalPersonImg_PowerCut>().setPortalPersonImg((string)decidePlayer.CustomProperties["Role"]);
            }
            else if (portalLeft_yellow.activeSelf == true) //黃色
            {
                portalLeft_yellow.GetComponent<showPortalPersonImg_PowerCut>().setPortalPersonImg((string)decidePlayer.CustomProperties["Role"]);
            }
        }
        else if(portalPos == 1)
        {
            //上面
            if (portalUp_green.activeSelf == true) //綠色
            {
                portalUp_green.GetComponent<showPortalPersonImg_PowerCut>().setPortalPersonImg((string)decidePlayer.CustomProperties["Role"]);
            }
            else if (portalUp_purple.activeSelf == true) //紫色
            {
                portalUp_purple.GetComponent<showPortalPersonImg_PowerCut>().setPortalPersonImg((string)decidePlayer.CustomProperties["Role"]);
            }
            else if (portalUp_red.activeSelf == true) //紅色
            {
                portalUp_red.GetComponent<showPortalPersonImg_PowerCut>().setPortalPersonImg((string)decidePlayer.CustomProperties["Role"]);
            }
            else if (portalUp_yellow.activeSelf == true) //黃色
            {
                portalUp_yellow.GetComponent<showPortalPersonImg_PowerCut>().setPortalPersonImg((string)decidePlayer.CustomProperties["Role"]);
            }
        }
        else if(portalPos == 2)
        {
            //右邊
            if (portalRight_green.activeSelf == true) //藍色
            {
                portalRight_green.GetComponent<showPortalPersonImg_PowerCut>().setPortalPersonImg((string)decidePlayer.CustomProperties["Role"]);
            }
            else if (portalRight_purple.activeSelf == true) //紫色
            {
                portalRight_purple.GetComponent<showPortalPersonImg_PowerCut>().setPortalPersonImg((string)decidePlayer.CustomProperties["Role"]);
            }
            else if (portalRight_red.activeSelf == true) //紅色
            {
                portalRight_red.GetComponent<showPortalPersonImg_PowerCut>().setPortalPersonImg((string)decidePlayer.CustomProperties["Role"]);
            }
            else if (portalRight_yellow.activeSelf == true) //黃色
            {
                portalRight_yellow.GetComponent<showPortalPersonImg_PowerCut>().setPortalPersonImg((string)decidePlayer.CustomProperties["Role"]);
            }
        }
    }

    void decideWhichBallColor()
    {
        if(partyColor == "green") //綠黨
        {
            partyColorNum = 0;
        }
        else if(partyColor == "purple") //紫(巨龍)
        {
            partyColorNum = 1;
        }
        else if(partyColor == "red") //紅(中油)
        {
            partyColorNum = 2;
        }
        else if(partyColor == "yellow") //黃(台電)
        {
            partyColorNum = 3;
        }
    }
    #region ReadyAndGO

    void openReady() //先跑ready
    {
        ready.SetActive(true);
        GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(readyMusic);
        Invoke("openGo",2);
    }

    void openGo() //再跑go
    {
        ready.SetActive(false);
        go.SetActive(true);
        GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(goMusic);
        Invoke("closeReadyGo",1);
    }

    void closeReadyGo() //關掉readyGo
    {
        go.SetActive(false);
        ReadyGoPanel.SetActive(false);
        setTimeCountDown();
        //開始建立球
        GameObject.Find("左邊框").GetComponent<GenerateBall>().startGenerateBall();
        GameObject.Find("右邊框").GetComponent<GenerateBall>().startGenerateBall();
    }

    #endregion

    void setTimeCountDown() //啟動時間倒數
    {
        GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(bgm);
        InvokeRepeating("timeCountDown", 1, 1); //每一秒執行一次，時間減一
    }

    void timeCountDown() //時間倒數，每次減一秒
    {
        timeText.text = time + " sec";
        time--;
        if (time == 0) //如果時間歸零，就停止減少
        {
            hash = new Hashtable();
            hash.Add("Money", money); //把錢加進hash
            hash.Add("WinPlayer", winPlayerIndex);
            PhotonNetwork.player.SetCustomProperties(hash);
            CancelInvoke("timeCountDown");
            timeText.text = "Game Over";
            Time.timeScale = 0f; //時間暫停

            photonView.RPC("sendOk",PhotonTargets.MasterClient,2);

        }
    }

    void OnTriggerEnter2D(Collider2D collision) //進洞
    {
        //根據故事，判斷是有幾種顏色的球(金、藍、綠)


        if (collision.gameObject.CompareTag("" + ballColor[partyColorNum])) //如果為同黨色的球，加分
        {
            money += 10;
            moneyText.text = money + "(百萬)";
            GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(goldBallMusic);
            setFaceImg(SneerPersonImg); //把圖片換成奸笑的表情
            photonView.RPC("sendPartyGoodMessage", PhotonTargets.All, partyColorNum); //第三個參數:傳送要顯示的話
        }
        else if (collision.gameObject.CompareTag("黑球"))
        { //黑球
            money -= 10;
            moneyText.text = money + "(百萬)";
            GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(blackBallMusic);
            setFaceImg(cryPersonImg); //把圖片換成奸笑的表情
            badMessage = role + "講幹話";
            photonView.RPC("sendBadMessage", PhotonTargets.All, badMessage); //第三個參數:傳送要顯示的話
        }
        else if (collision.gameObject.CompareTag("報紙")) //報紙效果:出現選單可以陷害人，指定敵對黨某人有誹聞(意涵:爆料)，被指定者扣錢20%
        {
            //先顯示報紙選單
            paperMenu.SetActive(true);
            GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(itemMusic);
            //自己金額增加20%
            money += (int)(money * 0.2);
            moneyText.text = money + "(百萬)";
        }
        else if (collision.gameObject.CompareTag("麥克風"))
        {
            GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(itemMusic);
            photonView.RPC("sendMicrophoneMessage", PhotonTargets.All, (int)playerCode); //傳送此玩家吃到麥克風的訊息

            photonView.RPC("sendFaceImg", PhotonTargets.Others); //改變表情
            microphoneEffect();
        }
        else //皆不是的話，就是不同色的球，扣分
        {
            money -= 10;
            moneyText.text = money + "(百萬)";
            GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(blackBallMusic);
            setFaceImg(cryPersonImg); //把圖片換成奸笑的表情
            photonView.RPC("sendPartyBadMessage", PhotonTargets.All, partyColorNum); //第三個參數:傳送要顯示的話
        }

        //每次金錢變動時，來檢查金錢總額
        photonView.RPC("sendPlayerMoney", PhotonTargets.All, player, money); 
        Destroy(collision.gameObject); //把碰觸到的球刪掉
    }

    [PunRPC]
    void sendPlayerMoney(PhotonPlayer targetPlayer,int targetMoney)     //判斷是哪個Player,加到對應的錢
    {
        //取得玩家list(同樣順序)
        PlayerList = new List<PhotonPlayer>();
        PlayerList.Add(PhotonNetwork.masterClient); //1
        PlayerList.Add(PhotonNetwork.masterClient.GetNext()); //2
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext()); //3
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext().GetNext()); //4

        for(int i = 0; i < PlayerList.Count; i++)
        {
            if(targetPlayer == PlayerList[i]) //判斷是哪個Player，把對應的錢加近陣列
            {
                playerMoney[i] = targetMoney;
                break;
            }
        }

        identificateWinPlayer();
    }

    void identificateWinPlayer() //判斷哪個玩家是最多錢
    {
        PlayerList = new List<PhotonPlayer>();
        PlayerList.Add(PhotonNetwork.masterClient); //1
        PlayerList.Add(PhotonNetwork.masterClient.GetNext()); //2
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext()); //3
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext().GetNext()); //4

        int j;
        for (int i = 0; i< playerMoney.Length; i++)
        {
            int targetMoney = playerMoney[i]; //儲存要比較的錢
            for (j = 0; j < playerMoney.Length; j++)
            {
                if (i == j) { continue; } //如果是比到自己，跳過
                if(targetMoney - playerMoney[j] < 30) //如果目標的錢沒有比其他玩家的錢多30，跳出
                {
                    break;
                }
            }
            if(j == 3) //如果全部跑完，表示這個玩家錢都大於其他人，儲存起來
            {
                winPlayerIndex = i +1; //表示Player1、2、3、4
                break;
            }
        }

        if(winPlayerIndex > 0) //如果不是預設-1，表示有更動到
        {
            if (player == PlayerList[winPlayerIndex - 1]) //把閃電按鈕打開
            {
                Debug.Log("open btn");
                bombObj.SetActive(true);
            }
            else //把閃電按鈕關閉
            {
                Debug.Log("close btn");
                bombObj.SetActive(false);
                forbiddenObj.SetActive(false);
            }
        }
        
    }
    #region 人物表情

    void setRoleImg(Image personImg, string person) //根據player選的人物，給予相應的照片
    {
        if (person == "蔡中聞")
        {
            personImg.sprite = role1;
        }
        else if (person == "承包商")
        {
            personImg.sprite = role2;
        }
        else if (person == "陳銀德")
        {
            personImg.sprite = role3;
        }
        else if (person == "朱聞誠")
        {
            personImg.sprite = role4;
        }
    }
    
    void setFaceImg(Sprite[] img) //根據吃到的道具，將Player設誠相關的照片
    {
        if (role == "蔡中聞")
        {
            PlayerCharacterImg.sprite = img[0];
        }
        else if (role == "承包商")
        {
            PlayerCharacterImg.sprite = img[1];
        }
        else if (role == "陳銀德")
        {
            PlayerCharacterImg.sprite = img[2];
        }
        else if (role == "朱聞誠")
        {
            PlayerCharacterImg.sprite = img[3];
        }
        Invoke("changeToOriginalImg", 3); //顯示3秒換回來
    }

    void changeToOriginalImg() //把圖片換回來
    {
        if (role == "蔡中聞")
        {
            PlayerCharacterImg.sprite = role1;
        }
        else if (role == "承包商")
        {
            PlayerCharacterImg.sprite = role2;
        }
        else if (role == "陳銀德")
        {
            PlayerCharacterImg.sprite = role3;
        }
        else if (role == "朱聞誠")
        {
            PlayerCharacterImg.sprite = role4;
        }
    }
    #endregion

    #region 黑訊息
    [PunRPC]
    void sendBadMessage(string text) //傳遞好訊息
    {
        microphoneNoticeBoard.SetActive(false); //關閉麥克風顯示
        for (int i = 0; i < partyColorNoticeBoard.Length; i++) //把政黨相關訊息關閉
        {
            partyColorNoticeBoard[i].SetActive(false);
            partyColorGoodText[i].SetActive(false);
            partyColorBadText[i].SetActive(false);
        }
        badNoticeBoard.SetActive(true); //開啟壞訊息顯示
        badNoticeBoardText.text = text;
        Invoke("badTimeCountDown", 3); //三秒便會關閉訊息
    }

    void badTimeCountDown() //3秒顯示訊息
    {
        badNoticeBoard.SetActive(false);
    }
    #endregion

    #region 政黨相關訊息
    [PunRPC]
    void sendPartyGoodMessage(int nowPartyColorNum) //傳遞好訊息
    {
        microphoneNoticeBoard.SetActive(false); //麥克風訊息關閉
        badNoticeBoard.SetActive(false); //關閉壞訊息顯示
        for(int i = 0; i < partyColorNoticeBoard.Length; i++) //把政黨相關訊息關閉
        {
            partyColorNoticeBoard[i].SetActive(false);
            partyColorGoodText[i].SetActive(false);
            partyColorBadText[i].SetActive(false);
        }
        partyColorNoticeBoard[nowPartyColorNum].SetActive(true); //把對應政黨訊息開啟
        partyColorGoodText[nowPartyColorNum].SetActive(true); //好訊息開啟
        Invoke("partyGoodTimeCountDown",3);//三秒便會關閉訊息
    }

    [PunRPC]
    void sendPartyBadMessage(int nowPartyColorNum) //傳遞好訊息
    {
        microphoneNoticeBoard.SetActive(false); //麥克風訊息關閉
        badNoticeBoard.SetActive(false); //關閉壞訊息顯示
        for (int i = 0; i < partyColorNoticeBoard.Length; i++) //把政黨相關訊息關閉
        {
            partyColorNoticeBoard[i].SetActive(false);
            partyColorGoodText[i].SetActive(false);
            partyColorBadText[i].SetActive(false);
        }
        partyColorNoticeBoard[nowPartyColorNum].SetActive(true); //把對應政黨訊息開啟
        partyColorBadText[nowPartyColorNum].SetActive(true); //好訊息開啟
        Invoke("partyBadTimeCountDown", 3);//三秒便會關閉訊息
    }

    void partyGoodTimeCountDown()//3秒顯示訊息
    {   
        for(int i = 0; i < partyColorNoticeBoard.Length; i++)
        {
            if(partyColorGoodText[i].activeSelf == true) //如果好訊息有開啟，則關閉
            {
                partyColorNoticeBoard[i].SetActive(false);//把對應政黨訊息關閉
                partyColorGoodText[i].SetActive(false);//好訊息關閉
            }
        }
    }

    void partyBadTimeCountDown()
    {
        for(int i = 0; i< partyColorNoticeBoard.Length; i++) 
        {
            if (partyColorBadText[i].activeSelf == true) //如果壞訊息有開啟，則關閉
            {
                partyColorNoticeBoard[i].SetActive(false);//把對應政黨訊息關閉
                partyColorBadText[i].SetActive(false); //壞訊息關閉
            }
        }
    }
    #endregion

    #region 報紙
    public void sendPaperMessage(int pIndex) //點選按鈕才可以傳送開啟報紙的資訊
    {
        PlayerList = new List<PhotonPlayer>();
        PlayerList.Add(PhotonNetwork.masterClient); //1
        PlayerList.Add(PhotonNetwork.masterClient.GetNext()); //2
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext()); //3
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext().GetNext()); //4

        switch (pIndex) //判斷是改哪個玩家的圖片
        {
            case 0: //第一個人
                sendRoleImg(paperPersonImage[0]);
                if (player == PlayerList[0]) //Player1
                {
                    photonView.RPC("paperEffect2",PhotonTargets.All); //扣Player2
                }
                else if(player == PlayerList[1]) //Player2
                {
                    photonView.RPC("paperEffect3", PhotonTargets.All); //扣Player3
                }
                else if(player == PlayerList[2]) //Player3
                {
                    photonView.RPC("paperEffect4", PhotonTargets.All); //扣Player4
                }
                else if(player == PlayerList[3]) //Player4
                {
                    photonView.RPC("paperEffect1", PhotonTargets.All); //扣Player1
                }
                break;
            case 1: //第二個人
                sendRoleImg(paperPersonImage[1]);
                if (player == PlayerList[0]) //Player1
                {
                    photonView.RPC("paperEffect3", PhotonTargets.All); //扣Player3
                }
                else if (player == PlayerList[1]) //Player2
                {
                    photonView.RPC("paperEffect4", PhotonTargets.All); //扣Player4
                }
                else if (player == PlayerList[2]) //Player3
                {
                    photonView.RPC("paperEffect1", PhotonTargets.All); //扣Player1
                }
                else if (player == PlayerList[3]) //Player4
                {
                    photonView.RPC("paperEffect2", PhotonTargets.All); //扣Player2
                }
                break;
            case 2: //第三個人
                sendRoleImg(paperPersonImage[2]);
                if (player == PlayerList[0]) //Player1
                {
                    photonView.RPC("paperEffect4", PhotonTargets.All); //扣Player4
                }
                else if (player == PlayerList[1]) //Player2
                {
                    photonView.RPC("paperEffect1", PhotonTargets.All); //扣Player1
                }
                else if (player == PlayerList[2]) //Player3
                {
                    photonView.RPC("paperEffect2", PhotonTargets.All); //扣Player2
                }
                else if (player == PlayerList[3]) //Player4
                {
                    photonView.RPC("paperEffect3", PhotonTargets.All); //扣Player3
                }
                break;
        }
    }

    void sendRoleImg(Image img) //判斷是哪張圖片，傳送該圖片
    {
        if (img.sprite.name == "蔡中聞-半身")
        {
            photonView.RPC("SetPaperOn1", PhotonTargets.All);
        }
        else if (img.sprite.name == "承包商-半身")
        {
            photonView.RPC("SetPaperOn2", PhotonTargets.All);
        }
        else if (img.sprite.name == "陳銀德-半身")
        {
            photonView.RPC("SetPaperOn3", PhotonTargets.All);
        }
        else if (img.sprite.name == "朱聞誠-半身")
        {
            photonView.RPC("SetPaperOn4", PhotonTargets.All);
        }
    }

    //傳遞報紙訊息
    [PunRPC]
    void SetPaperOn1() //Player1
    {
        paper.SetActive(true); //開啟報紙
        GameObject.Find("人物圖片").GetComponent<ChangePaperPersonImg_PowerCut>().ChangeImg1();
        Invoke("paperTimeCountDown", 1);
    }

    [PunRPC]
    void SetPaperOn2() //Player2
    {
        paper.SetActive(true); //開啟報紙
        GameObject.Find("人物圖片").GetComponent<ChangePaperPersonImg_PowerCut>().ChangeImg2();
        Invoke("paperTimeCountDown", 1);
    }

    [PunRPC]
    void SetPaperOn3() //Player3
    {
        paper.SetActive(true); //開啟報紙
        GameObject.Find("人物圖片").GetComponent<ChangePaperPersonImg_PowerCut>().ChangeImg3();
        Invoke("paperTimeCountDown", 1);
    }

    [PunRPC]
    void SetPaperOn4() //Player4
    {
        paper.SetActive(true); //開啟報紙
        GameObject.Find("人物圖片").GetComponent<ChangePaperPersonImg_PowerCut>().ChangeImg4();
        Invoke("paperTimeCountDown", 1);
    }

    void paperTimeCountDown() //過3秒報紙自動關閉
    {
        paper.SetActive(false);
        paperMenu.SetActive(false);
    }

    //報紙效果:指定人並扣錢
    [PunRPC]
    void paperEffect1() //扣Player1的錢
    {
        paperNoticeBoardText.text = "立委質疑政府的能源政策漏洞百出";
        if (player == PhotonNetwork.masterClient) //如果是Player1才執行
        {
            if(money > 0)
            {
                money = (int)(money * 0.8);
                moneyText.text = money + "(百萬)";
            }
            setFaceImg(AngryPersonImg); //把圖片換成生氣的表情
            //每次金錢變動時，來檢查金錢總額
            photonView.RPC("sendPlayerMoney", PhotonTargets.All, player, money);
        }
    }

    [PunRPC]
    void paperEffect2() //扣Player2的錢
    {
        paperNoticeBoardText.text = "案件調查顯示中油監督不周";
        if (player == PhotonNetwork.masterClient.GetNext()) //如果是Player2才執行
        {
            if(money > 0)
            {
                money = (int)(money * 0.8);
                moneyText.text = money + "(百萬)";
            }
            setFaceImg(AngryPersonImg); //把圖片換成生氣的表情
            //每次金錢變動時，來檢查金錢總額
            photonView.RPC("sendPlayerMoney", PhotonTargets.All, player, money);
        }
    }

    [PunRPC]
    void paperEffect3() //扣Player3的錢
    {
        paperNoticeBoardText.text = "立委質疑台電供電系統脆弱";
        if (player == PhotonNetwork.masterClient.GetNext().GetNext()) //如果是Player3才執行
        {
            if(money > 0)
            {
                money = (int)(money * 0.8);
                moneyText.text = money + "(百萬)";
            }
            setFaceImg(AngryPersonImg); //把圖片換成生氣的表情
            //每次金錢變動時，來檢查金錢總額
            photonView.RPC("sendPlayerMoney", PhotonTargets.All, player, money);
        }
    }

    [PunRPC]
    void paperEffect4() //扣Player4的錢
    {
        paperNoticeBoardText.text = "巨路股價本周持續下跌";
        if (player == PhotonNetwork.masterClient.GetNext().GetNext().GetNext()) //如果是Player4才執行
        {
            if(money > 0)
            {
                money = (int)(money * 0.8);
                moneyText.text = money + "(百萬)";
            }
            setFaceImg(AngryPersonImg); //把圖片換成生氣的表情
            //每次金錢變動時，來檢查金錢總額
            photonView.RPC("sendPlayerMoney", PhotonTargets.All, player, money);
        }
    }

    #endregion

    #region 麥克風
    [PunRPC]
    void sendMicrophoneMessage(int whichPlayer) //根據哪個player，顯示麥克風的訊息
    {
        badNoticeBoard.SetActive(false); //關閉壞訊息顯示
        for (int i = 0; i < partyColorNoticeBoard.Length; i++) //把政黨相關訊息關閉
        {
            partyColorNoticeBoard[i].SetActive(false);
            partyColorGoodText[i].SetActive(false);
            partyColorBadText[i].SetActive(false);
        }
        microphoneNoticeBoard.SetActive(true);
        microphoneNoticeBoardText.text = microphoneMessage[whichPlayer];
        Invoke("microphoneTimeCountDown", 3);//三秒便會關閉訊息
    }

    void micrphoneTimeCountDown()//3秒顯示訊息
    {
        microphoneNoticeBoard.SetActive(false);
    }

    void microphoneEffect() //麥克風效果，把該player場上的敵對的球變自己的球
    {
        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject))) //得到所有hierarchy的物件
        {
            if (obj.CompareTag("綠球")|| obj.CompareTag("紫球")|| obj.CompareTag("紅球")|| obj.CompareTag("黃球")) //先確定是四種球
            {
                if (!obj.CompareTag("" + ballColor[partyColorNum])) //如果不是跟同政黨色，則轉色
                {
                    //int ballIndex = 0; //存取該黑球的index
                    Vector3 ballPosition = obj.transform.position; //存取該黑球的位置
                    int newBallIndex = 0; //先預設為紅1
                    //先判斷要轉成哪種顏色的球
                    if (partyColorNum == 0) //綠
                    {
                        newBallIndex = Random.Range(0, 3); //隨機產生0到2的數字
                    }
                    else if(partyColorNum == 1) //紫
                    {
                        newBallIndex = Random.Range(3, 6); //隨機產生3到5的數字
                    }
                    else if(partyColorNum == 2) //紅
                    {
                        newBallIndex = Random.Range(6, 9); //隨機產生6到8的數字
                    }
                    else if(partyColorNum == 3) //黃
                    {
                        newBallIndex = Random.Range(9, 12); //隨機產生9到11的數字
                    }
                    Destroy(obj); //刪除此黑球
                    GameObject newBall = Instantiate(allArray[newBallIndex], ballPosition, new Quaternion(0, 0, 0, 0)); //建立相對應的金球
                    newBall.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1); //不知道速度要指派怎樣，都往下好ㄌ
                }
            }
        }        
    }
    /*[PunRPC]
    void microphoneEffect2(PhotonPlayer useItPlayer) //麥克風效果2:把其他人的球吸過來
    {
        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject))) //得到所有hierarchy的物件
        {
            if (obj.CompareTag("金球"))
            {
                //取得玩家list(同樣順序)
                PlayerList = new List<PhotonPlayer>();
                PlayerList.Add(PhotonNetwork.masterClient); //1
                PlayerList.Add(PhotonNetwork.masterClient.GetNext()); //2
                PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext()); //3
                PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext().GetNext()); //4

                if (player == PlayerList[0]) //player1
                {
                    if (useItPlayer == PlayerList[1]) //左
                    {
                        obj.GetComponent<BallMove>().microphoneEffect(leftPortalPos); //指定位置
                    }
                    else if (useItPlayer == PlayerList[2]) //上
                    {
                        obj.GetComponent<BallMove>().microphoneEffect(upPortalPos); //指定位置
                    }
                    else if (useItPlayer == PlayerList[3]) //右
                    {
                        obj.GetComponent<BallMove>().microphoneEffect(rightPortalPos); //指定位置
                                                                                    // TowardTarget(obj, rightPortalPos);
                    }
                }
                else if (player == PlayerList[1]) // player2
                {
                    if (useItPlayer == PlayerList[2]) //左
                    {
                        obj.GetComponent<BallMove>().microphoneEffect(leftPortalPos); //指定位置
                    }
                    else if (useItPlayer == PlayerList[3]) //上
                    {
                        obj.GetComponent<BallMove>().microphoneEffect(upPortalPos); //指定位置
                    }
                    else if (useItPlayer == PlayerList[0]) //右
                    {
                        obj.GetComponent<BallMove>().microphoneEffect(rightPortalPos); //指定位置
                    }
                }
                else if (player == PlayerList[2]) // player3
                {
                    if (useItPlayer == PlayerList[3]) //左
                    {
                        obj.GetComponent<BallMove>().microphoneEffect(leftPortalPos); //指定位置
                    }
                    else if (useItPlayer == PlayerList[0]) //上
                    {
                        obj.GetComponent<BallMove>().microphoneEffect(upPortalPos); //指定位置
                    }
                    else if (useItPlayer == PlayerList[1]) //右
                    {
                        obj.GetComponent<BallMove>().microphoneEffect(rightPortalPos); //指定位置
                    }
                }
                else if (player == PlayerList[3]) //player4
                {
                    if (useItPlayer == PlayerList[0]) //左
                    {
                        obj.GetComponent<BallMove>().microphoneEffect(leftPortalPos); //指定位置
                    }
                    else if (useItPlayer == PlayerList[1]) //上
                    {
                        obj.GetComponent<BallMove>().microphoneEffect(upPortalPos); //指定位置
                    }
                    else if (useItPlayer == PlayerList[2]) //右
                    {
                        obj.GetComponent<BallMove>().microphoneEffect(rightPortalPos); //指定位置
                    }
                }
            }
        }
    }
    */
    [PunRPC]
    void sendFaceImg() //傳送給其他人，生氣
    {
        setFaceImg(AngryPersonImg); //把圖片換成生氣的表情
    }
    #endregion

    #region 停電效果
    public void openPowerCutEffect() //使用PowerCut停電
    {
        photonView.RPC("sendPowerCutEffect", PhotonTargets.All, partyColor); //第三個參數:傳送要顯示的話
    }

    [PunRPC]
    void sendPowerCutEffect(string getColor) //傳送停電效果
    {
        Debug.Log("getColor:" + getColor);
        Debug.Log("partyColor:" + partyColor);
        if (partyColor != getColor) //如果是不同政黨，才要被停電
        {
            powerCutPanel.SetActive(true);
            Invoke("closePowerCutEffect", 2); //一秒後關閉
        }
    }

    void closePowerCutEffect() //關閉停電效果
    {
        powerCutPanel.SetActive(false);
    }

    #endregion
    public override void OnDisconnectedFromPhoton()
	{
		SceneManager.LoadScene("Main");
	}

	public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
	{
		SceneManager.LoadScene("Main");
		PhotonNetwork.LeaveRoom ();
	}


}
