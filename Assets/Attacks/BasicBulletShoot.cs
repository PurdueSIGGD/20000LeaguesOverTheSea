using UnityEngine;
using System.Collections;

public class BasicBulletShoot : Attack {
	public GameObject bullet= (GameObject)Resources.Load("SpaceBullet");
	public float shotVelocity=30;
	public float offset=5;
	public int bulletLife=500;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		
	}
	
	public override bool shoot (Vector3 direction)
	{
	
		Vector3 initialPosition = this.transform.position + direction * offset;
		Vector3 initialVelocity;
	
	
		initialVelocity = direction * shotVelocity;
		GameObject newBullet = (GameObject)Instantiate(bullet,initialPosition,new Quaternion(0,0,0,0));
		newBullet.GetComponent<Bullet>().maxLife=bulletLife;
		newBullet.GetComponent<Orbit>().center = this.gameObject.GetComponent<Orbit>().center;
		newBullet.GetComponent<Orbit>().initialForce = 0;
		newBullet.rigidbody.AddForce(initialVelocity, ForceMode.VelocityChange);
		//this function never fails to shoot when called.
		return true;
		
	}
}
