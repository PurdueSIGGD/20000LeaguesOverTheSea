using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour {

	public Color greenHealth;
	public Color yellowHealth;
	public Color redHealth;
	public float distanceFromPlanet = 1.1f;
	public float size = 0.4f;
	public int segments;


	//  Right now we just use the escape key to go back to the menu
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel("Menu");	
		}
	}

	//Exmaple given line material.
	static Material lineMaterial;
	static void CreateLineMaterial() {
		if( !lineMaterial ) {
			lineMaterial = new Material( "Shader \"Lines/Colored Blended\" {" +
			                            "SubShader { Pass { " +
			                            "    Blend SrcAlpha OneMinusSrcAlpha " +
			                            "    ZWrite Off Cull Off Fog { Mode Off } " +
			                            "    BindChannels {" +
			                            "      Bind \"vertex\", vertex Bind \"color\", color }" +
			                            "} } }" );
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
		}
	}

	void drawPlanetHealth(GameObject planet) {
		float radius = planet.rigidbody.collider.bounds.extents.x;

		if (segments <= 0) {
			Debug.Log ("Planet Health: Number of segments for drawing less than zero.");
			return;
		}

		int health = planet.GetComponent<PlanetCollision>().health;
		Color color;
		if (health > 50) {
			color = greenHealth; 
		} else if (health <= 50 && health > 25) {
			color = yellowHealth;
		} else {
			color = redHealth;
		}	

		//Build Circle
		Vector3 builder;
		for(float i = 0; i < 360 * (health / 100f) - 1; i += 360 / segments) {
			//Build Segments little messy.
			GL.Begin(GL.QUADS);
			GL.Color(color);
		 
			builder = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (90 + i)), Mathf.Sin(Mathf.Deg2Rad * (90 + i)), 0);
			builder *= radius * distanceFromPlanet;
			GL.Vertex(builder);

			builder = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (90 + i + 360 / segments)), Mathf.Sin(Mathf.Deg2Rad * (90 + i + 360 / segments)), 0);
			builder *= radius * distanceFromPlanet;
			GL.Vertex(builder);

			builder = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (90 + i + 360 / segments)), Mathf.Sin(Mathf.Deg2Rad * (90 + i + 360 / segments)), 0);
			builder *= radius * distanceFromPlanet + radius * size;
			GL.Vertex(builder);

			builder = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (90 + i)), Mathf.Sin(Mathf.Deg2Rad * (90 + i)), 0);
			builder *= radius * distanceFromPlanet + radius * size;
			GL.Vertex(builder);

			GL.End();
		}
	}

	void OnPreRender() {
		CreateLineMaterial();
		// set the current material
		GL.PushMatrix();
		lineMaterial.SetPass(0);
		foreach (GameObject g in PlayerControl.getPlayer().GetComponents<Orbit>()[0].getPlanets()) {
			drawPlanetHealth(g);
		}
		GL.MultMatrix (transform.localToWorldMatrix);
		GL.PopMatrix();
	}
}
