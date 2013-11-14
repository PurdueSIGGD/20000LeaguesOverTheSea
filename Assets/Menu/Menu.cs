using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	/**
	 * Class where we handle Menu GUI events.
	 * 
	 * is Part of the object 'Gui Constructor' on the Menu Scene
	 */
	
	//GUIText Title; 
	GUIText Play;
	GUIText Exit;
	
	void Start() {
    	//Title = GameObject.Find ("menuTitle").guiText;
		//Play = GameObject.Find ("menuPlay").guiText;
		//Exit = GameObject.Find ("menuExit").guiText;
    }
	
	void OnGUI () {

		//Best way to detect clicks on GuiText
		
		//Input.GetMouseButtonDown(0) 
		//	Returns the state of the left mousebutton
		//Play.GetScreenRect().Contains()
		//	Checks if mouseclick is in area of GuiText
		/*
		if(Input.GetMouseButtonDown(0))
		{
			/*if( Play.GetScreenRect().Contains(Input.mousePosition)) 
			{
				Application.LoadLevel("stageselect");
			}

			if( Exit.GetScreenRect().Contains(Input.mousePosition))
			{
				Application.Quit();	
			}
		}*/
	}
	
}
