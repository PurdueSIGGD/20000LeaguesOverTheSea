using UnityEngine;
using System.Collections;

public class OctoSquidBehaviourScript : MonoBehaviour 
{
	GameObject playerObject;

	// Use this for initialization
	void Start () 
	{
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
		
	}
}
