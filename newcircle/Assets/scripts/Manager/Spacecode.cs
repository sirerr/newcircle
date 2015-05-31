using UnityEngine;
using System.Collections;

public class Spacecode : MonoBehaviour {

	// The idea of this scritp is to handle the creation of the arena 

	public GameObject arenacenter;
	//bool object will decide if player is out of the area
	public bool playerout = false;

	// Use this for initialization
	void Start () {
		Instantiate(arenacenter,transform.transform.position,transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
