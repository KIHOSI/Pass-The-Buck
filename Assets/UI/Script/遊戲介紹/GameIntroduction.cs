using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement; 


namespace Com.MyProject.MyPassTheBuckGame
{
	public class GameIntroduction :Photon.PunBehaviour 
	{

		public string ButtonText;

		public void Click(Button bt)
		{
			ButtonText = bt.GetComponentInChildren<Text>().text;

			if (ButtonText == "遊戲介面") 
			{
				SceneManager.LoadScene ("Layout Intro");
			}
			else if (ButtonText == "升級制度") 
			{
				SceneManager.LoadScene ("Level Intro");
			}
			else if (ButtonText == "關卡介紹") 
			{
				SceneManager.LoadScene ("Map Intro");
			}
			else if (ButtonText == "經典模式") 
			{
				SceneManager.LoadScene ("Classic Intro");
			}
			else if (ButtonText == "815大停電") 
			{
				SceneManager.LoadScene ("PowerCut Intro");
			}

		}
		public void ButtonClickForCL(Button bt)
		{
			ButtonText = bt.GetComponentInChildren<Text>().text;

			if (ButtonText == "遊戲介紹") 
			{
				GameObject.Find ("IntroPanel1ForCL").GetComponent<Image> ().color = new Color32 (255, 255, 225, 1);
				GameObject.Find ("IntroPanel2ForCL").GetComponent<Image> ().color = new Color32 (255, 255, 225, 0);
			}
			else if (ButtonText == "道具介紹") 
			{
				GameObject.Find ("IntroPanel1ForCL").GetComponent<Image> ().color = new Color32 (255, 255, 225, 0);
				GameObject.Find ("IntroPanel2ForCL").GetComponent<Image> ().color = new Color32 (255, 255, 225, 1);
			}
		}

		public void ButtonClickForPC(Button bt)
		{
			ButtonText = bt.GetComponentInChildren<Text>().text;

			if (ButtonText == "遊戲介紹") 
			{
				GameObject.Find ("IntroPanel1ForPC").GetComponent<Image> ().color = new Color32 (255, 255, 225, 1);
				GameObject.Find ("IntroPanel2ForPC").GetComponent<Image> ().color = new Color32 (255, 255, 225, 0);
			}
			else if (ButtonText == "道具介紹") 
			{
				GameObject.Find ("IntroPanel1ForPC").GetComponent<Image> ().color = new Color32 (255, 255, 225, 0);
				GameObject.Find ("IntroPanel2ForPC").GetComponent<Image> ().color = new Color32 (255, 255, 225, 1);
			}
		}

		public void Back()
		{
			SceneManager.LoadScene ("GameIntro");
		}


	}
}
