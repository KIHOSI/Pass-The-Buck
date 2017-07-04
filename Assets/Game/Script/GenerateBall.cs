using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBall : MonoBehaviour {

    public GameObject[] BallArray; //球的陣列
    int ballIndex = 0; //球從0開始

    // Use this for initialization
    void Start () {
        //Invoke("generateBall", 3);
        InvokeRepeating("generateBall", 5, 5); //第一個為方法名、第二個為「第一次調用」要隔幾秒、第三個則是「每隔幾秒調用一次」
    }
	
	// Update is called once per frame
	void Update () {
    }

    void generateBall()
    {
        ballIndex = Random.Range(0, BallArray.Length); //隨機產生一個在0到最大值間的數(含0)
            GameObject ball = (GameObject)Instantiate(BallArray[ballIndex], transform.position, new Quaternion(0, 0, 0, 0));
    }
}
