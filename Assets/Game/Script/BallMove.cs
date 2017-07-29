using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class BallMove : MonoBehaviour
{
    //public GameObject test;
    //GameObject thisBall; //此ball
    Rigidbody2D ballRigidbody2D;
    public float speedX;    //球的水平速度
    public float speedY;    //球的垂直速度
    GameObject ballCollisionMusic; //球撞擊聲
    bool blueTrue = false; //現在是否為藍色執政? true:yes、false:no
    //麥克風效果
    bool microphoneTrue = false;
    Vector3 targetPos; //目標位置            
              
    //初始化函数，在游戏开始时系统自动调用。一般用来创建变量之类的东西。  
    void Awake() //寫在start會覆蓋掉後續的速度改變
    {
        //thisBall.GetComponent<GameObject>();
        ballRigidbody2D = GetComponent<Rigidbody2D>();
        ballRigidbody2D.velocity = new Vector2(speedX, speedY); //初始速度
        ballCollisionMusic = GameObject.Find("球反彈音樂");
    }
    void Start()//初始化函数，在所有Awake函数运行完之后（一般是这样，但不一定），在所有Update函数前系统自动条用。一般用来给变量赋值。 
    {
        
    }
    void Update()
    {
        if(microphoneTrue == true) //開啟麥克風效果
        {
            //TowardTarget(targetPos);
            ForceCalculate(targetPos);
        }


        //判斷現在速度是否為0，讓他可以移動
        float nowSpeedX = ballRigidbody2D.velocity.x;
        float nowSpeedY = ballRigidbody2D.velocity.y;
        if(nowSpeedX == 0 && nowSpeedY == 0)
        {
            //Debug.Log("我有跑進來");
            ballRigidbody2D.AddForce(new Vector2(2,2));
            //ballRigidbody2D.velocity = new Vector2(1, 1);
        }

        /*if (transform.position.x > 3 || transform.position.x < -3.1 || transform.position.y > 5 || transform.position.y < -5)
        {
            //如果物件的X值大於或小於3，物件的Y值大於5或小於5就將物件刪除(差不多離開手機範圍)
            Destroy(gameObject);
        }*/
    }
    private void OnCollisionEnter2D(Collision2D collision) //發生碰撞
    {
        if(blueTrue == true)
        {
            lockSpeed(); //開啟鎖住速度
        }

        GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(ballCollisionMusic);
    }

    public void blueEffectButton() //藍黨執政效果，才將速度鎖起來
    {
        if(blueTrue == false) //原本是no，改成yes
        {
            blueTrue = true;
        }
        else if(blueTrue == true) //原本是yes，改成no
        {
            blueTrue = false;
        }
        
    }

    public void microphoneEffect(Vector3 pos) //麥克風效果
    {
        targetPos = pos;
        microphoneTrue = true;
       
    }

    void lockSpeed() //鎖住速度
    {
        Vector2 lockSpeed = new Vector2(resetSpeedX(), resetSpeedY()); //固定的速度
        ballRigidbody2D.velocity = lockSpeed; //改變現在速度為固定的
    }

    float resetSpeedX() //發生碰撞後，因為速度會不一樣，所以要讓它等速
    {
        float currentSpeedX = ballRigidbody2D.velocity.x;
        if (currentSpeedX < 0) //向左移動
        {
            return -speedX; //向左等速
        }
        else //向右移動
        {
            return speedX; //向右等速
        }
    }

    float resetSpeedY() //發生碰撞後，因為速度會不一樣，所以要讓它等速
    {
        float currentSpeedY = ballRigidbody2D.velocity.y;
        if (currentSpeedY < 0) //向下移動
        {
            return -speedY; //向下等速
        }
        else //向上移動
        {
            return speedY; //向上等速
        }
    }

    //麥克風效果，金球會移到目標位置
    Vector3 v;
    public float maxSpeed = 5.0f;
    public float speedDelta = 1.0f;
    void TowardTarget(Vector3 pos)
    {
        //Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 10f)); //Assume your camera's z is -10 and cube's z is 0
        Vector3 targetPos = pos;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref v, speedDelta, maxSpeed);
    }

    //麥克風效果2，給他個力
    void ForceCalculate(Vector3 pos)
    {
        //Vector3 curPos = Input.mousePosition;
        Vector3 curPos = transform.position;
        Vector3 dir = pos - curPos;
        float dist = dir.magnitude;
        float v = dist / Time.deltaTime;

        ballRigidbody2D.AddForce(dir.normalized * v * Time.deltaTime * speedDelta);
        //pos = curPos;
    }

}
