using UnityEngine;
using System.Collections;

public class GunGunShoot : BaseWeapon {
	
	GameObject bullet;
	public int shotCooldownInitial = 30;
	int shotCooldown = 0;
	Orbit parentOrbit;
	
	public float shotVelocity=30;
	// Use this for initialization
	void Start () {
		parentOrbit=parent.GetComponent<Orbit>();
        bullet = (GameObject)Resources.Load("GunGunBullet");
		
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
		if (Input.GetMouseButtonDown(0) && shotCooldown <= 0)
        {
            GameObject newBullet = (GameObject)Instantiate(bullet,this.transform.position+ direction * 4,new Quaternion(0,0,0,0));
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
