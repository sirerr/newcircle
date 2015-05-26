using UnityEngine;
using System.Collections;

public class wallcode : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider ahit)
	{
		if(ahit.gameObject.tag =="Player")
		{

		}

	}

}
