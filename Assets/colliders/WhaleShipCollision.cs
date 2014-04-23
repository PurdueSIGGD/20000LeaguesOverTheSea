using UnityEngine;
using System.Collections;

public class WhaleShipCollision : BasicCollision 
{
	public override void hit(GameObject collider)
	{
		if(collider.tag == "Bullet")
		{
			GameObject.Find("WhaleShip").GetComponent<WhaleShip>().gotHit();
			GameObject.DestroyObject(collider.gameObject);
		}

		if (collider.tag == "Planet") 
		{
			Debug.Log("WhaleShip hit a planet");
			GameObject.Find("WhaleShip").GetComponent<WhaleShip>().decreasePlanetCount();
			GameObject.DestroyObject(collider.gameObject);
		}

		if(collider.tag == "Player")
		{
			GameObject.Destroy(collider.gameObject);
		}
		
	}
}
