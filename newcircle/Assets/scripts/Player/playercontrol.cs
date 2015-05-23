﻿using UnityEngine;
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


	// Use this for initialization
	void Start () {
	
		if(Application.platform == RuntimePlatform.WindowsEditor)
		{
			inUnity = true;
		}

		if(Application.platform ==  RuntimePlatform.Android)
		{
			inAndroid = true;
		}
		defaultquat = transform.rotation;
		shipdefaultquat = shipbody.transform.rotation;
		defaultspeed = shipmainspeed;

		screenhor = Screen.width;
		screenver = Screen.height;
		screencenter = Mathf.Abs(screenhor/2);
	}
	
	// Update is called once per frame
	void Update () {
	
		//main movement forward
		transform.Translate(0,0,shipmainspeed *Time.deltaTime);

		 
		shiphor = Input.GetAxis("Horizontal");
		shipver = Input.GetAxis("Vertical");

		
		mousepos = Input.mousePosition;
		mousecenter = screencenter - mousepos.x;

		if(inUnity)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				if(shipmainspeed<= finalspeed)
				{
					shipmainspeed+= incspeed;
				}
			}
			else
			{
				shipmainspeed = defaultspeed;
			}

			//break in code


	 	

//			if(mousepos.y != screencenter)
//			{
//				if(mousecenter>=yright && mousecenter<=yleft){
//				transform.eulerAngles = new Vector3(transform.eulerAngles.x,mousecenter * Time.deltaTime * rotatespeed *-1,transform.eulerAngles.z);
//				print(mousecenter);
//				}
//			}

			if(shiphor>0)
			{
				transform.Translate(shipmovementspeed * Time.deltaTime,0,0);
				shipbody.transform.Rotate(-Vector3.forward * Time.deltaTime *rotatespeed);
				if(shipbody.transform.eulerAngles.z <= rotateright)
				{
					shipbody.transform.eulerAngles = new Vector3(shipbody.transform.eulerAngles.x,shipbody.transform.eulerAngles.y,rotateright);
				//	print (shipbody.transform.eulerAngles.z);
				}

			}else if(shiphor<0)
			{
				transform.Translate(-1 * shipmovementspeed * Time.deltaTime,0,0);
				shipbody.transform.Rotate(Vector3.forward * Time.deltaTime *rotatespeed);
				if(shipbody.transform.eulerAngles.z >= rotateleft)
				{
					shipbody.transform.eulerAngles = new Vector3(shipbody.transform.eulerAngles.x,shipbody.transform.eulerAngles.y,rotateleft);
				//	print (shipbody.transform.eulerAngles.z);
				}
			}	
			else if(shipver>0)
			{
				transform.Rotate(-Vector3.right * Time.deltaTime * rotatespeed);
				//shipbody.transform.Rotate(Vector3.right * Time.deltaTime * rotatespeed);
			//	print (shipbody.transform.eulerAngles.x);

				if(transform.eulerAngles.x<= rotateup)
				{
					transform.eulerAngles = new Vector3(rotateup,0,0);
				}

			}else if(shipver<0)
			{
				transform.Rotate(Vector3.right * Time.deltaTime * rotatespeed);
			//	shipbody.transform.Rotate(-Vector3.right * Time.deltaTime * rotatespeed);
			//	print (shipbody.transform.eulerAngles.x);

				if(transform.eulerAngles.x>= rotatedown)
				{
					transform.eulerAngles = new Vector3(rotatedown,0,0);
				}
			}
			else
			{
				shipbody.transform.localRotation = Quaternion.Slerp(transform.localRotation,shipdefaultquat,Time.time *quatfloat);
				transform.rotation = Quaternion.Slerp(transform.rotation,defaultquat,Time.time *quatfloat);

			 
			}

		}

	}
}
