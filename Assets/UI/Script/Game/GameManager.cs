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
		public GameObject audio;

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
			DontDestroyOnLoad(audio);
		}




		#endregion


		#region Public Methods


		//開始連線
		public void Connect(Button pt)
		{

			Debug.Log("system try to connect!");


			//檢查是否已連線，是:轉換到開房間創房間頁面；否:與伺服器建立連線
			if (PhotonNetwork.connected)
			{
				SceneManager.LoadScene("Create&Join Room");  //載入開房間創房間頁面
			}else{

				//連線到server
				pt.GetComponent<Image> ().color = new Color32(255,255,225,0);
				pt.GetComponentInChildren<Text>().text = "與伺服器連線中，請稍後...";
				pt.GetComponentInChildren<Text> ().fontSize = 25;
				PhotonNetwork.ConnectUsingSettings(_gameVersion); 
			}
		}

		//載入主頁
		public void LoadJMainPageScene()
		{
			SceneManager.LoadScene("Main");
		}

		//載入加入、創建房間頁面
		public void LoadJCreateJoinScene()
		{
			SceneManager.LoadScene("Create&Join Room");
		}

		//載入選地圖頁面
		public void LoadMapChoosingScene()
		{
			SceneManager.LoadScene ("Map Choosing");
		}

		//載入角色選擇頁面
		public void LoadCharacterChoosingScene()
		{
			SceneManager.LoadScene ("Character Choosing");
		}

		//載入角色選擇頁面
		public void LoadCharacterChoosingforJoinScene()
		{
			SceneManager.LoadScene ("ChracterChoosingForJoin");
		}

		//載入個人背包頁面
		public void LoadBagScene()
		{
			SceneManager.LoadScene("Bag");
		}

		//載入故事頁面
		public void LoadStoryScene()
		{
			SceneManager.LoadScene("Story");
		}

		//載入設定頁面
		public void LoadSettingScene()
		{
			SceneManager.LoadScene("Setting");
		}


		#endregion

		#region Photon.PunBehaviour CallBacks

		//成功與master server連線
		public override void OnConnectedToMaster()
		{

			Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
			SceneManager.LoadScene("Create&Join Room");

		}
			
		public override void OnDisconnectedFromPhoton()
		{
			Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
			SceneManager.LoadScene("Main");
		}


		#endregion

	}
}
