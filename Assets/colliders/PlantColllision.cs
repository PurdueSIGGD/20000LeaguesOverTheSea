using UnityEngine;
using System.Collections;

public class PlantColllision : BasicCollision {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void hit(GameObject collider)
	{
		//lower health of planet if hit by planet lowering health sort of thing (not a bullet)
	}
}
