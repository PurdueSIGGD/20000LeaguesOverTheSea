using UnityEngine;
using System.Collections;

public class ShieldOrbitShip : MonoBehaviour {

	public GameObject parent;
	public float initialForce=10;
	float ForceMultiplier=500;
	public bool drawLine=false;
	public int lineResolution=20;
	public Vector3 ShipPos;//ships position for the shield bullets to orbit around
	Rigidbody centerBody;
	Rigidbody body;
	LineRenderer line;
	Vector3[] PredictedPosAndVel;
	float Distance; //the distance between the ship and the bullet/shield
	GameObject[] ships; // the equivalent of the planet thing to find a ship
	GameObject ship; // the player ship 
	float degree;

	// Use this for initialization
	void Start () {
		//center=GameObject.FindGameObjectsWithTag("Planet");
		body=this.GetComponent<Rigidbody>();
		//centerBody=center.GetComponent<Rigidbody>();
		//if (drawLine)
		//line= this.GetComponent<LineRenderer>();
		//give intital velocity perpendicular to gravity
		//givePerpBoost(initialForce);
		ships = GameObject.FindGameObjectsWithTag("Player");
		degree=0;

	}
	
	// Update is called once per frame
	void Update () {
		ShipPos = parent.transform.position;//goes for the first one because there is only one player for now
		Distance= 7;
		//changes the degreee of the shield ball
		this.transform.position=ShipPos+Distance*(new Vector3(Mathf.Cos (degree),Mathf.Sin (degree),0));
		degree++;
		if(degree==360){//changes the degree each time
			degree=0;}
	}

//	public void givePerpBoost(float force)
//	{
//		//apply force perpindicular to direction of orbit.
//		Vector3 difference= centerBody.position-body.position;
//		Vector3 perpDirection= new Vector3(-difference.y,difference.x,0)*-1;
//		perpDirection.Normalize();
//		body.AddForce(perpDirection*force,ForceMode.VelocityChange);
//		
//		PredictedPosAndVel=getNextPosAndVel(body.position,body.velocity);
//	}

//	public Vector3[] getNextPosAndVel(Vector3 position, Vector3 velocity)
//	{
//		Vector3 difference= centerBody.position-position;
//		float distance = difference.magnitude;
//		float force = (ForceMultiplier)/(Mathf.Pow (distance,2));
//		Vector3 direction = difference.normalized;
//		Vector3 newVelocity = Vector3.zero;
//		newVelocity+=(direction*force)+velocity;
//		Vector3 newPosition= newVelocity*Time.fixedDeltaTime+position;
//		Vector3[] ret = {newPosition,newVelocity};
//		return ret;
//	}
}
