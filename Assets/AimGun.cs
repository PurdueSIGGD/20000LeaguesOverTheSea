using UnityEngine;
using System.Collections;

public class AimGun : MonoBehaviour {
	
	GameObject parent;
    GameObject bullet;
	Orbit parentOrbit;
	public bool showLine=false;
	public float shotVelocity=30;

	// Use this for initialization
	void Start () {
	
		parent=this.GetComponent<AttachGun>().parent;
		parentOrbit=parent.GetComponent<Orbit>();
        bullet = (GameObject)Resources.Load("SpaceBullet");
	}
	
	// Update is called once per frame
	void Update () {
		
		Ray MousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distanceToXYPlane=-MousePosition.origin.z/MousePosition.direction.z;
		Vector3 screenPosition= MousePosition.GetPoint(distanceToXYPlane);
		Vector3 direction= screenPosition-this.transform.position;
		direction.Normalize();
		if (showLine)
		{
			Vector3 initialPosition= this.transform.position+direction*2;
			Vector3 initialVelocity = direction*shotVelocity;
			
			Vector3[] first = parentOrbit.getNextPosAndVel(initialPosition,initialVelocity);
			parentOrbit.drawOrbitLine(this.GetComponent<LineRenderer>(),10000,first);
			
		}
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = (GameObject)Instantiate(bullet,this.transform.position+ direction * 2,new Quaternion(0,0,0,0));
            //newBullet.rigidbody.position = this.rigidbody.position + direction * 5;
            //newBullet.transform.position = this.transform.position + direction * 5;
            newBullet.GetComponent<Orbit>().center = this.parent.GetComponent<Orbit>().center;
            newBullet.GetComponent<Orbit>().initialForce = 0;
            newBullet.rigidbody.AddForce(shotVelocity * direction, ForceMode.VelocityChange);
             

        }
	}
}
