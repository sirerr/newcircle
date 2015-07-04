using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

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
	public float shiprotatespeed = 0;

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
	public float defaultquatx =0;
	public float defaultquatz =0;
 	//mouse work, test code
	public Vector3 mousepos;
	public float screenhor = 0;
	public float screenver =0;
	public float screencenter = 0;
	public float screencentery = 0;
	public float mousecenter = 0;
	public float mousecenterx = 0;
	public float yleft= 0;
	public float yright =0;
	//test code
 
	//accelerometer code
	public float androidrotatefloat = 0;
	public float accystart = 0;
	public float accxstart = 0;
	public Vector3 dir;

	//joystick
	public UltimateJoystick joystick1;
	public UltimateJoystick joystick2;
	public Vector2 joystickpos;
	public Vector2 joystickpos2;
	public UIcode uicoderef;
	//test code

	// Use this for initialization
	void Start () {

		uicoderef = GameObject.Find("areamanager").GetComponent<UIcode>();



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


		//joystick ref

		joystick1 = uicoderef.thejoysticks[0].GetComponent<UltimateJoystick>();
		joystick2 = uicoderef.thejoysticks[1].GetComponent<UltimateJoystick>();


		// quaternion set
	
		shipdefaultquat = shipbody.transform.rotation;

		defaultquatx = transform.localRotation.x;
		defaultquatz = transform.localRotation.z;


		defaultspeed = shipmainspeed;

		screenhor = Screen.width;
		screenver = Screen.height;
		screencenter = Mathf.Abs(screenhor/2);
		screencentery = Mathf.Abs(screenver/2);

		StartCoroutine(waiter());
	}

	IEnumerator waiter()
	{
		yield return new WaitForSeconds(.5f);
		finalspeed = shipmainspeed + incspeed;
	}

	public void newdefaultrot()
	{
		defaultquat = transform.localRotation;
		
		defaultquat.x = defaultquatx;
		defaultquat.z = defaultquatz;

	//	print(defaultquat.y);

		transform.rotation = Quaternion.Lerp(transform.rotation,defaultquat,Time.deltaTime *quatfloat);
	}

	// Update is called once per frame
	void FixedUpdate () {

		//accelerometer code
 
		dir.x = (Input.acceleration.x - accxstart);
		dir.y = (Input.acceleration.y - accystart);

		if(dir.sqrMagnitude > 1)
			dir.Normalize();
 
		//invoke to get new rotation
		Invoke("newdefaultrot",2f);

		//main movement forward
		transform.Translate(0,0,shipmainspeed *Time.deltaTime);

		 // gain the axis from horizontal and vertical
		shiphor = Input.GetAxis("Horizontal");
		shipver = Input.GetAxis("Vertical");

		// gain the axis of the joysticks
		joystickpos = joystick1.JoystickPosition;
		joystickpos2 = joystick2.JoystickPosition;

		//for PC mouse position
		mousepos = Input.mousePosition;
		mousecenter = screencenter - mousepos.x;
		mousecenterx = screencenter - mousepos.y;

		if(inAndroid)
		{
			//for accelerometer on android
			//transform.Translate(dir.x * shipmovementspeed *Time.deltaTime , dir.y * shipmovementspeed * Time.deltaTime , 0);
		//	transform.Rotate(0,androidrotatefloat * shiprotatespeed * Time.deltaTime,0 );

			//for joystick on android
			transform.Translate(joystickpos.x *Time.deltaTime *shipmovementspeed,joystickpos.y *Time.deltaTime* shipmovementspeed,0);
			transform.Rotate(joystickpos2.y * Time.deltaTime *-shiprotatespeed, joystickpos2.x * Time.deltaTime *shiprotatespeed,0);


		}




		if(inUnity)
		{
		 

			//use for when doing remote work
			transform.Translate(dir.x *shipmainspeed *Time.deltaTime , dir.y * shipmainspeed * Time.deltaTime , 0);
			transform.Rotate(0,androidrotatefloat * shiprotatespeed * Time.deltaTime,0 );

			//for joystick on PC
			transform.Translate(joystickpos.x *Time.deltaTime *shipmovementspeed,joystickpos.y *Time.deltaTime *shipmovementspeed,0);
			transform.Rotate(joystickpos2.y * Time.deltaTime *-shiprotatespeed, joystickpos2.x * Time.deltaTime *shiprotatespeed,0);

			//for PC
			transform.Translate(shipmovementspeed * Time.deltaTime * shiphor,0,0);
			transform.Translate(Vector3.up * shipver * Time.deltaTime * shipmovementspeed);
			



			//test code
//			if(mousepos.y != screencenter || mousepos.x != screencentery )
//			{
//				if(mousecenter>=yright && mousecenter<=yleft)
//				{
//					transform.eulerAngles = new Vector3(mousecenterx *Time.deltaTime * shiprotatespeed,mousecenter * Time.deltaTime * shiprotatespeed *-1,transform.eulerAngles.z);
//				}
//			}


			//test code

			//transform.eulerAngles.x add this back into the eulerangle if statement above if script doesn't work

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
