using UnityEngine;
using System.Collections;

public class NpcHealthBar : MonoBehaviour {
	private float barAmount = 0;
	private float barTotal = 100;
	private Vector2 barSize = new Vector2(200, 20);
	
	public Transform npcTransform;
	
	private float timeTest;
	
	void Awake()
	{
		//myTransform = transform;
	}
	
	void OnGUI()
	{
		// npc healthbar
		GUI.BeginGroup(new Rect(npcTransform.position.x, npcTransform.position.y, barSize.x, barSize.y));
			GUI.Box(new Rect(0 ,0 ,barSize.x , barSize.y), "HP/HP");
			// draw the filled-in part
			GUI.BeginGroup(new Rect(0 ,0 , barSize.x * timeTest, barSize.y));
				GUI.Box(new Rect(0 ,0 , barSize.x , barSize.y), "100/100");
			GUI.EndGroup();
		GUI.EndGroup();
	}
	
	void Update()
	{
		timeTest = Time.deltaTime * 0.05f;
	}
}
