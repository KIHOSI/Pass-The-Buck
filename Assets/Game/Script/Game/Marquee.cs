using UnityEngine;
using UnityEngine.UI;


namespace Com.MyProject.MyPassTheBuckGame
{
   public class Marquee :Photon.PunBehaviour
   {
	 public string message = "歡迎來到政客踢皮球";
	 public float scrollSpeed = 50;
	 public GameObject MarqueeTx;
	 Text MText;

	 Rect messageRect;

	 void OnGUI ()
	 {
		MText = MarqueeTx.GetComponent<Text> ();
		// Set up message's rect if we haven't already.
		if (messageRect.width == 0) {

			// Start message past the left side of the screen.
				messageRect.x = -19;
				messageRect.width = 19;
				messageRect.height = 0;
		}

		messageRect.x += Time.deltaTime * scrollSpeed;

		// If message has moved past the right side, move it back to the left.
		if (messageRect.x > Screen.width) {
			messageRect.x = -messageRect.width;
		}

		GUI.Label(messageRect, message);
	 }
   }
}
