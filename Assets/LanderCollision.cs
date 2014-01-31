using UnityEngine;
using System.Collections;

public class LanderCollision : BasicCollision {
	bool landed=false;
	GameObject center;
	public int shootDelay=50;
	// Use this for initialization
	void Start () {
	center=this.GetComponent<Orbit>().center;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (landed)
		{
			if (shootDelay==0)
			{
			
			//this.rigidbody.
			Vector3 direction= this.transform.position-center.transform.position;
			Vector3 perpDirection= new Vector3(-direction.y,direction.x,0)*-1;
			perpDirection*=Random.Range(.5f,1f);
			direction+=perpDirection;
			direction.Normalize();
			
			this.GetComponent<Attack>().shoot (direction);
				shootDelay=50;
			}
			shootDelay--;
		
		}
		else
		{
			this.rigidbody.velocity*=1.02f;
			
		}
	}
	
	public override void hit(GameObject collider)
	{

	GameObject.DestroyObject(this.gameObject);
		
	}
	
	public override void OnCollisionEnter(Collision coll)
	{
		if (coll.collider.gameObject.tag=="Planet")
		{
			landed=true;
		}
	}
	
	
}
