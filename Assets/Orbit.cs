using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {
	
	
	public GameObject center;
	public float initialForce=10;
	float ForceMultiplier=500;
	public bool drawLine=false;
	public int lineResolution=20;
	
	Rigidbody centerBody;
	Rigidbody body;
	LineRenderer line;
	Vector3[] PredictedPosAndVel;
	GameObject[] planets;
	// Use this for initialization
	void Start () {
		body=this.GetComponent<Rigidbody>();
		centerBody=center.GetComponent<Rigidbody>();
		if (drawLine)
			line= this.GetComponent<LineRenderer>();
		//give intital velocity perpendicular to gravity
		givePerpBoost(initialForce);
		 planets = GameObject.FindGameObjectsWithTag("Planet");
	}
	
	// Update is called once per frame
	void Update () {

		
	}
	
	void FixedUpdate(){

        //changes the orbit if there are more than 2 planets; changes based on distance to planet
		bool centerChanged=false;
		if (planets.Length > 1) {
			GameObject closestPlanet = center;
			int distanceFromThis = (int) Mathf.Sqrt(Mathf.Pow(center.transform.position.x - this.transform.position.x, 2) - Mathf.Pow(center.transform.position.y - this.transform.position.y, 2));
			for (int i = 0; i < planets.Length; i++) {
				if (Mathf.Sqrt(Mathf.Pow(planets[i].transform.position.x - this.transform.position.x, 2) - Mathf.Pow(planets[i].transform.position.y - this.transform.position.y, 2)) <= distanceFromThis) {
					center = planets[i];
					centerBody = center.GetComponent<Rigidbody>();
					centerChanged=true;
				}
			}
		}

		//calculate force and direction towards center of gravity 
		Vector3 difference= centerBody.position-body.position;
		float distance = difference.magnitude;
		float force = (1)/(Mathf.Pow (distance,2));
		Vector3 direction = difference.normalized;
		force*=ForceMultiplier;
		body.AddForce(direction.x*force,direction.y*force,0,ForceMode.VelocityChange);
		if (drawLine)
		{
			if (PredictedPosAndVel[0]==body.position && PredictedPosAndVel[1]==body.velocity && !centerChanged)
			{
				//assume calculations haven't changed and line does not need recalculated.
				PredictedPosAndVel=getNextPosAndVel(body.position,body.velocity);
				
			}
			else
			{
				Vector3[] next=getNextPosAndVel(body.position,body.velocity);
				PredictedPosAndVel=next;
				drawOrbitLine(line, 10000, next);
			}
		}
	}
	
	public void drawOrbitLine(LineRenderer liner, int maxTicks, Vector3[] next)
	{
		liner.SetVertexCount(maxTicks+1);
		int currentVertex=0;
		int i;
		for (i=0;i<maxTicks;i++)
		{
					
			//if orbit line loops back to start, or if it hits the center of whatever it's orbiting around, stop calculating
			if (i>50 && ((Mathf.Abs(next[0].x-body.position.x)<2 && Mathf.Abs(next[0].y-body.position.y)<2)))
			{
				liner.SetPosition(currentVertex,next[0]);
				liner.SetVertexCount(currentVertex+1);	
				
				break;
	
			}
			
			if(i%100 == 0)
			{
				 if(!CameraUtility.isInCameraFrame(next[0]))
				{
					liner.SetPosition(currentVertex,next[0]);
					liner.SetVertexCount(currentVertex+1);	
				
					break;	
				}
			}
			if ((Mathf.Abs(next[0].x-centerBody.position.x)<5 && Mathf.Abs(next[0].y-centerBody.position.y)<5))
			{
				liner.SetVertexCount(currentVertex+1);	
				
				break;
			}
			if (i%lineResolution==0)
			{			
				liner.SetPosition(currentVertex,next[0]);
				currentVertex++;
	
			}
			if (next[1].magnitude>30 && lineResolution>1)
			{
				lineResolution=1;
					
			}
			else if (lineResolution==1)
			{
				lineResolution=20;
			}
			next=getNextPosAndVel(next[0],next[1]);
		}
		liner.SetVertexCount(currentVertex+1);
		liner.SetPosition(currentVertex,next[0]);
		lineResolution=20;
	}
	
	public Vector3[] getNextPosAndVel(Vector3 position, Vector3 velocity)
	{
		Vector3 difference= centerBody.position-position;
		float distance = difference.magnitude;
		float force = (ForceMultiplier)/(Mathf.Pow (distance,2));
		Vector3 direction = difference.normalized;
		Vector3 newVelocity = Vector3.zero;
		newVelocity+=(direction*force)+velocity;
		Vector3 newPosition= newVelocity*Time.fixedDeltaTime+position;
		Vector3[] ret = {newPosition,newVelocity};
		return ret;
	}
	
	public void givePerpBoost(float force)
	{
		//apply force perpindicular to direction of orbit.
		Vector3 difference= centerBody.position-body.position;
		Vector3 perpDirection= new Vector3(-difference.y,difference.x,0)*-1;
		perpDirection.Normalize();
		body.AddForce(perpDirection*force,ForceMode.VelocityChange);
		
		PredictedPosAndVel=getNextPosAndVel(body.position,body.velocity);
		}
}
