using UnityEngine;
using System.Collections;

public class WhaleShip : MonoBehaviour 
{
	GameObject whaleShip;
	int hitPoints;
	//public Color whaleShipColor;
	public GameObject dropType;
	public static int dropTimerCountdownBase = 1000;
	int dropTimer;
	Vector3 direction;
	GameObject playerObject;
	bool centered = false;
	public AudioClip[] whaleNoises;

	GameObject[] planets;
	int planetCount;

	void Start() 
	{
		whaleShip = this.gameObject;
		playerObject =  GameObject.Find("Player");
		planets = GameObject.FindGameObjectsWithTag("Planet");
		foreach (GameObject planet in planets) 
		{
			Debug.Log ("planet!");
		}
		planetCount = planets.Length - 1;
		dropTimer = dropTimerCountdownBase;
		hitPoints = 50;
		//gameObject.renderer.material = new Material("blank");//Shader.Find("Unlit/Texture")
		//gameObject.renderer.material.shader = null;//receiveShadows = false;
		//gameObject.renderer.material.color = whaleShipColor;
			
		//	Transform trans = whaleShip.GetComponent<Transform>();
		//	trans.Rotate(new Vector3(90, 0, 0));
		//	Vector3 scale = trans.localScale;
		//	Vector3 newScale = new Vector3(scale.x + 9, scale.y + 9, scale.z + 9);
		//	trans.localScale = newScale;
	}
	
	void FixedUpdate() 
	{
		dropTimer--;
		//GameObject center = GameObject.Find("CenterOfGravity");
		//Vector2 whalePoint = rigidbody.position;
		
		if( dropTimer % 50 == 0 && dropTimer > dropTimerCountdownBase / 4 && dropTimer < dropTimerCountdownBase * 3 / 4 ) {
			shoot (3);
		}
		
		if( dropTimer <= 0 ) {
			
			//dropBomb();
			
			dropTimer = dropTimerCountdownBase;
		}
		if (planets [planetCount].GetType is WhaleShip) 
		{
			planetCount--;
			if(planetCount < 0)
				return;
		}
		//Debug.Log (planetCount);
		Vector3 moveDirection = planets[planetCount].transform.position - this.transform.position;
		moveDirection.Normalize ();
		transform.position += moveDirection * .1f;
		//transform.LookAt (playerObject.transform.position, new Vector3(0,0,-1));
		transform.LookAt (planets[planetCount].transform.position, new Vector3(0,0,-1));


		//rigidbody.velocity = playerObject.transform.position;
		//rigidbody.velocity.Normalize ();
		/*Vector2 centerPoint = center.rigidbody.position;
		//float angle = Mathf.Atan2 (whalePoint.y - centerPoint.y, whalePoint.x - centerPoint.x);
		//Vector3 lookAngle = new Vector3(Mathf.Cos (angle), Mathf.Sin (angle), 0 );
		if( (whalePoint - centerPoint).magnitude > 300 ) {
			int minSpawnRadius = center.GetComponent<Spawner>().minSpawnRadius;
			int difference = center.GetComponent<Spawner>().maxSpawnRadius - minSpawnRadius;
				Vector2 point = Random.insideUnitCircle * difference;
				Vector2 direction = point;
				direction.Normalize();
				direction *= minSpawnRadius;

				point += direction;
			
			rigidbody.position = point;
			//Vector3 euler = whaleShip.rigidbody.rotation.eulerAngles;
			//euler.y = angle;
			//whaleShip.rigidbody.rotation.SetLookRotation( lookAngle, new Vector3( 1, 0, 0) );
		}
		rigidbody.transform.LookAt (center.transform, Vector3.back);
		rigidbody.velocity = new Vector3(0, 0, 0);
		//whaleShip.rigidbody.position = center.rigidbody.position;
		*/
	}
	
	public void gotHit()
	{
		int rand = Random.Range(0,1);

		Debug.Log ("Hit!");
		audio.PlayOneShot(whaleNoises[rand]);
		hitPoints--;

		if(hitPoints < 0)
		{
			GameObject.Destroy(GameObject.Find("WhaleShip"));
		}
		
		shoot (5);
	}
	
	void shoot(int numShots){
		bool isOdd = false;
		if(playerObject != null){
				direction = playerObject.transform.position - this.transform.position;
			}
			else{
				direction = GameObject.Find("CenterOfGravity").transform.position - this.transform.position;
			}
		Vector3 perp = new Vector3(-direction.y, direction.x, 0);
		perp.Normalize();
		direction.Normalize();
		if(numShots%2 != 0){
			isOdd = true;
			//this.GetComponent<BasicBulletShoot>().shoot(direction);
			//Debug.Log("Odd Fire!\n");
		}
		Vector3 originalDirection = direction;
		for(int spread = numShots/2; numShots > 0; numShots--, spread--){
			if(spread == 0){
				continue;
			}
			direction = originalDirection;
			direction += perp * spread * .25f;
			direction.Normalize();
			//float tempOffset = this.GetComponent<BasicBulletShoot>().offset;
//			this.GetComponent<BasicBulletShoot>().shoot(direction * 1.25f);
		}
		if(isOdd){
//			this.GetComponent<BasicBulletShoot>().shoot(originalDirection * 1.25f);
			//Debug.Log("Middle Fire!\n");
		}
		direction = originalDirection;
		isOdd = false;
		//StartCoroutine(StupidWaitingMethod());
	}

	public void decreasePlanetCount()
	{
		planetCount--;

		if (planets [planetCount].GetType is WhaleShip) 
		{
			planetCount--;
		}

		if(planetCount < 0)
		{
			//The game is over!

		}
	}


	void OnCollisionEnter(Collision coll)
	{
		Collider other = coll.collider;
		BasicCollision otherCollider= other.gameObject.GetComponent<BasicCollision>();
		//if other object has a collision behavior, hit it in whatever way it wants.
		if (otherCollider!=null)
		{
			//should take into account what type of projectile this is.
			otherCollider.hit (this.gameObject);
			if (other.tag == "Planet") 
			{
				Debug.Log("WhaleShip hit a planet");
				decreasePlanetCount();
				GameObject.DestroyObject(other.gameObject);
			}
			
		}
		else
		{
			
			 //GameObject.DestroyObject(coll.gameObject);
			
		}
		//GameObject.DestroyObject(this.gameObject);
	}
	/*void dropBomb() {
		GameObject center = GameObject.Find("CenterOfGravity");
		Vector2 whalePoint = rigidbody.position;
		float ang = Mathf.Atan2 (center.rigidbody.position.y - whalePoint.y, center.rigidbody.position.x - whalePoint.x);
			
		int spawnDistance = 45;
		Vector2 point = new Vector2(whalePoint.x + Mathf.Cos(ang)*spawnDistance,whalePoint.y + Mathf.Sin(ang)*spawnDistance);
		//GameObject.Instantiate(dropType,,new Quaternion(0,0,0,1));
			
		GameObject newSpawn = (GameObject)Instantiate(dropType,point,new Quaternion(0,0,0,1));
		//
		//newSpawn.GetComponent<Orbit>().center=center;
		newSpawn.renderer.material.color = new Color(1.0f, 0.6f, 0.1f, 0.0f);
	}*/
}
