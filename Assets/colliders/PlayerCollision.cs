using UnityEngine;
using System.Collections;

public class PlayerCollision : BasicCollision{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void hit(GameObject collider)
	{
		this.GetComponent<PlayerControl>().resetInput();
		this.gameObject.SetActive(false);
	}
}
