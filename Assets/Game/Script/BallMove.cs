using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour {

    Rigidbody2D ballRigidbody2D;

    public float speedX;    //球的水平速度
    public float speedY;    //球的垂直速度
  
    void Start () {
        ballRigidbody2D = GetComponent<Rigidbody2D>();
        ballRigidbody2D.velocity = new Vector2(speedX, speedY);
    }
	
	
	void Update () {
        
      
    }

    void OnCollisionEnter2D(Collision2D collision) //發生碰撞時
    {
        lockSpeed();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("背景"))
        {
            // = true;
            //EdgeCollider2D collisionEdgeCollider2D = collision.gameObject.GetComponent<EdgeCollider2D>(); //取得EdgeCollider2D
            //collisionEdgeCollider2D.enabled = true;
        }
    }

    void lockSpeed() //保持速度
    {
        Vector2 lockSpeed = new Vector2(ResetSpeedX(), ResetSpeedY());
        ballRigidbody2D.velocity = lockSpeed;
    }

    float ResetSpeedX() //保持水平速度
    {
        float currentSpeedX = ballRigidbody2D.velocity.x; //現在球的實際水平速度
        if(currentSpeedX < 0) //左邊
        {
            return -speedX;
        }
        else //右邊
        {
            return speedX;
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
