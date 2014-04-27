using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour {
	public float xspeed, yspeed;
	// Use this for initialization
	void Start () {
	

	}

	// Update is called once per frame
	void FixedUpdate () {

		Rect b=this.GetComponent<GUITexture>().pixelInset;
		b.x=b.x+xspeed;
		b.y=b.y+yspeed;
		this.GetComponent<GUITexture>().pixelInset=b;
	}
}
