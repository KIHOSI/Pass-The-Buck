using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

namespace Com.MyProject.MyPassTheBuckGame
{
   public class ChooseMap : MonoBehaviour {

	  public Sprite island;
	  public Sprite islandWithLock;

	  public GameObject Island1Bt;
	  public GameObject Island2Bt;
	  public GameObject Island3Bt;

	  Button Island1;
	  Button Island2;
	  Button Island3;

	  // Use this for initialization
	  void Start () 
	  {
		 Island1 = Island1Bt.GetComponent<Button> ();
		 Island2 = Island2Bt.GetComponent<Button> ();
		 Island3 = Island3Bt.GetComponent<Button> ();

		 //會有資料庫存玩家已解鎖的島嶼名稱

		 //一開始GUI全部島嶼都會在畫面上
		 //剛載入地圖選擇頁面時，系統從資料庫讀取玩家的解鎖島嶼名稱
		 //將解鎖島嶼的button圖案換成無解鎖圖
	  }

	 #region Public Methods

	  //檢查關卡已解鎖或未解鎖
	  public void CheckLockorUnlock(Button bt)
	  {
		  //button.isenable = false;

		  if (bt.GetComponent<Image> ().sprite == island) 
		  {
			 LoadCharacterChoosingScene ();
		  } 
		  else if (bt.GetComponent<Image> ().sprite == islandWithLock)
		  {
			 Debug.Log("該關卡尚未解鎖，請選擇已解鎖關卡");
		  }


	  }

	 //載入角色選擇頁面
	 public void LoadCharacterChoosingScene()
	 {
		SceneManager.LoadScene ("Character Choosing");
		//之後會在這加上判斷式，判斷玩家選擇的島嶼Button物件名稱
		//然後將選擇的島嶼名稱加入playerPrefs裡(playerPrefs定義一個mapChoosing參數，存玩家選擇的地圖)
	 }



	#endregion


  }
}
