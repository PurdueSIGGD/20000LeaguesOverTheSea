using UnityEngine;
using System.Collections;

public class CameraUtility : MonoBehaviour {

	/// <summary>
	/// Checks if position is in the camera viewing area.
	/// </summary>
	/// <returns>
	/// If position is inside the camera viewing area.
	/// </returns>
	/// <param name='pos'>
	/// Position to check
	/// </param>
	public static bool isInCameraFrame(Vector3 pos) {
		
		//Viewpoint to World Point is used to get the world cordinates of the camera view.
		//0, 0 is bottom left
		//1, 1 is top right
		//0 - Camera.main.transform.position.z is units away from the camera.
		Vector3 topleft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, -Camera.main.transform.position.z));
		Vector3 bottomright = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, -Camera.main.transform.position.z));
		
		//Contruct the current Rect object 
		Rect cameraRec = Rect.MinMaxRect(topleft.x, bottomright.y, bottomright.x, topleft.y);
		
		//Rect has this nifty functions to check if point is in the bounaries.
		return cameraRec.Contains(pos);
	}
	
	/// <summary>
	/// Checks if object is in the camera viewing area.
	/// </summary>
	/// <returns>
	/// If object position is inside the camera viewing area.
	/// </returns>
	/// <param name='obj'>
	/// Object to check
	/// </param>
	public static bool isInCameraFrame(GameObject obj) {
		return isInCameraFrame(obj.transform.position);	
	}
}
