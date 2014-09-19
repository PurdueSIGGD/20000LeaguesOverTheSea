using UnityEngine;
using System.Collections;

public class RandomizeBackgroundTexture : MonoBehaviour {

	string[] backgrounds = { "Backgrounds/Background Concept 1",
						     "Backgrounds/Background Concept 2",
						     "Backgrounds/Background Concept 3",
						     "Backgrounds/Background Concept 4",
						     "Backgrounds/Background Concept 5",
						     "Backgrounds/Background Concept 6" };
	
	
	// Use this for initialization
	void Start () 
	{
		int backNum = Random.Range (0, 6);
		gameObject.guiTexture.texture = Resources.Load(backgrounds[backNum], typeof(Texture)) as Texture;
	}
}
