using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateBomb : MonoBehaviour {
    public GameObject Bomb; //要生成的炸彈
    public GameObject Forbidden; //禁止的符號
    public GameObject useBombMusic; //使用炸彈音樂
    public GameObject canUseBombMusic; //可使用炸彈的音效
    public GameObject powerCutPanel; //造成停電效果
    PhotonPlayer player; //玩家
    string partyColor; //該玩家政黨顏色
    PhotonView photonView;

    // Use this for initialization
    void Start() {
        photonView = PhotonView.Get(this);
        player = PhotonNetwork.player;
        partyColor = (string)PhotonNetwork.player.CustomProperties["PartyColor"]; //政黨顏色
        //photonView.RPC("sendPowerCutEffect", PhotonTargets.All); //第三個參數:傳送要顯示的話
        GetComponent<Button>().onClick.AddListener(ClickEvent); //點選按鈕，啟動動作
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ClickEvent()
    {
        //判斷是什麼模式，經典or停電
        photonView.RPC("sendPowerCutEffect", PhotonTargets.All,partyColor); //第三個參數:傳送要顯示的話



        GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(useBombMusic);
        Instantiate(Bomb, Vector2.zero, Quaternion.identity); //生成一顆炸彈(從中間生出)
        //如果按一次的話，要等15秒才能再次使用炸彈
        Forbidden.SetActive(true); 
        Invoke("hideForbidden", 15); 
    }

    void hideForbidden()
    {
        Forbidden.SetActive(false); //把禁止隱藏，可再次使用炸彈
        GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(canUseBombMusic);
    }

    [PunRPC]
    void sendPowerCutEffect(string getColor) //傳送停電效果
    {
        Debug.Log("getColor");
        if (partyColor != getColor) //如果是不同政黨，才要被停電
        {
            powerCutPanel.SetActive(true);
            Invoke("closePowerCutEffect", 5); //一秒後關閉
        }
    }

    void closePowerCutEffect() //關閉停電效果
    {
        powerCutPanel.SetActive(false);
    }
    
}
