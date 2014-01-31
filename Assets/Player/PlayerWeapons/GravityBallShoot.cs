using UnityEngine;
using System.Collections;

public class GravityBallShoot : Attack {
	public GameObject gravityBall = (GameObject)Resources.Load ("GravityBall");
	public Vector3 initialVelocity;
	public float shotVelocity = 30;
	public float offset = 5;
	public int gravBallLife = 600;
	public int moveTimer = 90;	//This number will be used when figuring out how long the gravBall will move
	public bool hasShot = false;	//This will be set to true immediately after the first shot
	public bool forceAdded = false; //In update you dont want to keep adding the force everytime its called. 
									//When true, this stops additional force
	
	//Use for initialzation
	void Start () {
		
	}
	
	//Update is called once per frame
	//game moves roughly 60 frames per second
	//Should consider moving this code to fixedUpdate, since Update is frame dependent 
	//and fixedUpdate is not (it is based on the fixed time step specified in Unity).
	
	void Update () {
		
		if(hasShot == true && moveTimer != 0)
		{
			if(forceAdded = false)
			{
					gravityBall.rigidbody.AddForce(initialVelocity, ForceMode.VelocityChange);
					forceAdded = true;
			}
		}
		else if(moveTimer == 0)
		{
			gravityBall.rigidbody.AddForce(initialVelocity *= -1, ForceMode.VelocityChange);	
		}
		else
		{
			moveTimer--;
		}
	}
	
	public override bool shoot (Vector3 direction)
	{
			hasShot = true;		//User has clicked the button to shoot the ball	
		
			Vector3 initialPosition = this.transform.position + direction * offset;
			
			if(GameObject.Find("Player") != null){
				initialVelocity = direction * shotVelocity + GameObject.Find("Player").rigidbody.velocity;
			}
			else{
				initialVelocity = direction * shotVelocity + GameObject.Find("CenterOfGravity").rigidbody.velocity;
  			}
		
			//GameObject gravBall = (GameObject)Instantiate(GravityBall,initialPosition,new Quaternion(0,0,0,0));
			//gravBall.GetComponent<GravityBall>().maxLife=gravBallLife;
        	//gravBall.GetComponent<Orbit>().center = this.gameObject.GetComponent<Orbit>().center;
        	//gravBall.GetComponent<Orbit>().initialForce = 0;
		
			//this function never fails to shoot when called.
			return true;
			
	}
}