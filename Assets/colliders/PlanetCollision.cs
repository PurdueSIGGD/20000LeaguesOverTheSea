using UnityEngine;
using System.Collections;

public class PlanetCollision : BasicCollision {

	private bool gameOver = false;
	public int health = 100;
	public GUISkin skin;

	protected override void OnCollisionEnter(Collision col)
	{
		Collider collider = col.collider;
		if (collider.tag == "Player" || collider.tag == "Enemy") {
				health -= 10;
		}
		//lower health of planet if hit by planet lowering health sort of thing (not a bullet)

		if (health <= 0) {
			//GameObject.DestroyObject(this.gameObject);
			gameOver = true;
		}
	}

	// Game Over Menu
	// I know this is a terrible place to put this but it works and we don't have much time to finish this game
	void OnGUI () 
	{
		if (gameOver) 
		{
			new InGameMenu().paused = true;

			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(0, new Vector3(0, 0, 0)), 
			                           new Vector3(Screen.width/Menu.scaledR.x, Screen.height/Menu.scaledR.y, 1));
			gameOverMenu();
		}
	}

	void gameOverMenu() 
	{
		
		GUI.BeginGroup(Menu.scale_rect(new Rect(0,0,100,100), Menu.scaledR));
		GUI.Box(Menu.scale_rect(new Rect(35,20,30,55), Menu.scaledR), "Pause Menu", skin.box);
		
		if( GUI.Button(Menu.scale_rect(new Rect(35, 30, 30, 15), Menu.scaledR), "Retry", skin.customStyles[1]))
		{
			Application.LoadLevel("stage1");
		}
		if( GUI.Button(Menu.scale_rect(new Rect(35, 45, 30, 15), Menu.scaledR), "Exit", skin.customStyles[1]))
		{
			Application.LoadLevel("Menu");
		}
		GUI.EndGroup();
	}
}
