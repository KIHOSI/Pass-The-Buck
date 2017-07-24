using UnityEngine;
using UnityEngine.UI;


namespace Com.MyProject.MyPassTheBuckGame
{
   public class Marquee :Photon.PunBehaviour
   {

	 public float scrollSpeed = 100;
	 float x;
	 float y;
	 public Text marqueeTx;


	 // Use this for initialization
	 void Start () 
	 {
		//MarqueeTx.transform.position = new Vector3(368.0f, 175.0f, 0.0f);
		marqueeTx=GameObject.Find ("MarqueeTx").GetComponent<Text> ();
		Vector3 Pos = marqueeTx.transform.localPosition;
		x = marqueeTx.transform.localPosition.x;
		y = marqueeTx.transform.localPosition.y;

	 }

	 void Update()
	 {
		float step = scrollSpeed * Time.deltaTime;
		Vector3 temp = new Vector3(step,0.0f,0.0f);
		Vector3 forReset = new Vector3 (-327, 0.0F, 0.0f);

		if (Vector3.Distance(marqueeTx.transform.localPosition, forReset) < 1.0f) 
		{
			marqueeTx.transform.localPosition = new Vector3 (x, y, 0.0f);
		} 
		else 
		{
			marqueeTx.transform.localPosition -= temp;
		}
	 }



	 #region Public Methods

	 public void setText(string t)
	 {
		this.marqueeTx.text = t;
	 
	 }


	 #endregion

	
	 
	 /*void OnGUI ()
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

	 } */
	 
   }
}
