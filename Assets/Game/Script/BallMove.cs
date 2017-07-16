using UnityEngine;
using UnityEngine.UI;

public class BallMove : MonoBehaviour {

    Rigidbody2D ballRigidbody2D;

    public float speedX;    //球的水平速度
    public float speedY;    //球的垂直速度

    public bool ballState = false; //球的狀態，false:左 | true:右

    //public enum DragMethod { Force,SmoothDamp}; //兩種拖曳方式
    //public DragMethod method = DragMethod.SmoothDamp; 

    public float speedDelta = 1.0f;
    //public float decelerate = 1.0f;
    private bool startDrag;
    private Vector3 prePos;

    void Start()
    {
        ballRigidbody2D = GetComponent<Rigidbody2D>();
        ballRigidbody2D.velocity = new Vector2(speedX, speedY);

        prePos = Camera.main.WorldToScreenPoint(transform.position);
        //ballRigidbody2D.drag = decelerate; //使用這個來讓物體逐漸停止，也可以在Rigidbody中設定drag值，這樣就可以移除這行
    
}
	
	
	void Update () {
        //switch (method)
        //{
       /*     case DragMethod.Force:
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    startDrag = false;
                }
                if (startDrag)
                {
                    ForceCalculate(); //如果拖曳中，就一直給物體施加滑鼠造成的力
                }
            break;
*/
          //  case DragMethod.SmoothDamp:
                if (Input.GetKeyUp(KeyCode.Mouse0)) //當按下滑鼠左鍵
                {
                    if (startDrag) //如果放開滑鼠，就停止物體追隨滑鼠的效果，並讓物體朝著最後移動的向量移動
                    {
                        ballRigidbody2D.velocity = v;
                        startDrag = false;
                        v = Vector3.zero;
                    }
                }
                if (startDrag)
                {
                    prePos = Input.mousePosition;
                    TowardTarget(); //如果拖曳中，就讓物體往滑鼠的座標移動
                }
           // break;
        //}


        if (transform.position.y > 10 || transform.position.y < -10)
        {
            //如果物件的Y值大於10或小於10就將物件刪除
            Destroy(gameObject);
        }
    }

    private void OnMouseDown() //當滑鼠點下物件的時候重設一些數值
    {
        //switch (method)
        //{
        //    case DragMethod.Force:
        //        prePos = Input.mousePosition;
         //   break;

         //   case DragMethod.SmoothDamp:
                prePos = Camera.main.WorldToScreenPoint(transform.position);
        //    break;
        //}
        startDrag = true;
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

    /*//施力的方式移動
    void ForceCalculate()
    {
        Vector3 curPos = Input.mousePosition;
        Vector3 dir = curPos - prePos;
        float dist = dir.magnitude;
        float v = dist / Time.deltaTime;

        ballRigidbody2D.AddForce(dir.normalized * v * Time.deltaTime * speedDelta);
        prePos = curPos;
    }*/

    //朝著滑鼠的方式移動
    public Vector3 v;
    public float maxSpeed = 5.0f;
    void TowardTarget()
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(prePos.x, prePos.y, 10f)); //Assume your camera's z is -10 and cube's z is 0
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref v, speedDelta, maxSpeed);
    }

}
