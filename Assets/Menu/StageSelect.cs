using UnityEngine;
using System.Collections;

public class StageSelect : MonoBehaviour 
{
    /**
     * Class where we handle StageSelect GUI events.
     * 
     * is Part of the object 'Gui Constructor' on the Menu Scene
     */

    GUIText StageI, StageII, StageIII, StageIV, StageV, StageVI;
	
	void Start() 
    {
        StageI = GameObject.Find("Stage I").guiText;		
    }
	
	void OnGUI () 
    {		
		if(Input.GetMouseButtonDown(0))
		{
			if( StageI.GetScreenRect().Contains(Input.mousePosition)) 
			{
				Application.LoadLevel("first");
			}
		}
	}
}
