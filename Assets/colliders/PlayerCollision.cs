using UnityEngine;
using System.Collections;

public class PlayerCollision : BasicCollision{
	public override void hit(GameObject collider)
	{
		if(!PlayerSpawner.isInvincible || collider.tag == "Planet")
		{
			this.GetComponent<PlayerControl>().resetInput();
			this.gameObject.SetActive(false);
		}
	}
}
