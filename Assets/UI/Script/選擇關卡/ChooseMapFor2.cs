using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;
using System;

namespace Com.MyProject.MyPassTheBuckGame
{
	public class ChooseMapFor2 : Photon.PunBehaviour {

	  static string playerMapPrefKey = "PlayerMap";
	  static string playerModePrefKey = "PlayerMode";

	  int n;
	  string map;
	  string MaxPlayerChoosed; 
	  byte MaxPlayersPerRoom;
	  string GameRoomName;

	  public Sprite Building1;
	  public Sprite Building1WithLock;
	  public Sprite Building2;
	  public Sprite Building2WithLock;
	  public Sprite Building3;
	  public Sprite Building3WithLock;
	  public Sprite Building4;
	  public Sprite Building4WithLock;
	  public Sprite Building5;
	  public Sprite Building5WithLock;

	  public Button Building1Bt;
	  public Button Building2Bt;
	  public Button Building3Bt;
	  public Button Building4Bt; 
	  public Button Building5Bt;
	  public Button ChooseBt;
	  public Text BuildingDescriptTx;
	  
	  string Building1Tx = "第一個關卡尚未解鎖\n敬請期待!";
	  string Building2Tx = "第二個關卡尚未解鎖\n敬請期待!";
	  string Building3Tx = "經典中的經典\n一起體會平時開議會的氛圍!";
      string Building4Tx = "第四個關卡尚未解鎖\n敬請期待!";
	  string Building5Tx = "第五個關卡尚未解鎖\n敬請期待!";

	  void Start () 
	  {

		 //會有資料庫存玩家已解鎖的島嶼名稱

		 //一開始GUI全部島嶼都會在畫面上
		 //剛載入地圖選擇頁面時，系統從資料庫讀取玩家的解鎖島嶼名稱
		 //將解鎖島嶼的button圖案換成無解鎖圖
	  }

	 #region Public Methods

		//點擊關卡
		public void ClickBuilding(Button bt)
		{
			Sprite buttonImg = bt.GetComponent<Image> ().sprite;
			BuildingDescriptTx = GameObject.Find ("BuildingDescriptTx-2").GetComponent<Text> ();
			ChooseBt = GameObject.Find ("ChooseBt-2").GetComponent<Button> ();


			if (buttonImg.Equals (Building1)) 
			{

			}
			else if (buttonImg.Equals (Building1WithLock)) 
			{
				ChooseBt.interactable = false;
				BuildingDescriptTx.text = Building1Tx;
			}
			else if (buttonImg.Equals (Building2)) 
			{
				
			}
			else if (buttonImg.Equals (Building2WithLock)) 
			{
				ChooseBt.interactable = false;
				BuildingDescriptTx.text = Building2Tx;
			}
			else if (buttonImg.Equals (Building3))
			{
				ChooseBt.interactable = true;
				BuildingDescriptTx.text = Building3Tx;
			}
			else if (buttonImg.Equals (Building4))
			{

			}
			else if (buttonImg.Equals (Building4WithLock)) 
			{
				ChooseBt.interactable = false;
				BuildingDescriptTx.text = Building4Tx;
			}
			else if (buttonImg.Equals (Building5)) 
			{

			}
			else if (buttonImg.Equals (Building5WithLock)) 
			{
				ChooseBt.interactable = false;
				BuildingDescriptTx.text = Building5Tx;
			}
		}

	  	public void ClickChoose()
		{
			BuildingDescriptTx = GameObject.Find ("BuildingDescriptTx-2").GetComponent<Text> ();

			if(BuildingDescriptTx.text.Equals(Building3Tx))
			{
				map = "Classic";
				PlayerPrefs.SetString(playerMapPrefKey,map);
				CreateRoom ();
			}

		}
	 


	  public void CreateRoom()
	  {
		  System.Random rnd = new System.Random();
		  n = rnd.Next(100, 1000);
		  GameRoomName = n.ToString();
		  MaxPlayerChoosed = PlayerPrefs.GetString (playerModePrefKey);

		  if (MaxPlayerChoosed =="2")
		  {
			 MaxPlayersPerRoom = 2;
		  }
		  else if (MaxPlayerChoosed=="4")
		  {
			 MaxPlayersPerRoom = 4;
		  }

		  RoomOptions options = new RoomOptions ();
		  options.MaxPlayers = MaxPlayersPerRoom;

		  PhotonNetwork.CreateRoom(GameRoomName,options,null);

	  }

	//按返回鍵
	public void Back()
	{
		SceneManager.LoadScene("SetMode");
	}
			



	public void onTips(string tips_str)
	{
		GameObject parent = GameObject.Find ("MapChoosePanel");
		GameObject toast = GameObject.Find ("Toast"); // 加载预制体
		GameObject m_toast = GameObject.Instantiate(toast, parent.transform, false);  // 对象初始化
		//m_toast.transform.parent = parent.transform;            //　附加到父节点（需要显示的UI下）
		m_toast.transform.localScale = Vector3.one;
		m_toast.transform.localPosition = new Vector3 (3.3f, -257.0f, 0.0f);
		Text tips = m_toast.GetComponent<Text>();
		tips.text = tips_str;
		Destroy(m_toast, 2); // 2秒后 销毁
	}
			
			
	#endregion

	public override void OnCreatedRoom()
	{
		SceneManager.LoadScene("Waiting Room");
	}

	public override void OnPhotonCreateRoomFailed (object[] codeAndMsg)
	{
		onTips ("開房間失敗!");
	}

    public override void OnDisconnectedFromPhoton()
	{
		Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
		SceneManager.LoadScene("Main");
	}


  }
}
