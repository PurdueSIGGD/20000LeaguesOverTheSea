using UnityEngine;
using System.Collections;

public class Orbit2 : MonoBehaviour {

	//Custom center of gravity, if set it overrides tagged planets
	public GameObject customGravityAnchor; 
	private GameObject[] gravityAnchors; //Array of Planets, Stars, etc

	//Initial Perpindicular Force 
	public float initialPerpForce = 10;

	//Gravity Force Constant, calculation simplification, estimation G*m*M
	//private float gravityForceConstants = 500;
	// TODO: gravity contant is the mass of the planet

	public bool drawLine = false;
	
	void Start () {
		//If customGravityAnchor is not null make it the only member of the gravityAnchors
		if (customGravityAnchor != null) {
			gravityAnchors = new GameObject[1];
			gravityAnchors[0] = customGravityAnchor;
		} else {
			//Unity editor's tag system
			gravityAnchors = GameObject.FindGameObjectsWithTag("Planet");
		}

		applyPerpForce(initialPerpForce);
	}

	void FixedUpdate(){

		//Get the force vector from all gravitational bodies
		Vector3 force = Vector3.zero;
		foreach (GameObject go in gravityAnchors) {
			force += gravitied(go);
		}

		// NOTE: ForceMode Velocity Change ignores this object's mass so gravity 
		//			is applied the same across all objects
		this.rigidbody.AddForce(force, ForceMode.VelocityChange);
	}


	//Apply a force perpendicular the gravitational anchors
	//	Used to start the object off orbiting around the planet and not falling straight into it.
	void applyPerpForce(float force) {

		//Find which gravity object has the largest influence on the craft
		GameObject closestPlanet = gravityAnchors[0];
		Vector3 max = gravitied(closestPlanet); //Max dist vector
		foreach (GameObject go in gravityAnchors) {
			if (max.magnitude < gravitied(go).magnitude) {
				closestPlanet = go;
				max = gravitied(go);
			}
		}

		//Rotates the craft to planet vector 90 degrees, tis a neat vector trick
		Vector3 perpDirection = new Vector3(-max.y, max.x, 0).normalized;

		this.rigidbody.AddForce(perpDirection * force, ForceMode.VelocityChange);
	}

	//Calculate the Gravitational Force!
	//Simplified to planet mass / dist ^ 2 * vectorNorm(direction of craft to planet)
	Vector3 gravitied(GameObject go) {
		Vector3 vector = go.rigidbody.position - this.rigidbody.position;
		float magnitude = go.rigidbody.mass / vector.sqrMagnitude;
		return magnitude * vector.normalized;
	}
}