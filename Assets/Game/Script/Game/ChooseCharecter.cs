using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;


namespace Com.MyProject.MyPassTheBuckGame
{
	public class ChooseCharecter : MonoBehaviour {

		public Sprite role1;
		public Sprite role2;
		public Sprite role3;
		//public GameObject CharecterImg;
		//public GameObject CharecterInfoTx;
		Button charecterImg;
		Text charecterInfoTx;

		public string role1Intro="大家好，我是蔡中文";
		public string role2Intro="大家好阿! 我是馬英八";
		public string role3Intro="搭給後，挖系蘇貞昌! 衝衝衝!";


		// Use this for initialization
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
			//之後會在這加上判斷式，判斷玩家選擇的角色圖片名稱
			//然後將選擇的角色名稱加入playerPrefs裡(playerPrefs定義一個charecterChoosing參數，存玩家選擇的角色)

			SceneManager.LoadScene (5);
		}

		//載入加入房間頁面
		public void LoadJoinRoomScene()
		{
			//之後會在這加上判斷式，判斷玩家選擇的角色圖片名稱
			//然後將選擇的角色名稱加入playerPrefs裡(playerPrefs定義一個charecterChoosing參數，存玩家選擇的角色)

			SceneManager.LoadScene (7);
		}



		#endregion


	}
}
