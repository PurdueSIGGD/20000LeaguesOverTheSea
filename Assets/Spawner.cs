using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	
	public int spawnTimer=100;
	public float spawnRadius=100;
	public int minSpawnRadius=50;
	int timer;
	GameObject pickedEnemy;
	public GameObject[] spawnEnemies;
	public int[] percentChance; //Chance for enemy to spawn
	
	// Use this for initialization
	void Start () {
	timer=spawnTimer;
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	// Spawns enemy after specified time (spawnTimer)
	void FixedUpdate()
	{
		timer--;
		if (timer<=0)
		{
			pickedEnemy = pickSpawn();
			if(pickedEnemy != null){
				Vector2 point = Random.insideUnitCircle* spawnRadius;

				while (point.magnitude<minSpawnRadius)
				{
					point = Random.insideUnitCircle* spawnRadius;
				}
				
				GameObject newSpawn = (GameObject)Instantiate(pickedEnemy,point,new Quaternion(0,0,0,0));
				newSpawn.GetComponent<Orbit>().center=this.gameObject;
			}
			
			timer = spawnTimer;
		}
		
	}
	
	// Picks which enemy to spawn based on % chance and random number
	public GameObject pickSpawn(){
		var randomNumber = Random.Range(1,101);
		int i, k;
		k = 1;
		for(i = 0; i < spawnEnemies.Length; i++){
			if(randomNumber > k && randomNumber < percentChance[i] + k){
				return spawnEnemies[i];
			}
			k += percentChance[i];
		}
		return null;
	}
	
}
