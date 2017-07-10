using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BallMove : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Rigidbody2D ballRigidbody2D;
    public float speedX;    //球的水平速度
    public float speedY;    //球的垂直速度
    public bool ballState = false; //球的狀態，false:左 | true:右

    private Vector3 prePos; //滑鼠點選位置
    private Vector3 clickPos; //滑鼠最初點選的位置
    private float speedDelta = 1.0f;


    //觸控拖曳
    public static GameObject DraggedInstance;

    Vector3 _startPosition;
    Vector3 _offsetToMouse;
    float _zDistanceToCamera;

    #region Interface Implementations

    void Start()
    {
        ballRigidbody2D = GetComponent<Rigidbody2D>();
        ballRigidbody2D.velocity = new Vector2(speedX, speedY);

    }

    void Update()
    {
        if (transform.position.y > 10 || transform.position.y < -10)
        {
            //如果物件的Y值大於10或小於10就將物件刪除
            Destroy(gameObject);
        }
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
        //lockSpeed();
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



    public void OnBeginDrag(PointerEventData eventData)
    {
        clickPos = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.touchCount > 1)
            return;

        prePos = Input.mousePosition; //現在滑鼠位置
        TowardTarget();

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DraggedInstance = null;
        _offsetToMouse = Vector3.zero;

        //在滑鼠放開的時候，根據一開始按球的點與放球的點距離，來做速度
        Vector3 curPos = Input.mousePosition;
        Vector3 dir = curPos - clickPos;
        float dist = dir.magnitude;
        float v = dist / Time.deltaTime;

        ballRigidbody2D.AddForce(dir.normalized * v * Time.deltaTime * speedDelta);

    }

    #endregion


    //朝著滑鼠的方式移動
    Vector3 v;
    float maxSpeed = 5.0f;
    void TowardTarget()
    { //讓物體朝著最後移動的向量移動
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(prePos.x, prePos.y, 10f)); //Assume your camera's z is -10 and cube's z is 0
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref v, speedDelta, maxSpeed);
    }
}
