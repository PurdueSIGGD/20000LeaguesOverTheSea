using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	
	
	public GameObject spawn;
	public int spawnTimer=100;
	public float spawnRadius=100;
	public int minSpawnRadius=50;
	int timer;
	// Use this for initialization
	void Start () {
	timer=spawnTimer;
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void FixedUpdate()
	{
		
	
	timer--;
	if (timer<=0)
		{
	Vector2 point = Random.insideUnitCircle* spawnRadius;
		
	while (point.magnitude<minSpawnRadius)
		{
			point = Random.insideUnitCircle* spawnRadius;
		}
	GameObject newSpawn = (GameObject)Instantiate(spawn,point,new Quaternion(0,0,0,0));
	newSpawn.GetComponent<Orbit>().center=this.gameObject;

	timer=spawnTimer;
		}
		
	}
}
