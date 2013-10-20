using UnityEngine;
using System.Collections;

public class GravityBall : MonoBehaviour {
	
	public float maxRadius=500;
	//player bullets have no life, enemy bullets have life, to prevent enemy spam.
	public int maxLife=0;
	int currentLife=0;
	// Use this for initialization
	void Start () {
		currentLife=maxLife;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.transform.position.magnitude>maxRadius)
			this.GetComponent<Rigidbody>().velocity -= this.GetComponent<Rigidbody>().velocity;	//If it hits the edge of the frame, it should stop(hopefully).		
	}
	void FixedUpdate()
	{
		if (maxLife!=0)
		{
			currentLife--;
			if (currentLife==0)
				GameObject.DestroyObject(this.gameObject);
		}
	}


}
