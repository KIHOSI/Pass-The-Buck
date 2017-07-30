using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

namespace Com.MyProject.MyPassTheBuckGame
{
    public class FirstInGame : MonoBehaviour 
	{

	    void Start () 
		{
			//第一次進遊戲
			if (!PlayerPrefs.HasKey ("HasPlayed"))
			{
				PlayerPrefs.SetInt ("HasPlayed", 1);
				SceneManager.LoadScene("Opening Story");
			} 
			//不是第一次進遊戲
			else 
			{
				SceneManager.LoadScene("Launcher");
			}
	    }
	}
}
