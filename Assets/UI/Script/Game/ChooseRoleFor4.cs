using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UI;


namespace Com.MyProject.MyPassTheBuckGame
{
	public class ChooseRoleFor4 : Photon.PunBehaviour
	{


		public List<PhotonPlayer> PlayerList;
		public Sprite BlueRole1;
		public Sprite BlueRole2;
		public Sprite GreenRole1;
		public Sprite GreenRole2;
		public Sprite BluePanel;
		public Sprite GreenPanel;
		public Sprite GrayPanel;
		public Text Player1NameTx;
		public Text Player2NameTx;
		public Text Player1RoleInfoTx;
		public Text Player2RoleInfoTx;
		public Text Player1MessageTx;
		public Text Player2MessageTx;
		public Text AfterChooseTx1;
		public Text AfterChooseTx2;
		public Image Player1RoleImg;
		public Image Player2RoleImg;
		public Image AfterChooseImg1;
		public Image AfterChooseImg2;
		public Button ChooseBt1;
		public Button ChooseBt2;
		public Button LeftArrowBt1;
		public Button RightArrowBt1;
		public Button LeftArrowBt2;
		public Button RightArrowBt2;
		public string PartyColor;
		string RoleChoosed;
		public Hashtable hash;

		public string BlueRole1Intro = " 【洪咻柱】 "+"\n"+"\n"+"祝鑄炷佇駐助蛀柱。";
		public string BlueRole2Intro = " 【吳指癢】 "+"\n"+"\n"+"曾為縣長的新科立委。";
		public string GreenRole1Intro = " 【蔡中聞】 "+"\n"+"\n"+"天天怒的新上任總理。";
		public string GreenRole2Intro = " 【蘇嘎拳】 "+"\n"+"\n"+"新流派權法的院長。";

		void Start () 
		{

			//將房間裡的player儲存到player list
			PlayerList = new List<PhotonPlayer>();
			PlayerList.Add (PhotonNetwork.masterClient);
			PlayerList.Add (PhotonNetwork.masterClient.GetNext());
			PlayerList.Add (PhotonNetwork.masterClient.GetNext().GetNext());
			PlayerList.Add (PhotonNetwork.masterClient.GetNext().GetNext().GetNext());
		
		
			//第一個玩家
			if (PhotonNetwork.player.Equals (PlayerList [0]))
			{
				Player1NameTx.text = "Player1:" + PlayerList [0].NickName+"(you)" ;
				Player2NameTx.text = "Player2:" + PlayerList [1].NickName;

			} 
			//第二個玩家
			else if (PhotonNetwork.player.Equals (PlayerList [ 1]))
			{
				Player1NameTx.text = "Player1:" + PlayerList [0].NickName;
				Player2NameTx.text = "Player2:" + PlayerList [1].NickName+"(you)" ;
				Player1MessageTx.GetComponent<Text> ().text = "選擇中...";
				Player2MessageTx.GetComponent<Text> ().text = "請等待對方選擇...";
				LeftArrowBt1.GetComponent< Button> ().interactable = false;
				RightArrowBt1.GetComponent< Button> ().interactable = false;
				ChooseBt1.GetComponent< Button> ().interactable = false;
			} 
			//第三個玩家
			else if (PhotonNetwork.player.Equals (PlayerList [ 2]))
			{
				Player1NameTx.text = "Player1:" + PlayerList [2].NickName+"(you)" ;
				Player2NameTx.text = "Player2:" + PlayerList [3].NickName;
				Player1RoleImg.sprite = GreenRole1;
				Player1RoleInfoTx.text = GreenRole1Intro;
				Player2RoleImg.sprite = GreenRole1;
				Player2RoleInfoTx.text = GreenRole1Intro;
				GameObject.Find ("RoleChoosePanel1-4" ).GetComponent<Image> ().sprite = GreenPanel;

			}
			//第四個玩家
			else if (PhotonNetwork.player.Equals (PlayerList [ 3]))
			{
				Player1NameTx.text = "Player1:" + PlayerList [2].NickName;
				Player2NameTx.text = "Player2:" + PlayerList [3].NickName+"(you)" ;
				Player1MessageTx.GetComponent<Text> ().text = "選擇中...";
				Player2MessageTx.GetComponent<Text> ().text = "請等待對方選擇...";
				Player1RoleImg.sprite = GreenRole1;
				Player1RoleInfoTx.text = GreenRole1Intro;
				Player2RoleImg.sprite = GreenRole1;
				Player2RoleInfoTx.text = GreenRole1Intro;
				LeftArrowBt1.GetComponent< Button> ().interactable = false;
				RightArrowBt1.GetComponent< Button> ().interactable = false;
				ChooseBt1.GetComponent< Button> ().interactable = false;
				GameObject.Find ("RoleChoosePanel1-4" ).GetComponent<Image> ().sprite = GreenPanel;
			}

		}


