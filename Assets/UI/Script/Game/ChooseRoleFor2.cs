using System.Collections;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;


namespace Com.MyProject.MyPassTheBuckGame
{
	public class ChooseRoleFor2 : Photon.PunBehaviour
	{

		public List<PhotonPlayer> PlayerList;
		public Sprite BlueRole1;
		public Sprite BlueRole2;
		public Sprite GreenRole1;
		public Sprite GreenRole2;
		public Text Player1NameTx;
		public Text Player2NameTx;
		public Text Player1RoleInfoTx;
		public Text Player2RoleInfoTx;
		public Text Player1MessageTx;
		public Text Player2MessageTx;
		public Image Player1RoleImg;
		public Image Player2RoleImg;
		public Button ChooseBt1;
		public Button ChooseBt2;
		public Button LeftArrowBt1;
		public Button RightArrowBt1;
		public Button LeftArrowBt2;
		public Button RightArrowBt2;
		public string PartyColor;
		public string RoleChoosed;
		public Hashtable hash;

		public string BlueRole1Intro = "大名:馬英八"+"\n"+"性別:男"+"\n"+"所屬黨籍:不露黨"+"\n"+"興趣:跑步"+"\n";
		public string BlueRole2Intro = "大名:蘇貞昌"+"\n"+"性別:男"+"\n"+"所屬黨籍:不露黨"+"\n"+"興趣:喊口號"+"\n"+"口頭禪:衝衝衝";
		public string GreenRole1Intro = "大名:蔡中文"+"\n"+"性別:女"+"\n"+"所屬黨籍:格ring黨"+"\n"+"興趣:養貓"+"\n";
		public string GreenRole2Intro = "大名:陳橘"+"\n"+"性別:女"+"\n"+"所屬黨籍:格ring黨"+"\n"+"興趣:?"+"\n";


		void Start () 
		{
			
			PlayerList = new List<PhotonPlayer>();
			PlayerList.Add (PhotonNetwork.masterClient);
			PlayerList.Add (PhotonNetwork.masterClient.GetNext());

			if (PhotonNetwork.player.Equals (PlayerList [0]))
			{
				Player1NameTx.text = "Player1:" + PlayerList [0].NickName+"(you)" ;
				Player2NameTx.text = "Player2:" + PlayerList [1].NickName;

			} 
			else if (PhotonNetwork.player.Equals (PlayerList [1]))
			{
				Player1NameTx.text = "Player1:" + PlayerList [0].NickName;
				Player2NameTx.text = "Player2:" + PlayerList [1].NickName+"(you)" ;
				Player1MessageTx.GetComponent<Text> ().text = "選擇中...";
				Player2MessageTx.GetComponent<Text> ().text = "請等待對方選擇...";
				LeftArrowBt1.GetComponent< Button> ().interactable = false;
				RightArrowBt1.GetComponent< Button> ().interactable = false;
				ChooseBt1.GetComponent< Button> ().interactable = false;
			} 

		}


		void Update () 
		{
			if (PhotonNetwork.player.Equals (PlayerList [1]))
			{
				//判斷玩家1黨派顏色是否有資料
				//有的話player2的按紐設成可以選，然後從player1取資料，設定圖片

				if (PlayerList [0].CustomProperties ["PartyColor"] != null && PlayerList [1].CustomProperties ["PartyColor"] == null)
				{
					//設定按鈕
					ChooseBt2.GetComponent<Button> ().interactable = true;
					LeftArrowBt2.GetComponent< Button> ().interactable = true;
					RightArrowBt2.GetComponent< Button> ().interactable = true;
					//更改玩家選角訊息顯示文字與顏色
					Player1MessageTx.GetComponent<Text> ().text = "選擇了: "+"'"+PlayerList [0].CustomProperties["Role"]+"'";
					Player2MessageTx.GetComponent<Text> ().text = "請選擇角色";
				}
			} 

			if (PlayerList [0].CustomProperties ["PartyColor"] != null && PlayerList [1].CustomProperties ["PartyColor"] != null) 
			{
				if (PhotonNetwork.isMasterClient) 
				{
					PhotonNetwork.LoadLevel ("Stage2");
				}
			}
		}

