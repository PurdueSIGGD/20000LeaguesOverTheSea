using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	int counter = 0;
	Rigidbody body;
	bool up, left, down, right;
	int respawnCountdown;
	GameObject gun;
	public float maxx=110;
	public float maxy=65;
	// Use this for initialization
	public GameObject[] guns;
	public int currentGun=0;
	public bool drawGunLine=true;
	void Start () {
	body=this.GetComponent<Rigidbody>();
	attachGun (guns[currentGun]);
	}
	
	// Update is called once per frame
	void Update () {

		
		if (Input.GetKeyDown(KeyCode.Q))
		{
			if (gun.GetComponent<BaseWeapon>().unequip())
			{
			currentGun=(currentGun+1)%guns.Length;
			attachGun (guns[currentGun]);
			}
			
			
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (gun.GetComponent<BaseWeapon>().unequip())
			{
			currentGun=(currentGun+1)%guns.Length;
			attachGun (guns[currentGun]);
			}
		}
		
		if(counter == 0)
		{
			if (Mathf.Abs(this.gameObject.transform.position.x)>maxx || Mathf.Abs(this.gameObject.transform.position.y)>maxy)
				this.GetComponent<Rigidbody>().velocity*=-1;
			counter++;
		}
		else
		{
			if(counter < 10)
				counter++;
			else
				counter = 0;
		}
		if (gun==null)
			attachGun (guns[currentGun]);
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
		
		
				
		if (drawGunLine)
		{
			gun.GetComponent<BaseWeapon>().drawLine();
		}
		
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
	
	
	
	
	void attachGun(GameObject gunPrefab)
	{
		if (gun!=null)
		{
			gun.GetComponent<BaseWeapon>().destroy();
		}
		gunPrefab.GetComponent<BaseWeapon>().parent=this.gameObject;
		gun= (GameObject)Instantiate(gunPrefab,this.transform.position,new Quaternion(0,0,0,0));
		
		
	}
	
	public void resetInput()
	{
		up=false;
		down=false;
		left=false;
		right=false;
	}
	
	void OnCollisionEnter(Collision coll)
	{
		
		//quick way to make you die when you run into the planet. Should probably be standardized.
		Collider other = coll.collider;
		if (other.tag=="Planet")
		{
			this.GetComponent<PlayerCollision>().hit (other.gameObject);
		}
		
	}
}
