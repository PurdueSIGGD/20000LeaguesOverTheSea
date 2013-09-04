using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision coll)
    {
        Collider other = coll.collider;
        if (other.tag == "Ship" || other.tag == "Player")
        {
            //need real death stuff
            GameObject.DestroyObject(coll.gameObject);

        }
        GameObject.DestroyObject(this.gameObject);
    }
}
