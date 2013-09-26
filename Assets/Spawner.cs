using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	
	public int spawnTimer=100;
	public float maxSpawnRadius=100;
	public int minSpawnRadius=50;
	int timer, totalWeight;
	float x, y;
	int sign;
	int lenAvail;
	bool complete;
	GameObject pickedEnemy;
	public GameObject[] spawnEnemies;
	protected Vector2 point;
	public int[] weight; //weight: Chance for enemy to spawn
	public int[] delay;  //delay: delay in seconds before inserting enemy in spawn sequence.
	private int[] available;
	int i;
	
	// Use this for initialization
	void Start () {
	complete = false;
	available = new int[spawnEnemies.Length];
		
	timer=spawnTimer;
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	// Spawns enemy after specified time (spawnTimer)
	void FixedUpdate()
	{
		
		if(!complete)
		{
			lenAvail = 0;
			totalWeight=0;
			for(i = 0; i < spawnEnemies.Length; i++)
			{
				if(delay[i] <= Time.time)
				{
					available[lenAvail] = i;
					lenAvail++;
					totalWeight += weight[i];
				}
			}
			
			if(lenAvail == spawnEnemies.Length)
			{
				complete = true;
			}
		}
		
		timer--;
		if (timer <= 0 && lenAvail > 0)
		{
			pickedEnemy = pickSpawn();
			if(pickedEnemy != null)
			{
				sign = Random.Range(0,2);
				x = Random.Range(minSpawnRadius,maxSpawnRadius);
				
				if(sign == 0)
				{
					x *= -1;
				}
				
				sign = Random.Range(0,2);
				
				y = Random.Range(minSpawnRadius,maxSpawnRadius);
				
				if(sign == 0)
				{
					y *= -1;
				}
				
				

				point.Set (x,y);
				GameObject newSpawn = (GameObject)Instantiate(pickedEnemy,point,new Quaternion(0,0,0,0));
				newSpawn.GetComponent<Orbit>().center=this.gameObject;
			}
			
			timer = spawnTimer;
		}
		
	}
	
	// Picks which enemy to spawn based on their weight against a random number
	public GameObject pickSpawn()
	{
		var randomNumber = Random.Range(1,totalWeight+1);
		int k;
		k = 1;
		for(i = 0; i < lenAvail; i++)
		{
			if(randomNumber >= k && randomNumber < weight[available[i]] + k)
			{
				return spawnEnemies[available[i]];
			}
			
			k += weight[available[i]];
		}
		
		return null; //If it gets here then there is an issue
	}
	
}

