using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


namespace Com.MyProject.MyPassTheBuckGame
{
	public class GameIntroduction :Photon.PunBehaviour 
	{

		public string ButtonText;


		public void ButtonClick(Button bt)
		{
			ButtonText = bt.GetComponentInChildren<Text>().text;

			if (ButtonText == "遊戲介紹") 
			{
				GameObject.Find ("IntroPanel1").GetComponent<Image> ().color = new Color32 (255, 255, 225, 19);
				GameObject.Find ("IntroPanel2").GetComponent<Image> ().color = new Color32 (255, 255, 225, 0);
				GameObject.Find ("IntroPanel3").GetComponent<Image> ().color = new Color32 (255, 255, 225, 0);
			}
			else if (ButtonText == "遊戲介面") 
			{
				GameObject.Find ("IntroPanel1").GetComponent<Image> ().color = new Color32 (255, 255, 225, 0);
				GameObject.Find ("IntroPanel2").GetComponent<Image> ().color = new Color32 (255, 255, 225, 19);
				GameObject.Find ("IntroPanel3").GetComponent<Image> ().color = new Color32 (255, 255, 225, 0);
			}
			else if (ButtonText == "道具介紹") 
			{
				GameObject.Find ("IntroPanel1").GetComponent<Image> ().color = new Color32 (255, 255, 225, 0);
				GameObject.Find ("IntroPanel2").GetComponent<Image> ().color = new Color32 (255, 255, 225, 0);
				GameObject.Find ("IntroPanel3").GetComponent<Image> ().color = new Color32 (255, 255, 225, 19);
			}
		}


	}
}
