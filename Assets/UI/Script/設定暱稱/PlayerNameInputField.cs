using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; 


namespace Com.MyProject.MyPassTheBuckGame
{

	public class PlayerNameInputField : MonoBehaviour
	{

		static string playerNamePrefKey = "PlayerName";

		public Sprite EditImg;
		public Sprite SubmitImg;
		public InputField inputField;
		Button EditBt;


		void Start () 
		{
			inputField.text = PlayerPrefs.GetString (playerNamePrefKey);

		}


		public void SetPlayerName()
		{
			
			inputField = GameObject.Find ("NameIp").GetComponent<InputField>(); 
			string value = inputField.text;

			if (value == "") 
			{
				onTips("取個名字吧!");
			}
			else 
			{
				PhotonNetwork.player.NickName = value; 
				PlayerPrefs.SetString(playerNamePrefKey,value);
				SceneManager.LoadScene("Launcher");
			}

		}

		public void SetPlayerNameInBag()
		{
			EditBt = GameObject.Find ("EditBt").GetComponent<Button>(); 
			Sprite BtSp = EditBt.GetComponent<Image> ().sprite;

			inputField = GameObject.Find ("NameIpFd").GetComponent<InputField>(); 

			if (BtSp == EditImg)
			{
				inputField.interactable = true;
				EditBt.GetComponent<Image> ().sprite = SubmitImg;
			} 
			else if (BtSp == SubmitImg) 
			{
				string value = inputField.text;
				PhotonNetwork.player.NickName = value; 
				PlayerPrefs.SetString(playerNamePrefKey,value);

				inputField.interactable = false;
				EditBt.GetComponent<Image> ().sprite = EditImg;
			}




		}

		public void onTips(string tips_str)
		{
			GameObject parent = GameObject.Find ("Canvas");
			GameObject toast = GameObject.Find ("Toast"); // 加载预制体
			GameObject m_toast = GameObject.Instantiate(toast, parent.transform, false);  // 对象初始化
			//m_toast.transform.parent = parent.transform;            //　附加到父节点（需要显示的UI下）
			m_toast.transform.localScale = Vector3.one;
			m_toast.transform.localPosition = new Vector3 (1.0f, -230.0f, 0.0f);
			Text tips = m_toast.GetComponent<Text>();
			tips.text = tips_str;
			Destroy(m_toast, 3); // 2秒后 销毁
		}
			
	}
}