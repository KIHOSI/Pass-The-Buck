using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyProject.MyPassTheBuckGame
{
    public class Audio : MonoBehaviour {	
		
		AudioSource audiosre; //音檔
		public Sprite musicPlay;
		public Sprite musicPause;
		Button musicControlBt;
		Text musicControlTx;

		void Start() {

			musicControlBt=GameObject.Find ("MusicControlBt").GetComponent<Button> ();
			musicControlTx=GameObject.Find ("MusicControlTx").GetComponent<Text> ();
			AudioSource audiosre = GameObject.Find ("BackGroundMusic").GetComponent<AudioSource> ();

			if (audiosre.isPlaying)
			{
				musicControlBt.GetComponent<Image> ().sprite = musicPlay;
				musicControlTx.text = "音樂播放中";
			}
			else 
			{
				musicControlBt.GetComponent<Image> ().sprite = musicPause;
				musicControlTx.text = "音樂暫停";
			}
		}

		//控制音樂撥放與暫停按鈕
		public void CheckPlayOrPause()
		{
			musicControlBt=GameObject.Find ("MusicControlBt").GetComponent<Button> ();
			musicControlTx=GameObject.Find ("MusicControlTx").GetComponent<Text> ();

			if (musicControlBt.GetComponent<Image> ().sprite == musicPlay)
			{
				AudioSource audiosre = GameObject.Find ("BackGroundMusic").GetComponent<AudioSource> ();
				audiosre.Pause ();
				musicControlBt.GetComponent<Image> ().sprite = musicPause;
				musicControlTx.text="音樂暫停";
			}
			else 
			{
				AudioSource audiosre = GameObject.Find ("BackGroundMusic").GetComponent<AudioSource> ();
				audiosre.Play ();
				musicControlBt.GetComponent<Image> ().sprite = musicPlay;
				musicControlTx.text="音樂播放中";
			}
		}


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
