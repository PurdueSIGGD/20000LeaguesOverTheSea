using UnityEngine;
using System.Collections;

public class ChargerCollision : BasicCollision {
	public override void hit(GameObject collider){
			this.GetComponent<BigBulletShoot>().gotHit();	
	}
}
