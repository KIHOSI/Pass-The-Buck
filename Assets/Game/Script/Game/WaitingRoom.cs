using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyProject.MyPassTheBuckGame
{
	public class WaitingRoom :Photon.PunBehaviour {

		public string RoomName;
		public GameObject Player1Tx;
		public GameObject Player2Tx;
		public GameObject Player3Tx;
		public GameObject Player4Tx;
		public GameObject FrameImg1;
		public GameObject FrameImg2;
		public GameObject FrameImg3;
		public GameObject FrameImg4;

		private List<GameObject> PlayerTextList;
		private List<GameObject> FrameImgList;
		//public  Vector2  vector1;
		//public  Vector2  vector2;

		//public float x;
		//public float y;

		// Use this for initialization
		void Start () 
		{
			PlayerTextList = new List<GameObject>(){Player1Tx,Player2Tx,Player3Tx,Player4Tx};
			FrameImgList = new List<GameObject>(){FrameImg1,FrameImg2,FrameImg3,FrameImg4};
			RoomName = PhotonNetwork.room.name;
			GameObject.Find ("RoomNameTx").GetComponent<Text> ().text="房名:"+RoomName;

			Debug.Log(PlayerTextList [0].GetComponent<Text>().text);

			for (int i = 0; i < PlayerTextList.Count; i++)
			{
				PlayerTextList [i].GetComponent<Renderer>().enabled=false;
			}

			for (int i = 0; i < FrameImgList.Count; i++)
			{
				FrameImgList [i].GetComponent<Renderer>().enabled=false;
			}
			//vector1.x=(float)680;
			//vector1.y=(float)1000;
			//vector2.x=(float)635;
			//vector2.y=(float)355;

			UpdatePlayerList ();

		}

		#region Public Methods


		public void UpdatePlayerList ()
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

				PlayerTextList [i].GetComponent<Renderer>().enabled=true;
				PlayerTextList [i].GetComponent<Text>().text = PhotonNetwork.playerList [i].name;
				FrameImgList [i].GetComponent<Renderer>().enabled=true;
				Debug.Log(PlayerTextList [i].GetComponent<Text>().text);


				Debug.Log(PhotonNetwork.playerList [i].name);
			}
		}

		#endregion

		#region Photon.PunBehaviour CallBacks

		void OnPhotonPlayerConnected(PhotonPlayer otherPlayer)
		{
			UpdatePlayerList ();
		}

		void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
		{
			UpdatePlayerList ();
		}

		#endregion

	}
}
