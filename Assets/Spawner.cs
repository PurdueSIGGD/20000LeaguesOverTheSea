using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	
	public int spawnTimer=100;
	public int minSpawnRadius=300;
	public int maxSpawnRadius=500;
	public int minWaveSize=5;
	public int maxWaveSize=10;

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
	bool[] bossesSpawned;
	public EnemyList[] enemyList;
	
	// Use this for initialization
	void Start () {
		complete = false;
		available = new int[enemyList.Length];
		bossesSpawned = new bool[enemyList.Length];
		difference = maxSpawnRadius - minSpawnRadius;
		timer=1;
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
			spawnWave();
			timer = spawnTimer;
			/*
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
				//newSpawn.GetComponent<Orbit>().center=this.gameObject;
			}
			
			timer = spawnTimer;*/
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
				if( !enemyList[available[i]].isBossEnemy || !bossesSpawned[available[i]] ) {
					bossesSpawned[available[i]] = true;
					return enemyList[available[i]].enemy;
				}
			}
			
			k += enemyList[available[i]].weight;
		}
		
		return null; //If it gets here then there is an issue
	}

	public bool spawnWave()
	{
		//which quadrant
		int circy =(int) Random.Range(-1,1);
		int circx=(int) Random.Range(-1,1);
		int max=(int)Random.Range (minWaveSize,maxWaveSize+1);
		int spawned=0;
		while (spawned<max)
		{
			Vector2 newPos=Random.insideUnitCircle;
			//trim to right quadrant, probably a better way to do this.
			if((circx==0 && newPos.x<0) || (circx==-1 && newPos.x>0))
				continue;
			if((circy==0 && newPos.y<0) || (circy==-1 && newPos.y>0))
				continue;
			Vector2 direction=newPos.normalized;

		
			direction*=minSpawnRadius;

			int offset=Random.Range (0,maxSpawnRadius-minSpawnRadius);
			newPos*=offset;
			newPos=newPos+direction;
			pickedEnemy=pickSpawn ();
			//if enemy list empty
			if (pickedEnemy==null)
				return false;
			GameObject newSpawn = (GameObject)Instantiate(pickedEnemy,newPos,new Quaternion(0,0,0,0));
			newSpawn.GetComponent<Orbit>().preferredOrbit=50+offset/3;
			spawned++;

		}
		return true;
	}
	
}

[System.Serializable]
public class EnemyList {

	public GameObject enemy;
	public int weight;
	public int delay;
	public bool isBossEnemy;
	
	EnemyList(){
		
	}
}