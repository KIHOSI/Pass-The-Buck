using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Com.MyProject.MyPassTheBuckGame
{

	public class PlayerNameInputField : MonoBehaviour
	{

		static string playerNamePrefKey = "PlayerName";

		InputField inputField;


		void Start () 
		{


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
				PhotonNetwork.playerName = value; 
				PlayerPrefs.SetString(playerNamePrefKey,value);
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