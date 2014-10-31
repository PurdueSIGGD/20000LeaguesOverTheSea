using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {


	public GUIStyle style;
	public Vector2 timerPosition = new Vector2(35,10);

	float startTime = 0;
	float highscore = 0;

	//Counting realtime with frames may have problems.
	void Start ()
	{
		startTime = Time.time;

		try {
			//We store highscore in InGameMenu.cs, because we kan.
			highscore = PlayerPrefs.GetFloat ("highscore");
		} catch(PlayerPrefsException ex) {
			Debug.LogError("Could not reterieve highscore");
		}
	}

	
	public void saveHighscore() {
		try {
			//Hopefully we can call this from game over. 
			PlayerPrefs.SetFloat ("highscore", highscore);
		} catch(PlayerPrefsException ex) {
			Debug.LogError("Could not save highscore");
		}
	}


	void OnGUI () 
	{
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(0, new Vector3(0, 0, 0)), 
		                           new Vector3(Screen.width/Menu.scaledR.x, Screen.height/Menu.scaledR.y, 1));
		GUI.BeginGroup(Menu.scale_rect(new Rect(0,0,100,100), Menu.scaledR));

		float gameTime = Time.time - startTime;

		if (highscore <= gameTime) {
			highscore = gameTime;
		}

		GUI.Box(Menu.scale_rect(new Rect(timerPosition.x, timerPosition.y, 10, 10), Menu.scaledR), "Score: " + timeToStr(gameTime), style);
		GUI.Box(Menu.scale_rect(new Rect(timerPosition.x, timerPosition.y + 5, 10, 10), Menu.scaledR), "Highscore: " + timeToStr(highscore), style);
	
		GUI.EndGroup();
	}

	// tim hacks
	string timeToStr(float tim) {
		int minutes = Mathf.RoundToInt (tim) / 60; 
		int seconds = Mathf.RoundToInt (tim) % 60;
		int hundreds = Mathf.RoundToInt (tim * 100) % 100;
		
		return string.Format ("{0}:{1:00}.{2:00}", minutes, seconds, hundreds);
	}
}
