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

	public float borderForce = 20f;
	public float borderGuardDistance = 5f;

	public float movementSpeed = 5f;

	private static GameObject player;
	public static GameObject getPlayer() {
		return player;
	}

	// Use this for initialization
	void Start () 
	{
		body = this.GetComponent<Rigidbody>();
		attachGun (guns[currentGun]);
		player = gameObject;
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
		if(body.angularVelocity != Vector3.zero)
		{
			body.angularVelocity = Vector3.zero;
			body.velocity = new Vector3 (body.velocity.x, body.velocity.y, 0);
			body.position = new Vector3 (body.position.x, body.position.y, 0);
		}

		//Direction vectors for the player, we use this for moving the player.
		Vector3 direction = body.velocity;
		//Perpendicular vector of the direction. 
		Vector3 perpDirection= new Vector3(-direction.y, direction.x, 0) * -1; 
		//Now we normalize them to unit vectors.
		perpDirection.Normalize();
		direction.Normalize();
		
		
		//Physics done for Player movement here.
		if (up) // && body.velocity.magnitude < new Vector3(10,10,0).magnitude)
		{	
			body.AddForce(direction*(movementSpeed/10),ForceMode.Impulse);
		}
		if (left)
		{
			body.AddForce(perpDirection*-(2*movementSpeed/10),ForceMode.Impulse);
		}
		if (down) // && body.velocity.magnitude > new Vector3(2,2,0).magnitude)
		{
			body.AddForce(direction*-(movementSpeed/10),ForceMode.Impulse);
		}
		if (right)
		{
			body.AddForce(perpDirection*(2*movementSpeed/10),ForceMode.Impulse);
		}

		// rotate ship to look ahead (Work in progress)
		//transform.LookAt(body.velocity + body.position);//new Vector3(body.position.x, body.velocity.y + body.position.y, body.position.z));
		//transform.LookAt(this.GetComponent<Orbit>().getNextPosAndVel(body.position, body.velocity)[0], new Vector3(0,0,-1));
		transform.LookAt (body.velocity + body.position, new Vector3(0,0,-1));
		//transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		//body.transform.Rotate(new Vector3(0, 0, body.velocity.z * 100));

		//Stop player from leaving playing area.
		Rect window = CameraUtility.cameraViewingArea;
		Vector3[] borderGuards = new Vector3[4];
		
		borderGuards[0] = new Vector3(body.position.x, window.yMin); //Top 
		borderGuards[1] = new Vector3(window.xMax, body.position.y); //Right
		borderGuards[2] = new Vector3(body.position.x, window.yMax); //Bottom
		borderGuards[3] = new Vector3(window.xMin, body.position.y); //Left

		foreach (Vector3 v in borderGuards) {
			Vector3 bForce_v = (body.position - v); //Direction
			Vector3 bForce_f = bForce_v.normalized * borderForce / bForce_v.sqrMagnitude;
			if (bForce_v.magnitude < borderGuardDistance * 2) {
				body.AddForce(bForce_f);
				//Debug.Log ("Within 2 * border");
			}

			//If in border range reduce played speed.
			if (bForce_v.magnitude < borderGuardDistance && 
			    body.velocity.magnitude > 10) {
				//Estimate if the player's current momentum will carry it outside the window.
				if (window.Contains(body.position + body.velocity * 2)) {
					//body.velocity *= 0.75f;
				} else {
					body.velocity *= 0.75f;
				}

			}
		}
		if (!CameraUtility.isInCameraFrame(body.position)) {
			//this.GetComponent<PlayerCollision>().hit (null);
		}

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
}
