using UnityEngine;
using System.Collections;

public class AimAtPlayer: MonoBehaviour {
	
	GameObject parent;
    GameObject bullet;
	GameObject playerObject;
	Orbit parentOrbit;
	public bool showLine=false;
	public float shotVelocity=30;
	public int maxFrames=250;
	int frameCount = 0;
	
	// Use this for initialization
	void Start () {
		//playerObject = GameObject.Find("Player");
		parent=this.GetComponent<AttachGun>().parent;
		parentOrbit=parent.GetComponent<Orbit>();
        bullet = (GameObject)Resources.Load("SpaceBullet");
	}
	
	// Update is called once per frame
	void Update () {
		if (playerObject == null) {
			playerObject = GameObject.Find("Player");
			if (playerObject == null) {
				return;
			}
		}
			
		Vector3 direction = playerObject.transform.position - this.transform.position;
		direction.Normalize();
		
		Vector3 initialPosition= this.transform.position + direction * 5;
		Vector3 initialVelocity = direction * shotVelocity + playerObject.rigidbody.velocity;
		
		if (showLine) {
		
			Vector3[] first = parentOrbit.getNextPosAndVel(initialPosition,initialVelocity);
			parentOrbit.drawOrbitLine(this.GetComponent<LineRenderer>(),10000,first);
		}
		
		if (frameCount == maxFrames) {
			
			GameObject newBullet = (GameObject)Instantiate(bullet,initialPosition,new Quaternion(0,0,0,0));
			newBullet.GetComponent<Bullet>().maxLife=500;
        	newBullet.GetComponent<Orbit>().center = this.parent.GetComponent<Orbit>().center;
        	newBullet.GetComponent<Orbit>().initialForce = 0;
        	newBullet.rigidbody.AddForce(initialVelocity, ForceMode.VelocityChange);
			frameCount = 0;
		
		} else {
			frameCount++;
			
		}
	}
}
