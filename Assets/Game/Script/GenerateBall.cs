using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBall : MonoBehaviour {

    public GameObject Ball;

	// Use this for initialization
	void Start () {
        Invoke("generateBall", 3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void generateBall()
    {
        GameObject ball = (GameObject)Instantiate(Ball, transform.position, new Quaternion(0, 0, 0,0));
    }
}
