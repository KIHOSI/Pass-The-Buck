using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyProject.MyPassTheBuckGame
{
     public class LevelBar : MonoBehaviour {

		public float MaxExp;
		public float CurrentExp;
		public Text LevelBarTx;
		public float ExpPercent;

	    void Start ()
		{

	    }
	
	
	    void Update () 
		{
			ExpPercent = (CurrentExp / MaxExp)*100;
			this.transform.localPosition = new Vector3((-172.0f + 172.0f * ExpPercent), 0.0f, 0.0f);
			LevelBarTx.text = ExpPercent + "%";
	    }






	}
}
