using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	/**
	 * Class where we handle all Menu GUI.
	 * 
	 * is Part of the object 'Gui Constructor' on the Menu Scene
	 */
	
	GUIText Title; 
	GUIText Play;
	
	void Start() {
    	Title = GameObject.Find ("menuTitle").guiText;
		Play = GameObject.Find ("menuPlay").guiText;
    }
	
	void OnGUI () {
		//Best way to detect clicks on GuiText
		if(Input.GetMouseButtonDown(0) && Play.GetScreenRect().Contains(Input.mousePosition))
		{
			Application.LoadLevel("first");
		}
		
		if(Input.GetMouseButtonDown(0) && Play.GetScreenRect().Contains(Input.mousePosition))
		{
			Application.Quit();	
		}
		
	}
	
}
