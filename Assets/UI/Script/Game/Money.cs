using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyProject.MyPassTheBuckGame
{
    public class Money : MonoBehaviour 
	{
		
		public int money;
		public Text MoneyTx;
		static string playerMoneyPrefKey = "PlayerMoney";

	    void Start () 
		{
			//已經進入過遊戲
			if (PlayerPrefs.HasKey (playerMoneyPrefKey))
			{
				money = PlayerPrefs.GetInt (playerMoneyPrefKey);
				MoneyTx.text = money.ToString()+ " (百萬)";
				Debug.Log("已經有key了");
			} 
			//第一次進入遊戲
			else 
			{
				money = 0;
				PlayerPrefs.SetInt(playerMoneyPrefKey,money);
				MoneyTx.text = money.ToString() + " (百萬)";
				Debug.Log("沒有key");
			}

	    }
			

    }
}
