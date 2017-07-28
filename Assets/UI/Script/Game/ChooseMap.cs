using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

namespace Com.MyProject.MyPassTheBuckGame
{
	public class ChooseMap : Photon.PunBehaviour {

	  public Sprite Building1;
	  public Sprite Building1WithLock;
	  public Sprite Building2;
	  public Sprite Building2WithLock;
	  public Sprite Building3;
	  public Sprite Building3WithLock;
	  public Sprite Building4;
	  public Sprite Building4WithLock;
	  public Sprite Building5;
	  public Sprite Building5WithLock;

	  public Sprite level1WithLock;
	  public Sprite level2WithLock;
	  public Sprite level3WithLock;

	  public Button Building1Bt;
	  public Button Building2Bt;
	  public Button Building3Bt;
	  public Button Building4Bt; 
	  public Button Building5Bt;

	  public Button Level1Bt;
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
			Building1Bt = GameObject.Find ("Building1Bt").GetComponent<Button> ();
			Building2Bt = GameObject.Find ("Building2Bt").GetComponent<Button> ();
			Building3Bt = GameObject.Find ("Building3Bt").GetComponent<Button> ();
			Building4Bt = GameObject.Find ("Building4Bt").GetComponent<Button> ();
			Building5Bt = GameObject.Find ("Building5Bt").GetComponent<Button> ();

		  if (bt.Equals (Building1Bt)) 
		  {
				if (bt.GetComponent<Image> ().sprite == Building1) 
				{
					LoadPeopleIslandScene ();
				} 
				else if (bt.GetComponent<Image> ().sprite == Building1WithLock)
				{
					Debug.Log("該大樓尚未解鎖，請選擇已解鎖大樓");
					onTips1("該大樓尚未解鎖，請選擇已解鎖大樓");
				}
		  }
		  else if (bt.Equals (Building2Bt)) 
		  {
				if (bt.GetComponent<Image> ().sprite == Building2) 
				{
					LoadLiverBuildingScene ();
				} 
				else if (bt.GetComponent<Image> ().sprite == Building2WithLock)
				{
					Debug.Log("該大樓尚未解鎖，請選擇已解鎖大樓");
					onTips1("該大樓尚未解鎖，請選擇已解鎖大樓");
				}
		  }
		  else if (bt.Equals (Building3Bt)) 
		  {
				if (bt.GetComponent<Image> ().sprite == Building3) 
				{
					LoadLiverBuildingScene ();
				} 
				else if (bt.GetComponent<Image> ().sprite == Building3WithLock)
				{
					Debug.Log("該大樓尚未解鎖，請選擇已解鎖大樓");
					onTips1("該大樓尚未解鎖，請選擇已解鎖大樓");
				}
		  }
			else if (bt.Equals (Building4Bt)) 
			{
				if (bt.GetComponent<Image> ().sprite == Building4) 
				{
					LoadGIslandScene ();
				} 
				else if (bt.GetComponent<Image> ().sprite == Building4WithLock)
				{
					Debug.Log("該大樓尚未解鎖，請選擇已解鎖大樓");
					onTips1("該大樓尚未解鎖，請選擇已解鎖大樓");
				}
			}
			else if (bt.Equals (Building5Bt)) 
			{
				if (bt.GetComponent<Image> ().sprite == Building5) 
				{
					LoadGIslandScene ();
				} 
				else if (bt.GetComponent<Image> ().sprite == Building5WithLock)
				{
					Debug.Log("該大樓尚未解鎖，請選擇已解鎖大樓");
					onTips1("該大樓尚未解鎖，請選擇已解鎖大樓");
				}
			}

		  
	  }

		//檢查關卡已解鎖或未解鎖
		public void CheckLockorUnlock2(Button bt)
		{
			
			Level1Bt = GameObject.Find ("Level1Bt").GetComponent<Button> ();
			Level2Bt = GameObject.Find ("Level2Bt").GetComponent<Button> ();
			Level3Bt = GameObject.Find ("Level3Bt").GetComponent<Button> ();


			if (bt.Equals (Level2Bt)) 
			{
				if (bt.GetComponent<Image> ().sprite == level2WithLock)
				{
					Debug.Log("該關卡尚未解鎖，請選擇已解鎖關卡");
					onTips2("該關卡尚未解鎖，請選擇已解鎖關卡");
				}
			}
			else if (bt.Equals (Level3Bt)) 
			{
				if (bt.GetComponent<Image> ().sprite == level3WithLock)
				{
					Debug.Log("該關卡尚未解鎖，請選擇已解鎖關卡");
					onTips2("該關卡尚未解鎖，請選擇已解鎖關卡");
				}
			}
			else if (bt.Equals (Level1Bt)) 
			{
				if (bt.GetComponent<Image> ().sprite == level1WithLock)
				{
					Debug.Log("該關卡尚未解鎖，請選擇已解鎖關卡");
					onTips2("該關卡尚未解鎖，請選擇已解鎖關卡");
				}
			}


		}


	 //載入爆肝大樓頁面
	 public void LoadLiverBuildingScene()
	 {
		SceneManager.LoadScene ("LiverBuilding");
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

	//載入選擇地圖頁面
	public void LoadMapChoosingScene()
	{
		SceneManager.LoadScene ("Map Choosing");
		//之後會在這加上判斷式，判斷玩家選擇的島嶼Button物件名稱
		//然後將選擇的島嶼名稱加入playerPrefs裡(playerPrefs定義一個mapChoosing參數，存玩家選擇的地圖)
	}

	

	public void onTips1(string tips_str)
	{
		GameObject parent = GameObject.Find ("MapChoosePanel");
		GameObject toast = GameObject.Find ("Toast"); // 加载预制体
		GameObject m_toast = GameObject.Instantiate(toast, parent.transform, false);  // 对象初始化
		//m_toast.transform.parent = parent.transform;            //　附加到父节点（需要显示的UI下）
		m_toast.transform.localScale = Vector3.one;
		m_toast.transform.localPosition = new Vector3 (3.3f, -257.0f, 0.0f);
		Text tips = m_toast.GetComponent<Text>();
		tips.text = tips_str;
		Destroy(m_toast, 2); // 2秒后 销毁
	}

	public void onTips2(string tips_str)
	{
		GameObject parent = GameObject.Find ("ToastImg");
		GameObject toast = GameObject.Find ("Toast"); // 加载预制体
		GameObject m_toast = GameObject.Instantiate(toast, parent.transform, false);  // 对象初始化
		//m_toast.transform.parent = parent.transform;            //　附加到父节点（需要显示的UI下）
		m_toast.transform.localScale = Vector3.one;
		m_toast.transform.localPosition = new Vector3 (1f, -246.25f, 0.0f);
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
