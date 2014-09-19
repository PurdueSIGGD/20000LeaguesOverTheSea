using UnityEngine;
using System.Collections;

public class PlanetCollision : BasicCollision {
	
	public int health = 100;

	protected override void OnCollisionEnter(Collision col)
	{
		Collider collider = col.collider;
		if (collider.tag == "Player" || collider.tag == "Enemy") {
				health -= 10;
		}
		//lower health of planet if hit by planet lowering health sort of thing (not a bullet)

		if (health <= 0) {
			GameObject.DestroyObject(this.gameObject);
			Application.LoadLevel("Menu");
		}
	}
}
