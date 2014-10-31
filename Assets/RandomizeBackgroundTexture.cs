using UnityEngine;
using System.Collections;

public class RandomizeBackgroundTexture : MonoBehaviour {

	string[] backgrounds = { "Backgrounds/Background Concept 1",
						     "Backgrounds/Background Concept 2",
						     "Backgrounds/Background Concept 3",
						      };
	
	
	// Use this for initialization
	void Start () 
	{
		int backNum = Random.Range (0, 3);
		gameObject.guiTexture.texture = Resources.Load(backgrounds[backNum], typeof(Texture)) as Texture;
	}
}
