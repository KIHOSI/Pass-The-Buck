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

		#endregion


		#region Private Variables


		//設定遊戲版本
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
		}




		#endregion


		#region Public Methods


		//開始連線
		public void Connect()
		{

			Debug.Log("system try to connect!");


			//檢查是否已連線，是:轉換到開房間創房間頁面；否:與伺服器建立連線
			if (PhotonNetwork.connected)
			{
				SceneManager.LoadScene(2);  //載入開房間創房間頁面
			}else{

				//連線到server
				PhotonNetwork.ConnectUsingSettings(_gameVersion); 
			}
		}

		//載入主頁
		public void LoadJMainPageScene()
		{
			SceneManager.LoadScene(1);
		}

		//載入開房間頁面
		public void LoadCreateRoomScene()
		{
			SceneManager.LoadScene(3);
		}

		//載入加入房間頁面
		public void LoadJoinRoomScene()
		{
			SceneManager.LoadScene(4);
		}
			

		//離開房間
		public void LeaveRoom()
		{
			PhotonNetwork.LeaveRoom();
		}


		#endregion

		#region Photon.PunBehaviour CallBacks

		//成功與master server連
		public override void OnConnectedToMaster()
		{

			Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
			SceneManager.LoadScene(2);

		}
			
		public override void OnDisconnectedFromPhoton()
		{
			Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");        
		}
			
			

		public override void OnLeftRoom()
		{
			SceneManager.LoadScene(1);
		}


		#endregion

	}
}