		void FixedUpdate () 
		{

			//第二個玩家
			if (PhotonNetwork.player.Equals (PlayerList [1]))
			{
				//第二位玩家已完成選擇
				if (PlayerList [0].CustomProperties ["PartyColor"] != null && PlayerList [1].CustomProperties ["PartyColor"] != null) 
				{
					//設定按鈕
					ChooseBt2.GetComponent< Button> ().interactable = false;
					LeftArrowBt2.GetComponent< Button> ().interactable = false;
					RightArrowBt2.GetComponent< Button> ().interactable = false;
					Debug.Log ("第二個玩家已經有值了，值是:" + PlayerList [1].CustomProperties ["PartyColor"]);

					//顯示"等待綠隊選擇"
					if (PlayerList [3].CustomProperties ["PartyColor"] == null)
					{
						GameObject.Find ("AfterChoosePanel2-4" ).GetComponent<Image> ().color = new Color32(221,221,221,255);
						AfterChooseImg2.GetComponent<Image> ().color = new Color32(255,255,255,255);
						AfterChooseTx2.GetComponent<Text> ().text = "你選擇了"+"\n'"+PlayerList [1].CustomProperties ["Role"]+"'\n"+"請等待綠隊選擇";

						if (PlayerList [1].CustomProperties ["Role"].Equals("洪咻柱")) 
						{
							AfterChooseImg2.sprite = BlueRole1;
						} 
						else if (PlayerList [1].CustomProperties ["Role"].Equals("吳指癢")) 
						{
							AfterChooseImg2.sprite = BlueRole2;
						}
					} 
						
				
				} //第一位玩家已選擇，輪到第二位玩家
				else if (PlayerList [0].CustomProperties ["PartyColor"] != null && PlayerList [1].CustomProperties ["PartyColor"] == null)
				{
					Debug.Log ("第二個玩家還沒有值了");
					//設定按鈕
					ChooseBt2.GetComponent< Button> ().interactable = true;
					LeftArrowBt2.GetComponent< Button> ().interactable = true;
					RightArrowBt2.GetComponent< Button> ().interactable = true;
					//更改panel顏色
					GameObject.Find ("RoleChoosePanel1-4" ).GetComponent<Image> ().sprite = GrayPanel;
					GameObject.Find ("RoleChoosePanel2-4" ).GetComponent<Image> ().sprite = BluePanel;
					//更改玩家選角訊息顯示文字與顏色
					Player2MessageTx.GetComponent<Text> ().text = "請選擇角色";
					Player2MessageTx.GetComponent<Text> ().color = Color.white;

					GameObject.Find ("AfterChoosePanel1-4" ).GetComponent<Image> ().color = new Color32(221,221,221,255);
					AfterChooseImg1.GetComponent<Image> ().color = new Color32(255,255,255,255);
					AfterChooseTx1.GetComponent<Text> ().text = "選擇了"+"\n'"+PlayerList [0].CustomProperties ["Role"]+"'";

					if (PlayerList [0].CustomProperties ["Role"].Equals("洪咻柱")) 
					{
						AfterChooseImg1.sprite = BlueRole1;
						Player2RoleImg.sprite = BlueRole2;
						Player2RoleInfoTx.text = BlueRole2Intro;

					} 
					else if (PlayerList [0].CustomProperties ["Role"].Equals("吳指癢")) 
					{
						AfterChooseImg1.sprite = BlueRole2;
						Player2RoleImg.sprite = BlueRole1;
						Player2RoleInfoTx.text = BlueRole1Intro;
					}

				}
			} 
			//第四個玩家
			else if (PhotonNetwork.player.Equals (PlayerList [3]))
			{
				//第四位玩家已完成選擇
				if (PlayerList [2].CustomProperties ["PartyColor"] != null && PlayerList [3].CustomProperties ["PartyColor"] != null)
				{
					//設定按鈕
					ChooseBt2.GetComponent< Button> ().interactable = false;
					LeftArrowBt2.GetComponent< Button> ().interactable = false;
					RightArrowBt2.GetComponent< Button> ().interactable = false;

					//顯示"等待藍隊選擇"
					if (PlayerList [1].CustomProperties ["PartyColor"] == null) 
					{
						GameObject.Find ("AfterChoosePanel2-4").GetComponent<Image> ().color = new Color32(221,221,221,255);
						AfterChooseImg2.GetComponent<Image> ().color = new Color32(255,255,255,255);
						AfterChooseTx2.GetComponent<Text> ().text = "你選擇了"+"\n'"+PlayerList [3].CustomProperties ["Role"]+"'\n"+"請等待藍隊選擇";

						if (PlayerList [3].CustomProperties ["Role"].Equals("蔡中聞")) 
						{
							AfterChooseImg2.sprite = GreenRole1;
						} 
						else if (PlayerList [3].CustomProperties ["Role"].Equals("蘇嘎拳")) 
						{
							AfterChooseImg2.sprite = GreenRole2;
						}
					}
				} //第三位玩家已完成選擇，輪到第四位
				else if (PlayerList [2].CustomProperties ["PartyColor"] != null && PlayerList [3].CustomProperties ["PartyColor"] == null)
				{
					//設定按鈕
					ChooseBt2.GetComponent< Button> ().interactable = true;
					LeftArrowBt2.GetComponent< Button> ().interactable = true;
					RightArrowBt2.GetComponent< Button> ().interactable = true;
					//更改panel顏色
					GameObject.Find ("RoleChoosePanel2-4").GetComponent<Image> ().sprite = GreenPanel;
					GameObject.Find ("RoleChoosePanel1-4").GetComponent<Image> ().sprite = GrayPanel;
					//更改玩家選角訊息與顏色
					Player2MessageTx.GetComponent<Text> ().text = "請選擇角色";
					Player2MessageTx.GetComponent<Text> ().color = Color.white;

					GameObject.Find ("AfterChoosePanel1-4").GetComponent<Image> ().color = new Color32(221,221,221,255);
					AfterChooseImg1.GetComponent<Image> ().color = new Color32(255,255,255,255);
					AfterChooseTx1.GetComponent<Text> ().text = "選擇了"+"\n'"+PlayerList [2].CustomProperties ["Role"]+"'";

					if (PlayerList [2].CustomProperties ["Role"].Equals("蔡中聞")) 
					{
						AfterChooseImg1.sprite = GreenRole1;
						Player2RoleImg.sprite = GreenRole2;
						Player2RoleInfoTx.text = GreenRole2Intro;
					} 
					else if (PlayerList [2].CustomProperties ["Role"].Equals("蘇嘎拳")) 
					{
						AfterChooseImg1.sprite = GreenRole2;
						Player2RoleImg.sprite = GreenRole1;
						Player2RoleInfoTx.text = GreenRole1Intro;
					}
				}

			} 
			//第一位玩家
			else if (PhotonNetwork.player.Equals (PlayerList [0]))
			{
				//第二位玩家已完成選擇
				if (PlayerList [0].CustomProperties ["PartyColor"] != null && PlayerList [1].CustomProperties ["PartyColor"] != null)
				{

					//顯示"等待綠隊選擇"
					if (PlayerList [3].CustomProperties ["PartyColor"] == null)
					{
						GameObject.Find ("AfterChoosePanel2-4").GetComponent<Image> ().color = new Color32(221,221,221,255);
						AfterChooseImg2.GetComponent<Image> ().color = new Color32(255,255,255,255);
						AfterChooseTx2.GetComponent<Text> ().text = "選擇了"+"\n'"+PlayerList [1].CustomProperties ["Role"]+"'\n"+"請等待綠隊選擇";

						if (PlayerList [1].CustomProperties ["Role"].Equals("洪咻柱")) 
						{
							AfterChooseImg2.sprite = BlueRole1;
						} 
						else if (PlayerList [1].CustomProperties ["Role"].Equals("吳指癢")) 
						{
							AfterChooseImg2.sprite = BlueRole2;
						}
					} 
				} 
			}
			//第三位玩家
			else if (PhotonNetwork.player.Equals (PlayerList [2]))
			{
				//第四位玩家已完成選擇
				if (PlayerList [2].CustomProperties ["PartyColor"] != null && PlayerList [3].CustomProperties ["PartyColor"] != null)
				{
					//顯示"等待藍隊選擇"
					if (PlayerList [1].CustomProperties ["PartyColor"] == null) 
					{
						GameObject.Find ("AfterChoosePanel2-4").GetComponent<Image> ().color = new Color32(221,221,221,255);
						AfterChooseImg2.GetComponent<Image> ().color = new Color32(255,255,255,255);
						AfterChooseTx2.GetComponent<Text> ().text = "選擇了"+"\n'"+PlayerList [3].CustomProperties ["Role"]+"'\n"+"請等待藍隊選擇";

						if (PlayerList [3].CustomProperties ["Role"].Equals("蔡中聞")) 
						{
							AfterChooseImg2.sprite = GreenRole1;
						} 
						else if (PlayerList [3].CustomProperties ["Role"].Equals("蘇嘎拳")) 
						{
							AfterChooseImg2.sprite = GreenRole2;
						}
					}
				} 
			}
		 

			if (PlayerList [1].CustomProperties ["PartyColor"] != null && PlayerList [3].CustomProperties ["PartyColor"] != null) 
			{
				PhotonNetwork.LoadLevel ("Loading Animator");
			}

		}


