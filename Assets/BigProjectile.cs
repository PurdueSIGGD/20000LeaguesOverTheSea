using UnityEngine;
using System.Collections;
using System;

// This exists so a projectile can be hit multiple times before dying

public class BigProjectile : MonoBehaviour {
	int i = 2;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision coll)
    {
        Collider other = coll.collider;
		BasicCollision otherCollider= other.gameObject.GetComponent<BasicCollision>();
		//if other object has a collision behavior, hit it in whatever way it wants.
		if (otherCollider!=null)
		{
			//should take into account what type of projectile this is.
			otherCollider.hit (this.gameObject);
			
		}
        else
        {
            
           // GameObject.DestroyObject(coll.gameObject);
			
		}
		

		i--;
		if(String.Compare(coll.gameObject.ToString(), this.gameObject.ToString(), false)==0){
			GameObject.DestroyObject(this.gameObject);
		}
		if(i == 0 || coll.gameObject.tag != "Bullet") {	//Evan's amazing work, you can get autographs on monday wednesday and saturday
			GameObject.DestroyObject(this.gameObject);
		}
    } 
}
