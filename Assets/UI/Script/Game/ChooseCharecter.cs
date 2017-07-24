using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;


namespace Com.MyProject.MyPassTheBuckGame
{
	public class ChooseCharecter : Photon.PunBehaviour {



		#region Public Methods
	

		//載入創建房間頁面
		public void LoadCreateRoomScene()
		{

			SceneManager.LoadScene ("Create Room");
		}

		//載入加入房間頁面
		public void LoadJoinRoomScene()
		{
			
			SceneManager.LoadScene ("Join Room");
		}

		#endregion

		public override void OnDisconnectedFromPhoton()
		{
			Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
			SceneManager.LoadScene("Main");
		}


	}
}
