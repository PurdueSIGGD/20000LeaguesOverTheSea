using UnityEngine;
using System.Collections;

public class CameraUtility : MonoBehaviour {
	
	//Area you can see on the screen, in world units
	private static Rect _cameraViewingArea;
	public static Rect cameraViewingArea{
		get
		{
			/*
			#Bugged can load too early causing the camera rect to wrong (possibly the previous camera)
			//Updates the rectangle if its equal to default Rect
			if(Object.Equals(_cameraViewingArea, default(Rect)))
			{
				updateCameraRectangle();
				Debug.Log ("Updating Camera Rect");
				
			}*/
			//Temp Fix just update it every time.
			updateCameraRectangle();
			return _cameraViewingArea;
		}
		set{} 
	} 
	
	/// <summary>
	/// Updates the camera rectangle, to the static variable.
	/// Need to call this if we change the location of the camera, etc.
	/// </summary>
	public static void updateCameraRectangle() {
		//Viewpoint to World Point is used to get the world cordinates of the camera view.
		//0, 0 is bottom left
		//1, 1 is top right
		//0 - Camera.main.transform.position.z is units away from the camera.
		Vector3 topleft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, -Camera.main.transform.position.z));
		Vector3 bottomright = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, -Camera.main.transform.position.z));
		
		//Contruct the current Rect object 
		_cameraViewingArea = Rect.MinMaxRect(topleft.x, bottomright.y, bottomright.x, topleft.y);
	}
	
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
		//Rect has this nifty functions to check if point is in the bounaries.
		return cameraViewingArea.Contains(pos);
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
	
	/*/// <summary>
	/// Gets the camera viewing rectangle.
	/// Upates the cameraViewingArea if it hasnt be initialized.
	/// </summary>
	/// <returns>
	/// The camera rectangle, in world units.
	/// </returns>
	public static Rect getCameraRectangle()
	{
		//Updates the rectangle if its equal to default Rect
		if(Object.Equals(cameraViewingArea, default(Rect)))
		{
			updateCameraRectangle();
		}
			
		return cameraViewingArea;	
	}*/
}
