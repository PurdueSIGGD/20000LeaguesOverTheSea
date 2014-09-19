using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	/**
	 * Class where we create and handle click events for the Main Menu.
	 */
	
	//Scaled Resolution: what the gui is scaled to, we aim for 1080p
	public static Vector2 scaledR = new Vector2(1920, 1080); 
	public TextAsset creditsFile;
	public AudioClip click;

	GUISkin guiSkin;
	
	void Start() {
		guiSkin = (GUISkin) Resources.Load("Menu/guiSkinMenu");	
		state = 0;
		//click = (AudioClip) Resources.Load ("Sounds/menu_noise");
    }
	
	void OnGUI () {
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(0, new Vector3(0, 0, 0)), 
				new Vector3(Screen.width/scaledR.x, Screen.height/scaledR.y, 1));
		
		GUI.skin = guiSkin;
		
		display_state();//Display the current gui state.
		
	}
	
	
	private int state; //View state. In Main Menu, Stage Select, Settings..
	//Switch the current gui state.
	//// Should this be abstracted to an int?
	private void display_state()
	{
		switch (state) {
			case 0: state_Main(); break;
			case 1: state_StageSelect(); break;
			case 2: state_Credits(); break;
			default: Debug.LogError("Tried to switch to invaild GUI state."); break;
		}
	}



	private void state_Main()
	{
		GUI.skin.label.fontSize = 500;
		GUI.Label (scale_rect(new Rect(40, 12, 20, 20)), "20,000");
		
		GUI.skin.label.fontSize = 175;
		GUI.Label (scale_rect(new Rect(40, 55, 20, 20)), "Leagues Over the Sea");
		
		
		GUI.skin.label.fontSize = 150;
		if( GUI.Button(scale_rect(new Rect(8, 38, 22, 15)), "Play", GUI.skin.customStyles[0]))
		{
			audio.PlayOneShot(click, 0.7F);
			Application.LoadLevel("stage1");
			//state = 1;
		}
		if( GUI.Button(scale_rect(new Rect(8, 55, 22, 15)), "Credits", GUI.skin.customStyles[0]))
		{
			credits_height = 0;
			audio.PlayOneShot(click, 0.7F);
			state = 2;
		}
		if( GUI.Button(scale_rect(new Rect(8, 72, 22, 15)), "Exit", GUI.skin.customStyles[0])) 
		{
			audio.PlayOneShot(click, 0.7F);
			Application.Quit();
		}
	}
	
	private void state_StageSelect() 
	{
		GUI.skin.label.fontSize = 75;
		if( GUI.Button(scale_rect(new Rect(3, 2, 20, 12)), "Back", GUI.skin.customStyles[0]) ) 
		{
			state = 0;
		}
		
		GUI.skin.label.fontSize = 150;
		if( GUI.Button(scale_rect(new Rect(10, 20, 25, 12)), "Stage I", GUI.skin.customStyles[0])) 
		{
			audio.PlayOneShot(click, 0.7F);
			Application.LoadLevel("stage1");
		}
		
		if( GUI.Button(scale_rect(new Rect(40, 20, 25, 12)), "Stage II", GUI.skin.customStyles[0])) 
		{
			audio.PlayOneShot(click, 0.7F);
			Application.LoadLevel("stage2");
		}
		
		if( GUI.Button(scale_rect(new Rect(70, 20, 25, 12)), "Stage III", GUI.skin.customStyles[0])) 
		{
			audio.PlayOneShot(click, 0.7F);
			Application.LoadLevel("stage3");
		}
		
		if( GUI.Button(scale_rect(new Rect(10, 60, 25, 12)), "Stage IV", GUI.skin.customStyles[0])) 
		{
			audio.PlayOneShot(click, 0.7F);
			Application.LoadLevel("stage4");
		}
		
		if( GUI.Button(scale_rect(new Rect(40, 60, 25, 12)), "Stage V", GUI.skin.customStyles[0])) 
		{
			audio.PlayOneShot(click, 0.7F);
			Application.LoadLevel("stage5");
		}
		
		if( GUI.Button(scale_rect(new Rect(70, 60, 25, 12)), "Stage VI", GUI.skin.customStyles[0]))
		{
			audio.PlayOneShot(click, 0.7F);
			Application.LoadLevel("stage6");
		}
	}

	private float credits_height = 0;
	private void state_Credits() 
	{
		string text = creditsFile.text;
		int height = 0;
		foreach (string s in text.Split('\n'))
		{
			if (s.Contains("!!")) {
				GUI.Label (scale_rect(new Rect(0, credits_height + height, 100, 20)), s.Remove(0,2), GUI.skin.customStyles[2]);
				height += 20;
			} else if (s.Contains("!")) {
				GUI.Label (scale_rect(new Rect(0, credits_height + height, 100, 10)), s.Remove(0,1), GUI.skin.customStyles[3]);
				height += 9;
			} else {
				GUI.Label (scale_rect(new Rect(0, credits_height + height, 100, 10)), s, GUI.skin.customStyles[4]);
				height += 8;
			}
		}
		if (credits_height < -360) //Sadly a hard coded value.
		{
			state = 0;
		}
		credits_height -= 0.1f; //Rate of Ascent

	}
	
	
	//Scales the percentage(0-100) rectangle to the scaled Resolution.
	public static Rect scale_rect(Rect r, Vector2 scale)
	{
		r.x *= scale.x / 100;
		r.y *= scale.y / 100;
		r.width *= scale.x / 100;
		r.height *= scale.y / 100;	
		
		return r;
	}
	//Mehhhhhhh
	public Rect scale_rect(Rect r) {
		return scale_rect(r, scaledR);	
	}
}
