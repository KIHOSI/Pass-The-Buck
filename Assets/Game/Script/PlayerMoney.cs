using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour{
    public int[] playerMoney;
    List<PhotonPlayer> PlayerList; //儲存玩家(要照順序)

    // Use this for initialization
    void Start () {
		for(int i = 0; i< playerMoney.Length; i++)
        {
            playerMoney[i] = 100; //預設每個玩家都100(百萬)
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void storePlayerMoney(PhotonPlayer player,int money)
    {
        //取得玩家list(同樣順序)
        PlayerList = new List<PhotonPlayer>();
        PlayerList.Add(PhotonNetwork.masterClient); //1
        PlayerList.Add(PhotonNetwork.masterClient.GetNext()); //2
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext()); //3
        PlayerList.Add(PhotonNetwork.masterClient.GetNext().GetNext().GetNext()); //4

        for(int i = 0; i < PlayerList.Count;i++)
        {
            if(player == PlayerList[i]) //如果是該Player，就將錢存到對應位置
            {
                playerMoney[i] = money;
            }
        }

    }
}
