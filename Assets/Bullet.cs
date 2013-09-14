using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	int lifeSpan = 500;
		
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		lifeSpan--;
		if (lifeSpan == 0)
			GameObject.DestroyObject(this.gameObject);
	}

    void OnCollisionEnter(Collision coll)
    {
        Collider other = coll.collider;
				
        if (other.tag == "Ship" || other.tag == "Player" || other.tag == "Enemy")
        {
            //need real death stuff
            GameObject.DestroyObject(coll.gameObject);
			
		}
        GameObject.DestroyObject(this.gameObject);
    }
}
