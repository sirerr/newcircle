﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIcode : MonoBehaviour {


	public GameObject uicanvas;
 	//player info
	public GameObject uiplayerspeed;
	public GameObject uiplayerhealth;
	public GameObject uiplayerpower;
	//player ref
	public playercontrol playerconref;
	//ship ref
	public shipspec shipspecref;
	//player ref
	public GameObject playerref;

	//android values
//	public GameObject dirxref;
//	public GameObject diryref;
//	public GameObject dirzref;

	//movement bool
	public bool movebool = true;
	public bool stopbool = false;

	//ui weapon location
	public GameObject[] toolLocs;

	//all joystick array
	public GameObject[] thejoysticks;

	//all movement speed button array
	public GameObject[] themovementbuttons;

	//stop and go text obj
	public GameObject stopandgotext;

	// Use this for initialization
	void Start () {

		//main ref
		uicanvas = GameObject.Find("DefaultCanvas");
		playerref = GameObject.FindGameObjectWithTag("Player");

		//script refs
		playerconref = playerref.GetComponent<playercontrol>();
		shipspecref = playerref.GetComponent<shipspec>();

		// ui ref
	//	uiplayerspeed = GameObject.Find("pspeed");

		for(int i=0; i<uicanvas.transform.childCount;i++)
		{
			if(uicanvas.transform.GetChild(i).name == "Stopandgo")
			{
				stopandgotext = uicanvas.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject;
			}
		}
 

//		dirxref = GameObject.Find("dirx");
//		diryref = GameObject.Find("diry");
//		dirzref = GameObject.Find("dirz");
	}
	
	// Update is called once per frame
	void Update () {

		stopandgotext.GetComponent<Text>().text = playerconref.shipmainspeed.ToString();

		//ui test ui info
	//	uiplayerspeed.transform.GetComponent<Text>().text = playerconref.shipmainspeed.ToString();
 
//
//		dirxref.transform.GetComponent<Text>().text = "Acceleration x: " + playerconref.dir.x.ToString("F2");
//		diryref.transform.GetComponent<Text>().text = "Acceleration y: " + playerconref.dir.y.ToString("F2");
//		dirzref.transform.GetComponent<Text>().text = "Acceleration z: " + playerconref.dir.z.ToString("F2");

		//ui test ui info
		
	
	}



	public void nomovementbutton()
	{
		if(movebool)
		{
			movebool = false;
			playerconref.shipmainspeed = 0;
			print(movebool);
		}
	
	}


	public void stopandgo()
	{
		if(movebool)
		{
			stopbool = true;
			movebool = false;
			playerconref.shipmainspeed = 0;

		}
		else if(stopbool)
		{
			movebool = true;
			stopbool = false;
			playerconref.shipmainspeed = playerconref.defaultspeed;
		}

	}

}
