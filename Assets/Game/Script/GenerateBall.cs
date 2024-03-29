﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBall : MonoBehaviour {

    public GameObject[] BallArray; //球的陣列
    public GameObject[] ItemArray; //道具
    int ballIndex = 0; //球從0開始
    int itemIndex = 0; //道具從0開始
    bool showItem = false; //true代表生成、false代表不生成
    public int generateBallseconds; //產生球的秒數
    public int generateItemseconds; //產生道具的秒數
    public int leftOrRight; //判斷是左邊框還是右邊框，left:1、right:2
    public bool showLeftOrRight; //判斷是左邊產生或右邊產生
    public float changeSpeedX = 2; //指定球的速度 
    public float changeSpeedY = 2; //指定球的速度

    public void startGenerateBall()
    {
        InvokeRepeating("generateBall", 2, generateBallseconds); //第一個為方法名、第二個為「第一次調用」要隔幾秒、第三個則是「每隔幾秒調用一次」
        InvokeRepeating("generateItem", 15, generateItemseconds);
    }

    void generateBall() //產生球
    {
        GameObject ball;
        ballIndex = Random.Range(0, BallArray.Length); //隨機產生一個在0到最大值間的數(含0)
        ball = Instantiate(BallArray[ballIndex], transform.position, new Quaternion(0, 0, 0, 0));
        if(leftOrRight == 1) //左邊產生
        {
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(changeSpeedX,changeSpeedY);
        }
        else if(leftOrRight == 2) //右邊產生
        {
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(-changeSpeedX, changeSpeedY);
        }
    }

    void generateItem() //產生道具
    {
        showItem = (Random.value > 0.5f); //true or false
        showLeftOrRight = (Random.value > 0.5f); //判斷要左or右邊框產生
        if (showItem) //產生道具
        {
            GameObject item;
            itemIndex = Random.Range(0,ItemArray.Length); //隨機產生一個在0到最大值間的數(含0)
            if(showLeftOrRight == true) //左邊產生
            {
                if(leftOrRight == 1) //左邊
                {
                    item = Instantiate(ItemArray[itemIndex], transform.position, new Quaternion(0, 0, 0, 0));
                    item.GetComponent<Rigidbody2D>().velocity = new Vector2(changeSpeedX, changeSpeedY);
                }
            }
            else if(showLeftOrRight == false) //右邊產生
            {
                if(leftOrRight == 2) //右邊
                {
                    item = Instantiate(ItemArray[itemIndex], transform.position, new Quaternion(0, 0, 0, 0));
                    item.GetComponent<Rigidbody2D>().velocity = new Vector2(-changeSpeedX, changeSpeedY);
                }
            }
        }
    }
}
