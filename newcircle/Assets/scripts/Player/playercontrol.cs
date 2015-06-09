using UnityEngine;
using System.Collections;

public class playercontrol : MonoBehaviour {

	//determine gameplay location
	public bool inUnity = false;
	public bool inAndroid = false;
	

	//base movement speed
	public float shipmovementspeed = 0;

	//base main speed
	public float shipmainspeed = 0;
	//increase speed
	public float incspeed =0;
	//final speed
	public float finalspeed = 0;
	//default speed
	public float defaultspeed = 0;

	//hor and ver
	public float shiphor = 0;
	public float shipver = 0;

	//rotationspeed
	public float rotatespeed = 0;

	//rotation for left to right
	public float rotateright = 0;
	public float rotateleft = 0;
 
	//rotation for up and down
	public float rotateup =0;
	public float rotatedown = 0;

	//ship body
	public GameObject shipbody;

	//default quaternion
	public Quaternion shipdefaultquat;
	public Quaternion defaultquat;
	public float quatfloat;
 	//mouse work, test code
	public Vector3 mousepos;
	public float screenhor = 0;
	public float screenver =0;
	public float screencenter = 0;
	public float mousecenter = 0;
	public float yleft= 0;
	public float yright =0;
	//test code
	public float androidfloat = 0;
	public float androidrotatefloat = 0;
	public float accystart = 0;
	public float accxstart = 0;
	public Vector3 dir;

	//test code

	// Use this for initialization
	void Start () {

		dir = Vector3.zero;

	
	
		if(Application.platform == RuntimePlatform.WindowsEditor)
		{
			inUnity = true;
		}

		if(Application.platform ==  RuntimePlatform.Android)
		{
			inAndroid = true;
		}

		//get start acceleration
		accystart = Input.acceleration.y;
		accxstart = Input.acceleration.x;


		//

		defaultquat = transform.localRotation;
		shipdefaultquat = shipbody.transform.rotation;
		defaultspeed = shipmainspeed;

		screenhor = Screen.width;
		screenver = Screen.height;
		screencenter = Mathf.Abs(screenhor/2);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//test code
	// 	Vector3 dir = Vector3.zero;
		dir.x = (Input.acceleration.x - accxstart);
		dir.y = (Input.acceleration.y - accystart);
	

		if(dir.sqrMagnitude > 1)
			dir.Normalize();
		//test code

		//main movement forward
		transform.Translate(0,0,shipmainspeed *Time.deltaTime);

		 
		shiphor = Input.GetAxis("Horizontal");
		shipver = Input.GetAxis("Vertical");

		
		mousepos = Input.mousePosition;
		mousecenter = screencenter - mousepos.x;

		if(inAndroid)
		{
			transform.Translate(dir.x *androidfloat *Time.deltaTime , dir.y * androidfloat * Time.deltaTime , 0);
			 transform.Rotate(0,androidrotatefloat *androidfloat * Time.deltaTime,0 );
		}




		if(inUnity)
		{
		 

			//use for when doing remote work
			transform.Translate(dir.x *androidfloat *Time.deltaTime , dir.y * androidfloat * Time.deltaTime , 0);
			transform.Rotate(0,androidrotatefloat *androidfloat * Time.deltaTime,0 );

			print(androidrotatefloat);

			//break in code

//			if(mousepos.y != screencenter)
//			{
//				if(mousecenter>=yright && mousecenter<=yleft)
//				{
//					transform.eulerAngles = new Vector3(transform.eulerAngles.x,mousecenter * Time.deltaTime * rotatespeed *-1,transform.eulerAngles.z);
//				}
//			}

			transform.Translate(shipmovementspeed * Time.deltaTime * shiphor,0,0);

			transform.Translate(Vector3.up * shipver * Time.deltaTime);

//			if(shiphor>0 )
//			{
//				transform.Translate(shipmovementspeed * Time.deltaTime,0,0);
//				transform.Rotate(-Vector3.forward * Time.deltaTime *rotatespeed,Space.Self);
//				if(transform.eulerAngles.z <= rotateright)
//				{
//					 
//
//					transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,rotateright);
//					print (shipbody.transform.eulerAngles.z);
//				}
//
//			}
//			if(shiphor<0 )
//			{
//				transform.Translate(-1 * shipmovementspeed * Time.deltaTime,0,0);
//				transform.Rotate(Vector3.forward * Time.deltaTime *rotatespeed,Space.Self);
//				if(transform.eulerAngles.z >= rotateleft)
//				{
//					 transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,rotateleft);
//					print (shipbody.transform.eulerAngles.z);
//				}
//			}	


			//transform.Rotate((-Vector3.right * shipver) * Time.deltaTime * rotatespeed);

//			if(transform.eulerAngles.x<= rotateup)
//			{
//				transform.eulerAngles = new Vector3(rotateup,transform.eulerAngles.y,transform.eulerAngles.z);
//			}
//			else if(transform.eulerAngles.x>= rotatedown)
//			{
//				transform.eulerAngles = new Vector3(rotatedown,transform.eulerAngles.y,transform.eulerAngles.z);
//			}
//			else
//			{
//				//shipbody.transform.rotation = Quaternion.Lerp(transform.localRotation,shipdefaultquat,Time.deltaTime *quatfloat);
//				transform.localRotation = Quaternion.Lerp(transform.localRotation,defaultquat,Time.deltaTime *quatfloat);
//			}



//			if(shipver>0 )
//			{
//				transform.Rotate(-Vector3.right * Time.deltaTime * rotatespeed);
//				//shipbody.transform.Rotate(Vector3.right * Time.deltaTime * rotatespeed);
//			//	print (shipbody.transform.eulerAngles.x);
//
//				 	print (transform.eulerAngles.x);
//				if(transform.eulerAngles.x<= rotateup)
//				{
//					transform.eulerAngles = new Vector3(rotateup,transform.eulerAngles.y,transform.eulerAngles.z);
//				}
//
//			}
//
//		
//
//			if(shipver<0 )
//			{
//				transform.Rotate(Vector3.right * Time.deltaTime * rotatespeed);
//			//	shipbody.transform.Rotate(-Vector3.right * Time.deltaTime * rotatespeed);
//			//	print (shipbody.transform.eulerAngles.x);
//
//				print (transform.eulerAngles.x);
//				if(transform.eulerAngles.x>= rotatedown)
//				{
//					transform.eulerAngles = new Vector3(rotatedown,transform.eulerAngles.y,transform.eulerAngles.z);
//				}
//			}
//			else
//			{
//				//shipbody.transform.rotation = Quaternion.Lerp(transform.localRotation,shipdefaultquat,Time.deltaTime *quatfloat);
//				transform.localRotation = Quaternion.Lerp(transform.localRotation,defaultquat,Time.deltaTime *quatfloat);
//			}

		}

	}
}
