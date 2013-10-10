using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	
	public int spawnTimer=100;
	public int minSpawnRadius=50;
	public int maxSpawnRadius=100;
	int difference;
	Vector2 direction;
	int timer, totalWeight;
	Vector2 x, y;
	int lenAvail;
	bool complete;
	GameObject pickedEnemy;
	protected Vector2 point;
	int i;
	int[] available;
	public EnemyList[] enemyList;
	
	// Use this for initialization
	void Start () {
		complete = false;
		available = new int[enemyList.Length];
		difference = maxSpawnRadius - minSpawnRadius;
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
			for(i = 0; i < enemyList.Length; i++)
			{
				if(enemyList[i].delay <= Time.time)
				{
					available[lenAvail] = i;
					lenAvail++;
					totalWeight += enemyList[i].weight;
				}
			}
			
			if(lenAvail == enemyList.Length)
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
				point = Random.insideUnitCircle * difference;
				direction = point;
				direction.Normalize();
				direction *= minSpawnRadius;

				point += direction;
				
				//Debug.Log("spawned at: (" + point.x +", " + point.y + ") magnitude: " + point.magnitude + "\n");
					
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
			if(randomNumber >= k && randomNumber < enemyList[available[i]].weight + k)
			{
				return enemyList[available[i]].enemy;
			}
			
			k += enemyList[available[i]].weight;
		}
		
		return null; //If it gets here then there is an issue
	}
	
}

[System.Serializable]
public class EnemyList {

	public GameObject enemy;
	public int weight;
	public int delay;
	
	EnemyList(){
		
	}
}