using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour {
    Rigidbody2D ballRigidbody2D; //存被打到的物件
    GameObject gb;
    Touch touch;

    // private Vector3 prePos; //滑鼠點選位置
    private Vector3 clickPos; //滑鼠最初點選的位置
    private float speedDelta = 1.0f;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10, 1); //偵測
        
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
        }

        if (touch.phase == TouchPhase.Began) // Began: 觸控點開始移動時
        {
            if(hit == false) //沒擊到東西
            {
                return;
            }
            else if(hit.collider.name == "Edge1" || hit.collider.name == "Edge2") //不能移動背景物
            {
                return;
            }

            gb = hit.collider.gameObject; //得到選擇的物件
            ballRigidbody2D = hit.transform.GetComponent<Rigidbody2D>();
            ballRigidbody2D.velocity = Vector2.zero; //把該物件的速度設為0
            clickPos = Input.GetTouch(0).position; //是Vector2 不知道有沒有影響
        }
       /* if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary) //Moved: 觸控點移動中
        {
            prePos = Input.GetTouch(0).position;
            //TowardTarget();

        }*/
        if (touch.phase == TouchPhase.Ended) // Ended: 手指離開螢幕
        {
            //在滑鼠放開的時候，根據一開始按球的點與放球的點距離，來做速度
            Vector3 curPos = Input.mousePosition;
            Vector3 dir = curPos - clickPos;
            float dist = dir.magnitude;
            float v = dist / Time.deltaTime;

            if (ballRigidbody2D != null) 
            {
                ballRigidbody2D.AddForce(dir.normalized * v * Time.deltaTime * speedDelta);
            }

            gb = null; //初始化
            ballRigidbody2D = null;
        }
    }

    /*//朝著滑鼠的方式移動
    Vector3 v;
    float maxSpeed = 5.0f;
    void TowardTarget()
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(prePos.x, prePos.y, 10f)); //Assume your camera's z is -10 and cube's z is 0
        gb.transform.position = Vector3.SmoothDamp(gb.transform.position, targetPos, ref v, speedDelta, maxSpeed);
    }*/
}
