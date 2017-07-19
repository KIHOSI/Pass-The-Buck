using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class BallMove : MonoBehaviour
{
    Rigidbody2D ballRigidbody2D;
    public float speedX;    //球的水平速度
    public float speedY;    //球的垂直速度
    private string objName; //儲存物件名字，以便傳資訊

    void Start()
    {
        ballRigidbody2D = GetComponent<Rigidbody2D>();
        ballRigidbody2D.velocity = new Vector2(speedX, speedY); //初始速度
        objName = this.name;
        Debug.Log(objName);
    }

    void Update()
    {

        if (transform.position.x > 3.3 || transform.position.x < -3.3 || transform.position.y > 5 || transform.position.y < -5)
        {
            //如果物件的X值大於或小於3，物件的Y值大於5或小於5就將物件刪除(差不多離開手機範圍)
            Destroy(gameObject);
        }
    }

}
