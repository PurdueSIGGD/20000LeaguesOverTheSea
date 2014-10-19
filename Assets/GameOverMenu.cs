using UnityEngine;
using System.Collections;

public class GameOverMenu : MonoBehaviour {

	public GUISkin skin;
	private bool gameOver;
	

	/*void OnGUI () {
		if (gameOver) {
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(0, new Vector3(0, 0, 0)), 
			                           new Vector3(Screen.width/Menu.scaledR.x, Screen.height/Menu.scaledR.y, 1));
			gameMenu();
		}
	}*/
	
	void gameOverMenu() 
	{
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(0, new Vector3(0, 0, 0)), 
		                           new Vector3(Screen.width/Menu.scaledR.x, Screen.height/Menu.scaledR.y, 1));

		GUI.BeginGroup(Menu.scale_rect(new Rect(0,0,100,100), Menu.scaledR));
		GUI.Box(Menu.scale_rect(new Rect(35,20,30,55), Menu.scaledR), "Game Over", skin.box);
		
		if( GUI.Button(Menu.scale_rect(new Rect(35, 30, 30, 15), Menu.scaledR), "Retry", skin.customStyles[1]))
		{
			Application.LoadLevel("stage1");
		}
		if( GUI.Button(Menu.scale_rect(new Rect(35, 45, 30, 15), Menu.scaledR), "Exit", skin.customStyles[1]))
		{
			Application.LoadLevel("Menu");
		}
		GUI.EndGroup();
	}
}
