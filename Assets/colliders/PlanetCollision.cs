using UnityEngine;
using System.Collections;

public class PlanetCollision : BasicCollision {
	
	public int health = 100;

	public override void hit(GameObject collider)
	{
		Collider coll = collider.collider;
		if (coll.tag != "Bullet") {
				health -= 10;
		}
		//lower health of planet if hit by planet lowering health sort of thing (not a bullet)

		if (health <= 0) {
			GameObject.DestroyObject(this.gameObject);
			Application.LoadLevel("Menu");
		}
	}
}
