using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {
	public GameObject player;
	public int respawnTimer=120;
	public bool respawnPenalty = false;
	public int respawnPenaltyTime = 60;
	int respawnCountdown=-1;
	
	public int spawnInvincibilityInitial = 120;
	int spawnInvincibilityCounter;
	public static bool isInvincible;
	
	Vector3 initialPos;
	Vector3 initialVel;
	Quaternion initialRot;
	
	// Use this for initialization
	void Start () {		
		initialPos=new Vector3(player.transform.position.x, player.transform.position.y, 0);
		initialVel=new Vector3(0,0,0); //player.rigidbody.velocity;
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
			player.rigidbody.angularVelocity = Vector3.zero;
			
			player.SetActive(true);

			player.GetComponent<Orbit>().applyPerpForce();
			spawnInvincibilityCounter = spawnInvincibilityInitial;
			isInvincible = true;
			respawnCountdown--;
		}
		
		// Decrement the spawn invincibility variable until it becomes 0
		if(isInvincible && spawnInvincibilityCounter > 0)
		{
			spawnInvincibilityCounter--;
			// Check to see if invincibility is over
			if(spawnInvincibilityCounter == 0)
			{
				isInvincible = false;	
				player.rigidbody.angularVelocity = Vector3.zero;
			}
		}
	}
	
	// Expose the style of the gui text to the editor to allow font changes
	public GUIStyle style;
	public Vector2 respawnTimerPosition = new Vector2(35,20);
	
	void OnGUI () 
	{
		if(respawnCountdown <= respawnTimer && respawnCountdown > -1)
		{
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(0, new Vector3(0, 0, 0)), 
			                           new Vector3(Screen.width/Menu.scaledR.x, Screen.height/Menu.scaledR.y, 1));
			GUI.BeginGroup(Menu.scale_rect(new Rect(0,0,100,100), Menu.scaledR));
			// Draw the respawn countdown in the center of the screen. Magic numbers make everything look better 
			GUI.Box(Menu.scale_rect(new Rect(respawnTimerPosition.x, respawnTimerPosition.y,10,10), Menu.scaledR), (1 + respawnCountdown/60).ToString(), style);
			GUI.EndGroup();
		}
	}

}
