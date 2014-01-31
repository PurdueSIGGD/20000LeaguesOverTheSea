using UnityEngine;
using System.Collections;

public class ChargerCollision : BasicCollision {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void hit(GameObject collider){
			this.GetComponent<BigBulletShoot>().gotHit();	
	}
}
