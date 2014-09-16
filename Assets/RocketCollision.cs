using UnityEngine;
using System.Collections;
using System;

// This exists so a projectile can be hit multiple times before dying

public class RocketCollision : BasicCollision 
{
	int health = 3;
	
	protected override void OnCollisionEnter(Collision coll)
    {
		health--;

		Collider collider = coll.collider;

		if(collider.tag == "Planet")
			health = 0;

		if (health <= 0)
		{
			GameObject.DestroyObject(this.gameObject);
		}
    }
}
