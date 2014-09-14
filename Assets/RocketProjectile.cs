using UnityEngine;
using System.Collections;

public class RocketProjectile : MonoBehaviour {
	int lifetime;
	// Use this for initialization
	void Start () {
		lifetime=0;
	}
	
	public Vector3 getMouseDirection()
	{
		Ray MousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distanceToXYPlane=-MousePosition.origin.z/MousePosition.direction.z;
		Vector3 screenPosition= MousePosition.GetPoint(distanceToXYPlane);
		Vector3 direction= screenPosition-this.transform.position;
		direction.Normalize();
		return direction;
	}
	
	// Update is called once per frame
	void Update () {
		//If the bullet is greater than the the maxRadius: reverse the velocity.
		Vector3 direction= getMouseDirection();
		float coef = 0;
		if(lifetime <420)
			coef = 30-lifetime/60;
		this.rigidbody.AddForce(direction*coef,ForceMode.Acceleration);
		lifetime++;

		//Could try some Quat Slerp for more smoothness but this works
		transform.LookAt(rigidbody.position - rigidbody.velocity*1.5f - direction*coef/2);
	}
	
	/*void OnCollisionEnter(Collision coll)
    {
        Collider other = coll.collider;
		BasicCollision otherCollider= other.gameObject.GetComponent<BasicCollision>();
		//if other object has a collision behavior, hit it in whatever way it wants.
		if (otherCollider!=null)
		{
			//should take into account what type of projectile this is.
			//otherCollider.hit (this.gameObject);
			
		}
        else
        {
            
           // GameObject.DestroyObject(coll.gameObject);
			
		}
        GameObject.DestroyObject(this.gameObject);
    }*/
}
