using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class shipspec : MonoBehaviour {

	//health
	public float shiphealth = 0;
	public float defaultshiphealth = 0;
	//power
	public float shippower = 0;
	public float defaultshippower = 0;
	public float shippowertaken = 0;
	//ship info
	public string shipname;
	public string shipclass;

	//speed inc
	public float shipspeedinc;

	//weight
	public float shipweightadd =0;
	public float defaultshipweight = 0;

	public List<GameObject> shipadds = new List<GameObject>();
	public List<GameObject> enginepart = new List<GameObject>();
	public List<GameObject> bodypart = new List<GameObject>();
	public List<GameObject> specialpart = new List<GameObject>();
	public List<GameObject> weaponpart = new List<GameObject>();

	// part attached
	public List<bool> partattached = new List<bool>();
	// ship gameobj
	public GameObject shipbaseobj;
 
	// Use this for initialization
	void Start () {


		// set default values at beginning of game
		defaultshiphealth = shiphealth;
		defaultshippower = shippower;

		//
		shipbaseobj = transform.GetChild(0).gameObject;

		for(int i =0;i<shipbaseobj.transform.childCount;i++)
		{
			if(shipbaseobj.transform.GetChild(i).tag == "partpoints")
			{ 
				shipadds.Add(shipbaseobj.transform.GetChild(i).gameObject);
			}
		}
		//check to see if any ship add locations have a part attached, 

		for( int a =0;a< shipadds.Count;a++)
		{
 			


			GameObject temppart = shipadds[a].transform.GetChild(0).gameObject;

			if(temppart.activeSelf)
			{

			switch (temppart.tag)
			{
			case "enginepart":
				print ("I got a engine part");

				enginepart.Add (temppart);
				partattached.Add(true);

				if(temppart.GetComponent<Partspec>().pweight > 0)
				{
					shipweightadd += temppart.GetComponent<Partspec>().pweight;
				}
				if(temppart.GetComponent<Partspec>().powerusage)
				{
					shippowertaken += temppart.GetComponent<Partspec>().ppowerneeded;
				}
				if(temppart.GetComponent<Partspec>().incspeed)
				{
					shipspeedinc += temppart.GetComponent<Partspec>().addedspeed;
				}
				break;
			case "bodypart":
				print ("I got a bodypart");
				bodypart.Add(temppart);
				partattached.Add(true);
				if(temppart.GetComponent<Partspec>().pweight > 0)
				{
					shipweightadd += temppart.GetComponent<Partspec>().pweight;
				}

				break;
			case "specialpart":
				print ("I got a special part");

				specialpart.Add(temppart);
				partattached.Add(true);
				if(temppart.GetComponent<Partspec>().powerusage)
				{
					shippowertaken += temppart.GetComponent<Partspec>().ppowerneeded;
				}

				break;
			case "weaponpart":

				weaponpart.Add(temppart);
				partattached.Add(true);
				if(temppart.GetComponent<Partspec>().pweight > 0)
				{
					shipweightadd += temppart.GetComponent<Partspec>().pweight;
				}
				if(temppart.GetComponent<Partspec>().powerusage)
				{
					shippowertaken += temppart.GetComponent<Partspec>().ppowerneeded;
				}

				print ("I got a weapon part");
				break;
				}
			}
		}

		transform.GetComponent<playercontrol>().incspeed += shipspeedinc;
	}
	
	// Update is called once per frame
	void Update () {
	
	 }
}
