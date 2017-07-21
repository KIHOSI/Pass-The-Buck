using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;


namespace Com.MyProject.MyPassTheBuckGame
{
	public class ChooseCharecter : Photon.PunBehaviour {

		public Sprite role1;
		public Sprite role2;
		public Sprite role3;

		Button charecterImg;
		Text charecterInfoTx;

		public string role1Intro="大家好，我是蔡中文";
		public string role2Intro="大家好阿! 我是馬英八";
		public string role3Intro="搭給後，挖系蘇貞昌! 衝衝衝!";

		void Start () 
		{

		}

		#region Public Methods

		//按右箭頭
		public void ClickRightArrow()
		{
			charecterImg = GameObject.Find ("CharecterImg").GetComponent<Button> ();
			charecterInfoTx = GameObject.Find ("CharecterInfoTx").GetComponent<Text> ();

			if (charecterImg.GetComponent<Image> ().sprite == role1) 
			{
				charecterImg.GetComponent<Image> ().sprite = role2;
				charecterInfoTx.text =role2Intro;
				//從資料庫抓取角色資料的介紹，然後修改scrollrect內容
			} 
			else if (charecterImg.GetComponent<Image> ().sprite == role2)
			{
				charecterImg.GetComponent<Image> ().sprite = role3;
				charecterInfoTx.text =role3Intro;
				//從資料庫抓取角色資料的介紹，然後修改scrollrect內容
			}
			else if (charecterImg.GetComponent<Image> ().sprite == role3)
			{
				charecterImg.GetComponent<Image> ().sprite = role1;
				charecterInfoTx.text =role1Intro;
				//從資料庫抓取角色資料的介紹，然後修改scrollrect內容
			}


				
		}

		//按左箭頭
		public void ClickLeftArrow()
		{

			charecterImg = GameObject.Find ("CharecterImg").GetComponent<Button> ();
			charecterInfoTx = GameObject.Find ("CharecterInfoTx").GetComponent<Text> ();

			if (charecterImg.GetComponent<Image> ().sprite == role1) 
			{
				charecterImg.GetComponent<Image> ().sprite = role3;
				charecterInfoTx.text =role3Intro;
				//從資料庫抓取角色資料的介紹，然後修改scrollrect內容
			} 
			else if (charecterImg.GetComponent<Image> ().sprite == role2)
			{
				charecterImg.GetComponent<Image> ().sprite = role1;
				charecterInfoTx.text =role1Intro;
				//從資料庫抓取角色資料的介紹，然後修改scrollrect內容
			}
			else if (charecterImg.GetComponent<Image> ().sprite == role3)
			{
				charecterImg.GetComponent<Image> ().sprite = role2;
				charecterInfoTx.text =role2Intro;
				//從資料庫抓取角色資料的介紹，然後修改scrollrect內容
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
