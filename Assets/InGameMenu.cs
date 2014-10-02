using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour {

	private bool _paused;
	public bool paused {
		get {
			return _paused;
		}
		set{
			_paused = value;
			Time.timeScale = (value) ? 0 : 1;
		}
	}

	public GUISkin skin;

	//  Right now we just use the escape key to go back to the menu
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			paused = (paused) ? false : true; //toggle pause
		}
	}

	void OnGUI () {
		if (paused) {
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(0, new Vector3(0, 0, 0)), 
		                           new Vector3(Screen.width/Menu.scaledR.x, Screen.height/Menu.scaledR.y, 1));
			gameMenu();
		}
	}

	void gameMenu() {
		GUI.BeginGroup(Menu.scale_rect(new Rect(0,0,100,100), Menu.scaledR));
		GUI.Box(Menu.scale_rect(new Rect(35,20,30,55), Menu.scaledR), "Pause Menu", skin.box);
		
		if( GUI.Button(Menu.scale_rect(new Rect(35, 30, 30, 15), Menu.scaledR), "Resume", skin.customStyles[1]))
		{
			paused = false;
			return;
		}
		if( GUI.Button(Menu.scale_rect(new Rect(35, 45, 30, 15), Menu.scaledR), "Exit", skin.customStyles[1]))
		{
			paused = false;
			Application.LoadLevel("Menu");
			return;
		}
		GUI.EndGroup();
	}
}
