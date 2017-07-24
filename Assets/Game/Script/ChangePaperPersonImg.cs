using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePaperPersonImg : MonoBehaviour {
    Image paperPerson; //人物圖片
    public Image[] paperPersonImage; //全部圖片

    void Awake()
    {
        paperPerson = GetComponent<Image>(); //讀取該人物圖片
    }

    public void getPaperImg()
    {
        Debug.Log("get img");
        for (int i = 0; i < 3; i++) //得到圖片
        {
            paperPersonImage[i] = GameObject.Find("洞口").GetComponent<NowState>().paperPersonImage[i];
        }
        Debug.Log("change1");
        //paperPerson.sprite = paperPersonImage[0];
        Debug.Log("change2");
    }

    public void ChangePersonImg1() //Player1
    {
        Debug.Log("changeImg1");
        paperPerson.sprite = paperPersonImage[0].sprite;
    }
    public void ChangePersonImg2() //Player2
    {
        Debug.Log("changeImg2");
        paperPerson.sprite = paperPersonImage[1].sprite;
    }
    public void ChangePersonImg3() //Player3
    {
        Debug.Log("changeImg3");
        paperPerson.sprite = paperPersonImage[2].sprite;
    }

    public void ChangePersonImg1(Image img) //Player1
    {
        Debug.Log("changeImg1");
        paperPerson.sprite = img.sprite;
    }
    public void ChangePersonImg2(Image img) //Player2
    {
        Debug.Log("changeImg2");
        paperPerson.sprite = img.sprite;
    }
    public void ChangePersonImg3(Image img) //Player3
    {
        Debug.Log("changeImg3");
        paperPerson.sprite = img.sprite;
    }


}