		public void ClickChoose()
		{

			PlayerList = new List<PhotonPlayer>();
			PlayerList.Add (PhotonNetwork.masterClient);
			PlayerList.Add (PhotonNetwork.masterClient.GetNext());
			PlayerList.Add (PhotonNetwork.masterClient.GetNext().GetNext());
			PlayerList.Add (PhotonNetwork.masterClient.GetNext().GetNext().GetNext());

			//先取得UI元件
			ChooseBt1 = GameObject.Find ("ChooseBt1-4").GetComponent<Button> ();
			ChooseBt2 = GameObject.Find ("ChooseBt2-4").GetComponent<Button> ();
			LeftArrowBt1 = GameObject.Find ("LeftArrowBt1-4").GetComponent< Button> ();
			RightArrowBt1 = GameObject.Find ("RightArrowBt1-4").GetComponent< Button> ();
			LeftArrowBt2 = GameObject.Find ("LeftArrowBt2-4").GetComponent< Button> ();
			RightArrowBt2 = GameObject.Find ("RightArrowBt2-4").GetComponent< Button> ();
			Player1NameTx = GameObject.Find ("Player1NameTx-4").GetComponent<Text> ();
			Player2NameTx = GameObject.Find ("Player2NameTx-4").GetComponent<Text> ();
			Player1MessageTx = GameObject.Find ("Player1MessageTx-4").GetComponent<Text> ();
			Player2MessageTx = GameObject.Find ("Player2MessageTx-4").GetComponent<Text> ();
			Player1RoleInfoTx = GameObject.Find ("CharecterInfoTx1-4").GetComponent<Text> ();
			Player2RoleInfoTx = GameObject.Find ("CharecterInfoTx2-4").GetComponent<Text> ();
			AfterChooseTx1= GameObject.Find ("AfterChooseTx1-4").GetComponent<Text> ();
			AfterChooseTx2= GameObject.Find ("AfterChooseTx2-4").GetComponent<Text> ();
			AfterChooseImg1= GameObject.Find ("AfterChooseImg1-4").GetComponent<Image> ();
			AfterChooseImg2= GameObject.Find ("AfterChooseImg2-4").GetComponent<Image> ();

			//第一個玩家
			if (PhotonNetwork.player.Equals (PlayerList [0]))
			{
				//儲存黨派顏色和選擇的角色
				hash = new Hashtable();
				PartyColor = "blue";
				RoleChoosed = GetPlayer1RoleChoosed ();
				hash.Add("PartyColor", PartyColor);
				hash.Add ("Role",RoleChoosed);
				PlayerList [0].SetCustomProperties(hash);

				//設定按鈕
				ChooseBt1.GetComponent< Button> ().interactable = false;
				LeftArrowBt1.GetComponent< Button> ().interactable = false;
				RightArrowBt1.GetComponent< Button> ().interactable = false;
				//更改panel顏色
				GameObject.Find ("RoleChoosePanel2-4" ).GetComponent<Image> ().sprite = BluePanel;
				GameObject.Find ("RoleChoosePanel1-4" ).GetComponent<Image> ().sprite = GrayPanel;
				//更改玩家選角訊息與顏色
				Player2MessageTx.text = "選擇中...";
				Player2MessageTx.GetComponent<Text> ().color = Color.white;

				GameObject.Find ("AfterChoosePanel1-4" ).GetComponent<Image> ().color = new Color32(221,221,221,255);
				AfterChooseImg1.GetComponent<Image> ().color = new Color32(255,255,255,255);
				AfterChooseTx1.GetComponent<Text> ().text = "你選擇了"+"\n'"+PlayerList [0].CustomProperties ["Role"]+"'";

				if (PlayerList [0].CustomProperties ["Role"].Equals("洪咻柱")) 
				{
					AfterChooseImg1.GetComponent<Image> ().sprite = BlueRole1;
				} 
				else if (PlayerList [0].CustomProperties ["Role"].Equals("吳指癢")) 
				{
					AfterChooseImg1.GetComponent<Image> ().sprite = BlueRole2;
				}

			} 
			//第二個玩家
			else if (PhotonNetwork.player.Equals (PlayerList [1]))
			{
				//儲存黨派顏色和選擇的角色
				hash = new Hashtable();
				PartyColor = "blue";
				RoleChoosed = GetPlayer2RoleChoosed ();
				hash.Add("PartyColor", PartyColor);
				hash.Add ("Role",RoleChoosed);
				PlayerList [1].SetCustomProperties(hash);

			} 
			//第三個玩家
			else if (PhotonNetwork.player.Equals (PlayerList [2]))
			{
				//儲存黨派顏色和選擇的角色
				hash = new Hashtable();
				PartyColor = "green";
				RoleChoosed = GetPlayer1RoleChoosed ();
				hash.Add("PartyColor", PartyColor);
				hash.Add ("Role",RoleChoosed);
				PlayerList [2].SetCustomProperties(hash);

				//設定按鈕
				ChooseBt1.GetComponent< Button> ().interactable = false;
				LeftArrowBt1.GetComponent< Button> ().interactable = false;
				RightArrowBt1.GetComponent< Button> ().interactable = false;
				//更改panel顏色
				GameObject.Find ("RoleChoosePanel2-4" ).GetComponent<Image> ().sprite = GreenPanel;
				GameObject.Find ("RoleChoosePanel1-4" ).GetComponent<Image> ().sprite = GrayPanel;
				//更改玩家選角訊息與顏色
				Player2MessageTx.text = "選擇中...";
				Player2MessageTx.GetComponent<Text> ().color = Color.white;

				GameObject.Find ("AfterChoosePanel1-4" ).GetComponent<Image> ().color = new Color32(221,221,221,255);
				AfterChooseImg1.GetComponent<Image> ().color = new Color32(255,255,255,255);
				AfterChooseTx1.GetComponent<Text> ().text = "你選擇了"+"\n'"+PlayerList [2].CustomProperties ["Role"]+"'";

				if (PlayerList [2].CustomProperties ["Role"].Equals("蔡中聞")) 
				{
					AfterChooseImg1.GetComponent<Image> ().sprite = GreenRole1;
				} 
				else if (PlayerList [2].CustomProperties ["Role"].Equals("蘇嘎拳")) 
				{
					AfterChooseImg1.GetComponent<Image> ().sprite = GreenRole2;
				}

			}
			//第四個玩家
			else if (PhotonNetwork.player.Equals (PlayerList [ 3]))
			{
				//儲存黨派顏色和選擇的角色
				hash = new Hashtable();
				PartyColor = "green";
				RoleChoosed = GetPlayer2RoleChoosed ();
				hash.Add("PartyColor", PartyColor);
				hash.Add ("Role",RoleChoosed);
				PlayerList [3].SetCustomProperties(hash);


			}

		}

