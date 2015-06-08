using UnityEngine;
using System.Collections;

public class enginecontrol : MonoBehaviour {

	// will be adding the UI objects to increase speed

	public Partspec engpartref;
	public playercontrol playerconref;
	public shipspec shipspecsref;

	public float addtospeed = 0;
	public float takefrompower = 0;

	public bool extraspeedactive = false;

	// Use this for initialization
	void Start () {
	
		engpartref = transform.GetComponent<Partspec>();
		playerconref = transform.root.GetComponent<playercontrol>();
		shipspecsref = transform.root.GetComponent<shipspec>();

		if(engpartref.incspeed)
		{
			addtospeed = engpartref.addedspeed;
		}

		if(engpartref.powerusage)
		{
			takefrompower = engpartref.ppowerneeded;
		}
	}

	// goal to create ui objects to enable increases in speed if needed.

	// Update is called once per frame
	void Update () {
	
		if(Input.GetKey(KeyCode.Space) && (shipspecsref.shippower - takefrompower)>0 && playerconref.shipmainspeed < playerconref.finalspeed)
		{
			print ("going faster");
			playerconref.shipmainspeed += addtospeed;

			if(!extraspeedactive)
			{
				shipspecsref.shippower -= takefrompower;
				extraspeedactive = true;
			}
		}


		if(Input.GetKeyUp(KeyCode.Space)){
			print ("stay default");
			playerconref.shipmainspeed = playerconref.defaultspeed;
			shipspecsref.shippower = shipspecsref.defaultshippower;
			extraspeedactive = false;
		}


	}
}
