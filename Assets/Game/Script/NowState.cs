using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowState : MonoBehaviour {
    //計算錢
    public int money = 100; //錢
    public int time = 120; //時間(秒)
    public Text moneyText; //顯示錢的資訊
    public Text timeText; //顯示時間
    public GameObject canvasPrefab; //要產生的canvas

	// Use this for initialization
	void Start () {
        moneyText.text = "金錢 x" + money;
        InvokeRepeating("timeCountDown", 1, 1); //每隔一秒執行一次
	}
	
	// Update is called once per frame
	void Update () { 
        if (time == 0) //如果時間歸零，就停止減少
        {
            //Instantiate(canvasPrefab, Vector2.zero, Quaternion.identity);
            CancelInvoke("timeCountDown");
            timeText.text = "Game Over";
            Time.timeScale = 0f; //時間暫停
            
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("左邊的球") || collision.gameObject.CompareTag("右邊的球"))
        { //黑球
            money -= 10;
            moneyText.text = "金錢 x" + money;
        }
        if(collision.gameObject.CompareTag("左邊的金球") || collision.gameObject.CompareTag("右邊的金球"))
        { //金球
            money += 10;
            moneyText.text = "金錢 x" + money;
        }
        if (collision.gameObject.CompareTag("炸彈"))
        { //炸彈，扣50%的錢
            if(money > 0)
            {
                money = money / 2;
                moneyText.text = "金錢 x" + money;
            }
        }
    }

    void timeCountDown() //時間倒數，每次減一秒
    {
        timeText.text = "" + time;
        time--;
    }
}
