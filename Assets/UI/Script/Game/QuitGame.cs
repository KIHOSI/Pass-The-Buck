using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Com.MyProject.MyPassTheBuckGame
{
	public class QuitGame : Photon.PunBehaviour
	{
		// public
		public int windowWidth = 600;
		public int windowHight = 400;
		public GUISkin mySkin;

		// private
		Rect windowRect ;
		int windowSwitch = 0;
		float alpha = 0;

		void GUIAlphaColor_0_To_1 ()
		{
			if (alpha < 1) {
				alpha += Time.deltaTime;
				GUI.color = new Color (1, 1, 1, alpha);
			} 
		}

		// Init
		void Awake ()
		{
			windowRect = new Rect (
				(Screen.width - windowWidth) / 2,
				(Screen.height - windowHight) / 2,
				windowWidth,
				windowHight);
		}

		void Update ()
		{
			if (Input.GetKeyDown ("escape")) {
				windowSwitch = 1;
				alpha = 0; // Init Window Alpha Color
			}
		}

		void OnGUI ()
		{ 
			GUI.skin = mySkin;
			if (windowSwitch == 1) {
				GUIAlphaColor_0_To_1 ();
				windowRect = GUI.Window (0, windowRect, QuitWindow, "");
			}
		}

		void QuitWindow (int windowID)
		{
			
			GUI.Label (new Rect (100,100,600,70), "你確定要離開遊戲嗎?");
		

			if (GUI.Button (new Rect (80, 250, 200, 60), "離開")) {
				Application.Quit ();
			} 
			if (GUI.Button (new Rect (330, 250, 200, 60), "取消")) {
				windowSwitch = 0; 
			} 

			GUI.DragWindow (); 
		}

	}
}

