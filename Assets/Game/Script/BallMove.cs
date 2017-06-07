using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour {

    //float thrust = 1.5f;
    //float horizontal = Input.GetAxis("Horizontal");
    public Rigidbody2D rg2d;

    // Use this for initialization
    void Start () {
        rg2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        //transform.Translate(Vector3.up * 0.1f);


        /*Vector2 force2D = Vector2.zero; 
        force2D.y += 1.5f;
        rg2d.AddForce(force2D);
        */


        rg2d.AddForce(Vector3.up * 10f * Time.deltaTime);
        // rg2d.AddForce(new Vector2(horizontal,0));
        // rg2d.AddForce(new Vector2(horizontal, 0), ForceMode2D.Force); //Force mode: 對物件施加具方向性的力，同AddForce基本定義.
        //rg2d.AddForce(new Vector2(horizontal, 0), ForceMode2D.Impulse); //Impluse mode: 對物件施加一個脈衝力，該力量一樣具有方向性.
        //rg2d.AddForce(transform.up * horizontal);
        //rg2d.AddForce(transform.up * 100);

        //rg2d.AddForce(Vector3.up * 300);
    }
   /* void FixedUpdate()
    {
        rg2d.AddForce(transform.up * 100);
    }*/
}
