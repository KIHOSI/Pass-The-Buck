﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;


namespace Com.MyProject.MyPassTheBuckGame
{
	public class GameManager : Photon.PunBehaviour
	{

		public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;
		public GameObject audio;
		public Text WelcomeTx;
		public Hashtable hash;

		//設定遊戲版本
		string _gameVersion = "1";

		void Awake()
		{
			// we don't join the lobby. There is no need to join a lobby to get the list of rooms.
			PhotonNetwork.autoJoinLobby = false;

			// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
			PhotonNetwork.automaticallySyncScene = true;

			// Force LogLevel
			PhotonNetwork.logLevel = Loglevel;
		}
			
		void Start()
		{
			PhotonNetwork.player.NickName = PlayerPrefs.GetString ("PlayerName");

			hash = new Hashtable();
			hash.Add("PartyColor", null);
			hash.Add ("Role",null);
			PhotonNetwork.player.SetCustomProperties(hash); 

			DontDestroyOnLoad(audio);
		}
			

		#region Public Methods


		//開始連線
		public void Connect(Button pt)
		{

			Debug.Log("system try to connect!");


			//檢查是否已連線，是:轉換到開房間創房間頁面；否:與伺服器建立連線
			if (PhotonNetwork.connected)
			{
				SceneManager.LoadScene("Create&Join Room");  //載入開房間創房間頁面
			}
			else
			{

				//連線到server
				pt.GetComponent<Image> ().color = new Color32(255,255,225,0);
				pt.GetComponentInChildren<Text>().text = "與伺服器連線中，請稍後...";
				pt.GetComponentInChildren<Text> ().fontSize = 25;
				PhotonNetwork.ConnectUsingSettings(_gameVersion); 
			}
		}

		//載入開場故事
		public void LoadOpeningStoryScene()
		{
		     SceneManager.LoadScene ("Opening Story");
		}

		//載入開場故事
		public void LoadOpeningStoryForRepeatScene()
		{
			SceneManager.LoadScene ("OpeningStoryForRepeat");
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

		//載入選模式頁面
		public void LoadSetModeScene()
		{
			SceneManager.LoadScene ("SetMode");
		}

		public void LoadJoinRoomScene()
		{
			SceneManager.LoadScene ("Join Room");
		}
			

		//載入角色介紹頁面
		public void LoadRoleIntroScene()
		{
			SceneManager.LoadScene("RoleIntro");
		}

		//載入故事頁面
		public void LoadStoryScene()
		{
			SceneManager.LoadScene("Story");
		}

		//載入背包頁面
		public void LoadBagScene()
		{
			SceneManager.LoadScene("Bag");
		}

		//載入遊戲說明頁面
		public void LoadGameIntroScene()
		{
			SceneManager.LoadScene("GameIntro");
		}

		public void onTips(string tips_str)
		{
			GameObject parent = GameObject.Find ("Canvas");
			GameObject toast = GameObject.Find ("Toast"); // 加载预制体
			GameObject m_toast = GameObject.Instantiate(toast, parent.transform, false);  // 对象初始化
			//m_toast.transform.parent = parent.transform;            //　附加到父节点（需要显示的UI下）
			//m_toast.transform.localScale = Vector3.one;
			//m_toast.transform.localPosition = new Vector3 (15.5f, -113.0f, 0.0f);
			Text tips = m_toast.GetComponent<Text>();
			tips.text = tips_str;
			Destroy(m_toast, 3); // 2秒后 销毁
		}


		#endregion

		#region Photon.PunBehaviour CallBacks

		//成功與master server連線
		public override void OnConnectedToMaster()
		{

			Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
			SceneManager.LoadScene("Create&Join Room");

		}
			
		//與伺服器失去連接
		public override void OnDisconnectedFromPhoton()
		{
			Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
			GameObject.Find ("PlayBt").GetComponent<Image> ().color = Color.white;
			GameObject.Find ("PlayBt").GetComponentInChildren<Text>().text = "開始遊戲";
			GameObject.Find ("PlayBt").GetComponentInChildren<Text> ().fontSize = 20;
			GameObject.Find ("WelcomeTx").GetComponentInChildren<Text> ().text = "請確認是否連線喔!";
		}


		#endregion

	}
}
