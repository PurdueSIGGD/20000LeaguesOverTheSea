using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	Rigidbody body;
	bool up, left, down, right; //Movement Booleans
	int respawnCountdown;
	GameObject gun; // Players Current Gun Object
	
	
	public GameObject[] guns; //Array of Guns available to the player.
	public int currentGun = 0; //Current Gun in guns
	public bool drawGunLine = true; 
	
	// Use this for initialization
	void Start () 
	{
		body = this.GetComponent<Rigidbody>();
		attachGun (guns[currentGun]);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Switch Weapons Code (Perhaps could be consolidated?)
		//Q current weapon backwards 1 in array
		if (Input.GetKeyDown(KeyCode.Q))
		{
			if (gun.GetComponent<BaseWeapon>().unequip())
			{
				currentGun = (currentGun - 1) % guns.Length;
				if (currentGun < 0) 
					currentGun = guns.Length - 1;
				attachGun(guns[currentGun]);
			}
		}
		//E current weapon forwards 1 in array
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (gun.GetComponent<BaseWeapon>().unequip())
			{
				currentGun = (currentGun + 1) % guns.Length;
				attachGun(guns[currentGun]);
			}
		}
		
		// If the player is off the screen reverse the velocity.
		// Time.frameCount % 10 is used the only check every 10 frames
		// CameraUtility.isInCameraFrame returns true if it is in the camera view port.
		// TODO: Make perhaps a spring type function where slow the player, rather than bounce.
		if(Time.frameCount % 10 == 0 && !CameraUtility.isInCameraFrame(this.gameObject)) 
		{
			this.gameObject.transform.rigidbody.velocity *= -1; //Invert the Velocity.	
		}
		 
		//Attach gun if there isnt one.. is this possible?
		if (gun == null)
			attachGun (guns[currentGun]);
		
		//Input handling can only be done in update,  
		// Physics should be applied in fixedUpdate.
		//Input.GetKey returns true or false if key is down or up. (Was there a problem with this before?)
		up = Input.GetKey(KeyCode.W);
		left = Input.GetKey(KeyCode.A);
		down = Input.GetKey(KeyCode.S);
		right = Input.GetKey(KeyCode.D);
		
	
		if (drawGunLine)
		{
			gun.GetComponent<BaseWeapon>().drawLine();
		}
		
	}
	
	//Fixed Update, we do physics here.
	void FixedUpdate()
	{
		//Direction vectors for the player, we use this for moving the player.
		Vector3 direction = body.velocity;
		//Perpendicular vector of the direction. 
		Vector3 perpDirection= new Vector3(-direction.y, direction.x, 0) * -1; 
		//Now we normalize them to unit vectors.
		perpDirection.Normalize();
		direction.Normalize();
		
		
		//Physics done for Player movement here.
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

		// rotate ship to look ahead (Work in progress)
		//transform.LookAt(body.velocity + body.position);//new Vector3(body.position.x, body.velocity.y + body.position.y, body.position.z));
		transform.LookAt(this.GetComponent<Orbit>().getNextPosAndVel(body.position, body.velocity)[0], new Vector3(0,0,-1));
		//body.transform.Rotate(new Vector3(0, 0, body.velocity.z * 100));
	}
	
	//Attach this gun to the player.
	void attachGun(GameObject gunPrefab)
	{
		if (gun != null)
		{
			gun.GetComponent<BaseWeapon>().destroy();
		}
		gunPrefab.GetComponent<BaseWeapon>().parent = this.gameObject;
		gun = (GameObject)Instantiate(gunPrefab, this.transform.position, new Quaternion(0,0,0,0));
	}
	
	//Reset the Input
	public void resetInput()
	{
		up = false;
		down = false;
		left = false;
		right = false;
	}
	
	//Collision Handler for player.
	void OnCollisionEnter(Collision coll)
	{
		//Quick way to make you die when you run into the planet. Should probably be standardized.
		Collider other = coll.collider;
		if (other.tag=="Planet")
		{
			this.GetComponent<PlayerCollision>().hit (other.gameObject);
		}	
	}
}
