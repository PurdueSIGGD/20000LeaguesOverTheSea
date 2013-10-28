using UnityEngine;
using System.Collections;

public class OctoSquidBehaviourScript : MonoBehaviour 
{
	GameObject playerObject;
	Rigidbody octoSquid;
	public int followDistance = 15;
	public int initialMovementForce = 5;
	
	// Use this for initialization
	void Start () 
	{
		octoSquid = this.GetComponent<Rigidbody>();
		playerObject =  GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		UpdateMovement();
	}
	
	void UpdateMovement()
	{
		Vector3 moveDirection = playerObject.transform.position - this.transform.position;		
		
		/*if(moveDirection.magnitude > followDistance)
		{
			moveDirection.Normalize();
			octoSquid.velocity = new Vector3(moveDirection.x*initialMovementForce,moveDirection.y*initialMovementForce,0);	
		}
		else
		{*/
			octoSquid.transform.position = playerObject.transform.position + new Vector3(followDistance, followDistance, 0);	
		//}
	}
}
