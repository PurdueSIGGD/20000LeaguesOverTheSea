using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	/**
	 * Class where we create and handle click events for the Main Menu.
	 */
	
	//Scaled Resolution: what the gui is scaled to, we aim for 1080p
	private static Vector2 scaledR = new Vector2(1920,1080); 
	
	
	
	GUISkin guiSkin;
	
	void Start() {
		guiSkin = (GUISkin) Resources.Load("Menu/guiSkinMenu");	
    }
	
	void OnGUI () {
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(0, new Vector3(0, 1, 0)), 
				new Vector3(Screen.width/scaledR.x, Screen.height/scaledR.y, 1));
		
		GUI.skin = guiSkin;
		
		menu_Main();
		
	}
	
	private void menu_Main()
	{
		GUI.skin.label.fontSize = 500;
		GUI.Label (scale_rect(new Rect(40, 12, 20, 20)), "20,000");
		
		GUI.skin.label.fontSize = 175;
		GUI.Label (scale_rect(new Rect(40, 55, 20, 20)), "Leagues Over the Sea");
		
		GUI.skin.label.fontSize = 150;
		if( GUI.Button(scale_rect(new Rect(10, 55, 10, 12)), "Play", GUI.skin.label) ) 
		{
			Application.LoadLevel("stageselect");
		}
		if( GUI.Button(scale_rect(new Rect(10, 70, 10, 12)), "Exit", GUI.skin.label) ) 
		{
			Application.Quit();
		}
	}
	
	//Scales the percentage(0-100) rectangle to the scaled Resolution.
	private Rect scale_rect(Rect r, Vector2 scale)
	{
		r.x *= scale.x / 100;
		r.y *= scale.y / 100;
		r.width *= scale.x / 100;
		r.height *= scale.y / 100;	
		
		return r;
	}
	//Mehhhhhhh
	private Rect scale_rect(Rect r) {
		return scale_rect(r, scaledR);	
	}
}
