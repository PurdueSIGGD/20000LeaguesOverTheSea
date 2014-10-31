using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	//How far bullets can move away from the planent.
	public float maxRadius = 500;
	
	//player bullets have no life, enemy bullets have life, to prevent enemy spam.
	public int maxLife=0;
	
	// Initialization
	void Start () {
		if (maxLife != 0) {
			Destroy(this, (float)maxLife/50.0f); //! lifetime was originally calculated in FixedUpdate at timescale of .02 (or 50 frames/second). Division by 50 retains the original timing when maxLife is set exteranl to this script.
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//If the bullet is greater than the the maxRadius: reverse the velocity. 
		if (this.gameObject.transform.position.magnitude > maxRadius) {
			//this.GetComponent<Rigidbody>().velocity *= -1;
			Destroy(this.gameObject);
		}

		//transform.LookAt (new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, rigidbody.velocity.z), new Vector3(0,0,-1));
		transform.LookAt (-(transform.position + rigidbody.velocity*10));
	}
}
