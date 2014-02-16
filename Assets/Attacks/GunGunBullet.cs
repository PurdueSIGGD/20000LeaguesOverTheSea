using UnityEngine;
using System.Collections;

public class GunGunBullet : Bullet {

	GameObject bullet;
	
	
	public bool shot=false;
	public float shotVelocity=30;
	
	// Use this for initialization
	void Start () {
	
        bullet = (GameObject)Resources.Load("SpaceBullet");
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//If the bullet is greater than the the maxRadius: reverse the velocity. 
		if (this.gameObject.transform.position.magnitude > maxRadius)
			this.GetComponent<Rigidbody>().velocity *= -1;
	
		if(shot)
			return;
		
		Vector3 direction= getMouseDirection();
		drawLine();
		if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = (GameObject)Instantiate(bullet,this.transform.position+ direction * 4,new Quaternion(0,0,0,0));
            //newBullet.rigidbody.position = this.rigidbody.position + direction * 5;
            //newBullet.transform.position = this.transform.position + direction * 5;
            newBullet.GetComponent<Orbit>().center = this.GetComponent<Orbit>().center;
            newBullet.GetComponent<Orbit>().initialForce = 0;
            newBullet.rigidbody.AddForce(shotVelocity * direction+this.rigidbody.velocity, ForceMode.VelocityChange);
			shot=true;
			this.GetComponent<LineRenderer>().enabled=false;
        }
	
	
		
	}
	
	public Vector3 getMouseDirection()
	{
		Ray MousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distanceToXYPlane=-MousePosition.origin.z/MousePosition.direction.z;
		Vector3 screenPosition= MousePosition.GetPoint(distanceToXYPlane);
		Vector3 direction= screenPosition-this.transform.position;
		direction.Normalize();
		return direction;
	}
	
	public void drawLine()
	{
		if (this.GetComponent<Orbit>()==null)
			return;
		Vector3 direction= getMouseDirection();
		Vector3 initialPosition= this.transform.position+direction*4;
		Vector3 initialVelocity = direction*shotVelocity+this.rigidbody.velocity;
		
		Vector3[] first = this.GetComponent<Orbit>().getNextPosAndVel(initialPosition,initialVelocity);
		this.GetComponent<Orbit>().drawOrbitLine(this.GetComponent<LineRenderer>(),10000,first);
			
		
	}
	
	
}
