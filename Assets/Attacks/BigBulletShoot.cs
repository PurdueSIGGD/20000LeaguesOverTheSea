using UnityEngine;
using System.Collections;

public class BigBulletShoot : MonoBehaviour
{

	public GameObject bullet = (GameObject)Resources.Load("BigSpaceBullet");
	public float shotVelocity=30;
	//public float offset=5;
	public int bulletLife=500;
	public int chargeTime=5;
	public float radius = 3;
	
	GameObject newBullet, parent, playerObject;
	float radiusChange, offset;
	int i;	//not if
	Vector3 updateDir;
	bool isCharging = false;
	Vector3 initialVelocity, direction;
	
	void Start () 
	{
		radiusChange = (radius - 1) / (chargeTime * 10);
		offset = radius + 2;
		bulletLife += (int) (chargeTime / Time.deltaTime);
	}
	
	void Update () 
	{
		if(playerObject == null)
		{
			playerObject = GameObject.Find("Player");
		}
	}
	
	void FixedUpdate()
	{
		if(isCharging == true && newBullet != null && playerObject != null)
		{
			updateDir = playerObject.transform.position - parent.transform.position;
			updateDir.Normalize();
			newBullet.transform.position = parent.transform.position + (offset * updateDir);
		}
	}
	
	
	public bool shoot (GameObject newParent) 
	{
			parent = newParent;
			Vector3 initialPosition = this.transform.position + direction * offset;
			
			newBullet = (GameObject)Instantiate(bullet,initialPosition,new Quaternion(0,0,0,0));
			isCharging = true;
		
			newBullet.GetComponent<Bullet>().maxLife=bulletLife;
        	//newBullet.GetComponent<Orbit>().center = this.gameObject.GetComponent<Orbit>().center;
			newBullet.GetComponent<Orbit>().initialPerpForce = 0;
			
			StartCoroutine(waitingMethod());

			//this function never fails to shoot when called.
			return true;
	}
	
	IEnumerator waitingMethod()
	{
		for(i = 0; i < chargeTime * 10; i++)
		{
			yield return new WaitForSeconds(.1f);
			if(newBullet != null)
			{
				newBullet.transform.localScale += new Vector3(radiusChange, radiusChange, radiusChange);
			}
		}
		if(GameObject.Find("Player") != null)
		{
			initialVelocity = updateDir * shotVelocity + playerObject.rigidbody.velocity;
		}
		else
		{
			initialVelocity = updateDir * shotVelocity + GameObject.Find("CenterOfGravity").rigidbody.velocity;
		}

		isCharging = false;
		if(newBullet != null)
		{
	        newBullet.rigidbody.AddForce(initialVelocity, ForceMode.VelocityChange);
		}
	}

	public bool getStatus()
	{
		return isCharging;
	}

	public void gotHit()
	{
		GameObject.DestroyObject(newBullet);
		GameObject.DestroyObject(this.gameObject);
	}
}
