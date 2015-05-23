using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Partspec : MonoBehaviour {

	//weight of part
	public float pweight = 0;
	//does it take power from ship?
	public bool powerusage = false;
	public float ppowerneeded = 0;

	//specific class
	public bool classspecific = false;
	public string partclass = "universal";

	//speed inc
	public bool incspeed = false;
	public float addedspeed = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
