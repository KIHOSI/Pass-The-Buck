using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour {
    Rigidbody2D ballRigidbody2D; //存被打到的物件
    GameObject gb;
    Touch touch;
    private Vector3 clickPos; //滑鼠最初點選的位置
    private float speedDelta = 1.0f;
	
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
            if (hit == false) //沒擊到東西
            {
                return;
            }
            else if (hit.collider.name == "Edge1(藍)" || hit.collider.name == "Edge2(藍)" || hit.collider.name == "Edge1(綠)" || hit.collider.name == "Edge2(綠)") //不能移動背景物
            {
                return;
            }

            gb = hit.collider.gameObject; //得到選擇的物件
            ballRigidbody2D = hit.transform.GetComponent<Rigidbody2D>();
            ballRigidbody2D.velocity = Vector2.zero; //把該物件的速度設為0
            clickPos = Input.GetTouch(0).position; //是Vector2 不知道有沒有影響
        }
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
}
