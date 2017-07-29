using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;


namespace Com.MyProject.MyPassTheBuckGame
{
	public class LoadingAnimator :Photon.PunBehaviour 
	{
		public Sprite[] loadingImgArray;
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
		AudioSource audiosre;


		void Start () 
		{
			audiosre = GameObject.Find ("BackGroundMusic").GetComponent<AudioSource> ();
			audiosre.Pause ();
			loadingImgArray = new Sprite[] {loadingImg1,loadingImg2,loadingImg3,
				                            loadingImg4,loadingImg5,loadingImg6,loadingImg7,loadingImg8,loadingImg9};
			index = 1;
			count = 0;
		}

		void FixedUpdate ()
		{
			count++;

			if (count == 40)
			{
				
				loadingBackground.GetComponent<Image> ().sprite = loadingImgArray [index];
				index++;
				count = 0;

                if (index == 8)
                {  
					if (PhotonNetwork.isMasterClient) {
						PhotonNetwork.LoadLevel ("4PlayerGame");
					}
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
