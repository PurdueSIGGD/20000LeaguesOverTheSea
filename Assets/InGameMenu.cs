using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour {

	//  Right now we just use the escape key to go back to the menu
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel("Menu");	
		}
	}
}
