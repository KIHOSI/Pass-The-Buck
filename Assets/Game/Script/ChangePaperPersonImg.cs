using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePaperPersonImg : MonoBehaviour {
    Image paperPerson;
    public Sprite[] paperPersonImage;

    void Awake()
    {
        paperPerson = GetComponent<Image>(); //讀取該人物圖片
        //paperPerson.sprite = paperPersonImage[1];
        //Debug.Log("0");
    }
    public void ChangePersonImg1() //Player1
    {
        //Debug.Log("1");
        paperPerson.sprite = paperPersonImage[0];
    }
    public void ChangePersonImg2() //Player2
    {
        //Debug.Log("2");
        paperPerson.sprite = paperPersonImage[1];
    }
    public void ChangePersonImg3() //Player3
    {
        //Debug.Log("3");
        paperPerson.sprite = paperPersonImage[2];
    }
}