		public string GetPlayer1RoleChoosed()
		{
			Sprite P1RIMG = GameObject.Find ("RoleImg1-4").GetComponent<Image> ().sprite;

			if (P1RIMG == BlueRole1)
			{
				return "洪咻柱";
			} 
			else if (P1RIMG == BlueRole2) 
			{
				return "吳指癢";
			} 
			else if (P1RIMG == GreenRole1)
			{
				return "蔡中聞";
			} 
			else if (P1RIMG == GreenRole2)
			{
				return "蘇嘎拳";
			} 
			else 
			{
				return "null";
			}

		}

		public string GetPlayer2RoleChoosed()
		{
			Sprite P2RIMG = GameObject.Find ("RoleImg2-4").GetComponent<Image> ().sprite;

			if (P2RIMG == GreenRole1)
			{
				return "蔡中聞";
			} 
			else if (P2RIMG == GreenRole2)
			{
				return "蘇嘎拳";
			} 
			else if (P2RIMG == BlueRole1)
			{
				return "洪咻柱";
			} 
			else if (P2RIMG == BlueRole2) 
			{
				return "吳指癢";
			} 
			else 
			{
				return "null";
			}

		}

		//按右箭頭
		public void ClickRightArrow1()
		{
			Player1RoleImg = GameObject.Find ("RoleImg1-4").GetComponent<Image> ();
			Player1RoleInfoTx = GameObject.Find ("CharecterInfoTx1-4").GetComponent<Text> ();


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
			else if (Player1RoleImg.sprite == GreenRole1)
			{
				Player1RoleImg.sprite = GreenRole2;
				Player1RoleInfoTx.text =GreenRole2Intro;
			}
			else if (Player1RoleImg.sprite == GreenRole2)
			{
				Player1RoleImg.sprite = GreenRole1;
				Player1RoleInfoTx.text =GreenRole1Intro;
			}

		}

