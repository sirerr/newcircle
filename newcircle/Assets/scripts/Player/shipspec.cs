using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class shipspec : MonoBehaviour {

	public float shiphealth = 0;
	public string shipname;
	public string shipclass;
	public float shipweight =0;
	public List<GameObject> shipadds = new List<GameObject>();
	public GameObject shipbaseobj;
 
	// Use this for initialization
	void Start () {
		shipbaseobj = transform.GetChild(0).gameObject;

		for(int i =0;i<shipbaseobj.transform.childCount;i++)
		{
			if(shipbaseobj.transform.GetChild(i).tag == "partpoints")
			{ 
				shipadds.Add(shipbaseobj.transform.GetChild(i).gameObject);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
