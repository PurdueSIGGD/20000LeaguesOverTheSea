using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

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
			//otherCollider.hit (this.gameObject);
			
		}
        else
        {
            
           // GameObject.DestroyObject(coll.gameObject);
			
		}
        GameObject.DestroyObject(this.gameObject);
    }
	
}
