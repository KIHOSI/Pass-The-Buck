using UnityEngine;
using UnityEngine.UI;

public class BallMove : MonoBehaviour {

    Rigidbody2D ballRigidbody2D;

    public float speedX;    //球的水平速度
    public float speedY;    //球的垂直速度

    public bool ballState = false; //球的狀態，false:左 | true:右

    void Start()
    {
        ballRigidbody2D = GetComponent<Rigidbody2D>();
        ballRigidbody2D.velocity = new Vector2(speedX, speedY);
    }
	
	
	void Update () {
        
      
    }

    void OnCollisionEnter2D(Collision2D collision) //發生碰撞時
    {
        if (gameObject.CompareTag("左邊的球"))
        {
            ballState = false;
        }
        else if (gameObject.CompareTag("右邊的球"))
        {
            ballState = true;
        }
        lockSpeed();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.CompareTag("洞口"))
        {
            NowState.reduceMoney(); //減錢
            int money = NowState.getMoney(); //得到現在錢的狀態
            //money -= 10; //減錢
            print("現在的錢:" + money);
            moneyText.text = "金錢 x" + money;
        }*/
    }

    void lockSpeed() //保持速度
    {
        Vector2 lockSpeed = new Vector2(ResetSpeedX(), ResetSpeedY());
        ballRigidbody2D.velocity = lockSpeed;
    }

    float ResetSpeedX() //保持水平速度
    {
        float currentSpeedX = ballRigidbody2D.velocity.x; //現在球的實際水平速度
        if (ballState == true) //右邊的球
        {
            if (currentSpeedX < 0) //左邊
            {
                return speedX;
            }
            else //右邊
            {
                return -speedX;
            }
        }
        else  //左邊的球
        {
            if (currentSpeedX < 0) //左邊
            {
                return -speedX;
            }
            else //右邊
            {
                return speedX;
            }
        }
    }
    
    float ResetSpeedY() //保持垂直速度
    {
        float currentSpeedY = ballRigidbody2D.velocity.y; //現在球的實際水平速度
        if (currentSpeedY < 0) //下面
        {
            return -speedY;
        }
        else //上面
        {
            return speedY;
        }
    }
  
}
