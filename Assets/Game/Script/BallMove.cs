using UnityEngine;
using UnityEngine.UI;

public class BallMove : MonoBehaviour {

    Rigidbody2D ballRigidbody2D;

    public float speedX;    //球的水平速度
    public float speedY;    //球的垂直速度

    public bool ballState = false; //球的狀態，false:左 | true:右

    public float speedDelta = 1.0f;
    private bool startDrag;
    private Vector3 prePos;
    private Vector3 clickPos; //滑鼠點選的位置

    void Start()
    {
        ballRigidbody2D = GetComponent<Rigidbody2D>();
        ballRigidbody2D.velocity = new Vector2(speedX, speedY);

        prePos = Camera.main.WorldToScreenPoint(transform.position); //計算滑鼠座標換算成世界座標
    }
	
	
	void Update () {
       
          //  case DragMethod.SmoothDamp:
                if (Input.GetKeyUp(KeyCode.Mouse0)) //當放開滑鼠左鍵
                {
                    if (startDrag)
                    {
                        startDrag = false;
                      
                        //在滑鼠放開的時候，根據一開始按球的點與放球的點距離，來做速度
                        Vector3 curPos = Input.mousePosition;
                        Vector3 dir = curPos - clickPos;
                        float dist = dir.magnitude;
                        float v = dist / Time.deltaTime;
                            
                        ballRigidbody2D.AddForce(dir.normalized * v * Time.deltaTime * speedDelta);
                }

                }
                if (startDrag) //持續拖曳
                {
                    prePos = Input.mousePosition;
                    TowardTarget(); //如果拖曳中，就讓物體往滑鼠的座標移動
                    //Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(prePos.x, prePos.y, 10f));
                    //transform.position = targetPos;
                }

        if (transform.position.y > 10 || transform.position.y < -10)
        {
            //如果物件的Y值大於10或小於10就將物件刪除
            Destroy(gameObject);
        }
    }

    private void OnMouseDown() //當滑鼠點下物件的時候重設一些數值
    {
        prePos = Camera.main.WorldToScreenPoint(transform.position); //轉成
        clickPos = Input.mousePosition;
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

    //朝著滑鼠的方式移動
    public Vector3 v;
    public float maxSpeed = 5.0f;
    void TowardTarget()
    { //讓物體朝著最後移動的向量移動
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(prePos.x, prePos.y, 10f)); //Assume your camera's z is -10 and cube's z is 0
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref v, speedDelta, maxSpeed);
    }
}
