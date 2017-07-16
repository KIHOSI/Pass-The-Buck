using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;


namespace Com.MyProject.MyPassTheBuckGame
{

     public class JoinRoom : Photon.PunBehaviour {


		#region Public Variables

		public string GameRoomName;

		#endregion

	    // Use this for initialization
	    void Start () 
	    {
	 	    
	    }
	


		#region Public Methods

		//加入房間(按下加入按鈕)
		public void JoinGameRoom()
		{
			GameRoomName = GameObject.Find ("GameRoomNameIp2").GetComponent<InputField>().text;
			Debug.Log(GameRoomName);
			PhotonNetwork.JoinRoom(GameRoomName);
		}

		//按返回鍵
		public void Back()
		{
			SceneManager.LoadScene("ChracterChoosingForJoin");
		}

		public void onTips(string tips_str)
		{
			GameObject parent = GameObject.Find ("Canvas");
			GameObject toast = GameObject.Find ("Toast"); // 加载预制体
			GameObject m_toast = GameObject.Instantiate(toast, parent.transform, false);  // 对象初始化
			//m_toast.transform.parent = parent.transform;            //　附加到父节点（需要显示的UI下）
			m_toast.transform.localScale = Vector3.one;
			m_toast.transform.localPosition = new Vector3 (3.3f, -234.3f, 0.0f);
			Text tips = m_toast.GetComponent<Text>();
			tips.text = tips_str;
			Destroy(m_toast, 3); // 2秒后 销毁
		}

		#endregion

		#region Photon.PunBehaviour CallBacks

		public override void OnJoinedRoom()
		{
			Debug.Log("你已進入\""+PhotonNetwork.room.Name+"\"房");
			SceneManager.LoadScene("Waiting Room");

		}

		public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
		{
			onTips("加入房間失敗，請重新輸入");
			Debug.Log("加入房間失敗，請重新輸入");
		}

		#endregion



     }

}
