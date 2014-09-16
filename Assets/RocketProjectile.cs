using UnityEngine;
using System.Collections;

public class RocketProjectile : MonoBehaviour {
	public int speed = 30;
	public int lifetime = 300;
	int curLife;
	// Use this for initialization
	void Start () 
	{
		curLife = 0;
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
	void FixedUpdate () 
	{
		//If the bullet is greater than the the maxRadius: reverse the velocity.
		Vector3 direction= getMouseDirection();

		if(curLife >= lifetime)
		{
			GameObject.Destroy (this.gameObject);
		}

		this.rigidbody.AddForce(direction*speed,ForceMode.Impulse);
		curLife++;

		//Could try some Quat Slerp for more smoothness but this works
		transform.LookAt(rigidbody.position - rigidbody.velocity*1.5f - direction*speed/2);
	}
}
