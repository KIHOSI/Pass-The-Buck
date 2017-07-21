using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SendBall : MonoBehaviour {
    public PhotonPlayer targetPlayer; //該Portal要傳送的目標玩家
    byte playerCode; //此player的code，用以判斷要不要接收訊息
    byte targetCode; //判斷要傳給誰
    //GameObject[] ballArray; //球
    //GameObject[] itemArray; //道具
    public GameObject[] allArray; //全部
    string triggerName; //將要傳送的物件名稱
    //PhotonView photonView;
    bool reliable = true;

	// Use this for initialization
	void Start () {
        //ballArray = GameObject.Find("左邊框").GetComponent<GenerateBall>().BallArray; //取得所有球的列表
        //itemArray = GameObject.Find("左邊框").GetComponent<GenerateBall>().ItemArray; //取得道具列表
        //allArray = ballArray.Concat(itemArray).ToArray(); // 合併陣列
        //photonView = PhotonView.Get(this); //得到此photonView
        //playerCode = GetComponent<NowState>().playerCode; //得到playercode
        //Instantiate(ballArray[0], Vector2.zero, Quaternion.identity); //生成一顆炸彈(從中間生出)
    }
	
	// Update is called once per frame
	void Update () {
		
        //If your script is a Photon.MonoBehaviour or Photon.PunBehaviour you can use: this.photonView.RPC().
        //this.photonView.RPC("ChatMessage", PhotonTargets.All, "jup", "and jup!");
	}

    // setup our OnEvent as callback:
    void Awake()
    {
        PhotonNetwork.OnEventCall += this.OnEvent;
    }

    //handle events:
    private void OnEvent(byte eventcode, object content, int senderid)
    {
		Debug.Log ("eventcode:"+eventcode);
		Debug.Log ("content:"+(string)content);
		Debug.Log ("senderid:"+senderid);

        if(eventcode == playerCode) //如果是傳給自己的，就接收
        {
            PhotonPlayer sender = PhotonPlayer.Find(senderid); // who sent this?
			Debug.Log ("sender:"+sender.NickName);
            if(sender == targetPlayer) //如果剛好是要傳送的目標，才執行動作(達成1對1窗口)
            {
				Debug.Log ("ㄤㄤ");
                string objName = (string)content; //得到物件名稱
				Debug.Log("objName:"+objName);
				//Instantiate(allArray, Vector2.zero, new Quaternion(0, 0, 0, 0));
                for (int i = 0; i < allArray.Length; i++) //判斷是建立哪個物件
                {
					Debug.Log ("allArray:"+allArray[i].name);
					if (allArray[i].name+"(Clone)" == objName)
					//if(allArray[i].name.Equals(objName))
					{
						Debug.Log("allArray[i] name:"+allArray[i].name);
                        Instantiate(allArray[i], transform.position, new Quaternion(0, 0, 0, 0));
						//Instantiate(allArray[i], Vector2.zero, new Quaternion(0, 0, 0, 0));

                    }
                }
            }
        }
    }
    /*public void setTargetPlayer(PhotonPlayer tPlayer)
    {
        targetPlayer = tPlayer;
    }*/

    void OnTriggerEnter2D(Collider2D collision) //觸發portal，傳送球
    {
        //PhotonView.RPC("SetBall", targetPlayer,transform.position,transform.eulerAngles);
		Debug.Log("我有進來");
		triggerName = collision.name;
		Debug.Log ("triggerName:"+triggerName);
        PhotonNetwork.RaiseEvent(targetCode, triggerName, reliable, null); //使用RaiseEvent傳送，不需要PhotonView
    }

    public void setTarget(PhotonPlayer player,int index,byte pCode) //設定目標玩家和其陣列位置，以判斷傳送；最後為設定此玩家的playerCode，用以判斷是不是回傳給自己
    {
        targetPlayer = player;
        targetCode = (byte)index;
        playerCode = pCode;
		Debug.Log("targetPlayer:"+player);
		Debug.Log ("targetCode:"+index);
		Debug.Log ("playerCode:"+playerCode);
    }

    /*//加⼊⼀個RPC⽅法，以⽤作傳值及接收⽤
    [PunRPC]
    void SetBall(string sendName)
    {
        objName = sendName;
    }*/


    /*//加⼊OnPhotonSerializeView⽅法，利⽤SendNext傳輸資料，⽤ReceiveNext接收資料，傳輸及接收需對應
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //stream.SendNext(objName);
        }
        else
        {

        }
    }*/
}
