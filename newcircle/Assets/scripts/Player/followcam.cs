﻿using UnityEngine;
using System.Collections;

public class followcam : MonoBehaviour {

	//follow player object
	public GameObject playerobj;

	//distance between camera and player
	public float camdist = 0;

	//acceptable distance
	public float okaydistance = 0;

	//cam speed
	public float camspeed;

	public Quaternion defaultcamquat;

	// Use this for initialization
	void Start () {
		defaultcamquat = transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		transform.LookAt(playerobj.transform);
		float step = camspeed * Time.deltaTime;
		camdist = Vector3.Distance(playerobj.transform.position, transform.position);

		if(camdist>=okaydistance)
		{
			transform.position= Vector3.MoveTowards(transform.position,playerobj.transform.position ,step);
		}

	}
}
