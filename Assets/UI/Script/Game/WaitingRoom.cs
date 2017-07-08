using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

namespace Com.MyProject.MyPassTheBuckGame
{
	public class WaitingRoom :Photon.PunBehaviour {

		//public PhotonView photonView;
		public Sprite role1;
		public Sprite role2;
		public Sprite role3;

		public Image PlayerCharecter1Img;
		public Image PlayerCharecter2Img;
		public Image PlayerCharecter3Img;
		public Image PlayerCharecter4Img;
		public Text PlayerTx1;
	    public Text PlayerTx2;
		public Text PlayerTx3;
		public Text PlayerTx4;
		public Image Frame1Img;
		public Image Frame2Img;
		public Image Frame3Img;
		public Image Frame4Img;
		public Button StartBt;

		public string RoomName;
		public List<Text> PlayerTextList;
		public List<Image> FrameImgList;
		public List<Image> PlayerCharecterImgList;

		public void Start () 
		{

			PlayerTextList = new List<Text>(){PlayerTx1,PlayerTx2,PlayerTx3,PlayerTx4};
			FrameImgList = new List<Image>(){Frame1Img,Frame2Img,Frame3Img,Frame4Img};
			PlayerCharecterImgList = new List<Image>(){PlayerCharecter1Img,PlayerCharecter2Img,PlayerCharecter3Img,PlayerCharecter4Img};


			//取得房名
			RoomName = PhotonNetwork.room.Name;
			GameObject.Find ("RoomNameTx").GetComponent<Text> ().text="房名:"+RoomName;

			//更新玩家名單
			UpdatePlayerList (PlayerTextList,FrameImgList);

		}

		public void Update ()
		{
			if (PhotonNetwork.isMasterClient) 
			{
				if (PhotonNetwork.room.PlayerCount == PhotonNetwork.room.MaxPlayers)
				{
					StartBt.GetComponent<Button> ().interactable = true;
				}
			}
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
					FIL [i].color = Color.blue;
				} else {
					PTL [i].text = PhotonNetwork.playerList [i].NickName;
					FIL [i].color = Color.blue;
				}

				j++;

			}

			for ( int g=j ; g < PTL.Count; g++) 
			{

				PTL [g].text = "";
				FIL [g].color = new Color32(255,255,225,0);

			}
			
		}

		/*
		public void LeaveRoom2()
		{
			if (PhotonNetwork.isMasterClient) 
			{
				photonView.RPC ("Leave", PhotonTargets.All);
			} 
			else
			{
				Leave ();
			}
		}*/

		//開始遊戲
		public void StartGame()
		{
			PhotonNetwork.LoadLevel("Stage1");
		}

		//離開房間
		//[PunRPC]
		public void Leave()
		{
			PhotonNetwork.LeaveRoom ();
		}

		#endregion

		#region Photon.PunBehaviour CallBacks

		public override void OnPhotonPlayerConnected(PhotonPlayer otherPlayer)
		{
			UpdatePlayerList (PlayerTextList,FrameImgList);
		}

		public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
		{
			UpdatePlayerList (PlayerTextList,FrameImgList);
		}
			
		public override void OnMasterClientSwitched(PhotonPlayer player)
		{
			Leave ();
		}
			
		public override void OnLeftRoom()
		{
			SceneManager.LoadScene(2);
		}

		#endregion

	}
}
