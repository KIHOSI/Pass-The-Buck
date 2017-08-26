using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;


namespace Com.MyProject.MyPassTheBuckGame
{
	public class LoadingAnimator :Photon.PunBehaviour 
	{
		public Sprite loadingImg1;
		public Sprite loadingImg2;
		public Sprite loadingImg3;
		public Sprite loadingImg4;
		public Sprite loadingImg5;
		public Sprite loadingImg6;
		public Sprite loadingImg7;
		public Sprite loadingImg8;
		public Sprite loadingImg9;
		public Image loadingBackground;
		public int index;
		public int count;
		string map;
		AudioSource audiosre;


		void Start () 
		{
			audiosre = GameObject.Find ("BackGroundMusic").GetComponent<AudioSource> ();
			audiosre.Pause ();
			map = (string)PhotonNetwork.room.CustomProperties ["Map"];

			count = 0;
		}

		void FixedUpdate ()
		{
			count++;

			if (count == 40)
			{
				loadingBackground.GetComponent<Image> ().sprite = loadingImg2;
			} 
			else if (count == 80) 
			{
				loadingBackground.GetComponent<Image> ().sprite = loadingImg3;
			} 
			else if (count == 120)
			{
				loadingBackground.GetComponent<Image> ().sprite = loadingImg4;
			}
			else if (count == 160) 
			{
				loadingBackground.GetComponent<Image> ().sprite = loadingImg5;
			}
			else if (count == 200)
			{
				loadingBackground.GetComponent<Image> ().sprite = loadingImg6;
			}
			else if (count == 240)
			{
				loadingBackground.GetComponent<Image> ().sprite = loadingImg7;
			}
			else if (count == 280)
			{
				loadingBackground.GetComponent<Image> ().sprite = loadingImg8;
			}
			else if (count == 320)
			{
				loadingBackground.GetComponent<Image> ().sprite = loadingImg9;

				if (map.Equals ("Classic")) 
				{
					if (PhotonNetwork.room.MaxPlayers == 2)
					{
						PhotonNetwork.LoadLevel ("2PlayerGame");
					} 
					else if (PhotonNetwork.room.MaxPlayers == 4) 
					{
						PhotonNetwork.LoadLevel ("4PlayerGame");
					}
				} 
				else if (map.Equals ("PowerCut"))
				{
					PhotonNetwork.LoadLevel ("PowerCutGame");
				}
			}



		}

		public override void OnDisconnectedFromPhoton()
		{
			SceneManager.LoadScene("Main");
		}

		public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
		{
			SceneManager.LoadScene("Main");
			PhotonNetwork.LeaveRoom ();
		}
			

	}

}
