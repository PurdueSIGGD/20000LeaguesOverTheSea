using UnityEngine;
using System.Collections;

public class RandomizePlanetMaterial : MonoBehaviour {

	string[] materials = { "Materials/CherryMaterial",
						   "Materials/OrangeMaterial",
						   "Materials/WatermelonMaterial",
						   "Materials/MangoMaterial",
						   "Materials/Guac" };


	// Use this for initialization
	void Start () 
	{
		int matNum = Random.Range (0, 5);
		gameObject.renderer.material = Resources.Load(materials[matNum], typeof(Material)) as Material;
	}
}
