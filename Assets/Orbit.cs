using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Orbit : MonoBehaviour {

	//Custom center of gravity, if set it overrides tagged planets
	public GameObject customGravityAnchor; 
	private List<GameObject> gravityAnchors; //Array of Planets, Stars, etc
	public bool includeCustomAnchor=false; //Include the customGravityAnchor with other planets

	//Initial Perpindicular Force 
	public float initialPerpForce = 50;
	public bool startCircular=false;
	public bool preferCircularOrbit=false;
	public float preferredOrbit=0;

	//Gravity Force Constant, calculation simplification, estimation G*m*M
	//private float gravityForceConstants = 500;
	// TODO: gravity constant is the mass of the planet

	public bool drawOrbit = false;
	public int linelength = 500;
	private LineRenderer lineRender;
	bool hohmannInProgress=false;	
	float hohmannStartDist;
	void Start () {
		//If customGravityAnchor is not null make it the only member of the gravityAnchors
		gravityAnchors = new List<GameObject>();

		if (customGravityAnchor!=null)
		{
			gravityAnchors.Add(customGravityAnchor);

		}
		if (includeCustomAnchor ||customGravityAnchor==null )
			foreach(GameObject g in GameObject.FindGameObjectsWithTag("Planet"))
				gravityAnchors.Add(g);
		/*
		if (!includeCustomAnchor && customGravityAnchor) {
			gravityAnchors = new ArrayList<GameObject>();
			gravityAnchors.add(customGravityAnchor);
		} else {
			gravityAnchors = GameObject.FindGameObjectsWithTag("Planet");
			if (includeCustomAnchor && customGravityAnchor) {
				Array.Resize<GameObject>(ref gravityAnchors, gravityAnchors.Length + 1);
				gravityAnchors[gravityAnchors.Length - 1] = customGravityAnchor;
			}
		}
		*/

		if (startCircular)
			forceCircular();
		else
		applyPerpForce(initialPerpForce);

		lineRender = this.GetComponent<LineRenderer>();
	}

	//uhh.. draw lines.
	void Update() {
	gravityAnchors.Remove(null);

	
		if (drawOrbit) {

			//Our Orbit Path, with a smoothed via spline
			Vector3[] positions = Interpolate();
			drawLine(positions, lineRender, linelength);
		}
	}

	//Apply our physics.
	void FixedUpdate(){
		if(preferCircularOrbit)
		circularizeOrbit();

		if(preferredOrbit!=0)
		{
			OrbitAtHeight(preferredOrbit);
		}
		//Get the force vector from all gravitational bodies
		Vector3 force = Vector3.zero;
		foreach (GameObject go in gravityAnchors) {
			force += gravitied(go);
		}

		// NOTE: ForceMode Velocity Change ignores this object's mass so the force of gravity 
		//	is applied the same across all objects
		this.rigidbody.AddForce(force, ForceMode.Acceleration);
		//this.rigidbody.velocity =this.rigidbody.velocity + force*Time.fixedDeltaTime;
	}

	//Interpolate our ship's path
	public Vector3[] Interpolate() {
		return Interpolate(this.gameObject);
	}

	public Vector3[] Interpolate(GameObject go) {
		return Interpolate(go.rigidbody.position, go.rigidbody.velocity);
	}

	public Vector3[] Interpolate(Vector3 position, Vector3 velocity) {
		int time = linelength;
		List<Vector3> pos = new List<Vector3>();

		pos.Add(position);
		Vector3 vel = velocity;

		float deltaTime = Time.fixedDeltaTime;

		for (int i = 1; i < time; i++) {
			////Velocity_Verlet
			/// http://en.wikipedia.org/wiki/Velocity_Verlet#Velocity_Verlet
			/// http://gamedev.stackexchange.com/questions/15708/how-can-i-implement-gravity
			//Calcualte force/acceleration
			Vector3 force = Vector3.zero;
			foreach (GameObject ga in gravityAnchors) {
				force += gravitied(ga, pos[i-1]);
			}
			//r2 = r1 + v*t + a*t*t
			//Also force = accelleration as we ignore the craft's mass
			pos.Add(pos[i-1] + deltaTime * (vel + deltaTime  * force ));

			//With Verlet, we recalculate the gravity for the velocity
			/*Vector3 newforce = Vector3.zero;
			foreach (GameObject ga in gravityAnchors) {
				newforce += gravitied(ga, pos[i-1]);
			}
			*/
			//v2 = v1 + (a1 + a2) * t
			//We average the two accelerations/forces for a more accuracy 
			//vel += (force + newforce) / 2 * deltaTime;

			vel+=force*deltaTime;
		}
		return pos.ToArray();
	}

	public void drawLine(Vector3[] positions, 	LineRenderer lr, float dist) {
		IEnumerable<Vector3> spline = Spline.NewCatmullRom(positions, 1, false);
		Vector3[] something = Spline.somethingsomethingLine(spline, dist);
		lr.SetVertexCount(something.Length);	
		//Draw the splined interpolation of our path!
		IEnumerator<Vector3> thing = spline.GetEnumerator();
		for (int i = 0; i < something.Length; i++) {
			if(thing.MoveNext()) {
				lr.SetPosition(i, thing.Current);

				//Drawing checks.
				foreach (GameObject go in gravityAnchors) {
					//If we are closer to the center of the planet than the radius stop 
					if (Vector3.Distance(thing.Current, go.rigidbody.position) < (go.rigidbody.collider.bounds.extents.x)) {
						lr.SetVertexCount(i);
						return;
					}
				}
				if (!CameraUtility.isInCameraFrame(thing.Current) ||
				    (i > 500 &&Vector3.Distance(positions[1], thing.Current) < 1)){
					lr.SetVertexCount(i);
					return;
				}
			}
		}
	}


	//Apply a force perpendicular the gravitational anchors
	//	Used to start the object off orbiting around the planet and not falling straight into it.
	public void applyPerpForce() {
		applyPerpForce(initialPerpForce);
	}

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

	void circularizeOrbit()
	{
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

		float IdealSpeed= Mathf.Sqrt(closestPlanet.rigidbody.mass/((this.transform.position-closestPlanet.transform.position).magnitude));
		Vector3 idealVelocity=IdealSpeed*perpDirection;
		//this.rigidbody.velocity=idealVelocity;
		if (Mathf.Abs(this.rigidbody.velocity.magnitude-idealVelocity.magnitude)<.1f)
			return;
		if (this.rigidbody.velocity.magnitude<idealVelocity.magnitude)
		this.rigidbody.AddForce((this.rigidbody.velocity-idealVelocity)*.5f,ForceMode.Acceleration);
		if (this.rigidbody.velocity.magnitude>idealVelocity.magnitude)
			this.rigidbody.AddForce((idealVelocity-this.rigidbody.velocity)*.5f,ForceMode.Acceleration);

	}

	void forceCircular()
	{
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
		
		float IdealSpeed= Mathf.Sqrt(closestPlanet.rigidbody.mass/((this.transform.position-closestPlanet.transform.position).magnitude));
		Vector3 idealVelocity=IdealSpeed*perpDirection;
		this.rigidbody.velocity=idealVelocity;
	
	}


	void OrbitAtHeight(float height)
	{

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
		float currentHeight=Mathf.Abs((this.rigidbody.position-closestPlanet.rigidbody.position).magnitude);
		//if about right, circularize
		if (Mathf.Abs(height-currentHeight)>hohmannStartDist+5)
		{
			hohmannInProgress=false;
		}
		if(Mathf.Abs(height-currentHeight)<5)
		{
			circularizeOrbit();
			if (hohmannInProgress && Mathf.Abs(height-currentHeight)<1)
			{
				forceCircular();
				hohmannInProgress=false;
			}


			return;
		}
		if(!hohmannInProgress)
		{

			//If not near proper orbit, start transfer orbit
			//Hohmann transfers only work if circular
			forceCircular();
			float deltav= Mathf.Sqrt(closestPlanet.rigidbody.mass/currentHeight)*(Mathf.Sqrt(2*height/(currentHeight+height))+1);
			//Debug.Log("Hohmann "+deltav +"Default "+rigidbody.velocity);
			if(currentHeight-height<0)
			rigidbody.AddForce(deltav*rigidbody.velocity.normalized*5,ForceMode.Acceleration);
			else
				rigidbody.AddForce(-deltav*rigidbody.velocity.normalized*5,ForceMode.Acceleration);

			
			hohmannStartDist=Mathf.Abs(height-currentHeight);
			hohmannInProgress=true;
		}

		 


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

	public GameObject getClosestPlanet(Vector3 v) {
		GameObject planet = gravityAnchors[0];
		foreach (GameObject p in gravityAnchors) {
			if (Vector3.Distance(p.rigidbody.position, v) <=
			    Vector3.Distance(planet.rigidbody.position, v)) {
				planet = p;
			}
		}
		return planet;
	}

	public List<GameObject> getPlanets() {
		return gravityAnchors;		
	}
}