		//按左箭頭
		public void ClickLeftArrow1()
		{

			Player1RoleImg = GameObject.Find ("RoleImg1-4").GetComponent<Image> ();
			Player1RoleInfoTx = GameObject.Find ("CharecterInfoTx1-4").GetComponent<Text> ();


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
			else if (Player1RoleImg.sprite == GreenRole1)
			{
				Player1RoleImg.sprite = GreenRole2;
				Player1RoleInfoTx.text =GreenRole2Intro;
			}
			else if (Player1RoleImg.sprite == GreenRole2)
			{
				Player1RoleImg.sprite = GreenRole1;
				Player1RoleInfoTx.text =GreenRole1Intro;
			}

		}

		//按右箭頭
		public void ClickRightArrow2()
		{
			PlayerList = new List<PhotonPlayer>();
			PlayerList.Add (PhotonNetwork.masterClient);
			PlayerList.Add (PhotonNetwork.masterClient.GetNext());
			PlayerList.Add (PhotonNetwork.masterClient.GetNext().GetNext());
			PlayerList.Add (PhotonNetwork.masterClient.GetNext().GetNext().GetNext());

			Player2RoleImg = GameObject.Find ("RoleImg2-4").GetComponent<Image> ();
			Player2RoleInfoTx = GameObject.Find ("CharecterInfoTx2-4").GetComponent<Text> ();

			if (PhotonNetwork.player.Equals (PlayerList [1]))
			{
				if(PlayerList [0].CustomProperties ["Role"].Equals("洪咻柱"))
				{
						Player2RoleImg.sprite = BlueRole2;
						Player2RoleInfoTx.text = BlueRole2Intro;
				}
				else if(PlayerList [0].CustomProperties ["Role"].Equals("吳指癢"))
				{
					Player2RoleImg.sprite = BlueRole1;
					Player2RoleInfoTx.text = BlueRole1Intro;
				}
			} 

			if (PhotonNetwork.player.Equals (PlayerList [3]))
			{
				if(PlayerList [2].CustomProperties ["Role"].Equals("蔡中聞"))
				{
					Player2RoleImg.sprite = GreenRole2;
					Player2RoleInfoTx.text = GreenRole2Intro;
				}
				else if(PlayerList [2].CustomProperties ["Role"].Equals("蘇嘎拳"))
				{
					Player2RoleImg.sprite = GreenRole1;
					Player2RoleInfoTx.text = GreenRole1Intro;
				}
			} 

		}

