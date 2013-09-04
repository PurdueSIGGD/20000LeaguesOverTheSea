using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {
	
	
	public GameObject center;
	public float initialForce=10;
	float ForceMultiplier=250;
	public bool drawLine=false;
	public int lineResolution=10;
	
	Rigidbody centerBody;
	Rigidbody body;
	LineRenderer line;
	Vector3[] PredictedPosAndVel;
	// Use this for initialization
	void Start () {
	body=this.GetComponent<Rigidbody>();
	centerBody=center.GetComponent<Rigidbody>();
	if (drawLine)
	line= this.GetComponent<LineRenderer>();
	//give intital velocity perpendicular to gravity
	Vector3 difference= centerBody.position-body.position;
	Vector3 perpDirection= new Vector3(-difference.y,difference.x,0)*-1;
	perpDirection.Normalize();
	body.AddForce(perpDirection*initialForce,ForceMode.VelocityChange);
	PredictedPosAndVel=getNextPosAndVel(body.position,body.velocity);
	}
	
	// Update is called once per frame
	void Update () {

		
	}
	
	void FixedUpdate(){
		
	//calculate force and direction towards center of gravity 
	Vector3 difference= centerBody.position-body.position;
	float distance = difference.magnitude;
	float force = (1)/(Mathf.Pow (distance,2));
	Vector3 direction = difference.normalized;
	force*=ForceMultiplier;
	body.AddForce(direction.x*force,direction.y*force,0,ForceMode.VelocityChange);
	if (drawLine)
		{
	if (PredictedPosAndVel[0]==body.position && PredictedPosAndVel[1]==body.velocity)
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
	liner.SetVertexCount(maxTicks/lineResolution+1);
	int i;
	for (i=0;i<maxTicks;i++)
		{
				
			//if orbit line loops back to start, or if it hits the center of whatever it's orbiting around, stop calculating
			if (i>50 && ((Mathf.Abs(next[0].x-body.position.x)<1 && Mathf.Abs(next[0].y-body.position.y)<1)))
				{
					liner.SetPosition(i/lineResolution,next[0]);
					liner.SetVertexCount(i/lineResolution+1);	
					break;

				}
				
			if ((Mathf.Abs(next[0].x-centerBody.position.x)<3 && Mathf.Abs(next[0].y-centerBody.position.y)<3))
				{
					liner.SetVertexCount(i/lineResolution+1);	
					break;
				}
			if (i%lineResolution==0)
			{			
			liner.SetPosition(i/lineResolution,next[0]);

			}
			next=getNextPosAndVel(next[0],next[1]);
		}
	liner.SetPosition(i/lineResolution,next[0]);
	  	
		
		
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
}
