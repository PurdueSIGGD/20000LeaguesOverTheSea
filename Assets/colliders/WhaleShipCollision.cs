using UnityEngine;
using System.Collections;

public class WhaleShipCollision : BasicCollision {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void hit(GameObject collider){
		if(collider.tag == "Bullet"){
			this.GetComponent<WhaleShip>().gotHit();
		}
		
		if(collider.tag == "Player")
		{
			GameObject.Destroy(collider.gameObject);
		}
		
	}
}