		//按左箭頭
		public void ClickLeftArrow2()
		{
			PlayerList = new List<PhotonPlayer>();
			PlayerList.Add (PhotonNetwork.masterClient);
			PlayerList.Add (PhotonNetwork.masterClient.GetNext());
			PlayerList.Add (PhotonNetwork.masterClient.GetNext().GetNext());
			PlayerList.Add (PhotonNetwork.masterClient.GetNext().GetNext().GetNext());

			Player2RoleImg = GameObject.Find ("RoleImg2-4").GetComponent<Image> ();
			Player2RoleInfoTx = GameObject.Find ("CharecterInfoTx2-4").GetComponent<Text> ();

			if (PhotonNetwork.player.Equals (PlayerList [1]))
			{
				if(PlayerList [0].CustomProperties ["Role"].Equals("洪咻柱"))
				{
					Player2RoleImg.sprite = BlueRole2;
					Player2RoleInfoTx.text = BlueRole2Intro;
				}
				else if(PlayerList [0].CustomProperties ["Role"].Equals("吳指癢"))
				{
					Player2RoleImg.sprite = BlueRole1;
					Player2RoleInfoTx.text = BlueRole1Intro;
				}
			} 

			if (PhotonNetwork.player.Equals (PlayerList [3]))
			{
				if(PlayerList [2].CustomProperties ["Role"].Equals("蔡中聞"))
				{
					Player2RoleImg.sprite = GreenRole2;
					Player2RoleInfoTx.text = GreenRole2Intro;
				}
				else if(PlayerList [2].CustomProperties ["Role"].Equals("蘇嘎拳"))
				{
					Player2RoleImg.sprite = GreenRole1;
					Player2RoleInfoTx.text = GreenRole1Intro;
				}
			} 
				

		}

		public override void OnDisconnectedFromPhoton()
		{
			SceneManager.LoadScene("Main");
		}

		public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
		{
			SceneManager.LoadScene("Main");
			PhotonNetwork.LeaveRoom ();
		}



	}
}
