using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyProject.MyPassTheBuckGame
{
    public class Level : MonoBehaviour
	{
		public string level;
		static string playerLevelPrefKey = "PlayerLevel";
	
	    void Start () 
		{
			//已經進入過遊戲
			if (PlayerPrefs.HasKey (playerLevelPrefKey))
			{
				level = PlayerPrefs.GetString (playerLevelPrefKey);
				this.GetComponent<Text> ().text = "LV." + level;
				Debug.Log("已經有key了");
			} 
			//第一次進入遊戲
			else 
			{
				level = "1";
				PlayerPrefs.SetString(playerLevelPrefKey,level);
				this.GetComponent<Text> ().text = "LV." + level;
				Debug.Log("沒有key");
			}
				
	    }


    }
}
