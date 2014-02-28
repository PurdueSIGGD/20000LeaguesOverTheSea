using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {

	//Custom center of gravity, if set it overrides tagged planets
	public GameObject customGravityAnchor; 
	private GameObject[] gravityAnchors; //Array of Planets, Stars, etc

	//Initial Perpindicular Force 
	public float initialPerpForce = 10;

	//Gravity Force Constant, calculation simplification, estimation G*m*M
	//private float gravityForceConstants = 500;
	// TODO: gravity contant is the mass of the planet

	public bool drawLine = false;

	//Init 
	void Start () {


		//If customGravityAnchor is not null make it the only member of the gravityAnchors
		if (customGravityAnchor != null) {
			gravityAnchors = new GameObject[1];
			gravityAnchors[0] = customGravityAnchor;
		} else {
			//All GameObjects with the tag Planet are added to the array
			gravityAnchors = GameObject.FindGameObjectsWithTag("Planet");
		}
	}



	//Apply a force perpendicular the gravitational anchors
	void applyPerpForce(int force) {
		//Calcuate the center of gravity based on distance and gravity const.
		Vector3 centerOfGravity = new Vector();
		foreach (GameObject gravObj in gravityAnchors) {
			centerOfGravity
		}
	}
}