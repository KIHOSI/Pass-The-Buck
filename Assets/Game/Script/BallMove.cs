using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BallMove : MonoBehaviour
{
    Rigidbody2D ballRigidbody2D;
    public float speedX;    //球的水平速度
    public float speedY;    //球的垂直速度
    //public bool ballState = false; //球的狀態，false:左 | true:右

    private Vector3 prePos; //滑鼠點選位置
    private Vector3 clickPos; //滑鼠最初點選的位置
    private float speedDelta = 1.0f;
    bool startDrag; //判斷是不是點到這顆球

    void Start()
    {
        ballRigidbody2D = GetComponent<Rigidbody2D>();
        ballRigidbody2D.velocity = new Vector2(speedX, speedY);

    }

    void Update()
    {

        if (transform.position.x > 3 || transform.position.x < -3 || transform.position.y > 5 || transform.position.y < -5)
        {
            //如果物件的X值大於或小於3，物件的Y值大於5或小於5就將物件刪除(差不多離開手機範圍)
            Destroy(gameObject);
        }
    }  

}
