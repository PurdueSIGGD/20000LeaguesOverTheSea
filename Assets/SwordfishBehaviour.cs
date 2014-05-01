using UnityEngine;
using System.Collections;

public class SwordfishBehaviour : MonoBehaviour 
{

	public float speed;
	public GameObject planet;

	// Use this for initialization
	void Start () 
	{
		planet = GameObject.Find("CenterOfGravity");
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Vector3 direction = this.transform.position - planet.transform.position;
		direction.Normalize();

		rigidbody.AddForce (speed * -direction);

		transform.LookAt (planet.transform.position);
	}
}
