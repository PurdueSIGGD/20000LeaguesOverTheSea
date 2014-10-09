﻿using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour {

	public bool paused = false;

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
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
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
