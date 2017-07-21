using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateBomb : MonoBehaviour {
    public GameObject Bomb; //要生成的炸彈
    //public Button btn;

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
    }
    
}
