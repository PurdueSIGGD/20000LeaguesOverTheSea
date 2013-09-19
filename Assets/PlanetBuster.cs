using UnityEngine;
using System.Collections;

public class PlanetBuster : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	    void OnCollisionEnter(Collision coll)
    {
        Collider other = coll.collider;
        if (other.tag == "Ship")
        {
            //need real death stuff
            GameObject.DestroyObject(coll.gameObject);

        }
		if (other.tag=="Player")
		{
			other.gameObject.GetComponent<PlayerControl>().hit ();
		}
		
		if (other.tag== "Planet")
		{
			//should do damage to planet
		}
        GameObject.DestroyObject(this.gameObject);
    }
	
}
