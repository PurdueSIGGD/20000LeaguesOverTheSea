using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour {

	public Color greenHealth;
	public Color yellowHealth;
	public Color redHealth;


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

		GL.Begin(GL.QUADS);
		GL.Color(greenHealth);
		GL.Vertex3(radius * 1.1f, 0, 0);
		GL.Vertex3(radius * 1.50f, 0, 0);
		GL.Vertex3(radius * 1.5f * Mathf.Cos(Mathf.Deg2Rad * 15), radius * 1.5f * Mathf.Sin(Mathf.Deg2Rad * 15), 0);
		GL.Vertex3(radius * 1.1f * Mathf.Cos(Mathf.Deg2Rad * 15), radius * 1.1f * Mathf.Sin(Mathf.Deg2Rad * 15), 0);
		GL.End();
	}

	void OnPostRender() {
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
