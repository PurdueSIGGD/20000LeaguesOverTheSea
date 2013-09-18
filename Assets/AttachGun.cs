using UnityEngine;
using System.Collections;

public class AttachGun : MonoBehaviour {

	public GameObject parent;
	public Vector3 offset= new Vector3(0,0,0);
	// Use this for initialization
	void Start () {
	
		
	}
	
	// Update is called once per frame
	void Update () {
        if (parent==null)
        { 
            GameObject.Destroy(this.gameObject);
            return;
        }
		if (parent.activeSelf==false)
		{
			this.gameObject.SetActive(false);
		}
		else
		{
			this.gameObject.SetActive(true);
		}
			this.transform.position=parent.transform.position+offset;
		
	}
}
