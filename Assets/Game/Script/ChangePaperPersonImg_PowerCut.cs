using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePaperPersonImg_PowerCut : MonoBehaviour {
    Image paperPerson; //人物圖片
    public Sprite[] paperPersonAllImage; //全部圖片

    void Awake()
    {
        paperPerson = GetComponent<Image>(); //讀取該人物圖片
    }

    //直接改變人物圖片
    public void ChangeImg1() //蔡中聞
    {
        paperPerson.sprite = paperPersonAllImage[0];
    }
    public void ChangeImg2() //承包商
    {
        paperPerson.sprite = paperPersonAllImage[1];
    }
    public void ChangeImg3() //陳銀德
    {
        paperPerson.sprite = paperPersonAllImage[2];
    }
    public void ChangeImg4() //朱聞誠
    {
        paperPerson.sprite = paperPersonAllImage[3];
    }
}
