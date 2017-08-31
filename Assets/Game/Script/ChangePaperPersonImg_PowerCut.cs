using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePaperPersonImg_PowerCut : MonoBehaviour {
    Image paperPerson; //人物圖片
    //public Image[] paperPersonImage; //該Player的報紙按鈕圖片
    public Sprite[] paperPersonAllImage; //全部圖片

    void Awake()
    {
        paperPerson = GetComponent<Image>(); //讀取該人物圖片
    }

    /*public void getPaperImg()
    {
        for (int i = 0; i < 3; i++) //得到圖片
        {
            paperPersonImage[i] = GameObject.Find("洞口").GetComponent<NowState>().paperPersonImage[i];
        }
    }*/

    /*public void ChangePersonImg1() //Player1
    {
        paperPerson.sprite = paperPersonImage[0].sprite;
    }
    public void ChangePersonImg2() //Player2
    {
        paperPerson.sprite = paperPersonImage[1].sprite;
    }
    public void ChangePersonImg3() //Player3
    {
        paperPerson.sprite = paperPersonImage[2].sprite;
    }*/

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
