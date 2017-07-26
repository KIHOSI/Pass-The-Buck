using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyProject.MyPassTheBuckGame
{
    public class Audio : MonoBehaviour {

		public Sprite musicPlay;
		public Sprite musicPause;
		Button musicControlBt;
		Text musicControlTx;
		AudioSource audiosre;

		void Start() {

			musicControlBt=GameObject.Find ("MusicControlBt").GetComponent<Button> ();
			musicControlTx=GameObject.Find ("MusicControlTx").GetComponent<Text> ();
			audiosre = GameObject.Find ("BackGroundMusic").GetComponent<AudioSource> ();

			if (audiosre.isPlaying)
			{
				musicControlBt.GetComponent<Image> ().sprite = musicPlay;
				musicControlTx.text = "音樂播放";
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
				audiosre = GameObject.Find ("BackGroundMusic").GetComponent<AudioSource> ();
				audiosre.Pause ();
				musicControlBt.GetComponent<Image> ().sprite = musicPause;
				musicControlTx.text="音樂暫停";
			}
			else 
			{
				audiosre = GameObject.Find ("BackGroundMusic").GetComponent<AudioSource> ();
				audiosre.Play ();
				musicControlBt.GetComponent<Image> ().sprite = musicPlay;
				musicControlTx.text="音樂播放";
			}
		}

		public void MusicPlay (GameObject audio)
		{
			audiosre = audio.GetComponent<AudioSource> ();
			audiosre.Play ();
		}

		public void MusicStop (GameObject audio)
		{
			audiosre = audio.GetComponent<AudioSource> ();
			audiosre.Stop ();
		}


		/*
		void OnGUI() {

			//播放音乐按钮
			if (GUI.Button(new Rect(10, 10, 100, 50), "Play music"))  {

				//没有播放中
				if (!music.isPlaying){
					//播放音乐
					music.Play();
				}

			}

			//关闭音乐按钮
			if (GUI.Button(new Rect(10, 60, 100, 50), "Stop music"))  {

				if (music.isPlaying){
					//关闭音乐
					music.Stop();
				}
			}
			//暂停音乐
			if (GUI.Button(new Rect(10, 110, 100, 50), "Pause music"))  {
				if (music.isPlaying){
					//暂停音乐
					//这里说一下音乐暂停以后
					//点击播放音乐为继续播放
					//而停止以后在点击播放音乐
					//则为从新播放
					//这就是暂停与停止的区别
					music.Pause();
				}
			}

			//创建一个横向滑动条用于动态修改音乐音量
			//第一个参数 滑动条范围
			//第二个参数 初始滑块位置
			//第三个参数 起点
			//第四个参数 终点
			musicVolume = GUI.HorizontalSlider (new Rect(160, 10, 100, 50), musicVolume, 0.0F, 1.0F);

			//将音量的百分比打印出来
			GUI.Label(new Rect(160, 50, 300, 20), "Music Volueme is " + (int)(musicVolume * 100) + "%");

			if (music.isPlaying){
				//音乐播放中设置音乐音量 取值范围 0.0F到 1.0
				music.volume = musicVolume;
			}
			
		}

        */


	}
}
