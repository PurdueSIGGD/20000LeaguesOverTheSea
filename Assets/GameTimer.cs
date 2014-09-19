using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

	int ticks = 0;
	int gameTimer = 0;


	// Update is called once per frame
	void FixedUpdate () 
	{

		gameTimer++;
	}

	public GUIStyle style;
	public Vector2 timerPosition = new Vector2(35,10);

	void OnGUI () 
	{
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(0, new Vector3(0, 0, 0)), 
		                           new Vector3(Screen.width/Menu.scaledR.x, Screen.height/Menu.scaledR.y, 1));
		GUI.BeginGroup(Menu.scale_rect(new Rect(0,0,100,100), Menu.scaledR));
		// Draw the respawn countdown in the center of the screen. Magic numbers make everything look better 
		GUI.Box(Menu.scale_rect(new Rect(timerPosition.x, timerPosition.y,10,10), Menu.scaledR), (1 + gameTimer/60).ToString(), style);
		GUI.EndGroup();

	}
}
