using UnityEngine;
using System.Collections;

public class AimGun : MonoBehaviour {
	
	GameObject parent;
	Orbit parentOrbit;
	public bool showLine=false;
	public float shotVelocity=30;

	// Use this for initialization
	void Start () {
	
		parent=this.GetComponent<AttachGun>().parent;
		parentOrbit=parent.GetComponent<Orbit>();

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
			Vector3 initialPosition= this.transform.position;
			Vector3 initialVelocity = direction*shotVelocity;
			
			Vector3[] first = parentOrbit.getNextPosAndVel(initialPosition,initialVelocity);
			parentOrbit.drawOrbitLine(this.GetComponent<LineRenderer>(),10000,first);
			
		}
	}
}
