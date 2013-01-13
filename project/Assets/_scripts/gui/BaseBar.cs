using UnityEngine;
using System.Collections;

public class BaseBar : MonoBehaviour {
	private float barDisplay = 0;
	private Vector2 pos = new Vector2(0,0);
	private Vector2 size = new Vector2(200,20);
	
	private Texture2D progressBarEmpty;
	private Texture2D progressBarFull;
	
	void OnGUI()
	{
		// draw the background
		GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
			GUI.Box(new Rect(0 ,0 ,size.x , size.y), progressBarEmpty);
			// draw the filled-in part
			GUI.BeginGroup(new Rect(0 ,0 ,size.x * barDisplay, size.y));
				GUI.Box(new Rect(0 ,0 ,size.x , size.y), progressBarFull);
			GUI.EndGroup();
		GUI.EndGroup();
			
	}
	
	// Update is called once per frame
	void Update () {
		barDisplay = Time.deltaTime * 0.05f;
	}
}
