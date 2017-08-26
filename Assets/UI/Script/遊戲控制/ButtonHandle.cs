using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using UnityEngine;

namespace Com.MyProject.MyPassTheBuckGame
{
	public class ButtonHandle:Photon.PunBehaviour 
	{
		public string ButtonText;
		public Button MainBt;
		public Button RoleIntroBt;
		public Button BagBt;
		public Button StoryBt;
		public Button GameIntroBt;


		// public
		public int windowWidth = 600;
		public int windowHight = 400;
		public GUISkin mySkin;

		// private
		Rect windowRect ;
		int windowSwitch;
		float alpha = 0;


		// Init
		void Awake ()
		{
			windowRect = new Rect (
				(Screen.width - windowWidth) / 2,
				(Screen.height - windowHight) / 2,
				windowWidth,
				windowHight);
		}

		void Start()
		{
			MainBt.onClick.AddListener (MainOnClick);
			RoleIntroBt.onClick.AddListener (RoleIntroOnClick);
			BagBt.onClick.AddListener (SettingOnClick);
			StoryBt.onClick.AddListener (StoryOnClick);
			GameIntroBt.onClick.AddListener (GameIntroOnClick);
		}

		void OnGUI ()
		{ 
			GUI.skin = mySkin;
			if (windowSwitch == 1) {
				GUIAlphaColor_0_To_1 ();
				windowRect = GUI.Window (0, windowRect, QuitWindow1, "");
			} else if (windowSwitch == 2) {
				GUIAlphaColor_0_To_1 ();
				windowRect = GUI.Window (0, windowRect, QuitWindow2, "");
			} else if (windowSwitch == 3) {
				GUIAlphaColor_0_To_1 ();
				windowRect = GUI.Window (0, windowRect, QuitWindow3, "");
			} else if (windowSwitch == 4) {
				GUIAlphaColor_0_To_1 ();
				windowRect = GUI.Window (0, windowRect, QuitWindow4, "");
			} else if (windowSwitch == 5) {
				GUIAlphaColor_0_To_1 ();
				windowRect = GUI.Window (0, windowRect, QuitWindow5, "");
			}
		}

		void GUIAlphaColor_0_To_1 ()
		{
			if (alpha < 1) {
				alpha += Time.deltaTime;
				GUI.color = new Color (1, 1, 1, alpha);
			} 
		}

		void QuitWindow1 (int windowID)
		{

			GUI.Label (new Rect (100,100,600,70), "你確定要離開此頁面嗎?");


			if (GUI.Button (new Rect (80, 250, 200, 60), "離開")) {
				PhotonNetwork.LeaveRoom ();
				SceneManager.LoadScene("Main");
			} 
			if (GUI.Button (new Rect (330, 250, 200, 60), "取消")) {
				windowSwitch = 0; 
			} 

			GUI.DragWindow (); 
		}

		void QuitWindow2 (int windowID)
		{

			GUI.Label (new Rect (100,100,600,70), "你確定要離開此頁面嗎?");


			if (GUI.Button (new Rect (80, 250, 200, 60), "離開")) {
				PhotonNetwork.LeaveRoom ();
				SceneManager.LoadScene("RoleIntro");
			} 
			if (GUI.Button (new Rect (330, 250, 200, 60), "取消")) {
				windowSwitch = 0; 
			} 

			GUI.DragWindow (); 
		}

		void QuitWindow3 (int windowID)
		{

			GUI.Label (new Rect (100,100,600,70), "你確定要離開此頁面嗎?");


			if (GUI.Button (new Rect (80, 250, 200, 60), "離開")) {
				PhotonNetwork.LeaveRoom ();
				SceneManager.LoadScene("Bag");
			} 
			if (GUI.Button (new Rect (330, 250, 200, 60), "取消")) {
				windowSwitch = 0; 
			} 

			GUI.DragWindow (); 
		}

		void QuitWindow4 (int windowID)
		{

			GUI.Label (new Rect (100,100,600,70), "你確定要離開此頁面嗎?");


			if (GUI.Button (new Rect (80, 250, 200, 60), "離開")) {
				PhotonNetwork.LeaveRoom ();
				SceneManager.LoadScene("Story");
			} 
			if (GUI.Button (new Rect (330, 250, 200, 60), "取消")) {
				windowSwitch = 0; 
			} 

			GUI.DragWindow (); 
		}

		void QuitWindow5 (int windowID)
		{

			GUI.Label (new Rect (100,100,600,70), "你確定要離開此頁面嗎?");


			if (GUI.Button (new Rect (80, 250, 200, 60), "離開")) {
				PhotonNetwork.LeaveRoom ();
				SceneManager.LoadScene("GameIntro");
			} 
			if (GUI.Button (new Rect (330, 250, 200, 60), "取消")) {
				windowSwitch = 0; 
			} 

			GUI.DragWindow (); 
		}

		void MainOnClick()
		{
			windowSwitch = 1;
		}

		void RoleIntroOnClick()
		{
			windowSwitch = 2;
		}

		void SettingOnClick()
		{
			windowSwitch = 3;
		}

		void StoryOnClick()
		{
			windowSwitch = 4;
		}

		void GameIntroOnClick()
		{
			windowSwitch = 5;
		}



		public void enterButton(Button Bt)
		{
			ButtonText = Bt.GetComponentInChildren<Text>().text;

			if (ButtonText.Equals ("主頁")) 
			{
				Bt.GetComponentInChildren<Text>().color = new Color32 (129, 61, 9, 255);
			}
			else if (ButtonText.Equals ("角色介紹")) 
			{
				Bt.GetComponentInChildren<Text>().color = new Color32 (129, 61, 9, 255);
			}
			else if (ButtonText.Equals ("背包")) 
			{
				Bt.GetComponentInChildren<Text>().color = new Color32 (129, 61, 9, 255);
			}
			else if (ButtonText.Equals ("故事")) 
			{
				Bt.GetComponentInChildren<Text>().color = new Color32 (129, 61, 9, 255);
			}
			else if (ButtonText.Equals ("遊戲說明")) 
			{
				Bt.GetComponentInChildren<Text>().color = new Color32 (129, 61, 9, 255);
			}
		}

		public void exitButton(Button Bt)
		{
			ButtonText = Bt.GetComponentInChildren<Text>().text;

			if (ButtonText.Equals ("主頁")) 
			{
				Bt.GetComponentInChildren<Text>().color = new Color32 (129, 61, 9, 0);
			}
			else if (ButtonText.Equals ("角色介紹")) 
			{
				Bt.GetComponentInChildren<Text>().color = new Color32 (129, 61, 9, 0);
			}
			else if (ButtonText.Equals ("背包")) 
			{
				Bt.GetComponentInChildren<Text>().color = new Color32 (129, 61, 9, 0);
			}
			else if (ButtonText.Equals ("故事")) 
			{
				Bt.GetComponentInChildren<Text>().color = new Color32 (129, 61, 9, 0);
			}
			else if (ButtonText.Equals ("遊戲說明")) 
			{
				Bt.GetComponentInChildren<Text>().color = new Color32 (129, 61, 9, 0);
			}
		}



	}
}
