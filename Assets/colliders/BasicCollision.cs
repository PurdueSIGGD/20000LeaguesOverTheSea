using UnityEngine;
using System.Collections;

public class BasicCollision : MonoBehaviour 
{
	protected virtual void OnCollisionEnter(Collision coll)
	{
		Collider other = coll.collider;
		GameObject.DestroyObject(this.gameObject);
	}
}
