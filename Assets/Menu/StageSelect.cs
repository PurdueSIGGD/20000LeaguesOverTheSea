using UnityEngine;
using System.Collections;

public class StageSelect : MonoBehaviour 
{
     /**
     * Class where we handle StageSelect GUI events.
     * 
     * is Part of the object 'Gui Constructor' on the Menu Scene
     */

    GUIText StageI, StageII, StageIII, StageIV, StageV, StageVI, BossStage1;
	
	void Start() 
    {
        StageI = GameObject.Find("Stage I").guiText;
        StageII = GameObject.Find("Stage II").guiText;
        StageIII = GameObject.Find("Stage III").guiText;
        StageIV = GameObject.Find("Stage IV").guiText;
        StageV = GameObject.Find("Stage V").guiText;
        StageVI = GameObject.Find("Stage VI").guiText;
        BossStage1 = GameObject.Find("Boss Stage 1").guiText;
    }
	
	void OnGUI () 
    {		
		if(Input.GetMouseButtonDown(0))
		{
			if( StageI.GetScreenRect().Contains(Input.mousePosition)) 
			{
				Application.LoadLevel("stage1");
			}
            else if (StageII.GetScreenRect().Contains(Input.mousePosition))
            {
                Application.LoadLevel("stage2");
            }
            else if (StageIII.GetScreenRect().Contains(Input.mousePosition))
            {
                Application.LoadLevel("stage3");
            }
            else if (StageIV.GetScreenRect().Contains(Input.mousePosition))
            {
                Application.LoadLevel("stage4");
            }
            else if (StageV.GetScreenRect().Contains(Input.mousePosition))
            {
                Application.LoadLevel("stage5");
            }
            else if (StageVI.GetScreenRect().Contains(Input.mousePosition))
            {
                Application.LoadLevel("stage6");
            }
            else if (BossStage1.GetScreenRect().Contains(Input.mousePosition))
            {
                Application.LoadLevel("bossstage1");
            }
		}
	}
}