		public void ClickChoose()
		{

			PlayerList = new List<PhotonPlayer>();
			PlayerList.Add (PhotonNetwork.masterClient);
			PlayerList.Add (PhotonNetwork.masterClient.GetNext());

			//先取得UI元件
			ChooseBt1 = GameObject.Find ("ChooseBt1-2").GetComponent<Button> ();
			ChooseBt2 = GameObject.Find ("ChooseBt2-2").GetComponent<Button> ();
			LeftArrowBt1 = GameObject.Find ("LeftArrowBt1-2").GetComponent< Button> ();
			RightArrowBt1 = GameObject.Find ("RightArrowBt1-2").GetComponent< Button> ();
			LeftArrowBt2 = GameObject.Find ("LeftArrowBt2-2").GetComponent< Button> ();
			RightArrowBt2 = GameObject.Find ("RightArrowBt2-2").GetComponent< Button> ();
			Player1MessageTx = GameObject.Find ("Player1MessageTx-2").GetComponent<Text> ();
			Player2MessageTx = GameObject.Find ("Player2MessageTx-2").GetComponent<Text> ();
			Player1RoleInfoTx = GameObject.Find ("CharecterInfoTx1-2").GetComponent<Text> ();
			Player2RoleInfoTx = GameObject.Find ("CharecterInfoTx2-2").GetComponent<Text> ();


			if (PhotonNetwork .player.Equals (PlayerList [0]))
			{
				//儲存黨派顏色和選擇的角色
				hash = new Hashtable();
				PartyColor = "blue";
				RoleChoosed = GetPlayer1RoleChoosed ();
				hash.Add("PartyColor", PartyColor);
				hash.Add ("Role",RoleChoosed);
				PlayerList [0].SetCustomProperties(hash);

				LeftArrowBt1.GetComponent< Button> ().interactable = false;
				RightArrowBt1.GetComponent< Button> ().interactable = false;
				ChooseBt1.GetComponent< Button> ().interactable = false;
				Player1MessageTx.text = "你選擇了:"+PlayerList [0].CustomProperties["Role"]+"\n"+"請等待對方選擇...";
				Player2MessageTx.text = "選擇中...";

			} 
			else if (PhotonNetwork.player.Equals (PlayerList [1]))
			{
				//儲存黨派顏色和選擇的角色
				hash = new Hashtable();
				PartyColor = "green";
				RoleChoosed = GetPlayer2RoleChoosed ();
				hash.Add("PartyColor", PartyColor);
				hash.Add ("Role",RoleChoosed);
				PlayerList [1].SetCustomProperties(hash);

				LeftArrowBt2.GetComponent< Button> ().interactable = false;
				RightArrowBt2.GetComponent< Button> ().interactable = false;
				ChooseBt2.GetComponent< Button> ().interactable = false;
			} 

		}

		public string GetPlayer1RoleChoosed()
		{
			Sprite P1RIMG = GameObject.Find ("RoleImg1-2").GetComponent<Image> ().sprite;

			if (P1RIMG == BlueRole1)
			{
				return "馬英八";
			} 
			else if (P1RIMG == BlueRole2) 
			{
				return "蘇貞昌";
			} 
			else 
			{
				return "null";
			}

		}

		public string GetPlayer2RoleChoosed()
		{
			Sprite P2RIMG = GameObject.Find ("RoleImg2-2").GetComponent<Image> ().sprite;

			if (P2RIMG == GreenRole1)
			{
				return "蔡中文";
			} 
			else if (P2RIMG == GreenRole2)
			{
				return "陳橘";
			} 
			else 
			{
				return "null";
			}

		}

		//按右箭頭
		public void ClickRightArrow1()
		{
			Player1RoleImg = GameObject.Find ("RoleImg1-2").GetComponent<Image> ();
			Player1RoleInfoTx = GameObject.Find ("CharecterInfoTx1-2").GetComponent<Text> ();


			if (Player1RoleImg.sprite == BlueRole1) 
			{
				Player1RoleImg.sprite = BlueRole2;
				Player1RoleInfoTx.text =BlueRole2Intro;
			} 
			else if (Player1RoleImg.sprite == BlueRole2)
			{
				Player1RoleImg.sprite = BlueRole1;
				Player1RoleInfoTx.text =BlueRole1Intro ;
			}

		}

		//按左箭頭
		public void ClickLeftArrow1()
		{

			Player1RoleImg = GameObject.Find ("RoleImg1-2").GetComponent<Image> ();
			Player1RoleInfoTx = GameObject.Find ("CharecterInfoTx1-2").GetComponent<Text> ();


			if (Player1RoleImg.sprite == BlueRole1) 
			{
				Player1RoleImg.sprite = BlueRole2;
				Player1RoleInfoTx.text =BlueRole2Intro;
			} 
			else if (Player1RoleImg.sprite == BlueRole2)
			{
				Player1RoleImg.sprite = BlueRole1;
				Player1RoleInfoTx.text =BlueRole1Intro ;
			}

		}

		//按右箭頭
		public void ClickRightArrow2()
		{
			Player2RoleImg = GameObject.Find ("RoleImg2-2").GetComponent<Image> ();
			Player2RoleInfoTx = GameObject.Find ("CharecterInfoTx2-2").GetComponent<Text> ();



			if (Player2RoleImg.sprite == GreenRole1)
			{
				Player2RoleImg.sprite = GreenRole2;
				Player2RoleInfoTx.text =GreenRole2Intro;
			}
			else if (Player2RoleImg.sprite == GreenRole2)
			{
				Player2RoleImg.sprite = GreenRole1;
				Player2RoleInfoTx.text =GreenRole1Intro;
			}

		}

		//按左箭頭
		public void ClickLeftArrow2()
		{

			Player2RoleImg = GameObject.Find ("RoleImg2-2").GetComponent<Image> ();
			Player2RoleInfoTx = GameObject.Find ("CharecterInfoTx2-2").GetComponent<Text> ();

			if (Player2RoleImg.sprite == GreenRole1)
			{
				Player2RoleImg.sprite = GreenRole2;
				Player2RoleInfoTx.text =GreenRole2Intro;
			}
			else if (Player2RoleImg.sprite == GreenRole2)
			{
				Player2RoleImg.sprite = GreenRole1;
				Player2RoleInfoTx.text =GreenRole1Intro;
			}

		}




	}
}

