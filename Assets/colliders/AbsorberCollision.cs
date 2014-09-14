using UnityEngine;
using System.Collections;

public class AbsorberCollision : BasicCollision {
	protected override void OnCollisionEnter(Collision col)
	{
		Collider collider = col.collider;
		if(collider.tag == "Bullet")
		{
			this.GetComponent<Absorber>().gotHit();
		}
		
		if(collider.tag == "Player")
		{
			GameObject.Destroy(collider.gameObject);
		}
		
	}
}
