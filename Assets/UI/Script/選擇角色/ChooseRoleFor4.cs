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

		//經典關卡按鈕背景圖
		public Sprite RoleBack1;
		public Sprite RoleBack2;
		public Sprite RoleBack3;
		public Sprite RoleBack4;
		//點按鈕後的背景圖
		public Sprite BackWithFrame;
		//815停電關卡按鈕背景圖
		public Sprite RoleBack1ForPowerCut;
		public Sprite RoleBack2ForPowerCut;
		public Sprite RoleBack3ForPowerCut;
		public Sprite RoleBack4ForPowerCut;

		//815停電關卡的人物圖
		public Sprite Role1ForPowerCut;
		public Sprite Role2ForPowerCut;
		public Sprite Role3ForPowerCut;
		public Sprite Role4ForPowerCut;

		public Text Role1NameTx;
		public Text Role2NameTx;
		public Text Role3NameTx;
		public Text Role4NameTx;
		public Text Role1InfoTx;
		public Text Role2InfoTx;
		public Text Role3InfoTx;
		public Text Role4InfoTx;
		public Text AfterChooseTx1;
		public Text AfterChooseTx2;
		public Text AfterChooseTx3;
		public Text AfterChooseTx4;

		public Image Role1Img;
		public Image Role2Img;
		public Image Role3Img;
		public Image Role4Img;

		public Button Role1Bt;
		public Button Role2Bt;
		public Button Role3Bt;
		public Button Role4Bt;
		public Button ChooseBt;

		public string PartyColor;
		string MapChoosed;
		string RoleChoosed;
		public Hashtable hash;

		public string Role1IntroForPowerCut = "中油董事長";
		public string Role2IntroForPowerCut = "台電董事長";
		public string Role3IntroForPowerCut = "天天怒的新上任總理";
		public string Role4IntroForPowerCut = "巨路巨路巨路巨路";

		void Start () 
		{

			//將房間裡的player儲存到player list
			PlayerList = new List<PhotonPlayer>();
			PlayerList.Add (PhotonNetwork.masterClient);
			PlayerList.Add (PhotonNetwork.masterClient.GetNext());
			PlayerList.Add (PhotonNetwork.masterClient.GetNext().GetNext());
			PlayerList.Add (PhotonNetwork.masterClient.GetNext().GetNext().GetNext());
		
			MapChoosed = (string)PhotonNetwork.room.CustomProperties ["Map"];

			if (MapChoosed.Equals ("PowerCut")) 
			{
				Role1Bt.GetComponent<Image> ().sprite = RoleBack1ForPowerCut;
				Role2Bt.GetComponent<Image> ().sprite = RoleBack2ForPowerCut;
				Role3Bt.GetComponent<Image> ().sprite = RoleBack3ForPowerCut;
				Role4Bt.GetComponent<Image> ().sprite = RoleBack4ForPowerCut;

				Role1Img.GetComponent<Image> ().sprite = Role1ForPowerCut;
				Role2Img.GetComponent<Image> ().sprite = Role2ForPowerCut;
				Role3Img.GetComponent<Image> ().sprite = Role3ForPowerCut;
				Role4Img.GetComponent<Image> ().sprite = Role4ForPowerCut;

				Role1NameTx.text = "陳金德";
				Role2NameTx.text = "朱文成";
				Role4NameTx.text = "承包商";

				Role1InfoTx.text = Role1IntroForPowerCut;
				Role2InfoTx.text = Role2IntroForPowerCut;
				Role4InfoTx.text = Role4IntroForPowerCut;
			}
		

		}


		void Update () 
		{
			string Player1Choose = (string) PlayerList [0].CustomProperties ["Role"];
			string Player2Choose = (string) PlayerList [1].CustomProperties ["Role"];
			string Player3Choose = (string) PlayerList [2].CustomProperties ["Role"];
			string Player4Choose = (string) PlayerList [3].CustomProperties ["Role"];

			//關卡選擇經典模式
			if (MapChoosed.Equals("Classic"))
			{
				if (Player1Choose == "洪咻柱" || Player2Choose == "洪咻柱" || Player3Choose == "洪咻柱" || Player4Choose == "洪咻柱")
				{
					Role1Bt.interactable = false;
				}
				else if (Player1Choose == "吳指癢" || Player2Choose == "吳指癢" || Player3Choose == "吳指癢" || Player4Choose == "吳指癢")
				{
					Role2Bt.interactable = false;
				}
				else if (Player1Choose == "蔡中聞" || Player2Choose == "蔡中聞" || Player3Choose == "蔡中聞" || Player4Choose == "蔡中聞")
				{
					Role3Bt.interactable = false;
				}
				else if (Player1Choose == "蘇嘎拳" || Player2Choose == "蘇嘎拳" || Player3Choose == "蘇嘎拳" || Player4Choose == "蘇嘎拳")
				{
					Role4Bt.interactable = false;
				}
			} 
			//關卡選擇815大停電
			else if (MapChoosed.Equals("PowerCut"))
			{
				if (Player1Choose == "陳金德" || Player2Choose == "陳金德" || Player3Choose == "陳金德" || Player4Choose == "陳金德")
				{
					Role1Bt.interactable = false;
				}
				else if (Player1Choose == "朱文成" || Player2Choose == "朱文成" || Player3Choose == "朱文成" || Player4Choose == "朱文成")
				{
					Role2Bt.interactable = false;
				}
				else if (Player1Choose == "蔡中聞" || Player2Choose == "蔡中聞" || Player3Choose == "蔡中聞" || Player4Choose == "蔡中聞")
				{
					Role3Bt.interactable = false;
				}
				else if (Player1Choose == "承包商" || Player2Choose == "承包商" || Player3Choose == "承包商" || Player4Choose == "承包商")
				{
					Role4Bt.interactable = false;
				}
			} 


			if (Player1Choose !=null && Player2Choose !=null && Player3Choose !=null && Player4Choose !=null) 
			{
				if (PhotonNetwork.isMasterClient) 
				{
					PhotonNetwork.LoadLevel ("Loading Animator");
				}
			}

		}

		public void ClickButton(Button bt)
		{
			Role1Bt = GameObject.Find ("Role1Bt-4").GetComponent<Button> ();
			Role2Bt = GameObject.Find ("Role2Bt-4").GetComponent<Button> ();
			Role3Bt = GameObject.Find ("Role3Bt-4").GetComponent<Button> ();
			Role4Bt = GameObject.Find ("Role4Bt-4").GetComponent<Button> ();
			MapChoosed = (string)PhotonNetwork.room.CustomProperties ["Map"];

			if (MapChoosed.Equals ("Classic")) 
			{
				if (bt.Equals (Role1Bt))
				{
					bt.GetComponent<Image> ().sprite = BackWithFrame;
					Role2Bt.GetComponent<Image> ().sprite = RoleBack2;
					Role3Bt.GetComponent<Image> ().sprite = RoleBack3;
					Role4Bt.GetComponent<Image> ().sprite = RoleBack4;
				}
				else if (bt.Equals (Role2Bt))
				{
					bt.GetComponent<Image> ().sprite = BackWithFrame;
					Role1Bt.GetComponent<Image> ().sprite = RoleBack1;
					Role3Bt.GetComponent<Image> ().sprite = RoleBack3;
					Role4Bt.GetComponent<Image> ().sprite = RoleBack4;
				}
				else if (bt.Equals (Role3Bt))
				{
					bt.GetComponent<Image> ().sprite = BackWithFrame;
					Role1Bt.GetComponent<Image> ().sprite = RoleBack1;
					Role2Bt.GetComponent<Image> ().sprite = RoleBack2;
					Role4Bt.GetComponent<Image> ().sprite = RoleBack4;
				}
				else if (bt.Equals (Role4Bt))
				{
					bt.GetComponent<Image> ().sprite = BackWithFrame;
					Role1Bt.GetComponent<Image> ().sprite = RoleBack1;
					Role2Bt.GetComponent<Image> ().sprite = RoleBack2;
					Role3Bt.GetComponent<Image> ().sprite = RoleBack3;
				}
			}
			else if (MapChoosed.Equals ("PowerCut")) 
			{
				if (bt.Equals (Role1Bt))
				{
					bt.GetComponent<Image> ().sprite = BackWithFrame;
					Role2Bt.GetComponent<Image> ().sprite = RoleBack2ForPowerCut;
					Role3Bt.GetComponent<Image> ().sprite = RoleBack3ForPowerCut;
					Role4Bt.GetComponent<Image> ().sprite = RoleBack4ForPowerCut;
				}
				else if (bt.Equals (Role2Bt))
				{
					bt.GetComponent<Image> ().sprite = BackWithFrame;
					Role1Bt.GetComponent<Image> ().sprite = RoleBack1ForPowerCut;
					Role3Bt.GetComponent<Image> ().sprite = RoleBack3ForPowerCut;
					Role4Bt.GetComponent<Image> ().sprite = RoleBack4ForPowerCut;
				}
				else if (bt.Equals (Role3Bt))
				{
					bt.GetComponent<Image> ().sprite = BackWithFrame;
					Role1Bt.GetComponent<Image> ().sprite = RoleBack1ForPowerCut;
					Role2Bt.GetComponent<Image> ().sprite = RoleBack2ForPowerCut;
					Role4Bt.GetComponent<Image> ().sprite = RoleBack4ForPowerCut;
				}
				else if (bt.Equals (Role4Bt))
				{
					bt.GetComponent<Image> ().sprite = BackWithFrame;
					Role1Bt.GetComponent<Image> ().sprite = RoleBack1ForPowerCut;
					Role2Bt.GetComponent<Image> ().sprite = RoleBack2ForPowerCut;
					Role3Bt.GetComponent<Image> ().sprite = RoleBack3ForPowerCut;
				}
			}


		}


		public void ClickChoose()
		{
			PlayerList = new List<PhotonPlayer>();
			PlayerList.Add (PhotonNetwork.masterClient);
			PlayerList.Add (PhotonNetwork.masterClient.GetNext());
			PlayerList.Add (PhotonNetwork.masterClient.GetNext().GetNext());
			PlayerList.Add (PhotonNetwork.masterClient.GetNext().GetNext().GetNext());

			string Player1Choose = (string) PlayerList [0].CustomProperties ["Role"];
			string Player2Choose = (string) PlayerList [1].CustomProperties ["Role"];
			string Player3Choose = (string) PlayerList [2].CustomProperties ["Role"];
			string Player4Choose = (string) PlayerList [3].CustomProperties ["Role"];

			MapChoosed = (string)PhotonNetwork.room.CustomProperties ["Map"];

			//先取得UI元件
			AfterChooseTx1 = GameObject.Find ("AfterChooseTx1-4").GetComponent<Text> ();
			AfterChooseTx2 = GameObject.Find ("AfterChooseTx2-4").GetComponent<Text> ();
			AfterChooseTx3 = GameObject.Find ("AfterChooseTx3-4").GetComponent<Text> ();
			AfterChooseTx4 = GameObject.Find ("AfterChooseTx4-4").GetComponent<Text> ();
			Role1Bt = GameObject.Find ("Role1Bt-4").GetComponent<Button> ();
			Role2Bt = GameObject.Find ("Role2Bt-4").GetComponent<Button> ();
			Role3Bt = GameObject.Find ("Role3Bt-4").GetComponent<Button> ();
			Role4Bt = GameObject.Find ("Role4Bt-4").GetComponent<Button> ();
			ChooseBt = GameObject.Find ("ChooseBt-4").GetComponent<Button> ();


			//關卡是經典模式
			if (MapChoosed.Equals ("Classic")) 
			{
				//選擇第一個角色
				if (Role1Bt.GetComponent<Image> ().sprite == BackWithFrame)
				{
					
					if (Player1Choose == "洪咻柱" || Player2Choose == "洪咻柱" || Player3Choose == "洪咻柱" || Player4Choose == "洪咻柱")
					{
						onTips ("此角色已被選擇");
						Role1Bt.interactable = false;
					}
					else 
					{
						//儲存黨派顏色和選擇的角色
						hash = new Hashtable();
						PartyColor = "blue";
						RoleChoosed = "洪咻柱";
						hash.Add("PartyColor", PartyColor);
						hash.Add ("Role",RoleChoosed);
						PhotonNetwork.player.SetCustomProperties(hash);

						//更改panel顏色和文字
						GameObject.Find ("AfterChoosePanel1-4").GetComponent<Image> ().color = new Color32(255,255,255,255);
						AfterChooseTx1.text = "你選擇了 '洪咻柱'";

						Role1Bt.interactable = false;
						Role2Bt.interactable = false;
						Role3Bt.interactable = false;
						Role4Bt.interactable = false;
						ChooseBt.interactable = false;
					}


				} 
				//選擇第二個角色
				else if (Role2Bt.GetComponent<Image> ().sprite == BackWithFrame)
				{
					
					if (Player1Choose == "吳指癢" || Player2Choose == "吳指癢" || Player3Choose == "吳指癢" || Player4Choose == "吳指癢") 
					{
						onTips ("此角色已被選擇");
						Role2Bt.interactable = false;
					} 
					else
					{
						//儲存黨派顏色和選擇的角色
						hash = new Hashtable();
						PartyColor = "blue";
						RoleChoosed = "吳指癢";
						hash.Add("PartyColor", PartyColor);
						hash.Add ("Role",RoleChoosed);
						PhotonNetwork.player.SetCustomProperties(hash);

						//更改panel顏色和文字
						GameObject.Find ("AfterChoosePanel2-4").GetComponent<Image> ().color = new Color32(255,255,255,255);
						AfterChooseTx2.text = "你選擇了 '吳指癢'";

						Role1Bt.interactable = false;
						Role2Bt.interactable = false;
						Role3Bt.interactable = false;
						Role4Bt.interactable = false;
						ChooseBt.interactable = false;

					}

				} 
				//選擇第三個角色
				else if (Role3Bt.GetComponent<Image> ().sprite == BackWithFrame)
				{

					if (Player1Choose == "蔡中聞" || Player2Choose == "蔡中聞" || Player3Choose == "蔡中聞" || Player4Choose == "蔡中聞")
					{
						onTips ("此角色已被選擇");
						Role3Bt.interactable = false;
					} 
					else 
					{
						//儲存黨派顏色和選擇的角色
						hash = new Hashtable ();
						PartyColor = "green";
						RoleChoosed = "蔡中聞";
						hash.Add ("PartyColor", PartyColor);
						hash.Add ("Role", RoleChoosed);
						PhotonNetwork.player.SetCustomProperties (hash);

						//更改panel顏色和文字
						GameObject.Find ("AfterChoosePanel3-4").GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
						AfterChooseTx3.text = "你選擇了 '蔡中聞'";

						Role1Bt.interactable = false;
						Role2Bt.interactable = false;
						Role3Bt.interactable = false;
						Role4Bt.interactable = false;
						ChooseBt.interactable = false;
					}

				} 
				//選擇第四個角色
				else if (Role4Bt.GetComponent<Image> ().sprite == BackWithFrame)
				{

					if (Player1Choose == "蘇嘎拳" || Player2Choose == "蘇嘎拳" || Player3Choose == "蘇嘎拳" || Player4Choose == "蘇嘎拳")
					{
						onTips ("此角色已被選擇");
						Role4Bt.interactable = false;
					} 
					else
					{
						//儲存黨派顏色和選擇的角色
						hash = new Hashtable ();
						PartyColor = "green";
						RoleChoosed = "蘇嘎拳";
						hash.Add ("PartyColor", PartyColor);
						hash.Add ("Role", RoleChoosed);
						PhotonNetwork.player.SetCustomProperties (hash);

						//更改panel顏色和文字
						GameObject.Find ("AfterChoosePanel4-4").GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
						AfterChooseTx4.text = "你選擇了 '蘇嘎拳'";

						Role1Bt.interactable = false;
						Role2Bt.interactable = false;
						Role3Bt.interactable = false;
						Role4Bt.interactable = false;
						ChooseBt.interactable = false;
					}

				} 
			}
			//關卡是815大停電
			else if (MapChoosed.Equals ("PowerCut")) 
			{
				
				//選擇第一個角色
				if (Role1Bt.GetComponent<Image> ().sprite == BackWithFrame)
				{
					
					if (Player1Choose == "陳金德" || Player2Choose == "陳金德" || Player3Choose == "陳金德" || Player4Choose == "陳金德") 
					{
						onTips ("此角色已被選擇");
						Role1Bt.interactable = false;
					} 
					else 
					{
						//儲存黨派顏色和選擇的角色
						hash = new Hashtable ();
						PartyColor = "red";
						RoleChoosed = "陳金德";
						hash.Add ("PartyColor", PartyColor);
						hash.Add ("Role", RoleChoosed);
						PhotonNetwork.player.SetCustomProperties (hash);

						//更改panel顏色和文字
						GameObject.Find ("AfterChoosePanel1-4").GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
						AfterChooseTx1.text = "你選擇了 '中油董事長'";

						Role1Bt.interactable = false;
						Role2Bt.interactable = false;
						Role3Bt.interactable = false;
						Role4Bt.interactable = false;
						ChooseBt.interactable = false;
					}

				} 
				//選擇第二個角色
				else if (Role2Bt.GetComponent<Image> ().sprite == BackWithFrame)
				{
					
					if (Player1Choose == "朱文成" || Player2Choose == "朱文成" || Player3Choose == "朱文成" || Player4Choose == "朱文成") 
					{
						onTips ("此角色已被選擇");
						Role2Bt.interactable = false;
					} 
					else 
					{
						//儲存黨派顏色和選擇的角色
						hash = new Hashtable ();
						PartyColor = "yellow";
						RoleChoosed = "朱文成";
						hash.Add ("PartyColor", PartyColor);
						hash.Add ("Role", RoleChoosed);
						PhotonNetwork.player.SetCustomProperties (hash);

						//更改panel顏色和文字
						GameObject.Find ("AfterChoosePanel2-4").GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
						AfterChooseTx2.text = "你選擇了 '台電董事長'";

						Role1Bt.interactable = false;
						Role2Bt.interactable = false;
						Role3Bt.interactable = false;
						Role4Bt.interactable = false;
						ChooseBt.interactable = false;
					}

				} 
				//選擇第三個角色
				else if (Role3Bt.GetComponent<Image> ().sprite == BackWithFrame)
				{
					
					if (Player1Choose == "蔡中聞" || Player2Choose == "蔡中聞" || Player3Choose == "蔡中聞" || Player4Choose == "蔡中聞")
					{
						onTips ("此角色已被選擇");
						Role3Bt.interactable = false;
					} 
					else 
					{
						//儲存黨派顏色和選擇的角色
						hash = new Hashtable ();
						PartyColor = "green";
						RoleChoosed = "蔡中聞";
						hash.Add ("PartyColor", PartyColor);
						hash.Add ("Role", RoleChoosed);
						PhotonNetwork.player.SetCustomProperties (hash);

						//更改panel顏色和文字
						GameObject.Find ("AfterChoosePanel3-4").GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
						AfterChooseTx3.text = "你選擇了 '蔡中聞'";

						Role1Bt.interactable = false;
						Role2Bt.interactable = false;
						Role3Bt.interactable = false;
						Role4Bt.interactable = false;
						ChooseBt.interactable = false;
					}

				} 
				//選擇第四個角色
				else if (Role4Bt.GetComponent<Image> ().sprite == BackWithFrame)
				{

					if (Player1Choose == "承包商" || Player2Choose == "承包商" || Player3Choose == "承包商" || Player4Choose == "承包商")
					{
						onTips ("此角色已被選擇");
						Role4Bt.interactable = false;
					} 
					else
					{
						//儲存黨派顏色和選擇的角色
						hash = new Hashtable ();
						PartyColor = "purple";
						RoleChoosed = "承包商";
						hash.Add ("PartyColor", PartyColor);
						hash.Add ("Role", RoleChoosed);
						PhotonNetwork.player.SetCustomProperties (hash);

						//更改panel顏色和文字
						GameObject.Find ("AfterChoosePanel4-4").GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
						AfterChooseTx4.text = "你選擇了 '巨路承包商'";

						Role1Bt.interactable = false;
						Role2Bt.interactable = false;
						Role3Bt.interactable = false;
						Role4Bt.interactable = false;
						ChooseBt.interactable = false;

					}
				} 
			}


		}

		public void onTips(string tips_str)
		{
			GameObject parent = GameObject.Find ("Canvas");
			GameObject toast = GameObject.Find ("Toast"); // 加载预制体
			GameObject m_toast = GameObject.Instantiate(toast, parent.transform, false);  // 对象初始化
			//m_toast.transform.parent = parent.transform;            //　附加到父节点（需要显示的UI下）
			m_toast.transform.localScale = Vector3.one;
			m_toast.transform.localPosition = new Vector3 (144.0f, -260.0f, 0.0f);
			Text tips = m_toast.GetComponent<Text>();
			tips.text = tips_str;
			Destroy(m_toast, 2); // 2秒后 销毁
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
