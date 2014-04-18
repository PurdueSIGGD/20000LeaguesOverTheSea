using UnityEngine;
using System.Collections;

public class ShieldGun : BaseWeapon {


	GameObject shield;
	public int shotCooldownInitial = 30;
	int shotCooldown = 0;
	Orbit parentOrbit;
	public float shotVelocity=30;
	public float newDegree; //the new degree of the spawnpoint of the shield

	// Use this for initialization
	void Start () {
		parentOrbit=parent.GetComponent<Orbit>();
		shield=(GameObject)Resources.Load ("SpaceShield");

	}
	
	// Update is called once per frame
	void Update () {
		if (parent==null || parent.activeSelf==false)
		{ 
			GameObject.Destroy(this.gameObject);
			return;
		}
		this.transform.position=parent.transform.position+offset;
		if (Input.GetMouseButtonDown(0)&& shotCooldown <= 0){
		Vector3 direction= getMouseDirection();
		newDegree = Random.value*360;//the random placement of the spawn point
		  //spawns a shield bullet at a random position around the ship
			GameObject newshield = (GameObject)Instantiate(shield,
			            parent.transform.position+(new Vector3(Mathf.Cos(newDegree),Mathf.Sin(newDegree),0))*9,
			            new Quaternion(0,0,0,0));
			newshield.GetComponent<ShieldOrbitShip>().parent=this.parent;
			//newshield.rigidbody.position = this.rigidbody.position;
			//newshield.transform.position = this.transform.position;
			//newshield.GetComponent<Orbit>().center = this.parent.GetComponent<Orbit>().center;
			//newshield.GetComponent<Orbit>().initialForce = 0;
			shotCooldown = shotCooldownInitial;
		}
		else
		{
			shotCooldown--;
		}
	}



//	public override void drawLine()
//	{
//		if (parentOrbit==null)
//			return;
//		
//		Vector3 direction= getMouseDirection();
//		Vector3 initialPosition= this.transform.position + direction * 4;
//		Vector3 initialVelocity = direction * shotVelocity + parent.rigidbody.velocity;
//		
//		Vector3[] interpolation = parentOrbit.Interpolate(initialPosition, initialVelocity);
//		parentOrbit.drawLine(interpolation, this.GetComponent<LineRenderer>(), 500);
//	}
}
