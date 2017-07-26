using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;


namespace Com.MyProject.MyPassTheBuckGame
{
	public class ChooseCharecter : Photon.PunBehaviour {


		public Sprite BlueRole1;
		public Sprite BlueRole2;
		public Sprite GreenRole1;
		public Sprite GreenRole2;
		public Sprite BlueBackground;
		public Sprite GreenBackground;
		public Image CharecterImg;
		public Image IntroBackImg;
		public Text CharecterNameTx;
		public Text CharecterInfoTx;
		public Button LeftArrowBt;
		public Button RightArrowBt;

		public string BlueRole1Intro = "祝鑄炷佇駐助蛀柱";
		public string BlueRole2Intro = "曾為縣長的新科立委";
		public string GreenRole1Intro = "天天怒的新上任總理";
		public string GreenRole2Intro = "新流派權法的院長";


		#region Public Methods
	
		//按右箭頭
		public void ClickRightArrow()
		{
			CharecterImg = GameObject.Find ("CharecterImg").GetComponent<Image> ();
			CharecterNameTx = GameObject.Find ("CharecterNameTx").GetComponent<Text> ();
			CharecterInfoTx = GameObject.Find ("CharecterInfoTx").GetComponent<Text> ();
			IntroBackImg = GameObject.Find ("IntroBackImg").GetComponent<Image> ();

			if (CharecterImg.sprite == BlueRole1) 
			{
				CharecterImg.sprite = BlueRole2;
				CharecterNameTx.text = "吳指癢";
				CharecterInfoTx.text = BlueRole2Intro;
				IntroBackImg.sprite = BlueBackground;
			}
			else if (CharecterImg.sprite == BlueRole2) 
			{
				CharecterImg.sprite = GreenRole1;
				CharecterNameTx.text = "蔡中聞";
				CharecterInfoTx.text = GreenRole1Intro;
				IntroBackImg.sprite = GreenBackground;
			}
			else if (CharecterImg.sprite == GreenRole1) 
			{
				CharecterImg.sprite = GreenRole2;
				CharecterNameTx.text = "蘇嘎拳";
				CharecterInfoTx.text = GreenRole2Intro;
				IntroBackImg.sprite = GreenBackground;
			}
			else if (CharecterImg.sprite == GreenRole2) 
			{
				CharecterImg.sprite = BlueRole1;
				CharecterNameTx.text = "洪咻柱";
				CharecterInfoTx.text = BlueRole1Intro;
				IntroBackImg.sprite = BlueBackground;
			}

		}

		//按左箭頭
		public void ClickLeftArrow()
		{
			CharecterImg = GameObject.Find ("CharecterImg").GetComponent<Image> ();
			CharecterNameTx = GameObject.Find ("CharecterNameTx").GetComponent<Text> ();
			CharecterInfoTx = GameObject.Find ("CharecterInfoTx").GetComponent<Text> ();
			IntroBackImg = GameObject.Find ("IntroBackImg").GetComponent<Image> ();

			if (CharecterImg.sprite == BlueRole1) 
			{
				CharecterImg.sprite = GreenRole2;
				CharecterNameTx.text = "蘇嘎拳";
				CharecterInfoTx.text = GreenRole2Intro;
				IntroBackImg.sprite = GreenBackground;
			}
			else if (CharecterImg.sprite == BlueRole2) 
			{
				CharecterImg.sprite = BlueRole1;
				CharecterNameTx.text = "洪咻柱";
				CharecterInfoTx.text = BlueRole1Intro;
				IntroBackImg.sprite = BlueBackground;
			}
			else if (CharecterImg.sprite == GreenRole1) 
			{
				CharecterImg.sprite = BlueRole2;
				CharecterNameTx.text = "吳指癢";
				CharecterInfoTx.text = BlueRole2Intro;
				IntroBackImg.sprite = BlueBackground;
			}
			else if (CharecterImg.sprite == GreenRole2) 
			{
				CharecterImg.sprite = GreenRole1;
				CharecterNameTx.text = "蔡中聞";
				CharecterInfoTx.text = GreenRole1Intro;
				IntroBackImg.sprite = GreenBackground;
			}


		}

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
