using UnityEngine;
using System.Collections;

public class SkyBoxMenu : MonoBehaviour {
	
	GameObject skyBox; //Skybox of the menu.
	Vector3 delta_rot; //Rotation of the skybox each updated.
	
	// Use this for initialization
	void Start () {
		Random rnd = new Random();
		
		// Random.value returns a value between 0 and 1.0
		// We take that and subtract 0.5 so the value is between -0.5 and 0.5
		// and then divide that result by 10 get the wanted range of -0.05 to 0.05
		delta_rot = new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f) / 10;
		
		skyBox = this.gameObject; //Get the gameObject of this instance.
	}
	
	// Update is called once per frame
	void Update () {
		skyBox.transform.Rotate(delta_rot); //Apply Skybox Rotation.
	}
}
