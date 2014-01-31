using UnityEngine;
using System.Collections;

public class BasicCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
		
		
	}
	
	public virtual void hit(GameObject collider)
	{
		GameObject.DestroyObject(this.gameObject);
	}
	
	public virtual void OnCollisionEnter(Collision coll)
	{
		
	}
}
