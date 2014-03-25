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

	//uhh.. draw lines.
	void Update(){
		if (drawLine) {
			//Our Orbit Path, with a smoothed via spline
			Vector3[] positions = Interpolate();
			IEnumerable<Vector3> spline = Spline.NewCatmullRom(positions, 1, false);
			
			//Draw the splined interpolation of our path!
			lineRender.SetVertexCount(positions.GetLength(0));
			IEnumerator<Vector3> thing = spline.GetEnumerator();
			for (int i = 0; i < positions.GetLength(0); i++) {
				if(thing.MoveNext()) {
					lineRender.SetPosition(i, thing.Current);

					//Drawing checks.
					foreach (GameObject go in gravityAnchors) {
						//If we are closer to the center of the planet than the radius stop 
						if (Vector3.Distance(thing.Current, go.rigidbody.position) < (go.rigidbody.collider.bounds.extents.x)) {
							lineRender.SetVertexCount(i);
							return;
						}
					}
					if (!CameraUtility.isInCameraFrame(thing.Current) ||
					    (i > 1000 &&Vector3.Distance(positions[1], thing.Current) < 1)){
						lineRender.SetVertexCount(i);
						return;
					}
				}
			}
		}
	}

	//Apply our physics.
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
	}

	//Interpolate our ship's path
	Vector3[] Interpolate() {
		int time = 10000;


		List<Vector3> pos = new List<Vector3>();

		//pos[0] = this.rigidbody.position;
		pos.Add(this.rigidbody.position);

		Vector3 vel = this.rigidbody.velocity;

		float deltaTime = Time.smoothDeltaTime;

		for (int i = 1; i < time; i++) {
			////Velocity_Verlet
			/// http://en.wikipedia.org/wiki/Velocity_Verlet#Velocity_Verlet
			/// http://gamedev.stackexchange.com/questions/15708/how-can-i-implement-gravity
			//Calcualte force/acceleration
			Vector3 force = Vector3.zero;
			foreach (GameObject go in gravityAnchors) {
				force += gravitied(go, pos[i-1]);
			}
			//r2 = r1 + v*t + a*t*t
			//Also force = accelleration as we ignore the craft's mass
			pos.Add(pos[i-1] + deltaTime * (vel + deltaTime  * force / 2));

			//With Verlet, we recalculate the gravity for the velocity
			Vector3 newforce = Vector3.zero;
			foreach (GameObject go in gravityAnchors) {
				newforce += gravitied(go, pos[i]);
			}
			//v2 = v1 + (a1 + a2) * t
			//We average the two accelerations/forces for a more accuracy 
			vel += (force + newforce) / 2 * deltaTime;
		}
		return pos.ToArray();
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