using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;


namespace Com.MyProject.MyPassTheBuckGame
{
	public class GameManager : Photon.PunBehaviour
	{


		#region Public Variables


		/// The PUN loglevel
		public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;

		// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created. 
		public int MaxPlayersPerRoom;
		public string GameRoomName;

		public GameObject PlayButton;
		public GameObject ProcessingTx;
		//public GameObject GameRoomNameIp;
		//public GameObject PlayerNumberDd;


		#endregion


		#region Private Variables


		/// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
		string _gameVersion = "1";


		#endregion


		#region MonoBehaviour CallBacks


		// MonoBehaviour method called on GameObject by Unity during early initialization phase.
		void Awake()
		{
			// we don't join the lobby. There is no need to join a lobby to get the list of rooms.
			PhotonNetwork.autoJoinLobby = false;

			// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
			PhotonNetwork.automaticallySyncScene = true;

			// Force LogLevel
			PhotonNetwork.logLevel = Loglevel;
		}


		// MonoBehaviour method called on GameObject by Unity during initialization phase.
		void Start()
		{
			PlayButton.SetActive(true);
			ProcessingTx.SetActive(false);
		}


		#endregion


		#region Public Methods


		//開始連線
		public void Connect()
		{

			Debug.Log("system try to connect!");
			PlayButton.SetActive(false);
			ProcessingTx.SetActive(true);

			// we check if we are connected or not, we join if we are , else we initiate the connection to the server.
			if (PhotonNetwork.connected)
			{
				SceneManager.LoadScene(1);
				ProcessingTx.SetActive(false);
				//PhotonNetwork.JoinRandomRoom();       //要改
			}else{

				//連線到server
				PhotonNetwork.ConnectUsingSettings(_gameVersion); 
			}
		}

		//載入開房間頁面
		public void LoadCreateRoomScene()
		{
			SceneManager.LoadScene(2);
		}

		//載入加入房間頁面
		public void LoadJoinRoomScene()
		{
			SceneManager.LoadScene(3);
		}

		public void CreateGameRoom()
		{
			
			GameRoomName = GameObject.Find("GameRoomNameIp").GetComponent<InputField>().text;
			MaxPlayersPerRoom = Encoding.Default.GetBytes(GameObject.Find("PlayerNumberDd").GetComponent<Dropdown>().itemText.text);

			RoomOptions options = new RoomOptions ();
			options.MaxPlayers = MaxPlayersPerRoom;

			PhotonNetwork.CreateRoom(GameRoomName,options,null);

		}


		//離開房間
		public void LeaveRoom()
		{
			PhotonNetwork.LeaveRoom();
		}


		#endregion

		#region Photon.PunBehaviour CallBacks


		public override void OnConnectedToMaster()
		{


			Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
			SceneManager.LoadScene(1);
			ProcessingTx.SetActive(false);


			//PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);

			//Debug.Log("DemoAnimator/Launcher: Create Room Successfully");

			/*
			public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
			{
				Debug.Log("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);"); 

				// #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
				PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
			}

            */

		}


		public override void OnDisconnectedFromPhoton()
		{

			PlayButton.SetActive(true);
			Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");        
		}

		public override void OnJoinedRoom()
		{
			Debug.Log("你已進入遊戲室!"+"遊戲室名稱為:"+GameRoomName+"、最大遊玩人數:"+MaxPlayersPerRoom);
		}

		public void OnLeftRoom()
		{
			SceneManager.LoadScene(0);
		}


		#endregion

	}
}
