using UnityEngine;
using System.Collections;

public class AttachGun : MonoBehaviour {

	public GameObject parent;
	public Vector3 offset= new Vector3(0,0,0);
	// Use this for initialization
	void Start () {
	
		
	}
	
	// Update is called once per frame
	void Update () {
	this.transform.position=parent.transform.position+offset;
		
	}
}
