using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    int[] playerMoney = new int[4]; //player的錢
    //bool isFirst = true;

    //執政
    public GameObject winLogo; //中選logo
    public int winColor = 0 ; //判斷現在是誰執政，0:預設、1:綠、2:藍
    public GameObject[] allArray; //全部球+道具
    public Hashtable hash; //宣告HashTable變數:

    //炸彈
    public GameObject bombObj;

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
    public Image PlayerCharacterImg; //角色圖片
    
    //人物圖片
    public Sprite role1;
    public Sprite role2;
    public Sprite role3;
    public Sprite role4;

    //Edge、Portal
    public GameObject edge1_blue;
    public GameObject edge2_blue;
    public GameObject edge1_green;
    public GameObject edge2_green;
    public GameObject portalLeft_blue;
    public GameObject portalUp_blue;
    public GameObject portalRight_blue;
    public GameObject portalLeft_green;
    public GameObject portalUp_green;
    public GameObject portalRight_green;

    //佈告欄
    public GameObject greenNoticeBoard; //顯示綠黨執政
    public GameObject blueNoticeBoard; //顯示藍黨執政
    public GameObject noticeBoard; //顯示普通訊息
    public Text noticeBoardText; //普通訊息文字
    string[] blackMessage = { "反對年金改革", "反對同婚","支持一例一休","支持美牛進口","反對加入TPP","反對空服員罷工","反對調漲最低薪資","支持建造四","支持進口核災食品" };
    string[] goldMessage = { "支持年金改革","支持同婚","反對一例一休","反對美牛進口","支持加入TPP","支持空服員罷工","支持調漲最低薪資","反對建造核四","反對進口核災食品"};
    string bombMessage = "與企業董事秘密餐會!";
    string paperMessage = "酒後失態，服務員控訴性騷擾!";
    string microphoneMessage = "發表直播演說，\n獲得民眾支持";
    string showMessage; //要傳去sendMessage的參數

    //音樂
    public GameObject goldBallMusic; //金球音樂
    public GameObject blackBallMusic; //黑球音樂
    public GameObject itemMusic; //道具音樂
    public GameObject bombMusic; //炸彈音樂


    // Use this for initialization
    void Start() {
        
        //取得玩家list(同樣順序)
        PlayerList = new List<PhotonPlayer>();
        PlayerList.Add(PhotonNetwork.masterClient); //1
        PlayerList.Add(PhotonNetwork.masterClient.GetNext()); //2
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext()); //3
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext().GetNext()); //4
        
        photonView = PhotonView.Get(this);

        //錢初始化
        for(int i = 0; i < playerMoney.Length; i++)
        {
            playerMoney[i] = money;
        }

        player = PhotonNetwork.player; //取得現在的player
        playerName = PhotonNetwork.playerName; //取得現在的player的暱稱
        partyColor = (string)PhotonNetwork.player.CustomProperties["PartyColor"]; //政黨顏色
        role = (string)PhotonNetwork.player.CustomProperties["Role"]; //政黨角色

        //根據角色換角色圖片
        setRoleImg(PlayerCharacterImg,role);

        //獲得所有的球+道具
        allArray = GameObject.Find("左Portal(真)").GetComponent<SendBall>().allArray;

        //根據政黨顏色換配置(Edge)
        if (partyColor == "green") //綠黨
        {
            edge1_blue.SetActive(false);
            edge2_blue.SetActive(false);
        }
        else if (partyColor == "blue") //藍黨
        {
            edge1_green.SetActive(false);
            edge2_green.SetActive(false);
        }

        //UI
        moneyText.text = "金錢\n x" + money + "(百萬)";

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

        //把paper人物圖片儲存
        for(int i = 0; i < paperPersonMenu.Length; i++)
        {
            paperPersonImage[i] = paperPersonMenu[i].GetComponent<Image>();
        }
        

        //根據政黨顏色換配置(Edge)
        //左
        if ((string)portalLeft.GetComponent<SendBall>().targetPlayer.CustomProperties["PartyColor"] == "green")
        {
            portalLeft_blue.SetActive(false);
        }
        else
        {
            portalLeft_green.SetActive(false);
        }
        //上
        if ((string)portalUp.GetComponent<SendBall>().targetPlayer.CustomProperties["PartyColor"] == "green")
        {
            portalUp_blue.SetActive(false);
        }
        else
        {
            portalUp_green.SetActive(false);
        }
        //右
        if ((string)portalRight.GetComponent<SendBall>().targetPlayer.CustomProperties["PartyColor"] == "green")
        {
            portalRight_blue.SetActive(false);
        }
        else
        {
            portalRight_green.SetActive(false);
        }

        //讓遊戲時間一致
        //if (PhotonNetwork.isMasterClient) //若是Master Client，遊戲開始
        //{
          InvokeRepeating("timeCountDown", 1, 1); //每一秒執行一次，時間減一
            //photonView.RPC("sendTime", PhotonTargets.All);
            //Debug.Log("time:"+PhotonNetwork.time);
        //}
 
                                  
    }

    // Update is called once per frame
    void Update () {   
    }

    void OnTriggerEnter2D(Collider2D collision) //進洞
    {
        if (collision.gameObject.CompareTag("黑球"))
        { //黑球
            money -= 10;
            moneyText.text = "金錢\n x" + money + "(百萬)";
            GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(blackBallMusic); 
            identificatePlayerMoney(); //判斷是哪個player
            
            for(int i =0; i < allArray.Length; i++) //判斷是哪個球，給予對應的話
            {
                if (allArray[i].name+"(Clone)" == collision.name)
                {
                    showMessage = role + blackMessage[i]; //要記得照順序排
                    break;
                }
            }
            photonView.RPC("sendMessage", PhotonTargets.All,showMessage); //第三個參數:傳送要顯示的話
        }

        if (collision.gameObject.CompareTag("金球"))
        { //金球
            money += 10;
            moneyText.text = "金錢\n x" + money + "(百萬)";
            GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(goldBallMusic);
            identificatePlayerMoney(); //判斷是哪個player

            for (int i = 0; i < allArray.Length; i++) //判斷是哪個球，給予對應的話
            {
                if (allArray[i].name + "(Clone)" == collision.name)
                {
                    showMessage = role + goldMessage[i]; //要記得照順序排
                    break;
                }
            }
            photonView.RPC("sendMessage", PhotonTargets.All, showMessage); //第三個參數:傳送要顯示的話
        }
        if (collision.gameObject.CompareTag("炸彈"))
        { //炸彈，扣50%的錢
            if (money > 0)
            {
                money = money / 2;
                moneyText.text = "金錢\n x" + money + "(百萬)";
            }
            GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(bombMusic);
            identificatePlayerMoney(); //判斷是哪個player

            showMessage = role + bombMessage;  //炸彈訊息
            photonView.RPC("sendMessage", PhotonTargets.All, showMessage); //第三個參數:傳送要顯示的話
        }
        if (collision.gameObject.CompareTag("報紙")) //報紙效果:出現選單可以陷害人，指定敵對黨某人有誹聞(意涵:爆料)，被指定者扣錢20%
        {
            //先顯示報紙選單
            paperMenu.SetActive(true);
            GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(itemMusic);
            //showMessage = role + paperMessage;
            //photonView.RPC("sendMessage", PhotonTargets.All, showMessage); //第三個參數:傳送要顯示的話
        }
        if (collision.gameObject.CompareTag("麥克風"))
        {
            GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(itemMusic);
            //showMessage = role + microphoneMessage;
            //photonView.RPC("sendMessage", PhotonTargets.All, showMessage); //第三個參數:傳送要顯示的話
        }

        //每次金錢變動時，來檢查金錢總額
        identificateWinPlayer();
    }

    void identificateWinPlayer() //判斷誰是執政黨，把錢加總
    {
        greenMoney = 0; //初始化
        blueMoney = 0;

        //判斷是什麼陣營，將錢加總
        for (int i = 0; i < PlayerList.Count; i++) //4個player都過濾
        {
            if ((string)PhotonNetwork.playerList[i].CustomProperties["PartyColor"] == "green") //如果是綠的
            {
                greenMoney += playerMoney[i];
            }
            else if ((string)PhotonNetwork.playerList[i].CustomProperties["PartyColor"] == "blue") //藍的
            {
                blueMoney += playerMoney[i];
            }
        }

        //判斷是誰執政
        if (greenMoney - blueMoney >= 30) //綠黨執政
        {
            if (winColor != 1) //現在不是綠色執政的話，才改
            {
                photonView.RPC("showGreenNoticeBroad", PhotonTargets.All); //傳送給大家顯示
            }
        }
        else if (blueMoney - greenMoney >= 30) //藍黨執政
        {
            if (winColor != 2) //如果現在不是藍色執政，才變換
            {
                photonView.RPC("showBlueNoticeBroad", PhotonTargets.All); //傳送給大家顯示

            }
        }
    }

    void identificatePlayerMoney()     //判斷是哪個Player,加到對應的錢
    {
        if (player == PhotonNetwork.masterClient)  //player1
        {
            playerMoney[0] = money;
        }
        else if (player == PhotonNetwork.masterClient.GetNext()) //player2
        {
            playerMoney[1] = money;
        }
        else if (player == PhotonNetwork.masterClient.GetNext().GetNext()) //player3
        {
            playerMoney[2] = money;
        }
        else if (player == PhotonNetwork.masterClient.GetNext().GetNext().GetNext()) //player4
        {
            playerMoney[3] = money;
        }
    }

    void setWinPlayer() //設定執政黨
    {
        if(winColor == 1) //綠色執政
        {
            if (partyColor == "green") //如果是綠的，可使用執政黨能力
            {
                bombObj.SetActive(true); //可使用炸彈
                winLogo.SetActive(true); //有中選標誌
            }
            else if (partyColor == "blue") //藍的，關閉能力
            {
                bombObj.SetActive(false);
                winLogo.SetActive(false);
            }
        }
        else if(winColor == 2) //藍色執政
        {
            if (partyColor == "green") //綠的，關閉能力
            {
                bombObj.SetActive(false);
                winLogo.SetActive(false);
            }
            else if (partyColor == "blue") //藍的，可使用執政黨能力
            {
                bombObj.SetActive(true); //可使用炸彈
                winLogo.SetActive(true); //有中選標誌
            }
        }
    }

    void showWinPartyMessage() //僅顯示3秒
    {
        if(winColor == 1) //綠
        {
            Invoke("greenTimeCountDown", 3);
        }
        else if(winColor == 2) //藍
        {
            Invoke("blueTimeCountDown", 3);
        }
        
    }
    
    [PunRPC]
    void sendMessage(string text) //傳遞訊息
    {
        noticeBoard.SetActive(true); //開啟訊息顯示
        noticeBoardText.text = text;
        Invoke("threeTimeCountDown", 3); //三秒便會關閉訊息
    }

    void timeCountDown() //時間倒數，每次減一秒
    {
        timeText.text = "" + time;
        time--;
        if (time == 0) //如果時間歸零，就停止減少
        {
            hash = new Hashtable();
            hash.Add("Money", money); //把錢加進hash
            hash.Add("WinColor",winColor); //把執政黨判斷加進hash
            PhotonNetwork.player.SetCustomProperties(hash);

            CancelInvoke("timeCountDown");
            timeText.text = "Game Over";
            Time.timeScale = 0f; //時間暫停

        }
    }

    void greenTimeCountDown() //顯示三秒便結束(綠)
    {
        greenNoticeBoard.SetActive(false);
    }

    void blueTimeCountDown() //顯示三秒便結束(藍)
    {
        blueNoticeBoard.SetActive(false);
    }

    void threeTimeCountDown() //3秒顯示訊息
    {
        noticeBoard.SetActive(false);
    }

    void setRoleImg(Image personImg,string person) //根據player選的人物，給予相應的照片
    {
        if (person == "吳指癢")
        {
            personImg.sprite = role1;
        }
        else if (person == "洪咻柱")
        {
            personImg.sprite = role2;
        }
        else if (person == "蔡中聞")
        {
            personImg.sprite = role3;
        }
        else if (person == "蘇嘎拳")
        {
            personImg.sprite = role4;
        }
    }

    void sendRoleImg(Image img) //判斷是哪張圖片，傳送該圖片
    {
        if (img.sprite.name == "吳指癢-半身")
        {         
            photonView.RPC("SetPaperOn1", PhotonTargets.Others);
        }
        else if (img.sprite.name == "洪咻柱-半身")
        {
            photonView.RPC("SetPaperOn2", PhotonTargets.Others);
        }
        else if (img.sprite.name == "蔡中聞-半身")
        {
            photonView.RPC("SetPaperOn3", PhotonTargets.Others);
        }
        else if (img.sprite.name == "蘇嘎拳-半身")
        {
            photonView.RPC("SetPaperOn4", PhotonTargets.Others);
        }
    }

    public void sendPaperMessage(int pIndex) //點選按鈕才可以傳送開啟報紙的資訊
    {
        //playerIndex = pIndex; //設定圖片為誰，0:player1、1:player2、2:player3
        PlayerList = new List<PhotonPlayer>();
        PlayerList.Add(PhotonNetwork.masterClient); //1
        PlayerList.Add(PhotonNetwork.masterClient.GetNext()); //2
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext()); //3
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext().GetNext()); //4

        switch (pIndex) //判斷是改哪個玩家的圖片
        {
            case 0: //第一個人
                //photonView.RPC("SetPaperOn1", PhotonTargets.Others);
                sendRoleImg(paperPersonImage[0]);
                if (player == PlayerList[0]) //Player1
                {
                    photonView.RPC("paperEffect2",PhotonTargets.Others); //扣Player2
                }
                else if(player == PlayerList[1]) //Player2
                {
                    photonView.RPC("paperEffect3", PhotonTargets.Others); //扣Player3
                }
                else if(player == PlayerList[2]) //Player3
                {
                    photonView.RPC("paperEffect4", PhotonTargets.Others); //扣Player4
                }
                else if(player == PlayerList[3]) //Player4
                {
                    photonView.RPC("paperEffect1", PhotonTargets.Others); //扣Player1
                }
                break;
            case 1: //第二個人
                    // photonView.RPC("SetPaperOn2", PhotonTargets.Others);
                sendRoleImg(paperPersonImage[1]);
                if (player == PlayerList[0]) //Player1
                {
                    photonView.RPC("paperEffect3", PhotonTargets.Others); //扣Player3
                }
                else if (player == PlayerList[1]) //Player2
                {
                    photonView.RPC("paperEffect4", PhotonTargets.Others); //扣Player4
                }
                else if (player == PlayerList[2]) //Player3
                {
                    photonView.RPC("paperEffect1", PhotonTargets.Others); //扣Player1
                }
                else if (player == PlayerList[3]) //Player4
                {
                    photonView.RPC("paperEffect2", PhotonTargets.Others); //扣Player2
                }
                break;
            case 2: //第三個人
                sendRoleImg(paperPersonImage[2]);
                if (player == PlayerList[0]) //Player1
                {
                    photonView.RPC("paperEffect4", PhotonTargets.Others); //扣Player4
                }
                else if (player == PlayerList[1]) //Player2
                {
                    photonView.RPC("paperEffect1", PhotonTargets.Others); //扣Player1
                }
                else if (player == PlayerList[2]) //Player3
                {
                    photonView.RPC("paperEffect2", PhotonTargets.Others); //扣Player2
                }
                else if (player == PlayerList[3]) //Player4
                {
                    photonView.RPC("paperEffect3", PhotonTargets.Others); //扣Player3
                }
                break;
        }
    }

    [PunRPC] //傳送綠黨執政消息
    void showGreenNoticeBroad()
    {
        blueNoticeBoard.SetActive(false); //佈告欄-藍關掉
        greenNoticeBoard.SetActive(true);  //佈告欄-綠
        winColor = 1;
        showWinPartyMessage(); //三秒後結束畫面
        setWinPlayer(); //判斷執政黨
        //綠黨執政效果:道具改為每15秒產生一次
        GameObject.Find("左邊框").GetComponent<GenerateBall>().generateItemseconds = 15;
        GameObject.Find("右邊框").GetComponent<GenerateBall>().generateItemseconds = 15;
    }

    [PunRPC] //傳送藍黨執政消息
    void showBlueNoticeBroad()
    {
        greenNoticeBoard.SetActive(false); //佈告欄-綠關掉
        blueNoticeBoard.SetActive(true); //佈告欄 - 藍
        winColor = 2;
        showWinPartyMessage(); //三秒後結束畫面
        setWinPlayer(); //判斷執政黨
        //藍黨執政效果:所有東西速度變慢(新產生的物體，速度變為1)
        GameObject.Find("左邊框").GetComponent<GenerateBall>().changeSpeedX = 1;
        GameObject.Find("右邊框").GetComponent<GenerateBall>().changeSpeedY = 1;


    }

    //傳遞報紙訊息
    [PunRPC]
    void SetPaperOn1() //Player1
    {
        paper.SetActive(true); //開啟報紙
        GameObject.Find("人物圖片").GetComponent<ChangePaperPersonImg>().ChangeImg1();
    }

    [PunRPC]
    void SetPaperOn2() //Player2
    {
        paper.SetActive(true); //開啟報紙
        GameObject.Find("人物圖片").GetComponent<ChangePaperPersonImg>().ChangeImg2();
    }

    [PunRPC]
    void SetPaperOn3() //Player3
    {
        paper.SetActive(true); //開啟報紙
        GameObject.Find("人物圖片").GetComponent<ChangePaperPersonImg>().ChangeImg3();
    }
    [PunRPC]
    void SetPaperOn4() //Player4
    {
        paper.SetActive(true); //開啟報紙
        GameObject.Find("人物圖片").GetComponent<ChangePaperPersonImg>().ChangeImg4();
    }

    //報紙效果:指定人並扣錢
    [PunRPC]
    void paperEffect1() //扣Player1的錢
    {
        if(player == PhotonNetwork.masterClient) //如果是Player1才執行
        {
            if(money > 0)
            {
                money = (int)(money * 0.8);
                moneyText.text = "金錢\n x" + money + "(百萬)";
            }
            identificatePlayerMoney();
            identificateWinPlayer();
        }
    }

    [PunRPC]
    void paperEffect2() //扣Player2的錢
    {
        if (player == PhotonNetwork.masterClient.GetNext()) //如果是Player2才執行
        {
            if(money > 0)
            {
                money = (int)(money * 0.8);
                moneyText.text = "金錢\n x" + money + "(百萬)";
            }
            identificatePlayerMoney();
            identificateWinPlayer();
        }
    }

    [PunRPC]
    void paperEffect3() //扣Player3的錢
    {
        if (player == PhotonNetwork.masterClient.GetNext().GetNext()) //如果是Player3才執行
        {
            if(money > 0)
            {
                money = (int)(money * 0.8);
                moneyText.text = "金錢\n x" + money + "(百萬)";
            }
            identificatePlayerMoney();
            identificateWinPlayer();
        }
    }

    [PunRPC]
    void paperEffect4() //扣Player4的錢
    {
        if (player == PhotonNetwork.masterClient.GetNext().GetNext().GetNext()) //如果是Player4才執行
        {
            if(money > 0)
            {
                money = (int)(money * 0.8);
                moneyText.text = "金錢\n x" + money + "(百萬)";
            }
            identificatePlayerMoney();
            identificateWinPlayer();
        }
    }


    
}
