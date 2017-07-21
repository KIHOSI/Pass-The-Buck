using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

namespace Com.MyProject.MyPassTheBuckGame
{
	
	public class CreateRoom : Photon.PunBehaviour{


		#region Public Variables

		public string MaxPlayerChoosed; 
		public byte MaxPlayersPerRoom;
		public string GameRoomName;
		public int menuIndex;
		public List<Dropdown.OptionData> menuOptions;


		#endregion

		#region MonoBehaviour CallBacks


	   void Start ()
	   {

       }

		#endregion

		#region Public Methods

		//開房間(按下創建按鈕)
		public void CreateGameRoom()
		{

			GameRoomName = GameObject.Find ("GameRoomNameIp1").GetComponent<InputField> ().text;
			menuOptions= GameObject.Find ("PlayerNumberDd").GetComponent<Dropdown> ().options;

			menuIndex =GameObject.Find ("PlayerNumberDd").GetComponent<Dropdown> ().value;

			MaxPlayerChoosed = menuOptions [menuIndex].text;

			Debug.Log(MaxPlayerChoosed);

			if (MaxPlayerChoosed =="2")
			{
				MaxPlayersPerRoom = 2;
			}
			else if (MaxPlayerChoosed=="3")
			{
				MaxPlayersPerRoom = 3;
			}
			else if (MaxPlayerChoosed=="4")
			{
				MaxPlayersPerRoom = 4;
			}

			Debug.Log(MaxPlayersPerRoom);

			RoomOptions options = new RoomOptions ();
			options.MaxPlayers = MaxPlayersPerRoom;

			PhotonNetwork.CreateRoom(GameRoomName,options,null);

		}
			

		//按返回鍵
		public void Back()
		{
			SceneManager.LoadScene("Character Choosing");
		}

		#endregion

		#region Photon.PunBehaviour CallBacks

		public override void OnCreatedRoom()
		{
			Debug.Log("你已進入遊戲室!"+"遊戲室名稱為:"+PhotonNetwork.room.Name+"、最大遊玩人數:"+MaxPlayersPerRoom);
			SceneManager.LoadScene("Waiting Room");
		}

		public override void OnPhotonCreateRoomFailed (object[] codeAndMsg)
		{
			Debug.Log ("創立房間失敗");
		}

		public override void OnDisconnectedFromPhoton()
		{
			Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
			SceneManager.LoadScene("Main");
		}
			

		#endregion

	}
}
