using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class BallMove : MonoBehaviour
{
    //public GameObject test;
    Rigidbody2D ballRigidbody2D;
    public float speedX;    //球的水平速度
    public float speedY;    //球的垂直速度
    GameObject ballCollisionMusic; //球撞擊聲
                          
    //初始化函数，在游戏开始时系统自动调用。一般用来创建变量之类的东西。  
    void Awake() //寫在start會覆蓋掉後續的速度改變
    {
        ballRigidbody2D = GetComponent<Rigidbody2D>();
        ballRigidbody2D.velocity = new Vector2(speedX, speedY); //初始速度
        ballCollisionMusic = GameObject.Find("球反彈音樂");
    }
    void Start()//初始化函数，在所有Awake函数运行完之后（一般是这样，但不一定），在所有Update函数前系统自动条用。一般用来给变量赋值。 
    {
        
    }
    void Update()
    {
        if (transform.position.x > 3 || transform.position.x < -3.1 || transform.position.y > 5 || transform.position.y < -5)
        {
            //如果物件的X值大於或小於3，物件的Y值大於5或小於5就將物件刪除(差不多離開手機範圍)
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(ballCollisionMusic);
    }

}
