using UnityEngine;
using System.Collections;

public class Spacecode : MonoBehaviour {

	// The idea of this scritp is to handle the creation of the arena 

	public GameObject arenacenter;

	// Use this for initialization
	void Start () {
		Instantiate(arenacenter,transform.transform.position,transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
