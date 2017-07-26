using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyProject.MyPassTheBuckGame
{
    public class Audio : MonoBehaviour {	
		AudioSource audiosre; //音檔

		public void MusicPlay (GameObject audio) //play music
		{
			audiosre = audio.GetComponent<AudioSource> ();
			audiosre.Play ();
		}

		public void MusicStop (GameObject audio) //play stop
		{
			audiosre = audio.GetComponent<AudioSource> ();
			audiosre.Stop ();
		}
	}
}
