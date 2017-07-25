using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateBomb : MonoBehaviour {
    public GameObject Bomb; //要生成的炸彈
    public GameObject Forbidden; //禁止的符號

    // Use this for initialization
    void Start() {
        GetComponent<Button>().onClick.AddListener(ClickEvent); //點選按鈕，啟動動作
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ClickEvent()
    {
        Instantiate(Bomb, Vector2.zero, Quaternion.identity); //生成一顆炸彈(從中間生出)
        //如果按一次的話，要等15秒才能再次使用炸彈
        Forbidden.SetActive(true); 
        Invoke("hideForbidden", 15); 
    }

    void hideForbidden()
    {
        Forbidden.SetActive(false); //把禁止隱藏，可再次使用炸彈
    }
    
}
