using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

namespace Com.MyProject.MyPassTheBuckGame
{
	public class ChooseMap : Photon.PunBehaviour {

	  public Sprite Pisland;
	  public Sprite PislandWithLock;
	  public Sprite Gisland;
	  public Sprite GislandWithLock;
	  public Sprite Lisland;
	  public Sprite LislandWithLock;
	  public Sprite level2WithLock;
	  public Sprite level3WithLock;

	  public Button Island1Bt;
	  public Button Island2Bt;
	  public Button Island3Bt;
	  public Button Level2Bt;
	  public Button Level3Bt;



	  // Use this for initialization
	  void Start () 
	  {

		 //會有資料庫存玩家已解鎖的島嶼名稱

		 //一開始GUI全部島嶼都會在畫面上
		 //剛載入地圖選擇頁面時，系統從資料庫讀取玩家的解鎖島嶼名稱
		 //將解鎖島嶼的button圖案換成無解鎖圖
	  }

	 #region Public Methods

	  //檢查關卡已解鎖或未解鎖
	  public void CheckLockorUnlock1(Button bt)
	  {
		  Island1Bt = GameObject.Find ("Island1Bt").GetComponent<Button> ();
		  Island2Bt = GameObject.Find ("Island2Bt").GetComponent<Button> ();
		  Island3Bt = GameObject.Find ("Island3Bt").GetComponent<Button> ();

		  if (bt.Equals (Island1Bt)) 
		  {
				if (bt.GetComponent<Image> ().sprite == Pisland) 
				{
					LoadPeopleIslandScene ();
				} 
				else if (bt.GetComponent<Image> ().sprite == PislandWithLock)
				{
					Debug.Log("該島嶼尚未解鎖，請選擇已解鎖島嶼");
					onTips("該島嶼尚未解鎖，請選擇已解鎖島嶼");
				}
		  }
		  else if (bt.Equals (Island2Bt)) 
		  {
				if (bt.GetComponent<Image> ().sprite == Lisland) 
				{
					LoadLiverIslandScene ();
				} 
				else if (bt.GetComponent<Image> ().sprite == LislandWithLock)
				{
					Debug.Log("該島嶼尚未解鎖，請選擇已解鎖島嶼");
					onTips("該島嶼尚未解鎖，請選擇已解鎖島嶼");
				}
		  }
		  else if (bt.Equals (Island3Bt)) 
		  {
				if (bt.GetComponent<Image> ().sprite == Gisland) 
				{
					LoadGIslandScene ();
				} 
				else if (bt.GetComponent<Image> ().sprite == GislandWithLock)
				{
					Debug.Log("該島嶼尚未解鎖，請選擇已解鎖島嶼");
					onTips("該島嶼尚未解鎖，請選擇已解鎖島嶼");
				}
		  }

		  
	  }

		//檢查關卡已解鎖或未解鎖
		public void CheckLockorUnlock2(Button bt)
		{

			Level2Bt = GameObject.Find ("Level2Bt").GetComponent<Button> ();
			Level3Bt = GameObject.Find ("Level3Bt").GetComponent<Button> ();


			if (bt.Equals (Level2Bt)) 
			{
				if (bt.GetComponent<Image> ().sprite == level2WithLock)
				{
					Debug.Log("該關卡尚未解鎖，請選擇已解鎖關卡");
					onTips("該關卡尚未解鎖，請選擇已解鎖關卡");
				}
			}
			else if (bt.Equals (Level3Bt)) 
			{
				if (bt.GetComponent<Image> ().sprite == level3WithLock)
				{
					Debug.Log("該關卡尚未解鎖，請選擇已解鎖關卡");
					onTips("該關卡尚未解鎖，請選擇已解鎖關卡");
				}
			}


		}


	 //載入爆肝島頁面
	 public void LoadLiverIslandScene()
	 {
		SceneManager.LoadScene ("LiverIsland");
		//之後會在這加上判斷式，判斷玩家選擇的島嶼Button物件名稱
		//然後將選擇的島嶼名稱加入playerPrefs裡(playerPrefs定義一個mapChoosing參數，存玩家選擇的地圖)
	 }

	 //載入大眾島頁面
	 public void LoadPeopleIslandScene()
	 {
		//SceneManager.LoadScene ("LiverIsland");
		//之後會在這加上判斷式，判斷玩家選擇的島嶼Button物件名稱
		//然後將選擇的島嶼名稱加入playerPrefs裡(playerPrefs定義一個mapChoosing參數，存玩家選擇的地圖)
	 }

	 //載入金雞島頁面
	 public void LoadGIslandScene()
	 {
		//SceneManager.LoadScene ("LiverIsland");
		//之後會在這加上判斷式，判斷玩家選擇的島嶼Button物件名稱
		//然後將選擇的島嶼名稱加入playerPrefs裡(playerPrefs定義一個mapChoosing參數，存玩家選擇的地圖)
	 }

	//載入設定暱稱頁面
	public void LoadCharacterChoosingScene()
	{
		SceneManager.LoadScene ("Character Choosing");
		//之後會在這加上判斷式，判斷玩家選擇的島嶼Button物件名稱
		//然後將選擇的島嶼名稱加入playerPrefs裡(playerPrefs定義一個mapChoosing參數，存玩家選擇的地圖)
	}
	

	public void onTips(string tips_str)
	{
		GameObject parent = GameObject.Find ("MapChoosePanel");
		GameObject toast = GameObject.Find ("Toast"); // 加载预制体
		GameObject m_toast = GameObject.Instantiate(toast, parent.transform, false);  // 对象初始化
		//m_toast.transform.parent = parent.transform;            //　附加到父节点（需要显示的UI下）
		m_toast.transform.localScale = Vector3.one;
		m_toast.transform.localPosition = new Vector3 (3.3f, -234.3f, 0.0f);
		Text tips = m_toast.GetComponent<Text>();
		tips.text = tips_str;
		Destroy(m_toast, 2); // 2秒后 销毁
	}
			
	#endregion

    public override void OnDisconnectedFromPhoton()
	{
		Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
		SceneManager.LoadScene("Main");
	}


  }
}
