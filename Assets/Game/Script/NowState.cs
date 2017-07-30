using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NowState : Photon.PunBehaviour { //控制連線及背景component
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
    //bool isFirst = true;

    //執政
    public GameObject winLogo; //中選logo
    public int winColor = 0; //判斷現在是誰執政，0:預設、1:綠、2:藍
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
    public Image PlayerCharacterImg; //角色圖片

    //人物圖片
    public Sprite role1;
    public Sprite role2;
    public Sprite role3;
    public Sprite role4;

    public Sprite[] SneerPersonImg; //人物奸笑
    public Sprite[] AngryPersonImg; //人物憤怒
    public Sprite[] cryPersonImg; //人物哭泣

    public Sprite[] leftPersonImg; //左邊位置顯示人物圖
    public Sprite[] upPersonImg; //上面位置顯示人物圖
    public Sprite[] rightPersonImg; //右邊位置顯示人物圖


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
    public GameObject noColorNoticeBoard; //顯示無黨執政
    public GameObject badNoticeBoard; //顯示黑訊息
    public GameObject goodNoticeBoard; //顯示金訊息
    public GameObject bombNoticeBoard; //顯示炸彈訊息
    public Text badNoticeBoardText; //黑訊息文字
    public Text goodNoticeBoardText; //金訊息文字
    public Text paperNoticeBoardText; //報紙訊息文字
    public Text bombNoticeBoardText; //炸彈訊息文字
    string[] blackMessage = { "反對年金改革", "反對同婚", "支持一例一休", "支持美牛進口", "反對加入TPP", "反對空服員罷工", "反對調漲最低薪資", "支持建造四", "支持進口核災食品" };
    string[] goldMessage = { "支持年金改革", "支持同婚", "反對一例一休", "反對美牛進口", "支持加入TPP", "支持空服員罷工", "支持調漲最低薪資", "反對建造核四", "反對進口核災食品" };
    string badMessage; //黑訊息
    string goodMessage; //金訊息
    string paperMessage;//報紙訊息
    string bombMessage; //炸彈訊息(毀謗)

    //音樂
    public GameObject bgm; //遊戲bgm
    public GameObject goldBallMusic; //金球音樂
    public GameObject blackBallMusic; //黑球音樂
    public GameObject itemMusic; //道具音樂
    public GameObject bombMusic; //炸彈音樂
    public GameObject readyMusic; //ready音樂
    public GameObject goMusic; //go音樂

    //麥克風功能，儲存目的地位置
    Vector3 leftPortalPos;
    Vector3 upPortalPos;
    Vector3 rightPortalPos;

    // Use this for initialization
    void Start() {

        //獲得所有的球+道具
        allArray = GameObject.Find("左Portal(真)").GetComponent<SendBall>().allArray;

        //麥克風功能，儲存目的地位置
        leftPortalPos = GameObject.Find("左Portal(真)").transform.position; //左邊portal位置
        upPortalPos = GameObject.Find("上Portal(真)").transform.position; //上邊portal位置
        rightPortalPos = GameObject.Find("右Portal(真)").transform.position; //右邊portal位置

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
            decideWhichPortal(PlayerList[1], 0); //顯示portal人物圖片
            decideWhichPortal(PlayerList[2], 1); //顯示portal人物圖片
            decideWhichPortal(PlayerList[3], 2); //顯示portal人物圖片
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
            decideWhichPortal(PlayerList[2], 0); //顯示portal人物圖片
            decideWhichPortal(PlayerList[3], 1); //顯示portal人物圖片
            decideWhichPortal(PlayerList[0], 2); //顯示portal人物圖片
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
            decideWhichPortal(PlayerList[3], 0); //顯示portal人物圖片
            decideWhichPortal(PlayerList[0], 1); //顯示portal人物圖片
            decideWhichPortal(PlayerList[1], 2); //顯示portal人物圖片
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
            decideWhichPortal(PlayerList[0], 0); //顯示portal人物圖片
            decideWhichPortal(PlayerList[1], 1); //顯示portal人物圖片
            decideWhichPortal(PlayerList[2], 2); //顯示portal人物圖片
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
            Debug.Log("playerName:" + playerName);
            Debug.Log("okCount:"+okCount);
            if(okCount == 4) //如果大家都準好就可以一起開始
            {
                if(okLevel == 1) //一開始畫面
                {
                    Debug.Log("Level1");
                    photonView.RPC("sendReady", PhotonTargets.All);
                }
                else if(okLevel == 2) //結束畫面
                {
                    Debug.Log("Level1");
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
        Debug.Log("PlayerName:"+decidePlayer.NickName);
        if(portalPos == 0)
        {
            //左邊
            if (portalLeft_blue.activeSelf == true) //藍色
            {
                portalLeft_blue.GetComponent<showPortalPersonImg>().setPortalPersonImg((string)decidePlayer.CustomProperties["Role"]);
            }
            else if (portalLeft_blue.activeSelf == false) //綠色
            {
                portalLeft_green.GetComponent<showPortalPersonImg>().setPortalPersonImg((string)decidePlayer.CustomProperties["Role"]);
            }
        }
        else if(portalPos == 1)
        {
            //上面
            if (portalUp_blue.activeSelf == true) //藍色
            {
                portalUp_blue.GetComponent<showPortalPersonImg>().setPortalPersonImg((string)decidePlayer.CustomProperties["Role"]);
            }
            else if (portalUp_blue.activeSelf == false) //綠色
            {
                portalUp_green.GetComponent<showPortalPersonImg>().setPortalPersonImg((string)decidePlayer.CustomProperties["Role"]);
            }
        }
        else if(portalPos == 2)
        {
            //右邊
            if (portalRight_blue.activeSelf == true) //藍色
            {
                portalRight_blue.GetComponent<showPortalPersonImg>().setPortalPersonImg((string)decidePlayer.CustomProperties["Role"]);
            }
            else if (portalRight_blue.activeSelf == false) //綠色
            {
                portalRight_green.GetComponent<showPortalPersonImg>().setPortalPersonImg((string)decidePlayer.CustomProperties["Role"]);
            }
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
            hash.Add("WinColor", winColor); //把執政黨判斷加進hash
            PhotonNetwork.player.SetCustomProperties(hash);
            CancelInvoke("timeCountDown");
            timeText.text = "Game Over";
            Time.timeScale = 0f; //時間暫停

            photonView.RPC("sendOk",PhotonTargets.MasterClient,2);

           
           
        }
    }

    void OnTriggerEnter2D(Collider2D collision) //進洞
    {
        if (collision.gameObject.CompareTag("黑球"))
        { //黑球
            Debug.Log("collisionName:" + collision.gameObject.name);
            money -= 10;
            moneyText.text = money + "(百萬)";
            GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(blackBallMusic);
            setFaceImg(cryPersonImg); //把圖片換成奸笑的表情
            identificatePlayerMoney(); //判斷是哪個player
            
            for(int i =0; i < allArray.Length; i++) //判斷是哪個球，給予對應的話
            {
                if (allArray[i].name+"(Clone)" == collision.name)
                {
                    badMessage = role + blackMessage[i]; //要記得照順序排
                    break;
                }
            }
            //Destroy(collision.gameObject); //把黑球刪掉
            photonView.RPC("sendBadMessage", PhotonTargets.All, badMessage); //第三個參數:傳送要顯示的話
        }

        else if (collision.gameObject.CompareTag("金球"))
        { //金球 
            Debug.Log("collisionName:" + collision.gameObject.name);
            money += 10;
            moneyText.text = money + "(百萬)";
            GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(goldBallMusic);
            setFaceImg(SneerPersonImg); //把圖片換成奸笑的表情
            //Invoke("setRoleImg(PlayerCharacterImg,role)",3);
            identificatePlayerMoney(); //判斷是哪個player

            int ballIndex = 0; //儲存球的位置

            for (int i = 0; i < allArray.Length; i++) //判斷是哪個球，給予對應的話
            {
                if (allArray[i].name + "(Clone)" == collision.name)
                {
                    ballIndex = i;
                    goodMessage = role + goldMessage[i - (allArray.Length-3)/2]; //要記得照順序排
                    break;
                }
            }
            Destroy(collision.gameObject); //把金球刪掉
            //金球吃完後會產生黑球彈出去，意味著拿完好處就丟掉
            int newBallIndex = ballIndex - (allArray.Length - 3) / 2; //此金球的黑球相應位置，要記得排好
            //int newBallIndex = 1;
            Debug.Log("allArray.Length:" + allArray.Length);
            GameObject newBlackBall = Instantiate(allArray[newBallIndex], transform.position + new Vector3(0, 1, 0), new Quaternion(0, 0, 0, 0));
            newBlackBall.GetComponent<Rigidbody2D>().velocity = new Vector2(0,2); //新產生一顆黑球，彈出去
            photonView.RPC("sendGoodMessage", PhotonTargets.All, goodMessage); //第三個參數:傳送要顯示的話
        }
        else if (collision.gameObject.CompareTag("炸彈"))
        { //炸彈，扣50%的錢
            Debug.Log("collisionName:" + collision.gameObject.name);
            if (money > 0)
            {
                money = money / 2;
                moneyText.text = money + "(百萬)";
            }
            GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(bombMusic);
            identificatePlayerMoney(); //判斷是哪個player
            bombMessage = role + "與企業董事秘密餐會!";
            setFaceImg(AngryPersonImg); //把圖片換成生氣的表情
            //Destroy(collision.gameObject); //把炸彈刪掉
            photonView.RPC("sendBombMessage", PhotonTargets.All, bombMessage); //第三個參數:傳送要顯示的話
            //showMessage = role + bombMessage;  //炸彈訊息
            //photonView.RPC("sendMessage", PhotonTargets.All, showMessage); //第三個參數:傳送要顯示的話
        }
        else if (collision.gameObject.CompareTag("報紙")) //報紙效果:出現選單可以陷害人，指定敵對黨某人有誹聞(意涵:爆料)，被指定者扣錢20%
        {
            //先顯示報紙選單
            Debug.Log("collisionName:" + collision.gameObject.name);
            paperMenu.SetActive(true);
            GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(itemMusic);
            //Destroy(collision.gameObject); //把報紙刪掉
            //paperMessage = "知名政治人物"+ role + "酒後失態，服務員控訴性騷擾!";
        }
        else if (collision.gameObject.CompareTag("麥克風"))
        {
            Debug.Log("collisionName:" + collision.gameObject.name);
            GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(itemMusic);
           // microphoneEffect(); //麥克風效果1
            goodMessage = role + "發表直播演說，\n獲得民眾支持";
            //Destroy(collision.gameObject); //把麥克風刪掉
            photonView.RPC("sendGoodMessage", PhotonTargets.All, goodMessage); //第三個參數:傳送要顯示的話
            photonView.RPC("sendFaceImg", PhotonTargets.Others); //改變表情
            photonView.RPC("microphoneEffect2", PhotonTargets.Others,player); //麥克風效果2，參數為使用麥克風的player
            //showMessage = role + microphoneMessage;
            //photonView.RPC("sendMessage", PhotonTargets.All, showMessage); //第三個參數:傳送要顯示的話
        }
        
        //每次金錢變動時，來檢查金錢總額
        identificateWinPlayer();
        Destroy(collision.gameObject); //把碰觸到的球刪掉
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

    #region 人物表情

    void setRoleImg(Image personImg, string person) //根據player選的人物，給予相應的照片
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
    
    void setFaceImg(Sprite[] img) //根據吃到的道具，將Player設誠相關的照片
    {
        if (role == "吳指癢")
        {
            PlayerCharacterImg.sprite = img[0];
        }
        else if (role == "洪咻柱")
        {
            PlayerCharacterImg.sprite = img[1];
        }
        else if (role == "蔡中聞")
        {
            PlayerCharacterImg.sprite = img[2];
        }
        else if (role == "蘇嘎拳")
        {
            PlayerCharacterImg.sprite = img[3];
        }
        Invoke("changeToOriginalImg", 3); //顯示3秒換回來
    }

    void changeToOriginalImg() //把圖片換回來
    {
        if (role == "吳指癢")
        {
            PlayerCharacterImg.sprite = role1;
        }
        else if (role == "洪咻柱")
        {
            PlayerCharacterImg.sprite = role2;
        }
        else if (role == "蔡中聞")
        {
            PlayerCharacterImg.sprite = role3;
        }
        else if (role == "蘇嘎拳")
        {
            PlayerCharacterImg.sprite = role4;
        }
    }
    #endregion

    #region 黑訊息
    [PunRPC]
    void sendBadMessage(string text) //傳遞好訊息
    {
        badNoticeBoard.SetActive(true); //開啟訊息顯示
        badNoticeBoardText.text = text;
        Invoke("badTimeCountDown", 3); //三秒便會關閉訊息
    }

    void badTimeCountDown() //3秒顯示訊息
    {
        badNoticeBoard.SetActive(false);
    }
    #endregion

    #region 金訊息
    [PunRPC]
    void sendGoodMessage(string text) //傳遞好訊息
    {
        goodNoticeBoard.SetActive(true);// 開啟訊息顯示
        goodNoticeBoardText.text = text;
        Invoke("goodTimeCountDown",3);//三秒便會關閉訊息
    }

    void goodTimeCountDown()//3秒顯示訊息
    {
        goodNoticeBoard.SetActive(false);
    }
    #endregion

    #region 毀謗

    [PunRPC]
    void sendBombMessage(string text) //傳遞毀謗訊息
    {
        bombNoticeBoard.SetActive(true);
        bombNoticeBoardText.text = text;
        Invoke("bombTimeCountDown", 3); //三秒便會關閉訊息
    }

    void bombTimeCountDown()
    {
        bombNoticeBoard.SetActive(false);
    }

    #endregion

    #region 報紙
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
                    // photonView.RPC("SetPaperOn2", PhotonTargets.Others);
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
        if (img.sprite.name == "吳指癢-半身")
        {
            photonView.RPC("SetPaperOn1", PhotonTargets.All);
        }
        else if (img.sprite.name == "洪咻柱-半身")
        {
            photonView.RPC("SetPaperOn2", PhotonTargets.All);
        }
        else if (img.sprite.name == "蔡中聞-半身")
        {
            photonView.RPC("SetPaperOn3", PhotonTargets.All);
        }
        else if (img.sprite.name == "蘇嘎拳-半身")
        {
            photonView.RPC("SetPaperOn4", PhotonTargets.All);
        }
    }

    //傳遞報紙訊息
    [PunRPC]
    void SetPaperOn1() //Player1
    {
        paper.SetActive(true); //開啟報紙
        GameObject.Find("人物圖片").GetComponent<ChangePaperPersonImg>().ChangeImg1();
        Invoke("paperTimeCountDown", 3);
    }

    [PunRPC]
    void SetPaperOn2() //Player2
    {
        paper.SetActive(true); //開啟報紙
        GameObject.Find("人物圖片").GetComponent<ChangePaperPersonImg>().ChangeImg2();
        Invoke("paperTimeCountDown", 3);
    }

    [PunRPC]
    void SetPaperOn3() //Player3
    {
        paper.SetActive(true); //開啟報紙
        GameObject.Find("人物圖片").GetComponent<ChangePaperPersonImg>().ChangeImg3();
        Invoke("paperTimeCountDown", 3);
    }

    [PunRPC]
    void SetPaperOn4() //Player4
    {
        paper.SetActive(true); //開啟報紙
        GameObject.Find("人物圖片").GetComponent<ChangePaperPersonImg>().ChangeImg4();
        Invoke("paperTimeCountDown", 3);
    }

    void paperTimeCountDown() //過3秒報紙自動關閉
    {
        paper.SetActive(false);
    }

    //報紙效果:指定人並扣錢
    [PunRPC]
    void paperEffect1() //扣Player1的錢
    {
        paperNoticeBoardText.text = "知名政治人物" + PhotonNetwork.masterClient.CustomProperties["Role"] +"酒後失態，服務員控訴性騷擾!";
        if (player == PhotonNetwork.masterClient) //如果是Player1才執行
        {
            if(money > 0)
            {
                money = (int)(money * 0.8);
                moneyText.text = money + "(百萬)";
            }
            setFaceImg(AngryPersonImg); //把圖片換成生氣的表情
            identificatePlayerMoney();
            identificateWinPlayer();
        }
    }

    [PunRPC]
    void paperEffect2() //扣Player2的錢
    {
        paperNoticeBoardText.text = "知名政治人物" + PhotonNetwork.masterClient.GetNext().CustomProperties["Role"] + "酒後失態，服務員控訴性騷擾!";
        if (player == PhotonNetwork.masterClient.GetNext()) //如果是Player2才執行
        {
            if(money > 0)
            {
                money = (int)(money * 0.8);
                moneyText.text = money + "(百萬)";
            }
            setFaceImg(AngryPersonImg); //把圖片換成生氣的表情
            identificatePlayerMoney();
            identificateWinPlayer();
        }
    }

    [PunRPC]
    void paperEffect3() //扣Player3的錢
    {
        paperNoticeBoardText.text = "知名政治人物" + PhotonNetwork.masterClient.GetNext().GetNext().CustomProperties["Role"] + "酒後失態，服務員控訴性騷擾!";
        if (player == PhotonNetwork.masterClient.GetNext().GetNext()) //如果是Player3才執行
        {
            if(money > 0)
            {
                money = (int)(money * 0.8);
                moneyText.text = money + "(百萬)";
            }
            setFaceImg(AngryPersonImg); //把圖片換成生氣的表情
            identificatePlayerMoney();
            identificateWinPlayer();
        }
    }

    [PunRPC]
    void paperEffect4() //扣Player4的錢
    {
        paperNoticeBoardText.text = "知名政治人物" + PhotonNetwork.masterClient.GetNext().GetNext().GetNext().CustomProperties["Role"] + "酒後失態，服務員控訴性騷擾!";
        if (player == PhotonNetwork.masterClient.GetNext().GetNext().GetNext()) //如果是Player4才執行
        {
            if(money > 0)
            {
                money = (int)(money * 0.8);
                moneyText.text = money + "(百萬)";
            }
            setFaceImg(AngryPersonImg); //把圖片換成生氣的表情
            identificatePlayerMoney();
            identificateWinPlayer();
        }
    }

    #endregion

    #region 麥克風
    void microphoneEffect() //麥克風效果，把該player場上的黑球變金球
    {
        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject))) //得到所有hierarchy的物件
        {
            Debug.Log(obj.name);
            if (obj.CompareTag("黑球")) //如果該物件是黑球，轉成金球
            {
                Debug.Log("我進來啦");
                int ballIndex = 0; //存取該黑球的index
                Vector3 ballPosition = obj.transform.position; //存取該黑球的位置
                for (int i = 0; i < allArray.Length; i++) //記得排好順序
                {
                    Debug.Log("obj.name:"+obj.name);
                    Debug.Log("allArray[" + i + "].name(Clone):" + allArray[i] + "(Clone)");
                    if (obj.name == allArray[i].name + "(Clone)")
                    {
                        Debug.Log("成功進入");
                        ballIndex = i; //存取黑球在此陣列的index
                        break;
                    }
                }
                Destroy(obj); //刪除此黑球
                Debug.Log("ballIndex:"+ballIndex);
                //int newBallIndex = ballIndex + (allArray.Length/2) ; //index + array全部/2 會得到其相應的金球位置
                int newBallIndex = 0; //先預設為TPP-金
                Debug.Log("newBallIndex:" + newBallIndex);
                GameObject goldBall = Instantiate(allArray[newBallIndex], ballPosition, new Quaternion(0, 0, 0, 0)); //建立相對應的金球
                Debug.Log("建立成功");
                goldBall.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-1); //不知道速度要指派怎樣，都往下好ㄌ
            }
        }        
    }

    [PunRPC]
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

                //Vector3 position = new Vector3(0, 0, 0);
                //targetBall = obj; //儲存目標球
                //microphoneTrue = true;

                if (player == PlayerList[0]) //player1
                {
                    if (useItPlayer == PlayerList[1]) //左
                    {
                        obj.GetComponent<BallMove>().microphoneEffect(leftPortalPos); //指定位置
                        //TowardTarget(obj, leftPortalPos);
                    }
                    else if (useItPlayer == PlayerList[2]) //上
                    {
                        //TowardTarget(obj, upPortalPos);
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

   

   /* //朝著滑鼠的方式移動
    Vector3 v;
    public float maxSpeed = 5.0f;
    public float speedDelta = 1.0f;
    void TowardTarget(GameObject targetObj,Vector3 pos)
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 10f)); //Assume your camera's z is -10 and cube's z is 0
        targetObj.transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref v, speedDelta, maxSpeed);
    }
    */

    [PunRPC]
    void sendFaceImg() //傳送給其他人，生氣
    {
        setFaceImg(AngryPersonImg); //把圖片換成生氣的表情
    }
    #endregion

    #region 政黨執政

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
        if (greenMoney - blueMoney >= 100) //綠黨執政
        {
            if (winColor != 1) //現在不是綠色執政的話，才改
            {
                photonView.RPC("showGreenNoticeBoard", PhotonTargets.All); //傳送給大家顯示
            }
        }
        else if (blueMoney - greenMoney >= 100) //藍黨執政
        {
            if (winColor != 2) //如果現在不是藍色執政，才變換
            {
                photonView.RPC("showBlueNoticeBoard", PhotonTargets.All); //傳送給大家顯示

            }
        }
        else //沒有黨超過對方30，無黨執政
        {
            if(winColor != 0)
            {
                photonView.RPC("showNoColorNoticeBoard", PhotonTargets.All); //傳送給大家顯示
            }
        }
    }

    [PunRPC] //傳送綠黨執政消息
    void showGreenNoticeBoard()
    {
        blueNoticeBoard.SetActive(false); //佈告欄-藍關掉
        noColorNoticeBoard.SetActive(false); //佈告欄-無黨執政關閉
        greenNoticeBoard.SetActive(true);  //佈告欄-綠
        winColor = 1;
        showWinPartyMessage(); //三秒後結束畫面
        setWinPlayer(); //判斷執政黨
        //綠黨執政效果:道具改為每15秒產生一次
        GameObject.Find("左邊框").GetComponent<GenerateBall>().generateItemseconds = 15;
        GameObject.Find("右邊框").GetComponent<GenerateBall>().generateItemseconds = 15;
        //藍黨執政的效果要改回來
        GameObject.Find("左邊框").GetComponent<GenerateBall>().changeSpeedX = 2;
        GameObject.Find("右邊框").GetComponent<GenerateBall>().changeSpeedY = 2;
        GameObject.Find("左Portal(真)").GetComponent<SendBall>().generateBallSpeed = 2;
        GameObject.Find("右Portal(真)").GetComponent<SendBall>().generateBallSpeed = 2;
        GameObject.Find("上Portal(真)").GetComponent<SendBall>().generateBallSpeed = 2;
            //場上的球速度也要變2
        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject))) //得到所有hierarchy的物件
        {
            if (obj.CompareTag("黑球") || obj.CompareTag("金球") || obj.CompareTag("麥克風") || obj.CompareTag("報紙") || obj.CompareTag("炸彈"))
            {
                //obj.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 2);
                checkOriginalSpeed(obj, 2); //改變速度
                obj.GetComponent<BallMove>().blueEffectButton(); //將鎖住速度功能關閉
           }
        }
    }

    [PunRPC] //傳送藍黨執政消息
    void showBlueNoticeBoard()
    {
        greenNoticeBoard.SetActive(false); //佈告欄-綠關掉
        noColorNoticeBoard.SetActive(false); //佈告欄-無黨執政關閉
        blueNoticeBoard.SetActive(true); //佈告欄 - 藍
        winColor = 2;
        showWinPartyMessage(); //三秒後結束畫面
        setWinPlayer(); //判斷執政黨
        //藍黨執政效果:所有東西速度變慢(新產生的物體，速度變為1)
        GameObject.Find("左邊框").GetComponent<GenerateBall>().changeSpeedX = 1;
        GameObject.Find("右邊框").GetComponent<GenerateBall>().changeSpeedY = 1;
        GameObject.Find("左Portal(真)").GetComponent<SendBall>().generateBallSpeed = 1;
        GameObject.Find("右Portal(真)").GetComponent<SendBall>().generateBallSpeed = 1;
        GameObject.Find("上Portal(真)").GetComponent<SendBall>().generateBallSpeed = 1;
            //場上的球速度也要變1
        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject))) //得到所有hierarchy的物件
        {
            if(obj.CompareTag("黑球") || obj.CompareTag("金球") || obj.CompareTag("麥克風") || obj.CompareTag("報紙") || obj.CompareTag("炸彈"))
            {
                // obj.GetComponent<Rigidbody2D>().velocity = new Vector2(1,1);
                checkOriginalSpeed(obj, 1); //改變速度
                obj.GetComponent<BallMove>().blueEffectButton(); //將鎖住速度功能開啟
            }
        }
        
        //綠黨執政的效果要改回來
        GameObject.Find("左邊框").GetComponent<GenerateBall>().generateItemseconds = 20;
        GameObject.Find("右邊框").GetComponent<GenerateBall>().generateItemseconds = 20;
    }

    [PunRPC] //傳送無黨執政消息
    void showNoColorNoticeBoard()
    {
        blueNoticeBoard.SetActive(false); //佈告欄-藍關掉
        greenNoticeBoard.SetActive(false); //佈告欄-綠關掉
        noColorNoticeBoard.SetActive(true); //佈告欄-無黨執政
        winColor = 0;
        showWinPartyMessage(); //三秒後結束畫面
        setWinPlayer(); //判斷執政黨

        //無黨執政，效果要改回來
        //綠黨
        GameObject.Find("左邊框").GetComponent<GenerateBall>().generateItemseconds = 20;
        GameObject.Find("右邊框").GetComponent<GenerateBall>().generateItemseconds = 20;
        //藍黨
        GameObject.Find("左邊框").GetComponent<GenerateBall>().changeSpeedX = 2;
        GameObject.Find("右邊框").GetComponent<GenerateBall>().changeSpeedY = 2;
        GameObject.Find("左Portal(真)").GetComponent<SendBall>().generateBallSpeed = 2;
        GameObject.Find("右Portal(真)").GetComponent<SendBall>().generateBallSpeed = 2;
        GameObject.Find("上Portal(真)").GetComponent<SendBall>().generateBallSpeed = 2;
            //場上的球速度要變2
        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject))) //得到所有hierarchy的物件
        {
            if (obj.CompareTag("黑球") || obj.CompareTag("金球") || obj.CompareTag("麥克風") || obj.CompareTag("報紙") || obj.CompareTag("炸彈"))
            {
                //obj.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 2);
                checkOriginalSpeed(obj, 2); //改變速度
                obj.GetComponent<BallMove>().blueEffectButton(); //將鎖住速度功能關閉
            }
        }
    }

    void checkOriginalSpeed(GameObject gObj, float speed) //判斷原本速度為何
    {
        float originSpeedX = gObj.GetComponent<Rigidbody2D>().velocity.x; //原本X速度
        float originSpeedY = gObj.GetComponent<Rigidbody2D>().velocity.y; //原本Y速度
        float changedSpeedX = speed; //更改後X速度
        float changedSpeedY = speed; //更改後Y速度

        if(originSpeedX > 0)
        {
            changedSpeedX = speed;
        }
        else if(originSpeedX < 0)
        {
            changedSpeedX = -speed;
        }

        if(originSpeedY > 0)
        {
            changedSpeedY = speed;
        }
        else if(originSpeedY < 0)
        {
            changedSpeedY = -speed;
        }
        gObj.GetComponent<Rigidbody2D>().velocity = new Vector2(changedSpeedX, changedSpeedY);
    }

    void showWinPartyMessage() //僅顯示3秒
    {
        if (winColor == 1) //綠
        {
            Invoke("greenTimeCountDown", 3);
        }
        else if (winColor == 2) //藍
        {
            Invoke("blueTimeCountDown", 3);
        }
        else if (winColor == 0) //無黨
        {
            Invoke("noColorTimeCountDown", 3);
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

    void noColorTimeCountDown() //顯示三秒便結束(無黨)
    {
        noColorNoticeBoard.SetActive(false);
    }

    void setWinPlayer() //設定執政黨
    {
        if (winColor == 1) //綠色執政
        {
            if (partyColor == "green") //如果是綠的，可使用執政黨能力
            {
                bombObj.SetActive(true); //可使用炸彈
                winLogo.SetActive(true); //有中選標誌
            }
            else if (partyColor == "blue") //藍的，關閉能力
            {
                bombObj.SetActive(false);
                forbiddenObj.SetActive(false);
                winLogo.SetActive(false);
            }
        }
        else if (winColor == 2) //藍色執政
        {
            if (partyColor == "green") //綠的，關閉能力
            {
                bombObj.SetActive(false);
                winLogo.SetActive(false);
                forbiddenObj.SetActive(false);
            }
            else if (partyColor == "blue") //藍的，可使用執政黨能力
            {
                bombObj.SetActive(true); //可使用炸彈
                winLogo.SetActive(true); //有中選標誌
            }
        }
        else if(winColor == 0) //無黨執政
        {
            bombObj.SetActive(false);
            winLogo.SetActive(false);
        }
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
