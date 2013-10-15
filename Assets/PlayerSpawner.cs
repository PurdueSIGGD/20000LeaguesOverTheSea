﻿using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {
	public GameObject player;
	public int respawnTimer=120;
	public bool respawnPenalty = false;
	public int respawnPenaltyTime = 60;
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
	void Update () 
	{
		if (respawnCountdown==-1 && player.activeSelf==false)
		{
			respawnCountdown=respawnTimer;
			respawnTimer += respawnPenaltyTime;
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
	
	// Expose the style of the gui text to the editor to allow font changes
	public GUIStyle custom;
	
	void OnGUI () 
	{
		if(respawnCountdown <= respawnTimer && respawnCountdown > -1)
		{
			// Draw the respawn countdown in the center of the screen. Magic numbers make everything look better 
			GUI.Box(Rect.MinMaxRect(Screen.width/2 - 4,Screen.height/2 - 5,Screen.width/2,Screen.height/2), (1 + respawnCountdown/60).ToString(), custom);
		}
	}

}
