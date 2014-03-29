using UnityEngine;
using System.Collections;

public class PlanetRotate : MonoBehaviour 
{
	public float speed = 1;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//speed = speed + 20;
		//planet.renderer.material.SetTextureOffset("CherryMaterial", new Vector2(speed,0));
		transform.Rotate(0, speed, 0, Space.World);
	}
}
