using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UI;


namespace Com.MyProject.MyPassTheBuckGame
{

	public class WinOrLose : MonoBehaviour
	{
		
		public List<PhotonPlayer> PlayerList;

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

		public Sprite SmileRedRole;
		public Sprite SmileYellowRole;
		public Sprite SmileGreenRole;
		public Sprite SmilePurpleRole;
		public Sprite CryRedRole;
		public Sprite CryYellowRole;
		public Sprite CryGreenRole;
		public Sprite CryPurpleRole;

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
		AudioSource audiosre;
		public int count=0;
		public Hashtable hash;

		public int money;
		public float exp;
		public float maxExp;
		public string map;
		public string currentlevel;



		void Start () 
		{

			GetMaxExp ();
			map = (string)PhotonNetwork.room.CustomProperties ["Map"];

			if (map.Equals ("Classic"))
			{
				//玩家的黨派贏了
				if (GetWinColor ().Equals ((string)PhotonNetwork.player.CustomProperties ["PartyColor"]))
				{
					
					audiosre = GameObject.Find ("GameWin").GetComponent<AudioSource> ();
					audiosre.Play ();

					currentlevel = PlayerPrefs.GetString ("PlayerLevel");

					//稱號是新進議員
					if (currentlevel.Equals("1") || currentlevel.Equals("2"))
					{
						Debug.Log ("我有進來!");
						//改變外觀
						RoleImg.sprite = GetWinPlayerRole ();
						GetMoneyTx.text = "你在遊戲中獲得了" + PhotonNetwork.player.CustomProperties ["Money"].ToString () + "百萬";
						MoneyPlusTx.text = "+20(百萬)";
						LevelPlusTx.text = "+3";

						//設定錢錢
						money = PlayerPrefs.GetInt ("PlayerMoney") + 20;
						PlayerPrefs.SetInt ("PlayerMoney", money);

						//設定經驗值
						exp = PlayerPrefs.GetFloat ("PlayerExp") + 3;
						PlayerPrefs.SetFloat ("PlayerExp", exp);
					}
					//稱號是地方議員，每場錢錢額外加5百萬
					else if (currentlevel.Equals("3") || currentlevel.Equals("4") || currentlevel.Equals("5"))
					{
						//改變外觀
						RoleImg.sprite = GetWinPlayerRole ();
						GetMoneyTx.text = "你在遊戲中獲得了" + PhotonNetwork.player.CustomProperties ["Money"].ToString () + "百萬";
						MoneyPlusTx.text = "+25(百萬)";
						LevelPlusTx.text = "+3";

						//設定錢錢
						money = PlayerPrefs.GetInt ("PlayerMoney") + 25;
						PlayerPrefs.SetInt ("PlayerMoney", money);

						//設定經驗值
						exp = PlayerPrefs.GetFloat ("PlayerExp") + 3;
						PlayerPrefs.SetFloat ("PlayerExp", exp);
					}
					//稱號是立法委員，每場錢錢額外加15百萬
					else if (currentlevel.Equals("6") || currentlevel.Equals("7") || currentlevel.Equals("8"))
					{
						//改變外觀
						RoleImg.sprite = GetWinPlayerRole ();
						GetMoneyTx.text = "你在遊戲中獲得了" + PhotonNetwork.player.CustomProperties ["Money"].ToString () + "百萬";
						MoneyPlusTx.text = "+35(百萬)";
						LevelPlusTx.text = "+3";

						//設定錢錢
						money = PlayerPrefs.GetInt ("PlayerMoney") + 35;
						PlayerPrefs.SetInt ("PlayerMoney", money);

						//設定經驗值
						exp = PlayerPrefs.GetFloat ("PlayerExp") + 3;
						PlayerPrefs.SetFloat ("PlayerExp", exp);
					}
					//稱號是立法院長，每場錢錢額外加25百萬
					else if (currentlevel.Equals("9") || currentlevel.Equals("10") || currentlevel.Equals("11"))
					{
						//改變外觀
						RoleImg.sprite = GetWinPlayerRole ();
						GetMoneyTx.text = "你在遊戲中獲得了" + PhotonNetwork.player.CustomProperties ["Money"].ToString () + "百萬";
						MoneyPlusTx.text = "+45(百萬)";
						LevelPlusTx.text = "+3";

						//設定錢錢
						money = PlayerPrefs.GetInt ("PlayerMoney") + 45;
						PlayerPrefs.SetInt ("PlayerMoney", money);

						//設定經驗值
						exp = PlayerPrefs.GetFloat ("PlayerExp") + 3;
						PlayerPrefs.SetFloat ("PlayerExp", exp);
					}
					//稱號是總統，每場錢錢額外加50百萬
					else if (currentlevel.Equals("12"))
					{
						//改變外觀
						RoleImg.sprite = GetWinPlayerRole ();
						GetMoneyTx.text = "你在遊戲中獲得了" + PhotonNetwork.player.CustomProperties ["Money"].ToString () + "百萬";
						MoneyPlusTx.text = "+70(百萬)";
						LevelPlusTx.text = "+3";

						//設定錢錢
						money = PlayerPrefs.GetInt ("PlayerMoney") + 70;
						PlayerPrefs.SetInt ("PlayerMoney", money);

						//設定經驗值
						exp = PlayerPrefs.GetFloat ("PlayerExp") + 3;
						PlayerPrefs.SetFloat ("PlayerExp", exp);
					}


					//加完後的經驗值已滿
				    if (exp >= maxExp) 
					{
						int level = int.Parse (PlayerPrefs.GetString ("PlayerLevel"));

						if ( level < 12)
						{
							//重設level
							PlayerPrefs.SetString ("PlayerLevel", (int.Parse (PlayerPrefs.GetString ("PlayerLevel")) + 1).ToString ());
						}

					}

					PhotonNetwork.LeaveRoom ();

				} 
				else if (!GetWinColor ().Equals ((string)PhotonNetwork.player.CustomProperties ["PartyColor"]))
				{
					//雙方平手
					if (GetWinColor ().Equals ("same")) 
					{

						audiosre = GameObject.Find ("GameLose").GetComponent<AudioSource> ();
						audiosre.Play ();

						RoleImg.sprite = GetWinPlayerRole ();
						CrownImg.color = new Color32 (255, 255, 255, 0);
						GetMoneyTx.text = "";
						GameObject.Find ("Background Panel1").GetComponent<Image> ().sprite = TieBackground;
						WinOrLoseImg.sprite = TieTx;
						DialogImg.color = new Color32 (255, 255, 255, 255);
						RoleTalkTx.text = "下次一定要拚回來><!";
						MoneyPlusTx.text = "+0(百萬)";
						LevelPlusTx.text = "+0";

						PhotonNetwork.LeaveRoom ();

					}
					//玩家的黨派輸了
					else 
					{
						audiosre = GameObject.Find ("GameLose").GetComponent<AudioSource> ();
						audiosre.Play ();

						//改變外觀
						RoleImg.sprite = GetLosePlayerRole ();
						GetMoneyTx.text = "你在遊戲中獲得了" + PhotonNetwork.player.CustomProperties ["Money"].ToString () + "百萬";
						GameObject.Find ("Background Panel1").GetComponent<Image> ().sprite = LoseBackground;
						WinOrLoseImg.sprite = LoseTx;
						CrownImg.color = new Color32 (255, 255, 255, 0);
						BackImg.sprite = LoseStatusBack;
						MoneyPlusTx.text = "+5(百萬)";
						LevelPlusTx.text = "+1";

						//設定錢錢
						money = PlayerPrefs.GetInt ("PlayerMoney") + 5;
						PlayerPrefs.SetInt ("PlayerMoney", money);

						//設定經驗值
						exp = PlayerPrefs.GetFloat ("PlayerExp") + 1;
						PlayerPrefs.SetFloat ("PlayerExp", exp);

						//加完後的經驗值已滿
						if (exp >= maxExp) 
						{
							
							int level = int.Parse (PlayerPrefs.GetString ("PlayerLevel"));

							if ( level < 12)
							{
								//重設level
								PlayerPrefs.SetString ("PlayerLevel", (int.Parse (PlayerPrefs.GetString ("PlayerLevel")) + 1).ToString ());
							}
						}

						PhotonNetwork.LeaveRoom ();

					}

				}
			} 
			else if (map.Equals ("PowerCut"))
			{
				//玩家贏了
				if (GetWinPlayerForPowerCut ().Equals (PhotonNetwork.player)) 
				{
					audiosre = GameObject.Find ("GameWin").GetComponent<AudioSource> ();
					audiosre.Play ();

					currentlevel = (string)PlayerPrefs.GetString ("PlayerLevel");

					if (currentlevel.Equals("1") || currentlevel.Equals("2"))
					{
						//改變外觀
						RoleImg.sprite = GetWinPlayerRoleForPowerCut ();
						GetMoneyTx.text = "你在遊戲中獲得了" + PhotonNetwork.player.CustomProperties ["Money"].ToString () + "百萬";
						MoneyPlusTx.text = "+20(百萬)";
						LevelPlusTx.text = "+3";

						//設定錢錢
						money = PlayerPrefs.GetInt ("PlayerMoney") + 20;
						PlayerPrefs.SetInt ("PlayerMoney", money);

						//設定經驗值
						exp = PlayerPrefs.GetFloat ("PlayerExp") + 3;
						PlayerPrefs.SetFloat ("PlayerExp", exp);
					}
					else if (currentlevel.Equals("3") || currentlevel.Equals("4") || currentlevel.Equals("5"))
					{
						//改變外觀
						RoleImg.sprite = GetWinPlayerRoleForPowerCut ();
						GetMoneyTx.text = "你在遊戲中獲得了" + PhotonNetwork.player.CustomProperties ["Money"].ToString () + "百萬";
						MoneyPlusTx.text = "+25(百萬)";
						LevelPlusTx.text = "+3";

						//設定錢錢
						money = PlayerPrefs.GetInt ("PlayerMoney") + 25;
						PlayerPrefs.SetInt ("PlayerMoney", money);

						//設定經驗值
						exp = PlayerPrefs.GetFloat ("PlayerExp") + 3;
						PlayerPrefs.SetFloat ("PlayerExp", exp);

					}
					else if (currentlevel.Equals("6") || currentlevel.Equals("7") || currentlevel.Equals("8"))
					{
						//改變外觀
						RoleImg.sprite = GetWinPlayerRoleForPowerCut ();
						GetMoneyTx.text = "你在遊戲中獲得了" + PhotonNetwork.player.CustomProperties ["Money"].ToString () + "百萬";
						MoneyPlusTx.text = "+35(百萬)";
						LevelPlusTx.text = "+3";

						//設定錢錢
						money = PlayerPrefs.GetInt ("PlayerMoney") + 35;
						PlayerPrefs.SetInt ("PlayerMoney", money);

						//設定經驗值
						exp = PlayerPrefs.GetFloat ("PlayerExp") + 3;
						PlayerPrefs.SetFloat ("PlayerExp", exp);

					}
					else if (currentlevel.Equals("9") || currentlevel.Equals("10") || currentlevel.Equals("11"))
					{
						//改變外觀
						RoleImg.sprite = GetWinPlayerRoleForPowerCut ();
						GetMoneyTx.text = "你在遊戲中獲得了" + PhotonNetwork.player.CustomProperties ["Money"].ToString () + "百萬";
						MoneyPlusTx.text = "+45(百萬)";
						LevelPlusTx.text = "+3";

						//設定錢錢
						money = PlayerPrefs.GetInt ("PlayerMoney") + 45;
						PlayerPrefs.SetInt ("PlayerMoney", money);

						//設定經驗值
						exp = PlayerPrefs.GetFloat ("PlayerExp") + 3;
						PlayerPrefs.SetFloat ("PlayerExp", exp);

					}
					else if (currentlevel.Equals("12"))
					{
						//改變外觀
						RoleImg.sprite = GetWinPlayerRoleForPowerCut ();
						GetMoneyTx.text = "你在遊戲中獲得了" + PhotonNetwork.player.CustomProperties ["Money"].ToString () + "百萬";
						MoneyPlusTx.text = "+70(百萬)";
						LevelPlusTx.text = "+3";

						//設定錢錢
						money = PlayerPrefs.GetInt ("PlayerMoney") + 70;
						PlayerPrefs.SetInt ("PlayerMoney", money);

						//設定經驗值
						exp = PlayerPrefs.GetFloat ("PlayerExp") + 3;
						PlayerPrefs.SetFloat ("PlayerExp", exp);
					}


					//加完後的經驗值已滿
					if (exp >= maxExp)
					{
						int level = int.Parse (PlayerPrefs.GetString ("PlayerLevel"));

						if ( level < 12)
						{
							//重設level
							PlayerPrefs.SetString ("PlayerLevel", (int.Parse (PlayerPrefs.GetString ("PlayerLevel")) + 1).ToString ());
						}
					}

					PhotonNetwork.LeaveRoom ();

				} 
				else if (!GetWinPlayerForPowerCut ().Equals (PhotonNetwork.player))
				{
					
					    audiosre = GameObject.Find ("GameLose").GetComponent<AudioSource> ();
					    audiosre.Play ();
				
						//改變外觀
					    RoleImg.sprite = GetLosePlayerRoleForPowerCut ();
						GetMoneyTx.text = "你在遊戲中獲得了" + PhotonNetwork.player.CustomProperties ["Money"].ToString () + "百萬";
						GameObject.Find ("Background Panel1").GetComponent<Image> ().sprite = LoseBackground;
						WinOrLoseImg.sprite = LoseTx;
						CrownImg.color = new Color32 (255, 255, 255, 0);
						BackImg.sprite = LoseStatusBack;
						MoneyPlusTx.text = "+5(百萬)";
						LevelPlusTx.text = "+1";

						//設定錢錢
						money = PlayerPrefs.GetInt ("PlayerMoney") + 5;
						PlayerPrefs.SetInt ("PlayerMoney", money);

						//設定經驗值
						exp = PlayerPrefs.GetFloat ("PlayerExp") + 1;
						PlayerPrefs.SetFloat ("PlayerExp", exp);

						//加完後的經驗值已滿
						if (exp >= maxExp)
					    {
						   int level = int.Parse (PlayerPrefs.GetString ("PlayerLevel"));

						   if ( level < 12)
						  {
							 //重設level
							 PlayerPrefs.SetString ("PlayerLevel", (int.Parse (PlayerPrefs.GetString ("PlayerLevel")) + 1).ToString ());
						  }
						}

						PhotonNetwork.LeaveRoom ();


				}
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


		public void GetMaxExp()
		{
			string currentLevel = PlayerPrefs.GetString ("PlayerLevel");

			if (currentLevel == "1")
			{
				maxExp = 15;
			} 
			else if (currentLevel == "2") 
			{
				maxExp = 25;
			}
			else if (currentLevel == "3")
			{
				maxExp = 40;
			}
			else if (currentLevel == "4")
			{
				maxExp = 50;
			}
			else if (currentLevel == "5")
			{
				maxExp = 60;
			}
			else if (currentLevel == "6")
			{
				maxExp = 75;
			}
			else if (currentLevel == "7")
			{
				maxExp = 85;
			}
			else if (currentLevel == "8")
			{
				maxExp = 95;
			}
			else if (currentLevel == "9")
			{
				maxExp = 110;
			}
			else if (currentLevel == "10")
			{
				maxExp = 120;
			}
			else if (currentLevel == "11")
			{
				maxExp = 130;
			}
			else if (currentLevel == "12")
			{
				maxExp = 145;
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

		public Sprite GetWinPlayerRoleForPowerCut()
		{
			string PlayerRole = (string)PhotonNetwork.player.CustomProperties ["Role"];

			if (PlayerRole == "陳金德")
			{
				return SmileRedRole;
			} 
			else if (PlayerRole == "朱文成") 
			{
				return SmileYellowRole;
			} 
			else if (PlayerRole == "蔡中聞")
			{
				return SmileGreenRole;
			} 
			else if (PlayerRole == "承包商")
			{
				return SmilePurpleRole;
			} 
			else 
			{
				return null;
			}

		}

		public Sprite GetLosePlayerRoleForPowerCut()
		{
			string PlayerRole = (string)PhotonNetwork.player.CustomProperties ["Role"];

			if (PlayerRole == "陳金德")
			{
				return CryRedRole;
			} 
			else if (PlayerRole == "朱文成") 
			{
				return CryYellowRole;
			} 
			else if (PlayerRole == "蔡中聞")
			{
				return CryGreenRole;
			} 
			else if (PlayerRole == "承包商")
			{
				return CryPurpleRole;
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
	
		public PhotonPlayer GetWinPlayerForPowerCut()
		{
			PlayerList = new List<PhotonPlayer>();
			PlayerList.Add (PhotonNetwork.masterClient);
			PlayerList.Add (PhotonNetwork.masterClient.GetNext());
			PlayerList.Add (PhotonNetwork.masterClient.GetNext().GetNext());
			PlayerList.Add (PhotonNetwork.masterClient.GetNext().GetNext().GetNext());

			int winplayer = (int)PhotonNetwork.player.CustomProperties ["WinPlayer"];

			if (winplayer == 1) 
			{
				return PlayerList[0];
			}
			else if (winplayer == 2) 
			{
				return PlayerList[1];
			} 
			else if (winplayer == 3) 
			{
				return PlayerList[2];
			} 
			else if (winplayer == 4) 
			{
				return PlayerList[3];
			} 
			else 
			{
				return null;
			}
		}


		public void ExitGame()
		{
			
			hash = new Hashtable();
			hash.Add("PartyColor", null);
			hash.Add ("Role",null);
			hash.Add ("WinColor",null);
			hash.Add ("WinPlayer",null);
			hash.Add ("Money",null);

			if (PhotonNetwork.isMasterClient) 
			{
				hash.Add("ClickStart", null);
				hash.Add("Map", null);
			}
				
			PhotonNetwork.player.SetCustomProperties(hash); 

			audiosre = GameObject.Find ("BackGroundMusic").GetComponent<AudioSource> ();
			audiosre.Play ();
			PhotonNetwork.LeaveRoom ();
			SceneManager.LoadScene ("Main");
		}
	}

}
