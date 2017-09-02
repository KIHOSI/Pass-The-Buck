using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateFlashing : MonoBehaviour {
    public GameObject Forbidden; //禁止的符號
    public GameObject useBombMusic; //使用炸彈音樂
    public GameObject canUseBombMusic; //可使用炸彈的音效

    // Use this for initialization
    void Start() {
        GetComponent<Button>().onClick.AddListener(ClickEvent); //點選按鈕，啟動動作
    }

    void ClickEvent()
    {
        GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(useBombMusic);
        GameObject.Find("洞口").GetComponent<powerCutNowState>().openPowerCutEffect(); //使用停電效果，因為object隱藏狀態無法傳送Photon View
        //如果按一次的話，要等15秒才能再次使用炸彈
        Forbidden.SetActive(true); 
        Invoke("hideForbidden", 15); 
    }

    void hideForbidden()
    {
        Forbidden.SetActive(false); //把禁止隱藏，可再次使用閃電
        GameObject.Find("Script").GetComponent<Com.MyProject.MyPassTheBuckGame.Audio>().MusicPlay(canUseBombMusic);
    }
}
