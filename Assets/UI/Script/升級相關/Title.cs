using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyProject.MyPassTheBuckGame
{
	public class Title : MonoBehaviour
	{
		public string title;
		static string playerTitlePrefKey = "PlayerTitle";

		void Start () 
		{
			title = PlayerPrefs.GetString (playerTitlePrefKey);
			this.GetComponent<Text> ().text = title;

		}


	}
}

