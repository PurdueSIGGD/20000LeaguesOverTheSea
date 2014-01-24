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
		totalDist+=(this.rigidbody.position-prevPos).magnitude;
		
		if (totalDist>maxDist && !returning)
		{
			returning=true;
			//this.rigidbody.velocity=Vector3.zero;
		}
		if (returning)
		{
			float distance=(parent.rigidbody.position-this.rigidbody.position).magnitude;
			Vector3 direction=(parent.rigidbody.position-this.rigidbody.position).normalized;
			Vector3 currentdirection=this.rigidbody.velocity.normalized;
			float dot=Vector3.Dot (direction,currentdirection);
			if (dot<.7)
				this.rigidbody.velocity-=(Mathf.Abs(dot))*this.rigidbody.velocity;
			this.rigidbody.AddForce(direction*10,ForceMode.VelocityChange);
		}
		if (hooked!=null)
		hooked.rigidbody.position=this.rigidbody.position;
	}
	void OnCollisionEnter(Collision coll)
	{
		if(coll.collider.gameObject.tag=="Player")
			GameObject.Destroy(this.gameObject);
		else if (hooked!=null)
		hooked=coll.collider.gameObject;
		if (!returning)
		{
		//this.rigidbody.velocity=Vector3.zero;
		returning=true;
		}
	}

}
