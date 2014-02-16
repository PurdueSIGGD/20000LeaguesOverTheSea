using UnityEngine;
using System.Collections;

public class BasicCollision : MonoBehaviour {
	public virtual void hit(GameObject collider)
	{
		GameObject.DestroyObject(this.gameObject);
	}
}
