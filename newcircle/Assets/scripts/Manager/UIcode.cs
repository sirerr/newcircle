using UnityEngine;
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

	// Use this for initialization
	void Start () {

		//main ref
		uicanvas = GameObject.Find("DefaultCanvas");
		playerref = GameObject.FindGameObjectWithTag("Player");

		//script refs
		playerconref = playerref.GetComponent<playercontrol>();
		shipspecref = playerref.GetComponent<shipspec>();

		// ui ref
		uiplayerspeed = GameObject.Find("pspeed");
		uiplayerhealth = GameObject.Find("phealth");
		uiplayerpower = GameObject.Find("ppower");


	}
	
	// Update is called once per frame
	void Update () {

		//ui test ui info
		uiplayerspeed.transform.GetComponent<Text>().text = "Speed: " + playerconref.shipmainspeed.ToString();
		uiplayerpower.transform.GetComponent<Text>().text = "Power: " + shipspecref.shippower.ToString();
		uiplayerhealth.transform.GetComponent<Text>().text = "Health: " + shipspecref.shiphealth.ToString();


		//ui test ui info

	
	}
}
