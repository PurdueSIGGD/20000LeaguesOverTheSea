using UnityEngine;
using System.Collections;

public class AimAtPlayer: MonoBehaviour {
	
	GameObject parent;
    
	GameObject playerObject;
	Orbit parentOrbit;
	public bool showLine=false;
	
	public int maxFrames=250;
	int frameCount = 0;
	
	// Use this for initialization
	void Start () {
		//playerObject = GameObject.Find("Player");
		parent=this.GetComponent<AttachGun>().parent;
		parentOrbit=parent.GetComponent<Orbit>();
        
	}
	
	// Update is called once per frame
	void Update () {
		if (playerObject == null) {
			playerObject = GameObject.Find("Player");
			if (playerObject == null) {
				return;
			}
		}
		
		if (!playerObject.activeSelf)
			return;
			
		Vector3 direction = playerObject.transform.position - this.transform.position;
		direction.Normalize();
		

		
		
		if (frameCount == maxFrames) {
			this.GetComponent<Attack>().shoot (direction);
			frameCount = 0;
		
		} else {
			frameCount++;
			
		}
	}
}
