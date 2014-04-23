using UnityEngine;
using System.Collections;

public class DrawWeaponSelect : MonoBehaviour {

	public Texture[] gunTexturesOff;
	public Texture[] gunTexturesOn;
	private PlayerControl control;

	public Rect position = new Rect(0,0,10,10);
	public Vector2 offset = new Vector2(10, 0);
	public GUIStyle border;

	// Use this for initialization
	void Start () {
		control = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		if (control.guns.Length != gunTexturesOff.Length) {
			Debug.LogError("gunTextures mismatch length with guns length");
		}
		if (gunTexturesOff.Length != gunTexturesOn.Length) {
			Debug.LogError("gunTexturesOff mismatch length with gunTexturesOn");
		}
	}

	void OnGUI() {
		//Build texture list.
		Texture[] textures = (Texture[]) gunTexturesOff.Clone();
		textures[control.currentGun] = gunTexturesOn[control.currentGun];

		//Draw Icons in Texture list.
		Rect r = position; //Temp postion
		for (int i = 0; i < textures.Length; i++) {
			//Strange offset procedure..
			r.x += (offset.x != 0) ? (r.width + offset.x) * i : 0;
			r.y += (offset.y != 0) ? (r.width + offset.y) * i : 0;

			GUI.DrawTexture(r, textures[i], ScaleMode.ScaleToFit);
		}
	}
}
