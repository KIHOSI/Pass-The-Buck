using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePortalBall : MonoBehaviour {


    public GameObject PortalBall; //球的陣列
    //public GameObject[] ItemArray; //道具
    //int ballIndex = 0; //球從0開始
    //int itemIndex = 0; //道具從0開始
    //bool showItem = false; //true代表生成、false代表不生成
    //Random ran = new Random();

    // Use this for initialization
    void Start()
    {
        //if (PhotonNetwork.isMasterClient)
       // {
            InvokeRepeating("generateBall", 5, 5); //第一個為方法名、第二個為「第一次調用」要隔幾秒、第三個則是「每隔幾秒調用一次」
        //}
        
        //InvokeRepeating("generateItem", 10, 10);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void generateBall() //產生球
    {
        //ballIndex = Random.Range(0, BallArray.Length); //隨機產生一個在0到最大值間的數(含0)
        Instantiate(PortalBall, transform.position, new Quaternion(0, 0, 0, 0));
    }

    /*void generateItem() //產生道具
    {
        showItem = (Random.value > 0.5f); //true or false
        if (showItem)
        {
            itemIndex = Random.Range(0,ItemArray.Length); //隨機產生一個在0到最大值間的數(含0)
            Instantiate(ItemArray[itemIndex], transform.position, new Quaternion(0, 0, 0, 0));
        }
    }*/
}


