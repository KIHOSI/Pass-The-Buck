using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestMove1 : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Rigidbody2D ballRigidbody2D;
    public float speedX;    //球的水平速度
    public float speedY;    //球的垂直速度
    public bool ballState = false; //球的狀態，false:左 | true:右

    //滑鼠拖曳
    /*private bool startDrag;
    private Vector3 prePos;
    public float speedDelta = 1.0f;*/

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

       // prePos = Camera.main.WorldToScreenPoint(transform.position);

    }

    void Update()
    {
        if (transform.position.y > 10 || transform.position.y < -10)
        {
            //如果物件的Y值大於10或小於10就將物件刪除
            Destroy(gameObject);
        }

        /*if (startDrag)
        {
            prePos = Input.mousePosition;
            TowardTarget(); //如果拖曳中，就讓物體往滑鼠的座標移動
        }*/
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

        //prePos = Camera.main.WorldToScreenPoint(transform.position);
        //startDrag = true;


        //if (startDrag) //如果放開滑鼠，就停止物體追隨滑鼠的效果，並讓物體朝著最後移動的向量移動
        // {
        // ballRigidbody2D.velocity = v;
        //   startDrag = false;
        //v = Vector3.zero;
        // }

        /*if (startDrag) //如果放開滑鼠，就停止物體追隨滑鼠的效果，並讓物體朝著最後移動的向量移動
        {
            ballRigidbody2D.velocity = v;
            startDrag = false;
            v = Vector3.zero;
        }*/


        DraggedInstance = gameObject;
        _startPosition = transform.position;
        _zDistanceToCamera = Mathf.Abs(_startPosition.z - Camera.main.transform.position.z);

        _offsetToMouse = _startPosition - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
        );
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.touchCount > 1)
            return;

        transform.position = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
            ) + _offsetToMouse;


        /*ballRigidbody2D.velocity = v;
        startDrag = false;
        v = Vector3.zero;*/


        //if (startDrag)
       // {
         //   prePos = Input.mousePosition;
        //    TowardTarget(); //如果拖曳中，就讓物體往滑鼠的座標移動
        //}

        //prePos = Input.mousePosition;
        //TowardTarget(); //如果拖曳中，就讓物體往滑鼠的座標移動
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DraggedInstance = null;
        _offsetToMouse = Vector3.zero;


        //if (startDrag)
        //{
          //  prePos = Input.mousePosition;
          //  TowardTarget(); //如果拖曳中，就讓物體往滑鼠的座標移動
       // }
    }

    #endregion

/*

    public Vector3 v;
    public float maxSpeed = 5.0f;
    void TowardTarget()
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(prePos.x, prePos.y, 10f)); //Assume your camera's z is -10 and cube's z is 0
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref v, speedDelta, maxSpeed);
    }*/
}

