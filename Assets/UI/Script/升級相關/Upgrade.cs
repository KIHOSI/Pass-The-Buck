using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyProject.MyPassTheBuckGame
{
	public class Upgrade : MonoBehaviour {

		public string level;
		static string playerLevelPrefKey = "PlayerLevel";

		void Start ()
		{
			level = PlayerPrefs.GetString (playerLevelPrefKey);

			if (level == "3" || level == "4" || level == "5") 
			{
				this.GetComponent<Text> ().text = "(獲勝錢錢+5百萬)";
			}
			else if (level == "6" || level == "7" || level == "8") 
			{
				this.GetComponent<Text> ().text = "(獲勝錢錢+15百萬)";
			}
			else if (level == "9" || level == "10" || level == "11") 
			{
				this.GetComponent<Text> ().text = "(獲勝錢錢+25百萬)";
			}
			else if (level == "12")
			{
				this.GetComponent<Text> ().text = "(獲勝錢錢+50百萬)";
			}

		}

	

	}
}

