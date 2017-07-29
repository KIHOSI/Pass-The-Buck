using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.SceneManagement; 

namespace Com.MyProject.MyPassTheBuckGame
{
	public class WaitingRoom :Photon.PunBehaviour {

		public Sprite role1;
		public Sprite role2;
		public Sprite role3;
		public Sprite FrameImg;

		public Text PlayerTx1;
	    public Text PlayerTx2;
		public Text PlayerTx3;
		public Text PlayerTx4;
		public Image Frame1Img;
		public Image Frame2Img;
		public Image Frame3Img;
		public Image Frame4Img;
		public Button StartBt;
		public Button LeaveBt;
		public Hashtable hash;
		public string ClickStart;

		public string RoomName;
		public List<Text> PlayerTextList;
		public List<Image> FrameImgList;

		public void Start () 
		{

			PlayerTextList = new List<Text>(){PlayerTx1,PlayerTx2,PlayerTx3,PlayerTx4};
			FrameImgList = new List<Image>(){Frame1Img,Frame2Img,Frame3Img,Frame4Img};

			if (!PhotonNetwork.isMasterClient) 
			{
				StartBt.GetComponent<Image> ().color = new Color32 (255, 255, 225, 0);
				StartBt.GetComponentInChildren<Text>().text = "";
				StartBt.GetComponent<Button> ().interactable = false;
				LeaveBt.transform.localPosition = new Vector3 (3.3f, -234.3f, 0.0f);
			} 
			else 
			{
				StartBt.GetComponent<Button> ().interactable = false;
			}

			//取得房名
			RoomName = PhotonNetwork.room.Name;
			GameObject.Find ("RoomNameTx").GetComponent<Text> ().text="房名:"+RoomName;

			//更新玩家名單
			UpdatePlayerList (PlayerTextList,FrameImgList);

		}
			
			

		#region Public Methods

		//更新玩家名單
		public void UpdatePlayerList (List<Text> PTL,List<Image> FIL)
		{
			
			int j = 0;
			for (int i = 0; i < PhotonNetwork.playerList.Length; i++) 
			{

				if (PhotonNetwork.playerList [i].IsMasterClient) {
					PTL [i].text = PhotonNetwork.playerList [i].NickName + "(房主)";
					FIL [i].color = new Color32(255,255,225,255);
					FIL [i].GetComponent<Image> ().sprite = FrameImg;
				} else {
					PTL [i].text = PhotonNetwork.playerList [i].NickName;
					FIL [i].color = new Color32(255,255,225,255);
					FIL [i].GetComponent<Image> ().sprite = FrameImg;
				}

				j++;

			}

			for ( int g=j ; g < PTL.Count; g++) 
			{

				PTL [g].text = "";
				FIL [g].color = new Color32(255,255,225,0);

			}
			
		}

		//檢查房間人數是否已滿。滿的話將開始按鈕設成可以按開始；反之不行
		public void CheckRoomFull ()
		{
			if (PhotonNetwork.isMasterClient) 
			{
				if (PhotonNetwork.room.PlayerCount == PhotonNetwork.room.MaxPlayers)
				{
					StartBt.GetComponent<Button> ().interactable = true;
				} 
				else
				{
					StartBt.GetComponent<Button> ().interactable = false;
				}
			}
		} 
			
		//開始遊戲
		public void StartGame()
		{
			hash = new Hashtable();
			ClickStart = "true";
			hash.Add("ClickStart", ClickStart);
			PhotonNetwork.player.SetCustomProperties(hash);

			if (PhotonNetwork.room.MaxPlayers == 2)
			{
				if (PhotonNetwork.isMasterClient) {
					PhotonNetwork.LoadLevel ("Role Choose for 2");
				}
			}
			else if (PhotonNetwork.room.MaxPlayers == 4)
			{
				if (PhotonNetwork.isMasterClient) {
					PhotonNetwork.LoadLevel ("Role Choosing for 4");
				}
			}
		}

		//離開房間
		public void Leave()
		{
			PhotonNetwork.LeaveRoom ();
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

		public override void OnPhotonPlayerConnected(PhotonPlayer otherPlayer)
		{
			UpdatePlayerList (PlayerTextList,FrameImgList);
			CheckRoomFull ();
		}

		public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
		{
			UpdatePlayerList (PlayerTextList,FrameImgList);
			CheckRoomFull ();
		}
			
		public override void OnMasterClientSwitched(PhotonPlayer player)
		{
			Leave ();
		}
			
		public override void OnLeftRoom()
		{
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
