using UnityEngine;
using System.Collections;

public class HarpoonProjectile : Projectile {
	GameObject hooked;
	bool returning;
	Vector3 prevPos;
	float totalDist;
	public float maxDist=100;
	public GameObject parent;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision coll)
	{
		if(coll.gameObject.name.Equals("WhaleShip"))
		{
			parent.GetComponent<HarpoonGun>().whaleShip=coll.gameObject;
			parent.GetComponent<HarpoonGun>().hooked=true;
			parent.GetComponent<HarpoonGun>().hookedPoint=this.rigidbody.position;
		}
			GameObject.Destroy(this.gameObject);
	}

}
