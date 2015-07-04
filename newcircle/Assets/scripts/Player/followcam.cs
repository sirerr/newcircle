using UnityEngine;
using System.Collections;

public class followcam : MonoBehaviour {

	//follow player object
	public GameObject playerobj;

	//distance between camera and player
	public float camdist = 0;

	//acceptable distance
	public float okaydistance = 0;

	//cam speed
	public float camspeed = 0;

	public Quaternion defaultcamquat;

	//height of the camera
	public float playerheight;

	//test code
	public Vector3 anchor;
	//testcode




	// Use this for initialization
	void Start () {


		anchor = transform.position - playerobj.transform.position;
	}

	void FixedUpdate () {
		//transform.rotation = playerobj.transform.rotation;

transform.position = Vector3.MoveTowards (transform.position, anchor+playerobj.transform.position, 100*Time.deltaTime);	
		transform.LookAt(playerobj.transform.position);

	//	transform.rotation = new Quaternion (transform.rotation.x,playerobj.transform.rotation.y,transform.rotation.z,0);
	}

//
//	void LateUpdate () {
//	
//
//		float step = camspeed * Time.deltaTime;
//		camdist = Vector3.Distance(playerobj.transform.position, transform.position);
//
//		if(camdist>=okaydistance)
//		{
//		//	transform.position= Vector3.MoveTowards(transform.position,playerobj.transform.position ,step);
//			transform.position = playerobj.transform.position;
//		}
//
//		transform.LookAt(playerobj.transform);
//	}


}
