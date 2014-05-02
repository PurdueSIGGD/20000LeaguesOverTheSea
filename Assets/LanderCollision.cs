using UnityEngine;
using System.Collections;

public class LanderCollision : BasicCollision {
	bool landed=false;
	GameObject center;
	public GameObject planet;
	public int shootDelay=50;
	public Animator anim;

	// Use this for initialization
	void Start () 
	{
		//center=this.GetComponent<Orbit>().center;
		planet = GameObject.Find ("CenterOfGravity");
		anim.speed=0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 direction= this.transform.position;//-center.transform.position;

		if (landed)
		{
			if (shootDelay==0)
			{
				Vector3 perpDirection= new Vector3(-direction.y,direction.x,0)*-1;
				//this.rigidbody.
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

			this.transform.LookAt(Vector3.zero, direction*-1);//new Vector3(-1,0,0));
		}
	}
	
	public override void hit(GameObject collider)
	{

		GameObject.DestroyObject(this.gameObject);
		
	}
	
	public void OnCollisionEnter(Collision coll)
	{
		if (coll.collider.gameObject.tag=="Planet")
		{
			landed=true;
			anim.speed=1;
			//anim.Play("OpenBarnacle", 0);
		}
	}
	
	
}
