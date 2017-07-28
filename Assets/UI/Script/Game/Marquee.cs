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
	 int count=0;


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
		Vector3 forReset = new Vector3 (-517, 0.0F, 0.0f);

		if (Vector3.Distance(marqueeTx.transform.localPosition, forReset) < 1.0f) 
		{

			marqueeTx.transform.localPosition = new Vector3 (x, y, 0.0f);

				if (count == 1) 
				{
					setText ("第一次遊玩可先觀看遊戲說明唷!");
				} 
				else if (count == 2)
				{
					setText ("遊玩前最好先確認連線是否穩定，才不會影響遊玩品質喔!");
				}
				else if (count == 3)
				{
					setText ("記得球球要按壓後拖動一段距離放開球才會自己跑喔");
				}
				else if (count == 4)
				{
					setText ("祝大家玩得開心!");
					count = 0;
				}
			count += 1;
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
	 
   }
}
