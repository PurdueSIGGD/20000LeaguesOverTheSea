using UnityEngine;
using System.Collections;

public class ChargerShot : MonoBehaviour {

	public int fireTime = 3;
	GameObject playerObject, chargeBeam1, chargeBeam2, chargeShot;
	Vector3 dirToPlayer, dirToFire;
	float rotation;
	int timer = 0;
	
	
	void Start () {
		fireTime = (int)(fireTime / Time.deltaTime);
		playerObject = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(playerObject != null){
			dirToPlayer = playerObject.transform.position - this.transform.position;
			transform.LookAt(playerObject.transform, Vector3.forward);
		}
		else{
			playerObject = GameObject.Find("Player");
		}
	}
		
	
	void FixedUpdate(){
		timer++;
		if(timer >= fireTime){
			//SpawnBeams();
			timer = 0;
			dirToPlayer.Normalize();
			if(this.GetComponent<BigBulletShoot>().getStatus() == false){
				this.GetComponent<BigBulletShoot>().shoot(this.gameObject);
			}
		}
	}
	
	/*void SpawnBeams(){
		chargeBeam1.transform.position = this.transform.position + dirToPlayer.Normalize();
	}*/
}
