using UnityEngine;
using System.Collections;

public class PlanetCollision : BasicCollision {

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
			//The little game engine that could.
			//He tried to game dev without globals.
			try {
				InGameMenu igm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<InGameMenu>();
				igm.gameOver = true;
				GameTimer gt = GetComponentInParent<GameTimer>();
				gt.saveHighscore();
			} catch(MissingComponentException ex) {
				Debug.LogError("welp we tried");
			}
		}
	}
}
