using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UI;


namespace Com.MyProject.MyPassTheBuckGame
{

	public class WinOrLose : Photon.PunBehaviour 
	{

		public Sprite WinBackground;
		public Sprite LoseBackground;
		public Sprite TieBackground;
		public Sprite LoseTx;
		public Sprite TieTx;
		public Sprite SmileBlueRole1;
		public Sprite SmileBlueRole2;
		public Sprite SmileGreenRole1;
		public Sprite SmileGreenRole2;
		public Sprite CryBlueRole1;
		public Sprite CryBlueRole2;
		public Sprite CryGreenRole1;
		public Sprite CryGreenRole2;
		public Sprite LoseStatusBack;
		public Image CrownImg;
		public Image WinOrLoseImg;
		public Image RoleImg;
		public Image BackImg;
		public Image DialogImg;
		public Text RoleTalkTx;
		public Text MoneyPlusTx;
		public Text LevelPlusTx;
		public Text GetMoneyTx;
		public Button ExitGameBt;
		AudioSource audiosre;
		public int count=0;
		public Hashtable hash;

		public int money;
		public float exp;
		public float maxExp;



		void Start () 
		{

			maxExp = GameObject.Find ("LevelBarTopImg").GetComponent<LevelBarForWl> ().MaxExp;


			//玩家的黨派贏了
			if (GetWinColor ().Equals ((string)PhotonNetwork.player.CustomProperties ["PartyColor"]))
			{
				//改變外觀
				RoleImg.sprite = GetWinPlayerRole ();
				GetMoneyTx.text = "你在遊戲中獲得了" + PhotonNetwork.player.CustomProperties ["Money"].ToString () + "百萬";
				MoneyPlusTx.text = "+20(百萬)";
				LevelPlusTx.text = "+3";

				//設定錢錢
				money = PlayerPrefs.GetInt ("PlayerMoney") + 20;
				PlayerPrefs.SetInt("PlayerMoney",money);

				//設定經驗值
				exp = PlayerPrefs.GetFloat ("PlayerExp") + 3;
				PlayerPrefs.SetFloat("PlayerExp",exp);

				//加完後的經驗值已滿
				if (exp >= maxExp) 
				{
					//重設level
					PlayerPrefs.SetString("PlayerLevel",(int.Parse(PlayerPrefs.GetString ("PlayerLevel"))+1).ToString());
					//重設經驗值
					PlayerPrefs.SetFloat("PlayerExp",exp-maxExp);

				}
			
			}
			else if (!GetWinColor ().Equals ((string)PhotonNetwork.player.CustomProperties ["PartyColor"])) 
			{
				//雙方平手
				if (GetWinColor ().Equals ("same")) 
				{
					RoleImg.sprite = GetWinPlayerRole ();
					CrownImg.color = new Color32(255,255,255,0);
					GetMoneyTx.text = "";
					GameObject.Find ("Background Panel1").GetComponent<Image> ().sprite = TieBackground;
					WinOrLoseImg.sprite = TieTx;
					DialogImg.color = new Color32(255,255,255,255);
					RoleTalkTx.text = "下次一定要拚回來><!";
					MoneyPlusTx.text = "+0(百萬)";
					LevelPlusTx.text = "+0";

				}
				//玩家的黨派輸了
				else 
				{
					//改變外觀
					RoleImg.sprite = GetLosePlayerRole ();
					GetMoneyTx.text = "你在遊戲中獲得了" + PhotonNetwork.player.CustomProperties ["Money"].ToString () + "百萬";
					GameObject.Find ("Background Panel1").GetComponent<Image> ().sprite = LoseBackground;
					WinOrLoseImg.sprite = LoseTx;
					CrownImg.color = new Color32(255,255,255,0);
					BackImg.sprite = LoseStatusBack;
					MoneyPlusTx.text = "+5(百萬)";
					LevelPlusTx.text = "+1";

					//設定錢錢
					money = PlayerPrefs.GetInt ("PlayerMoney") + 5;
					PlayerPrefs.SetInt("PlayerMoney",money);

					//設定經驗值
					exp = PlayerPrefs.GetFloat ("PlayerExp") + 1;
					PlayerPrefs.SetFloat("PlayerExp",exp);

					//加完後的經驗值已滿
					if (exp >= maxExp) 
					{
						//重設level
						PlayerPrefs.SetString("PlayerLevel",(int.Parse(PlayerPrefs.GetString ("PlayerLevel"))+1).ToString());
						//重設經驗值
						PlayerPrefs.SetFloat("PlayerExp",exp-maxExp);
					}

				}

				ExitGameBt.onClick.AddListener (ExitGame);

			}
		}

		void Update()
		{
			count++;
			Sprite s = GameObject.Find ("Background Panel1").GetComponent<Image> ().sprite;

			if (count == 400)
			{
				if (s.Equals(WinBackground)) 
				{
					GetMoneyTx.text = "";
					DialogImg.color = new Color32(255,255,255,255);
					RoleTalkTx.text = "謙卑謙卑再謙卑!";
				}
				else if (s.Equals(LoseBackground)) 
				{
					GetMoneyTx.text = "";
					DialogImg.color = new Color32(255,255,255,255);
					RoleTalkTx.text = "下次繼續努力!";
				}

			}
		}
			

		public Sprite GetWinPlayerRole()
		{
			string PlayerRole = (string)PhotonNetwork.player.CustomProperties ["Role"];

			if (PlayerRole == "洪咻柱")
			{
				return SmileBlueRole1;
			} 
			else if (PlayerRole == "吳指癢") 
			{
				return SmileBlueRole2;
			} 
			else if (PlayerRole == "蔡中聞")
			{
				return SmileGreenRole1;
			} 
			else if (PlayerRole == "蘇嘎拳")
			{
				return SmileGreenRole2;
			} 
			else 
			{
				return null;
			}

		}

		public Sprite GetLosePlayerRole()
		{
			string PlayerRole = (string)PhotonNetwork.player.CustomProperties ["Role"];

			if (PlayerRole == "洪咻柱")
			{
				return CryBlueRole1;
			} 
			else if (PlayerRole == "吳指癢") 
			{
				return CryBlueRole2;
			} 
			else if (PlayerRole == "蔡中聞")
			{
				return CryGreenRole1;
			} 
			else if (PlayerRole == "蘇嘎拳")
			{
				return CryGreenRole2;
			} 
			else 
			{
				return null;
			}

		}
			

		public string GetWinColor()
		{
			int wincolor = (int)PhotonNetwork.player.CustomProperties ["WinColor"];

			if (wincolor == 0) 
			{
				return "same";
			}
			else if (wincolor == 1) 
			{
				return "green";
			} 
			else if (wincolor == 2) 
			{
				return "blue";
			} 
			else 
			{
				return "null";
			}
		}


		void ExitGame()
		{
			hash = new Hashtable();
			hash.Add("PartyColor", null);
			hash.Add ("Role",null);
			hash.Add ("WinColor",null);
			hash.Add ("Money",null);

			if (PhotonNetwork.isMasterClient) 
			{
				hash.Add("ClickStart", null);
			}

			PhotonNetwork.player.SetCustomProperties(hash);

			audiosre = GameObject.Find ("BackGroundMusic").GetComponent<AudioSource> ();
			audiosre.Play ();
			SceneManager.LoadScene ("Main");
			PhotonNetwork.LeaveRoom ();
		}
	}

}
