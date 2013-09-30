using UnityEngine;
using System.Collections;

public class PlantColllision : BasicCollision {
	
	int health = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
				GameObject.DestroyObject(this.gameObject);
		}
	}
	
	public override void hit(GameObject collider)
	{
		Collider coll = collider.collider;
		if (coll.tag != "Ship") {
				health--;
		}
		//lower health of planet if hit by planet lowering health sort of thing (not a bullet)
	}
}
