using UnityEngine;
using System.Collections;

public class Absorber : MonoBehaviour {
	
	GameObject absorber;
	public int time;
	public Color vulnerable, absorbing;
	int timer, numShots, i;
	bool isAbsorbing; //true is absorbing, false is vulnerable
	bool isOdd = false;
	Vector3 direction;
	GameObject playerObject;

	void Start () {
		playerObject =  GameObject.Find("Player");
		absorber = this.gameObject;
		isAbsorbing = true;
//		gameObject.renderer.material.color = absorbing;
		numShots = 0;
		timer = time;
	}
	
	void Update () {
		if(playerObject == null){
			playerObject = GameObject.Find("Player");
		}
	}
	
	void FixedUpdate() {
		timer--;
		if(timer <= 0){
			changeState();
			timer = time;
		}
	}
	
	void changeState(){
		if(isAbsorbing){
			//gameObject.renderer.material.color = vulnerable;
			shoot();
			isAbsorbing = false;
		}
		else{
			//gameObject.renderer.material.color = absorbing;
			isAbsorbing = true;
		}
	}
	
	void shoot(){
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
			this.GetComponent<BasicBulletShoot>().shoot(direction);
		}
		if(isOdd){
			this.GetComponent<BasicBulletShoot>().shoot(originalDirection);
		}
		direction = originalDirection;
		isOdd = false;
		//StartCoroutine(StupidWaitingMethod());
	}
	
	
	/*IEnumerator MachineGunMethod(){
		for(; numShots > 0; numShots--){
			if(playerObject != null){
				direction = playerObject.transform.position - this.transform.position;
			}
			else{
				direction = GameObject.Find("CenterOfGravity").transform.position - this.transform.position;
			}
			direction.Normalize();
			this.GetComponent<BasicBulletShoot>().shoot(direction);
			yield return new WaitForSeconds(.3f);
		}		
	}*/
	
	public void gotHit(){
		if(!isAbsorbing){
			GameObject.Destroy(absorber);
		}
		else {
			numShots++;
		}
	}
}
