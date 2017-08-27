using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyProject.MyPassTheBuckGame
{
	public class LevelBarForWl : MonoBehaviour {

		public float MaxExp;
		public float CurrentExp;
		public Text LevelBarTx;
		public float ExpPercent;
		public string currentLevel;
		static string playerExpPrefKey = "PlayerExp";

		void Start ()
		{
			SetMaxExp ();

			//已經進入過遊戲
			if (PlayerPrefs.HasKey (playerExpPrefKey))
			{
				CurrentExp = PlayerPrefs.GetFloat (playerExpPrefKey);
				ExpPercent = (CurrentExp/MaxExp)*100;
				Debug.Log(CurrentExp);
				Debug.Log(MaxExp);
				Debug.Log(ExpPercent);
				this.transform.localPosition = new Vector3((-184.0f + 184.0f * (CurrentExp/MaxExp)), 0.0f, 0.0f);
				LevelBarTx.text = CurrentExp + "/" + MaxExp;
				Debug.Log("已經有key了");

			} 
			//第一次進入遊戲
			else 
			{
				CurrentExp= 0;
				PlayerPrefs.SetFloat(playerExpPrefKey,CurrentExp);
				ExpPercent = (CurrentExp / MaxExp)*100;
				this.transform.localPosition = new Vector3((-184.0f + 184.0f * (CurrentExp/MaxExp)), 0.0f, 0.0f);
				LevelBarTx.text = CurrentExp + "/" + MaxExp;
				Debug.Log("沒有key");
			}
		}


		//設定最大經驗值
		public void SetMaxExp()
		{
			currentLevel = PlayerPrefs.GetString ("PlayerLevel");
			int level = int.Parse (PlayerPrefs.GetString ("PlayerLevel"));

			if (currentLevel == "1")
			{
				MaxExp = 15;
			} 
			else if (currentLevel == "2") 
			{
				MaxExp = 25;
			}
			else if (currentLevel == "3")
			{
				MaxExp = 40;
			}
			else if (currentLevel == "4")
			{
				MaxExp = 50;
			}
			else if (currentLevel == "5")
			{
				MaxExp = 60;
			}
			else if (currentLevel == "6")
			{
				MaxExp = 75;
			}
			else if (currentLevel == "7")
			{
				MaxExp = 85;
			}
			else if (currentLevel == "8")
			{
				MaxExp = 95;
			}
			else if (currentLevel == "9")
			{
				MaxExp = 110;
			}
			else if (currentLevel == "10")
			{
				MaxExp = 120;
			}
			else if (currentLevel == "11")
			{
				MaxExp = 130;
			}
			else if ( level >= 12 )
			{
				MaxExp = 145;
			}
		}

	}
}
