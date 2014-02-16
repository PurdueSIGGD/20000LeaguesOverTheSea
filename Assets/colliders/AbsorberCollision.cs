using UnityEngine;
using System.Collections;

public class AbsorberCollision : BasicCollision {
	public override void hit(GameObject collider){
		if(collider.tag == "Bullet"){
			this.GetComponent<Absorber>().gotHit();
		}
		
		if(collider.tag == "Player")
		{
			GameObject.Destroy(collider.gameObject);
		}
		
	}
}
