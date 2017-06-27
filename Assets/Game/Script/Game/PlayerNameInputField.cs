using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Com.MyProject.MyPassTheBuckGame
{
	/// Player name input field. Let the user input his name, will appear above the player in the game.
	public class PlayerNameInputField : MonoBehaviour
	{
		#region Private Variables


		// Store the PlayerPref Key to avoid typos
		static string playerNamePrefKey = "PlayerName";

		InputField _inputField;

		#endregion
	

		/// MonoBehaviour method called on GameObject by Unity during initialization phase.
		void Start () {


			string defaultName = "";
			_inputField = this.GetComponent<InputField>();    //取得該元件

			//inputfield有值
			if (_inputField!=null)
			{
				if (PlayerPrefs.HasKey(playerNamePrefKey))
				{
					defaultName = PlayerPrefs.GetString(playerNamePrefKey);
					_inputField.text = defaultName;
				}
			}

			PhotonNetwork.playerName =  defaultName;
		}


		#region Public Methods


		/// <summary>
		/// Sets the name of the player, and save it in the PlayerPrefs for future sessions.
		/// </summary>
		/// <param name="value">The name of the Player</param>
		public void SetPlayerName()
		{
			_inputField = GameObject.Find ("NameIp").GetComponent<InputField>(); 
			string value = _inputField.text;

			PhotonNetwork.playerName = value + " "; // force a trailing space string in case value is an empty string, else playerName would not be updated.
			Debug.Log(PhotonNetwork.playerName);
			PlayerPrefs.SetString(playerNamePrefKey,value);

		}
			
		#endregion
	}
}