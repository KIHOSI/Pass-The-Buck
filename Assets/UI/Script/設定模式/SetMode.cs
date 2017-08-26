using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;


namespace Com.MyProject.MyPassTheBuckGame
{

	public class SetMode : Photon.PunBehaviour 
	{

		string mode;
		static string playerModePrefKey = "PlayerMode";


		void start()
		{
		}


		public void SetTwoPlayerMode()
		{
			mode = "2";
			PlayerPrefs.SetString(playerModePrefKey,mode);
			SceneManager.LoadScene("MapChooseFor2");
		}

		public void SetFourPlayerMode()
		{
			mode = "4";
			PlayerPrefs.SetString(playerModePrefKey,mode);
			SceneManager.LoadScene("MapChooseFor4");
		}

	


		public override void OnDisconnectedFromPhoton()
		{
			Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
			SceneManager.LoadScene("Main");
		}
			

	}

}
