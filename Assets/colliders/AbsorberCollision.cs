using UnityEngine;
using System.Collections;

public class AbsorberCollision : BasicCollision {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
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
