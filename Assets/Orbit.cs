using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Orbit : MonoBehaviour {

	//Custom center of gravity, if set it overrides tagged planets
	public GameObject customGravityAnchor; 
	private GameObject[] gravityAnchors; //Array of Planets, Stars, etc

	//Initial Perpindicular Force 
	public float initialPerpForce = 50;

	//Gravity Force Constant, calculation simplification, estimation G*m*M
	//private float gravityForceConstants = 500;
	// TODO: gravity constant is the mass of the planet

	public bool drawLine = false;
	private LineRenderer lineRender;
	
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

		lineRender = this.GetComponent<LineRenderer>();
	}

	void Update() {
		//Debug.Log (gravityAnchors[0].collider.bounds.extents.x + " / " + gravityAnchors[0].collider.bounds.extents.magnitude);
	}

	//Apply our physics, draw lines.
	void FixedUpdate(){
		//Get the force vector from all gravitational bodies
		Vector3 force = Vector3.zero;
		foreach (GameObject go in gravityAnchors) {
			force += gravitied(go);
		}

		// NOTE: ForceMode Velocity Change ignores this object's mass so the force of gravity 
		//	is applied the same across all objects
		this.rigidbody.AddForce(force, ForceMode.Force);
		//this.rigidbody.velocity =this.rigidbody.velocity + force*Time.fixedDeltaTime;


		if (drawLine) {
			//Our Orbit Path, with a smoothed via spline 
			int stupid = 2000;
			IEnumerable<Vector3> spline = Spline.NewCatmullRom(Interpolate(stupid), 3, false);

			//Draw the splined interpolation of our path!
			lineRender.SetVertexCount(stupid);
			IEnumerator thing = spline.GetEnumerator();
			for (int i = 0; i < stupid; i++) {
				if(thing.MoveNext()) {
					lineRender.SetPosition(i, (Vector3) thing.Current);
				}
			}
		}
	}

	//Interpolate our ship's path
	Vector3[] Interpolate(int steps) {
		Vector3[] pos = new Vector3[steps];
		pos[0] = this.rigidbody.position;

		Vector3 vel = this.rigidbody.velocity;

		for (int i = 1; i < steps; i++) {
			//Calcualte force/acceleration
			Vector3 force = Vector3.zero;
			foreach (GameObject go in gravityAnchors) {
				force += gravitied(go, pos[i-1]);
			}

			//v2 = v1 + a*t
			vel += force * Time.smoothDeltaTime;
			//r2 = r1 + v*t + a*t*t
			//Also force = accelleration as we ignore the craft's mass
			pos[i] = pos[i-1] + vel * Time.smoothDeltaTime;

			//Stop interpolating if we got off the screen 
			if(!CameraUtility.isInCameraFrame(pos[i])) {
				return pos;
			}
			//or into a planet.
			foreach (GameObject go in gravityAnchors) {
				//If we are closer to the center of the planet than the radius stop 
				if (Vector3.Distance(pos[i], go.rigidbody.position) < (go.rigidbody.collider.bounds.extents.x)) { //lol
					return pos;
				}
			}
		}

		return pos;
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

		this.rigidbody.AddForce(perpDirection * force * -1, ForceMode.VelocityChange);
	}

	//Calculate the Gravitational Force!
	//Simplified to planet mass / dist ^ 2 * vectorNorm(direction of craft to planet)
	Vector3 gravitied(GameObject go) {
		return gravitied(go, this.rigidbody.position);
	}

	Vector3 gravitied(GameObject go, Vector3 position) {
		Vector3 vector = go.rigidbody.position - position;
		float magnitude = go.rigidbody.mass / vector.sqrMagnitude;
		return magnitude * vector.normalized;
	}
}