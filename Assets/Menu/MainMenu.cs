using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	/**
	 * Class where we create and handle click events for the Main Menu.
	 */
	GUISkin guiSkin;
	
	void Start() {
		guiSkin = (GUISkin) Resources.Load("Menu/guiSkinMenu");	
    }
	
	void OnGUI () {
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(0, new Vector3(0, 1, 0)), new Vector3(Screen.width/1920f, Screen.height/1080f, 1));
		
		GUI.skin = guiSkin;
		
		GUI.skin.label.fontSize = 500;
		GUI.Label (new Rect(750, 150, 1000, 500), "20,000");
		
		GUI.skin.label.fontSize = 175;
		GUI.Label (new Rect(750, 600, 1000, 500), "Leagues Over the Sea");
		
		GUI.skin.label.fontSize = 150;
		if( GUI.Button(new Rect(150, 600, 500, 500), "Play", GUI.skin.label) ) 
		{
			Application.LoadLevel("stageselect");
		}
		if( GUI.Button(new Rect(150, 750, 500, 500), "Exit", GUI.skin.label) ) 
		{
			Application.Quit();
		}
		
	}
	
}
