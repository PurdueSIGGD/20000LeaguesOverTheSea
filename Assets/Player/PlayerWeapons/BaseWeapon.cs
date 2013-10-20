using UnityEngine;
using System.Collections;

public class BaseWeapon : MonoBehaviour {

	public GameObject parent;
	public Vector3 offset= new Vector3(0,0,0);
	
	// Use this for initialization
	 void Start () {
	
	}
	
	// Update is called once per frame
	 void Update () {
	//destroy guns if player dies.
	if (parent==null || parent.activeSelf==false)
        { 
            GameObject.Destroy(this.gameObject);
            return;
        }
			this.transform.position=parent.transform.position+offset;
		
		
	}
	
	public virtual bool unequip()
	{
		//called before weapon is changed.
		return true;
	}
	
	public void destroy()
	{
		GameObject.Destroy(this.gameObject);
	}
	
	public virtual void drawLine()
	{
		
	}
	
	public Vector3 getMouseDirection()
	{
		Ray MousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distanceToXYPlane=-MousePosition.origin.z/MousePosition.direction.z;
		Vector3 screenPosition= MousePosition.GetPoint(distanceToXYPlane);
		Vector3 direction= screenPosition-this.transform.position;
		direction.Normalize();
		return direction;
	}
	
	public Vector3 getMousePosition()
	{
		Ray MousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distanceToXYPlane=-MousePosition.origin.z/MousePosition.direction.z;
		return MousePosition.GetPoint(distanceToXYPlane);
	}
}
