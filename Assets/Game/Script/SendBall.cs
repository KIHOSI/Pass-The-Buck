using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SendBall : MonoBehaviour {
    public PhotonPlayer targetPlayer; //該Portal要傳送的目標玩家
    byte playerCode; //此player的code，用以判斷要不要接收訊息
    byte targetCode; //判斷要傳給誰
    public GameObject[] allArray; //全部
    string triggerName; //將要傳送的物件名稱
    bool reliable = true;
    int portalIndex; //判斷現在是左、上、右Portal，以便指定產生的球的位置；0:左、1:上、2:右

    // setup our OnEvent as callback:
    void Awake()
    {
        PhotonNetwork.OnEventCall += this.OnEvent;
    }

    //handle events:
    private void OnEvent(byte eventcode, object content, int senderid)
    {
        if(eventcode == playerCode) //如果是傳給自己的，就接收
        {
            PhotonPlayer sender = PhotonPlayer.Find(senderid); // who sent this?
            if(sender == targetPlayer) //如果剛好是要傳送的目標，才執行動作(達成1對1窗口)
            {
                string objName = (string)content; //得到物件名稱
                for (int i = 0; i < allArray.Length; i++) //判斷是建立哪個物件
                {
					if (allArray[i].name+"(Clone)" == objName) //因為是複製的球，名稱要+(clone)
					{
                        GameObject allGameObject;
                        switch (portalIndex) //判斷是左、上、右portal，已決定新的球產生速度
                        {
                            case 0: //左
                                allGameObject = Instantiate(allArray[i], transform.position + new Vector3(1, 0, 0), new Quaternion(0, 0, 0, 0)); //產生球，並對球指定速度；position+x橫向位置，是為了讓球傳送完不馬上觸發onTriggerEnter2D
                                allGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(2, -2);
                                break;
                            case 1: //上
                                allGameObject = Instantiate(allArray[i], transform.position + new Vector3(0, -1, 0), new Quaternion(0, 0, 0, 0)); //產生球，並對球指定速度；position+x橫向位置，是為了讓球傳送完不馬上觸發onTriggerEnter2D
                                allGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -2);
                                break;
                            case 2: //右
                                allGameObject = Instantiate(allArray[i], transform.position + new Vector3(-1, 0, 0), new Quaternion(0, 0, 0, 0)); //產生球，並對球指定速度；position+x橫向位置，是為了讓球傳送完不馬上觸發onTriggerEnter2D
                                allGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, -2);
                                break;
                        }
                    }
                }
            }
       }
    }
    void OnTriggerEnter2D(Collider2D collision) //觸發portal，傳送球
    {
		triggerName = collision.name;
        PhotonNetwork.RaiseEvent(targetCode, triggerName, reliable, null); //使用RaiseEvent傳送，不需要PhotonView
        
    }

    public void setTarget(PhotonPlayer player,int index,byte pCode,int pIndex) //設定目標玩家和其陣列位置，以判斷傳送；第三為設定此玩家的playerCode，用以判斷是不是回傳給自己；最後告訴這個portal為左、上、右
    {
        targetPlayer = player;
        targetCode = (byte)index;
        playerCode = pCode;
        portalIndex = pIndex;
    }

}
