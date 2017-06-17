using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NowState : MonoBehaviour {
    //玩家現在狀態
    public static int money = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static int getMoney()
    {
        return money;
    }
}
