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
			SceneManager.LoadScene(6);
		}

		#endregion

		#region Photon.PunBehaviour CallBacks

		public override void OnJoinedRoom()
		{
			Debug.Log("你已進入\""+PhotonNetwork.room.Name+"\"房");
			SceneManager.LoadScene(8);
		}

		public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
		{
			Debug.Log("加入房間失敗，請重新輸入");
		}

		#endregion

     }

}
