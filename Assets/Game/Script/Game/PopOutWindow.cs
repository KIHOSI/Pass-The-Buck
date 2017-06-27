using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MyProject.MyPassTheBuckGame
{
	public class PopOutWindow : MonoBehaviour
	{
		private Rect windowPosition;//儲存不可被拖曳的window的位置
		private Rect windowPositionMove;//儲存可被拖曳的window的位置
		private Rect buttonPosition;//儲存button在window內的位置

		void Start()
		{
			setWindowPosition();
			setButtonPosition();
		}

		private void setWindowPosition()//設定window的位置
		{
			float windowWidth = 150f;
			float windowHeight = 150f;
			float windowLeft = Screen.width * 0.5f - windowWidth * 0.5f;//window和Game左邊的距離，目前設定的值會讓window顯示在螢幕正中央
			float windowTop = Screen.height * 0.5f - windowHeight * 0.5f;//window和Game上面的距離，目前設定的值會讓window顯示在螢幕正中央
			windowPositionMove = new Rect(windowLeft, windowTop, windowWidth, windowHeight);//將可被拖曳的視窗設定在Game中央

			windowPosition = new Rect(0, 0, windowWidth, windowHeight);//將不可被拖曳的window設定在Game左上角
		}

		private void setButtonPosition()//設定windows內的button位置
		{
			float buttonWidth = 50f;//按鈕的寬度
			float buttonHeight = 50f;//按鈕的高度
			float buttonLeft = windowPosition.width * 0.5f - buttonWidth * 0.5f;//按鈕和window左邊的距離，目前的值會讓button顯示在window的正中央
			float buttonTop = windowPosition.height * 0.5f - buttonHeight * 0.5f;//按鈕和window上面的距離，目前的值會讓button顯示在window的正中央

			buttonPosition = new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight);//button將會顯示在window正中央
		}

		private void OnGUI()
		{
			//顯示window，但是無法拖曳
			GUI.Window(0, windowPosition, windowEvent, "不可拖曳");

			//顯示window，可以被拖曳
			windowPositionMove = GUI.Window(1, windowPositionMove, windowEvent, "可拖曳");
		}

		private void windowEvent(int id)//處理視窗裡面要顯示的文字、按鈕、事件處理。必須要有一個為int的傳入參數
		{
			if (GUI.Button(buttonPosition, "按鈕"))//在window上顯示按鈕
			{
				if (id == 0)//若是id為0，代表是不可被拖曳的window
				{
					Debug.Log("不可拖曳的window按鈕被按下");
				}
				else//若是id為1，代表是可被拖曳的window
				{
					Debug.Log("可被拖曳的window按鈕被按下");
				}
			}

			if (id == 1)//若是id為1，代表是可被拖曳的window
			{
				GUI.DragWindow();
			}
		}
	}

}
