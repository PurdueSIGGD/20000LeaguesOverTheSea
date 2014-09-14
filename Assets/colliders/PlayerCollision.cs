using UnityEngine;
using System.Collections;

public class PlayerCollision : BasicCollision
{
	//Collision Handler for player.
	protected override void OnCollisionEnter(Collision coll)
	{
		//Quick way to make you die when you run into the planet. Should probably be standardized.
		Collider other = coll.collider;

		if (!PlayerSpawner.isInvincible || other.tag == "Planet") 
		{
			this.GetComponent<PlayerControl> ().resetInput ();
			this.gameObject.SetActive (false);
		} 
		else 
		{
			GameObject.Destroy(other);
		}
	}
}
