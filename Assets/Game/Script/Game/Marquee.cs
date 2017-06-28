using UnityEngine;
using UnityEngine.UI;


namespace Com.MyProject.MyPassTheBuckGame
{
   public class Marquee :Photon.PunBehaviour
   {

	 public float scrollSpeed = 50;
	 public Text MarqueeTx;


	 // Use this for initialization
	 void Start () 
	 {
			
		//MarqueeTx.transform.position = new Vector3(368.0f, 175.0f, 0.0f);
		Vector3 Pos = MarqueeTx.transform.position;
		Debug.Log(Pos);

	 }

	 void Update()
	 {
		float step = scrollSpeed * Time.deltaTime;
		Vector3 temp = new Vector3(step,0.0f,0.0f);
		Vector3 forReset = new Vector3 (378.0f, 525.7f, 0.0f);

		if (Vector3.Distance(MarqueeTx.transform.position, forReset) < 1.0f) 
		{
			MarqueeTx.transform.position = new Vector3 (907.8f, 525.7f, 0.0f);
		} 
		else 
		{
			MarqueeTx.transform.position -= temp;
		}
	 }



	 #region Public Methods


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
