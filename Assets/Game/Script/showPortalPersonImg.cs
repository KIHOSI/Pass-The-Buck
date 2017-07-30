using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showPortalPersonImg : MonoBehaviour {
    //public showImg
    public int portalPosition; //判斷是左上右portal，0:左、1:上、2:右
    public Image portalPersonImg; //要放的人物
    public Sprite[] leftPortalImg; //左邊要放的人物
    public Sprite[] upPortalImg; //上面要放的人物
    public Sprite[] rightPortalImg; //右邊要放的人物

    public void setPortalPersonImg(string playerChooseRole)
    {
        if(portalPosition == 0) //左
        {
            if (playerChooseRole == "吳指癢")
            {
                portalPersonImg.sprite = leftPortalImg[0];
            }
            else if (playerChooseRole == "洪咻柱")
            {
                portalPersonImg.sprite = leftPortalImg[1];
            }
            else if (playerChooseRole == "蔡中聞")
            {
                portalPersonImg.sprite = leftPortalImg[2];
            }
            else if (playerChooseRole == "蘇嘎拳")
            {
                portalPersonImg.sprite = leftPortalImg[3];
            }
        }
        else if(portalPosition == 1) //上
        {
            if (playerChooseRole == "吳指癢")
            {
                portalPersonImg.sprite = upPortalImg[0];
            }
            else if (playerChooseRole == "洪咻柱")
            {
                portalPersonImg.sprite = upPortalImg[1];
            }
            else if (playerChooseRole == "蔡中聞")
            {
                portalPersonImg.sprite = upPortalImg[2];
            }
            else if (playerChooseRole == "蘇嘎拳")
            {
                portalPersonImg.sprite = upPortalImg[3];
            }
        }
        else if(portalPosition == 2) //右
        {
            if (playerChooseRole == "吳指癢")
            {
                portalPersonImg.sprite = rightPortalImg[0];
            }
            else if (playerChooseRole == "洪咻柱")
            {
                portalPersonImg.sprite = rightPortalImg[1];
            }
            else if (playerChooseRole == "蔡中聞")
            {
                portalPersonImg.sprite = rightPortalImg[2];
            }
            else if (playerChooseRole == "蘇嘎拳")
            {
                portalPersonImg.sprite = rightPortalImg[3];
            }
        }

        portalPersonImg.enabled = false; //預設圖片先不顯示
    }

    private void OnTriggerEnter2D(Collider2D collision) //如果求滑動到這個區域，顯示該區域的player
    {
        showPersonImg(0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        showPersonImg(1);
    }

    void showPersonImg(int enterOrExit) //顯示圖片
    {
        if(enterOrExit == 0) { 
            portalPersonImg.enabled = true; //顯示
        }
        else if(enterOrExit == 1)
        {
            portalPersonImg.enabled = false; //隱藏
        }
    }
}
