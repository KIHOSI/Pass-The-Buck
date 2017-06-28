using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyProject.MyPassTheBuckGame
{
	public class WaitingRoom :Photon.PunBehaviour {

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

		public string RoomName;
		public List<Text> PlayerTextList;
		public List<Image> FrameImgList;
		public List<Image> PlayerCharecterImgList;
		//public  Vector2  vector1;
		//public  Vector2  vector2;

		//public float x;
		//public float y;

		// Use this for initialization
		public void Start () 
		{

			PlayerTextList = new List<Text>(){PlayerTx1,PlayerTx2,PlayerTx3,PlayerTx4};
			FrameImgList = new List<Image>(){Frame1Img,Frame2Img,Frame3Img,Frame4Img};
			PlayerCharecterImgList = new List<Image>(){PlayerCharecter1Img,PlayerCharecter2Img,PlayerCharecter3Img,PlayerCharecter4Img};
			RoomName = PhotonNetwork.room.name;
			GameObject.Find ("RoomNameTx").GetComponent<Text> ().text="房名:"+RoomName;

			Debug.Log(PlayerTextList [0].text);

			//vector1.x=(float)680;
			//vector1.y=(float)1000;
			//vector2.x=(float)635;
			//vector2.y=(float)355;

			UpdatePlayerList (PlayerTextList,FrameImgList);

		}

		#region Public Methods


		public void UpdatePlayerList (List<Text> PTL,List<Image> FIL)
		{
			for (int i = 0; i < PhotonNetwork.playerList.Length; i++) 
			{
				//Text playerTx = (Text)Instantiate (PlayerPrefab,vector1,transform.rotation);
				//playerTx.GetComponent<Transform>().SetParent (GameObject.Find("Canvas").GetComponent<Transform>(),true);
				//playerTx.GetComponent<Text> ().text = PhotonNetwork.playerList [i].name;
				//vector1.y-=30;

				//Image frameImg = (Image)Instantiate (FrameImgPrefab,vector2,transform.rotation);
				//frameImg.GetComponent<Transform>().SetParent (GameObject.Find("Canvas").GetComponent<Transform>(),true);
				//vector2.y-=30;

				if (i == 0) 
				{
					PTL [i].text = PhotonNetwork.playerList [i].name + "(創建者)";
				} 
				else 
				{
					PTL [i].text = PhotonNetwork.playerList [i].name;
				}
				FIL [i].color = Color.blue;
				Debug.Log(PTL [i].text);


				Debug.Log(PhotonNetwork.playerList [i].name);
			}
		}

		#endregion

		#region Photon.PunBehaviour CallBacks

		void OnPhotonPlayerConnected(PhotonPlayer otherPlayer)
		{
			UpdatePlayerList (PlayerTextList,FrameImgList);
		}

		void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
		{
			UpdatePlayerList (PlayerTextList,FrameImgList);
			PhotonNetwork.Disconnect();
		}

		#endregion

	}
}
