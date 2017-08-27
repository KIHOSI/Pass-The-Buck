using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyProject.MyPassTheBuckGame
{
    public class WelcomeTx : MonoBehaviour
	{

	  string name;

	  void Start () 
	  {
		name = PlayerPrefs.GetString ("PlayerName");
		this.GetComponent<Text> ().text = "嗨~" + name + " ◕‿◕";
	  }

    }
}
