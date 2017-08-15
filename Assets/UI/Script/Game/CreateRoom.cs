using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

namespace Com.MyProject.MyPassTheBuckGame
{
	
	public class CreateRoom : Photon.PunBehaviour{


		#region Public Variables

		public string MaxPlayerChoosed; 
		public byte MaxPlayersPerRoom;
		public string GameRoomName;
		public int menuIndex;
		public List<Dropdown.OptionData> menuOptions;


		#endregion

		#region MonoBehaviour CallBacks


	   void Start ()
	   {

       }

		#endregion

		#region Public Methods

		//開房間(按下創建按鈕)
		public void CreateGameRoom()
		{

			GameRoomName = GameObject.Find ("GameRoomNameIp1").GetComponent<InputField> ().text;
			menuOptions= GameObject.Find ("PlayerNumberDd").GetComponent<Dropdown> ().options;

			menuIndex =GameObject.Find ("PlayerNumberDd").GetComponent<Dropdown> ().value;

			MaxPlayerChoosed = menuOptions [menuIndex].text;

			Debug.Log(MaxPlayerChoosed);

			if (MaxPlayerChoosed =="2")
			{
				MaxPlayersPerRoom = 2;
			}
			else if (MaxPlayerChoosed=="4")
			{
				MaxPlayersPerRoom = 4;
			}
			else if (MaxPlayerChoosed=="3")
			{
				MaxPlayersPerRoom = 3;
			}

			Debug.Log(MaxPlayersPerRoom);

			RoomOptions options = new RoomOptions ();
			options.MaxPlayers = MaxPlayersPerRoom;


			if (GameRoomName == "") 
			{
				onTips ("房名不可空白!");
			}
			else 
			{
				PhotonNetwork.CreateRoom(GameRoomName,options,null);
			}
				

		}
			
		//按返回鍵
		public void Back()
		{
			SceneManager.LoadScene("Character Choosing");
		}

		public void onTips(string tips_str)
		{
			GameObject parent = GameObject.Find ("Canvas");
			GameObject toast = GameObject.Find ("Toast"); // 加载预制体
			GameObject m_toast = GameObject.Instantiate(toast, parent.transform, false);  // 对象初始化
			//m_toast.transform.parent = parent.transform;            //　附加到父节点（需要显示的UI下）
			m_toast.transform.localScale = Vector3.one;
			m_toast.transform.localPosition = new Vector3 (3.3f, -234.3f, 0.0f);
			Text tips = m_toast.GetComponent<Text>();
			tips.text = tips_str;
			Destroy(m_toast, 3); // 2秒后 销毁
		}

		#endregion

		#region Photon.PunBehaviour CallBacks

		public override void OnCreatedRoom()
		{
			SceneManager.LoadScene("Waiting Room");
		}

		public override void OnPhotonCreateRoomFailed (object[] codeAndMsg)
		{
			onTips ("房間已存在");
		}

		public override void OnDisconnectedFromPhoton()
		{
			Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
			SceneManager.LoadScene("Main");
		}
			

		#endregion

	}
}
