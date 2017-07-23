using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePaperPersonImg : MonoBehaviour {
    Image paperPerson; //人物圖片
    public Sprite[] paperPersonImage; //全部圖片

    void Awake()
    {
        paperPerson = GetComponent<Image>(); //讀取該人物圖片
    }
    public void ChangePersonImg1() //Player1
    {
        paperPerson.sprite = paperPersonImage[0];
    }
    public void ChangePersonImg2() //Player2
    {
        paperPerson.sprite = paperPersonImage[1];
    }
    public void ChangePersonImg3() //Player3
    {
        paperPerson.sprite = paperPersonImage[2];
    }

}
