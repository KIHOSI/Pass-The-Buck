﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyProject.MyPassTheBuckGame
{
     public class LevelBar : MonoBehaviour {

		public float MaxExp;
		public float CurrentExp;
		public Text LevelBarTx;
		string title;
		public float ExpPercent;
		public string currentLevel;
		static string playerExpPrefKey = "PlayerExp";
		static string playerTitlePrefKey = "PlayerTitle";

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
				this.transform.localPosition = new Vector3((-167.0f + 167.0f * (CurrentExp/MaxExp)), 0.0f, 0.0f);
				LevelBarTx.text = CurrentExp + "/" + MaxExp;
				Debug.Log("已經有key了");

			} 
			//第一次進入遊戲
			else 
			{
				CurrentExp= 0;
				PlayerPrefs.SetFloat(playerExpPrefKey,CurrentExp);
				ExpPercent = (CurrentExp / MaxExp)*100;
				this.transform.localPosition = new Vector3((-167.0f + 167.0f * (CurrentExp/MaxExp)), 0.0f, 0.0f);
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
				title = "新進議員";
				PlayerPrefs.SetString(playerTitlePrefKey,title);
			} 
			else if (currentLevel == "2") 
			{
				MaxExp = 25;
				title = "新進議員";
				PlayerPrefs.SetString(playerTitlePrefKey,title);
			}
			else if (currentLevel == "3")
			{
				MaxExp = 40;
				title = "地方議員";
				PlayerPrefs.SetString(playerTitlePrefKey,title);
			}
			else if (currentLevel == "4")
			{
				MaxExp = 50;
				title = "地方議員";
				PlayerPrefs.SetString(playerTitlePrefKey,title);
			}
			else if (currentLevel == "5")
			{
				MaxExp = 60;
				title = "地方議員";
				PlayerPrefs.SetString(playerTitlePrefKey,title);
			}
			else if (currentLevel == "6")
			{
				MaxExp = 75;
				title = "立法委員";
				PlayerPrefs.SetString(playerTitlePrefKey,title);
			}
			else if (currentLevel == "7")
			{
				MaxExp = 85;
				title = "立法委員";
				PlayerPrefs.SetString(playerTitlePrefKey,title);
			}
			else if (currentLevel == "8")
			{
				MaxExp = 95;
				title = "立法委員";
				PlayerPrefs.SetString(playerTitlePrefKey,title);
			}
			else if (currentLevel == "9")
			{
				MaxExp = 110;
				title = "立法院長";
				PlayerPrefs.SetString(playerTitlePrefKey,title);
			}
			else if (currentLevel == "10")
			{
				MaxExp = 120;
				title = "立法院長";
				PlayerPrefs.SetString(playerTitlePrefKey,title);
			}
			else if (currentLevel == "11")
			{
				MaxExp = 130;
				title = "立法院長";
				PlayerPrefs.SetString(playerTitlePrefKey,title);
			}
			else if ( level >= 12 )
			{
				MaxExp = 145;
				title = "總統";
				PlayerPrefs.SetString(playerTitlePrefKey,title);
			}

		}

	}
}
