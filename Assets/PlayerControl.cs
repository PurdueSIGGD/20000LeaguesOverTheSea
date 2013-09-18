using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	Rigidbody body;
	bool up, left, down, right;
	int respawnCountdown;
	GameObject gunPrefab;
	GameObject gun;
	// Use this for initialization
	void Start () {
	body=this.GetComponent<Rigidbody>();
	gunPrefab = (GameObject)Resources.Load("PlayerGun");
	attachGun ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (gun==null)
			attachGun ();
		//input handling can only be done in update, but physics should be applied in fixedUpdate.
		if (Input.GetKeyDown(KeyCode.W))
			up=true;
		
		if (Input.GetKeyUp(KeyCode.W))
			up=false;
		
		if (Input.GetKeyDown(KeyCode.A))
			left=true;
		
		if (Input.GetKeyUp(KeyCode.A))
			left=false;
		
		if (Input.GetKeyDown(KeyCode.S))
			down=true;
		
		if (Input.GetKeyUp(KeyCode.S))
			down=false;
		
		if (Input.GetKeyDown(KeyCode.D))
			right=true;
		
		if (Input.GetKeyUp(KeyCode.D))
			right=false;
		
		
	}
	
	void FixedUpdate()
	{
		
	Vector3 direction = body.velocity;
	Vector3 perpDirection= new Vector3(-direction.y,direction.x,0)*-1;
	perpDirection.Normalize();
	direction.Normalize();
		
		
		if (up)
		{	
			body.AddForce(direction*.1f,ForceMode.Impulse);
		}
			if (left)
		{
			body.AddForce(perpDirection*-.2f,ForceMode.Impulse);
		}
			if (down)
		{
			body.AddForce(direction*-.1f,ForceMode.Impulse);
		}
			if (right)
		{
			body.AddForce(perpDirection*.2f,ForceMode.Impulse);
		}
		
			
		
	}
	
	public void hit()
	{
		resetInput();
		this.gameObject.SetActive(false);

	}
	
	void attachGun()
	{
		gunPrefab.GetComponent<AttachGun>().parent=this.gameObject;
		gun= (GameObject)Instantiate(gunPrefab,this.transform.position,new Quaternion(0,0,0,0));
		
		
	}
	
	void resetInput()
	{
		up=false;
		down=false;
		left=false;
		right=false;
	}
}
