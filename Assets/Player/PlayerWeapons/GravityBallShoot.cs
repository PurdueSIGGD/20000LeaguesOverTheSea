using UnityEngine;
using System.Collections;

public class GravityBallShoot : BaseWeapon {
	public GameObject gravityBall;
	public GameObject newBullet;
	public int shotCooldownInitial = 30;
	int shotCooldown = 0;
	Orbit parentOrbit;

	public float shotVelocity=30;
	public int timer = 120;				//we are using time to judge when to stop gravity ball
	public bool hasShot = false;		//has gravity ball shot yet?

	// Use this for initialization
	void Start () {
		parentOrbit=parent.GetComponent<Orbit>();
		gravityBall = (GameObject)Resources.Load ("GravityBall");
	}
	
	//Update is called once per frame
	//game moves roughly 60 frames per second
	//Should consider moving this code to fixedUpdate, since Update is frame dependent 
	//and fixedUpdate is not (it is based on the fixed time step specified in Unity).
	
	void Update () {


		if(hasShot == true && timer != 0){
			timer = timer -1;
		}

		if(timer == 0){
			//this hopefully will stop the gravityball immediately
			//newBullet.rigidbody.AddForce(-(shoot.initialVelocity), ForceMode.VelocityChange);
			newBullet.rigidbody.velocity = new Vector3(0,0,0);
		}

		if (parent==null || parent.activeSelf==false)
		{ 
			GameObject.Destroy(this.gameObject);
			return;
		}
		this.transform.position=parent.transform.position+offset;
		
		Vector3 direction= getMouseDirection();
		if (Input.GetMouseButtonDown(0) && shotCooldown <= 0)
		{
			newBullet = (GameObject)Instantiate(gravityBall,this.transform.position+ direction * 4,new Quaternion(0,0,0,0));
			//newBullet.rigidbody.position = this.rigidbody.position + direction * 5;
			//newBullet.transform.position = this.transform.position + direction * 5;
			newBullet.GetComponent<Orbit>().center = this.parent.GetComponent<Orbit>().center;
			newBullet.GetComponent<Orbit>().initialForce = 0;
			newBullet.rigidbody.AddForce(shotVelocity * direction+parent.rigidbody.velocity, ForceMode.VelocityChange);
			shotCooldown = shotCooldownInitial;
		}
		else
		{
			shotCooldown--;
		}
	}

	public override void drawLine()
	{
		if (parentOrbit==null)
			return;
		Vector3 direction= getMouseDirection();
		Vector3 initialPosition= this.transform.position+direction*4;
		Vector3 initialVelocity = direction*shotVelocity+parent.rigidbody.velocity;
		
		Vector3[] first = parentOrbit.getNextPosAndVel(initialPosition,initialVelocity);
		parentOrbit.drawOrbitLine(this.GetComponent<LineRenderer>(),10000,first);
		
		
	}
}