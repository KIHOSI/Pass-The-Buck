using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MyProject.MyPassTheBuckGame
{
     public class LevelBar : MonoBehaviour {

		public float MaxExp;
		public float CurrentExp;

	    void Start ()
		{

	    }
	
	
	    void Update () 
		{
			this.transform.localPosition = new Vector3((-172.0f + 172.0f * (CurrentExp/MaxExp)), 0.0f, 0.0f);
	    }






	}
}
