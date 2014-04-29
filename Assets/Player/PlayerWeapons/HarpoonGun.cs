using UnityEngine;
using System.Collections;

public class HarpoonGun : BaseWeapon {

	GameObject bullet;
	public int shotCooldownInitial = 100;
	int shotCooldown = 0;
	Orbit parentOrbit;
	public GameObject whaleShip;
	public bool hooked=false;
	public Vector3 hookedPoint;
	float maxDist;
	public float shotVelocity=30;
	Vector3 oldWhalePos;
	// Use this for initialization
	void Start () {
		parentOrbit=parent.GetComponent<Orbit>();
		bullet = (GameObject)Resources.Load("HarpoonBullet");
	}
	
	// Update is called once per frame
	void Update () {
		if (parent==null || parent.activeSelf==false)
		{ 
			GameObject.Destroy(this.gameObject);
			return;
		}
		this.transform.position=parent.transform.position+offset;
		
		Vector3 direction= getMouseDirection();
		if (Input.GetMouseButtonDown(0) && shotCooldown <= 0 && !hooked)
		{
			GameObject newBullet = (GameObject)Instantiate(bullet,this.transform.position+ direction * 4,new Quaternion(0,0,0,0));
			//newBullet.rigidbody.position = this.rigidbody.position + direction * 5;
			//newBullet.transform.position = this.transform.position + direction * 5;
			newBullet.GetComponent<HarpoonProjectile>().parent=this.gameObject;
			//newBullet.GetComponent<Orbit>().center = this.parent.GetComponent<Orbit>().center;
			newBullet.GetComponent<Orbit>().initialPerpForce = 0;
			newBullet.rigidbody.AddForce(shotVelocity * direction+parent.rigidbody.velocity, ForceMode.VelocityChange);
			shotCooldown = shotCooldownInitial;
		}
		else
		{
			shotCooldown--;
		}
		if (Input.GetMouseButtonDown(0) && shotCooldown <= 0 && hooked)
		{
			hooked=false;
			this.GetComponent<LineRenderer>().SetVertexCount(0);
			maxDist=0;
		}

		if(maxDist==0 && hooked)
		{

			maxDist=(parent.transform.position-hookedPoint).magnitude;
			Debug.Log ("hooked at "+ maxDist);
			oldWhalePos=whaleShip.transform.position;
		}



		if(hooked)


		{
			Vector3 whaleChange=whaleShip.transform.position-oldWhalePos;
			hookedPoint+=whaleChange;


			this.GetComponent<LineRenderer>().SetVertexCount(2);
				this.GetComponent<LineRenderer>().SetPosition(0,parent.transform.position);
			this.GetComponent<LineRenderer>().SetPosition(1,hookedPoint);


			float curDist=(parent.transform.position-hookedPoint).magnitude;
			Vector3 dir = (hookedPoint -parent.transform.position ); //Direction
			if (curDist>maxDist*.98)
			{Vector3 force = dir.normalized * 200;
			parent.rigidbody.AddForce(force);
			}

			else if (curDist>maxDist*.8)
			{
				Vector3 force = dir.normalized * 200;
				parent.rigidbody.AddForce(force/(Mathf.Pow(maxDist-curDist,2)));
			}

			oldWhalePos=whaleShip.transform.position;
		}

	}
	
	public override void drawLine()
	{
		/*if (parentOrbit==null)
			return;	
		Vector3 direction= getMouseDirection();
		Vector3 initialPosition= this.transform.position+direction*4;
		Vector3 initialVelocity = direction*shotVelocity+parent.rigidbody.velocity;
		
		//Vector3[] first = parentOrbit.getNextPosAndVel(initialPosition,initialVelocity);
		//parentOrbit.drawOrbitLine(this.GetComponent<LineRenderer>(),10000,first);
		*/

		
	}

}
