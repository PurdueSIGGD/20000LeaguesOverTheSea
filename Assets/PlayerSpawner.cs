using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {
	public GameObject player;
	public int respawnTimer=120;
	int respawnCountdown=-1;
	Vector3 initialPos;
	Vector3 initialVel;
	Quaternion initialRot;
	// Use this for initialization
	void Start () {
		initialPos=player.transform.position;
		initialVel=player.rigidbody.velocity;
		initialRot=player.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (respawnCountdown==-1 && player.activeSelf==false)
		{
			respawnCountdown=respawnTimer;
		}
		if (respawnCountdown != -1)
		{
			respawnCountdown--;
			
		}
		
		if (respawnCountdown==0)
		{
			
			//there's probably a better way to do this.
			player.transform.position=initialPos;
			player.rigidbody.velocity=initialVel;
			player.transform.rotation=initialRot;
			
			player.SetActive(true);
			player.GetComponent<Orbit>().givePerpBoost(player.GetComponent<Orbit>().initialForce);
			respawnCountdown--;
		}
		
		
	}
}
