using System.Collections;
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
    public Hashtable hash; //宣告HashTable變數
    public int playerMoney; //宣告要新增的變數名稱:
    public Text postText; //告示牌

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


    Image tt;

    // Use this for initialization
    void Start() {
        //取得玩家list(同樣順序)
        PlayerList = new List<PhotonPlayer>();
        PlayerList.Add(PhotonNetwork.masterClient); //1
        PlayerList.Add(PhotonNetwork.masterClient.GetNext()); //2
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext()); //3
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext().GetNext()); //4

        hash = new Hashtable();
        playerMoney = money;
        hash.Add("Money", playerMoney); //把變數存進剛剛宣告的hash裡
        PhotonNetwork.player.SetCustomProperties(hash);

        player = PhotonNetwork.player; //取得現在的player
        playerName = PhotonNetwork.playerName; //取得現在的player的暱稱
        partyColor = (string)PhotonNetwork.player.CustomProperties["PartyColor"]; //政黨顏色
        role = (string)PhotonNetwork.player.CustomProperties["Role"]; //政黨角色

        //根據角色換角色圖片
        setRoleImg(PlayerCharacterImg,role);


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
        moneyText.text = "金錢 x" + money;
        //bombObj.SetActive(false); //一開始炸彈不顯示

        //判斷是哪個player
        if (player == PlayerList[0]) //Player1
        {
            playerCode = (byte)0; //setTarget，1.傳送的目標，2.目標code，3.玩家自己的playerCode
            portalLeft.GetComponent<SendBall>().setTarget(PlayerList[1], 1, playerCode, 0); //Player2放左
            portalUp.GetComponent<SendBall>().setTarget(PlayerList[2], 2, playerCode, 1); //Player3放上
            portalRight.GetComponent<SendBall>().setTarget(PlayerList[3], 3, playerCode, 2); //Player4放右

            //setRoleImg(, (string)PlayerList[1].CustomProperties["role"]); //Paper第一個人物圖片

            //setRoleImg(paperPersonMenu[0].GetComponent<Image>(), (string)PlayerList[1].CustomProperties["Role"]); //Paper第一個人物圖片
            //setRoleImg(paperPersonMenu[0].GetComponent<Image>(), "蔡中聞"); //Paper第一個人物圖片

            setRoleImg(paperPersonMenu[0].GetComponent<Image>(), (string)PlayerList[1].CustomProperties["Role"]); //Paper第一個人物圖片
            setRoleImg(paperPersonMenu[1].GetComponent<Image>(), (string)PlayerList[2].CustomProperties["Role"]); //Paper第二個人物圖片
            setRoleImg(paperPersonMenu[2].GetComponent<Image>(), (string)PlayerList[3].CustomProperties["Role"]); //Paper第三個人物圖片

            //paperPersonImage[0].sprite = role1;           
            //Debug.Log("paperPersonImgae:"+paperPersonImage.Length);
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
        InvokeRepeating("timeCountDown", 1, 1); //每隔一秒執行一次 
        //}
        //else //其他client純顯示
        //{
        //    timeText.text = "" + time;
        //}
        

        
                                  
    }
	
	// Update is called once per frame
	void Update () {

        if (time == 0) //如果時間歸零，就停止減少
        {
            CancelInvoke("timeCountDown");
            timeText.text = "Game Over";
            Time.timeScale = 0f; //時間暫停
            
        }
    }

    void OnTriggerEnter2D(Collider2D collision) //進洞
    {
        /*//取得玩家list(同樣順序)
        PlayerList = new List<PhotonPlayer>();
        PlayerList.Add(PhotonNetwork.masterClient); //1
        PlayerList.Add(PhotonNetwork.masterClient.GetNext()); //2
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext()); //3
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext().GetNext()); //4*/

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
        if (collision.gameObject.CompareTag("金球"))
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
            if (money > 0)
            {
                money = money / 2;
                moneyText.text = "金錢 x" + money;
            }
        }
        if (collision.gameObject.CompareTag("報紙")) //報紙效果:出現選單可以陷害人，指定敵對黨某人有誹聞(意涵:爆料)，被指定者扣錢20%
        {
            //先顯示報紙選單
            paperMenu.SetActive(true);
            Debug.Log("報紙裡");
            //更改報紙上按鈕的照片
            //paperPersonMenu[0].GetComponent<Image>().sprite = role1;

            //paperPersonMenu[0].GetComponent<Image>().sprite = paperPersonImage[0].sprite;
            //paperPersonMenu[1].GetComponent<Image>().sprite = paperPersonImage[1].sprite;
            //paperPersonMenu[2].GetComponent<Image>().sprite = paperPersonImage[2].sprite;
            
            
            //paper.SetActive(true);
            //GameObject.Find("人物圖片").GetComponent<ChangePaperPersonImg>().ChangePersonImg3();
            //GameObject.Find("報紙選單").SetActive(false);

        }
        if (collision.gameObject.CompareTag("麥克風"))
        {

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

    void setRoleImg(Image personImg,string person) //根據player選的人物，給予相應的照片
    {
        Debug.Log("setRoleImg");
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

    public void sendPaperMessage(int pIndex) //點選按鈕才可以傳送開啟報紙的資訊
    {
        //playerIndex = pIndex; //設定圖片為誰，0:player1、1:player2、2:player3
        PlayerList = new List<PhotonPlayer>();
        PlayerList.Add(PhotonNetwork.masterClient); //1
        PlayerList.Add(PhotonNetwork.masterClient.GetNext()); //2
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext()); //3
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext().GetNext()); //4

        PhotonView photonView = PhotonView.Get(this);
        Debug.Log("pIndex:"+pIndex);
        switch (pIndex) //判斷是改哪個玩家的圖片
        {
            case 0: //第一個人
                photonView.RPC("SetPaperOn1", PhotonTargets.Others);
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
                photonView.RPC("SetPaperOn2", PhotonTargets.Others);
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
                Debug.Log("剛進來");
                photonView.RPC("SetPaperOn3", PhotonTargets.Others);
                if (player == PlayerList[0]) //Player1
                {
                    Debug.Log("imhere");
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

    //傳遞報紙訊息
    [PunRPC]
    void SetPaperOn1() //Player1
    {
        paper.SetActive(true); //開啟報紙
        GameObject.Find("人物圖片").GetComponent<ChangePaperPersonImg>().ChangePersonImg1(paperPersonImage[0]);
    }

    [PunRPC]
    void SetPaperOn2() //Player2
    {
        paper.SetActive(true); //開啟報紙
        GameObject.Find("人物圖片").GetComponent<ChangePaperPersonImg>().ChangePersonImg2(paperPersonImage[1]);
    }

    [PunRPC]
    void SetPaperOn3() //Player3
    {
        paper.SetActive(true); //開啟報紙
        Debug.Log("SetPapeprOn3");
        GameObject.Find("人物圖片").GetComponent<ChangePaperPersonImg>().ChangePersonImg3(paperPersonImage[2]);
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
                moneyText.text = "金錢 x" + money;
            }
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
                moneyText.text = "金錢 x" + money;
            }
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
                moneyText.text = "金錢 x" + money;
            }
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
                moneyText.text = "金錢 x" + money;
            }
        }
    }
}
