using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public virtual bool shoot(Vector3 direction)
	{
		//in overriding class, put shoot logic in here.
		return false;
	}
	
}
