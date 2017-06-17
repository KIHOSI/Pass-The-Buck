using UnityEngine;
using UnityEngine.SceneManagement; 


namespace Com.MyProject.MyPassTheBuckGame
{
	public class Launcher : Photon.PunBehaviour
	{


		#region Public Variables


		/// The PUN loglevel
		public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;

		// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created. 
		public byte MaxPlayersPerRoom = 6;

		public GameObject PlayButton;
		public GameObject ProcessingTx;


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


		/// Start the connection process. 
		/// if not yet connected, Connect this application instance to Photon Cloud Network
		public void Connect()
		{

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

			public override void OnJoinedRoom()
			{
				Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
			}

            */

		}


		public override void OnDisconnectedFromPhoton()
		{

			PlayButton.SetActive(true);
			Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");        
		}


		#endregion

	}
}