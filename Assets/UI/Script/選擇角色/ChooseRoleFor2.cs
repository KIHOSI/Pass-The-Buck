using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UI;


namespace Com.MyProject.MyPassTheBuckGame
{
	public class ChooseRoleFor2 : Photon.PunBehaviour
	{


		public List<PhotonPlayer> PlayerList;

		//經典關卡按鈕背景圖
		public Sprite RoleBack1;
		public Sprite RoleBack2;
		public Sprite RoleBack3;
		public Sprite RoleBack4;
		//點按鈕後的背景圖
		public Sprite BackWithFrame;


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


		void Start () 
		{

			//將房間裡的player儲存到player list
			PlayerList = new List<PhotonPlayer>();
			PlayerList.Add (PhotonNetwork.masterClient);
			PlayerList.Add (PhotonNetwork.masterClient.GetNext());

			MapChoosed = (string)PhotonNetwork.room.CustomProperties ["Map"];


		}


		void Update () 
		{
			
			string Player1Choose = (string) PlayerList [0].CustomProperties ["Role"];
			string Player2Choose = (string) PlayerList [1].CustomProperties ["Role"];


			//關卡選擇經典模式
			if (MapChoosed.Equals("Classic"))
			{
				if (Player1Choose == "洪咻柱" || Player2Choose == "洪咻柱")
				{
					Role1Bt.interactable = false;
					Role2Bt.interactable = false;
				}
				else if (Player1Choose == "吳指癢" || Player2Choose == "吳指癢")
				{
					Role1Bt.interactable = false;
					Role2Bt.interactable = false;
				}
				else if (Player1Choose == "蔡中聞" || Player2Choose == "蔡中聞" )
				{
					Role3Bt.interactable = false;
					Role4Bt.interactable = false;
				}
				else if (Player1Choose == "蘇嘎拳" || Player2Choose == "蘇嘎拳" )
				{
					Role3Bt.interactable = false;
					Role4Bt.interactable = false;
				}
			} 


			if (Player1Choose !=null && Player2Choose !=null ) 
			{
				if (PhotonNetwork.isMasterClient) 
				{
					PhotonNetwork.LoadLevel ("Loading Animator");
				}
			}

		}

		public void ClickButton(Button bt)
		{
			Role1Bt = GameObject.Find ("Role1Bt-2").GetComponent<Button> ();
			Role2Bt = GameObject.Find ("Role2Bt-2").GetComponent<Button> ();
			Role3Bt = GameObject.Find ("Role3Bt-2").GetComponent<Button> ();
			Role4Bt = GameObject.Find ("Role4Bt-2").GetComponent<Button> ();

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

		}


		public void ClickChoose()
		{
			PlayerList = new List<PhotonPlayer>();
			PlayerList.Add (PhotonNetwork.masterClient);
			PlayerList.Add (PhotonNetwork.masterClient.GetNext());

			string Player1Choose = (string) PlayerList [0].CustomProperties ["Role"];
			string Player2Choose = (string) PlayerList [1].CustomProperties ["Role"];

			MapChoosed = (string)PhotonNetwork.room.CustomProperties ["Map"];

			//先取得UI元件
			AfterChooseTx1 = GameObject.Find ("AfterChooseTx1-2").GetComponent<Text> ();
			AfterChooseTx2 = GameObject.Find ("AfterChooseTx2-2").GetComponent<Text> ();
			AfterChooseTx3 = GameObject.Find ("AfterChooseTx3-2").GetComponent<Text> ();
			AfterChooseTx4 = GameObject.Find ("AfterChooseTx4-2").GetComponent<Text> ();
			Role1Bt = GameObject.Find ("Role1Bt-2").GetComponent<Button> ();
			Role2Bt = GameObject.Find ("Role2Bt-2").GetComponent<Button> ();
			Role3Bt = GameObject.Find ("Role3Bt-2").GetComponent<Button> ();
			Role4Bt = GameObject.Find ("Role4Bt-2").GetComponent<Button> ();
			ChooseBt = GameObject.Find ("ChooseBt-2").GetComponent<Button> ();


			//關卡是經典模式
			if (MapChoosed.Equals ("Classic")) 
			{
				//選擇第一個角色
				if (Role1Bt.GetComponent<Image> ().sprite == BackWithFrame)
				{

					if (Player1Choose == "洪咻柱" || Player2Choose == "洪咻柱")
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
						GameObject.Find ("AfterChoosePanel1-2").GetComponent<Image> ().color = new Color32(255,255,255,255);
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

					if (Player1Choose == "吳指癢" || Player2Choose == "吳指癢") 
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
						GameObject.Find ("AfterChoosePanel2-2").GetComponent<Image> ().color = new Color32(255,255,255,255);
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

					if (Player1Choose == "蔡中聞" || Player2Choose == "蔡中聞")
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
						GameObject.Find ("AfterChoosePanel3-2").GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
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

					if (Player1Choose == "蘇嘎拳" || Player2Choose == "蘇嘎拳")
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
						GameObject.Find ("AfterChoosePanel4-2").GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
						AfterChooseTx4.text = "你選擇了 '蘇嘎拳'";

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
