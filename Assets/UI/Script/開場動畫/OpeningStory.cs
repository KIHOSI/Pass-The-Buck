using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

namespace Com.MyProject.MyPassTheBuckGame
{
   public class OpeningStory : MonoBehaviour
   {

		public Sprite BackImg1;
		public Sprite BackImg2;
		public Sprite BackImg3;
		public Sprite BackImg4;
		public Sprite BackImg5;
		public Sprite BackImg6;
		public Image BackGroundImg;
		public Text DialogTx;
		public Button SelectBt1;
		public Button SelectBt2;
		public int index;
		public int count;
		public string Dialog1 = "「花錢，花得不是自己的錢；做事，做得都是自己的事；講話，講得都是一堆廢話。」";
		public string Dialog2 = "「然而‒沒錯，事情就是這樣。」";
		public string Dialog3 = "「啊?...怎樣，不行嗎?要不然你來做啊?」";
		public string Dialog4 = "「那還用說，當然就是政客囉!」";
		public string Dialog5 = "「想知道嗎? 想知道就跟我走吧!」";
		public string Dialog6 = "「讓我們一起踏上偉大的政道!」";
		public int stage = 0;
		AudioSource audiosre;

	   void Start () 
	   {
			
				audiosre = GameObject.Find ("open story music").GetComponent<AudioSource> ();
				audiosre.Play ();

	   }

	   void FixedUpdate () 
	   {

			if (stage == 0) 
			{
				count++;
				if (count == 150)  //過3秒
				{ 
					DialogTx.text = Dialog1;
				} 
				else if (count == 300) 
				{
					DialogTx.text = Dialog2;
					SelectBt1.GetComponent<Image> ().color = new Color32(1,1,141,60);
					SelectBt2.GetComponent<Image> ().color = new Color32(1,1,141,60);
					SelectBt1.GetComponentInChildren<Text> ().text = "也太輕鬆了吧";
					SelectBt2.GetComponentInChildren<Text> ().text = "啊不就好棒棒";
					stage = 1;
					count = 0;
				}

			}
			else if (DialogTx.text == Dialog5 && stage == 1) 
			{
				count++;
				if (count == 150) //過3秒
				{  
					DialogTx.text = Dialog6;
					BackGroundImg.sprite = BackImg1;
					SelectBt1.GetComponent<Image> ().color = new Color32(0,0,0,0);
					SelectBt2.GetComponent<Image> ().color = new Color32(0,0,0,0);
					count = 0;
					stage = 2;
				} 
			
			}
			else if (stage== 2) 
			{
				
				count++;
				if (count == 150)
				{
					audiosre = GameObject.Find ("open story music").GetComponent<AudioSource> ();
					audiosre.volume = 0.5f;
					audiosre = GameObject.Find ("open door").GetComponent<AudioSource> ();
					audiosre.Play ();
					DialogTx.text = "";
					BackGroundImg.sprite = BackImg2;
				} 
				else if (count == 180) 
				{
					BackGroundImg.sprite = BackImg3;
				}
				else if (count == 210)
				{
					BackGroundImg.sprite = BackImg4;
				}
				else if (count == 240)
				{
					BackGroundImg.sprite = BackImg5;
				}
				else if (count == 270)
				{
					BackGroundImg.sprite = BackImg6;
					count = 0;
					stage = 3;
				}
			}
			else if (stage == 3) 
			{
				count++;
				if (count == 20) 
				{
				   SceneManager.LoadScene("SetPlayerName");
				}
			}
		
	   }

		public void ChooseOption()
		{
			//BackGroundImg = GameObject.Find ("BackGroundImg").GetComponent<Image> ();
			DialogTx = GameObject.Find ("DialogTx").GetComponent<Text> ();
			SelectBt1 = GameObject.Find ("SelectBt1").GetComponent<Button> ();
			SelectBt2 = GameObject.Find ("SelectBt2").GetComponent<Button> ();
			string buttonTx1 = SelectBt1.GetComponentInChildren<Text> ().text;
			string buttonTx2 = SelectBt2.GetComponentInChildren<Text> ().text;

			if (buttonTx1 == "也太輕鬆了吧") 
			{
				DialogTx.text = Dialog3;
				SelectBt1.GetComponentInChildren<Text> ().text = "好啊!你說要做什麼?"; 
				SelectBt2.GetComponentInChildren<Text> ().text = "不要。你到底是誰?";
			} 
			else if (buttonTx1 == "好啊!你說要做什麼?") 
			{
				DialogTx.text = Dialog4;
				SelectBt1.GetComponentInChildren<Text> ().text = "什...你說甚麼? 政客?"; 
				SelectBt2.GetComponent<Image> ().color = new Color32(0,0,185,0);
				SelectBt2.GetComponentInChildren<Text> ().text = "";
			}
			else if (buttonTx1 == "什...你說甚麼? 政客?") 
			{
				DialogTx.text = Dialog5;
				SelectBt1.GetComponent<Image> ().color = new Color32(0,0,185,0);
				SelectBt2.GetComponent<Image> ().color = new Color32(0,0,185,0);
				SelectBt1.GetComponentInChildren<Text> ().text = ""; 
				SelectBt2.GetComponentInChildren<Text> ().text = "";
			}


		}



   }
}
