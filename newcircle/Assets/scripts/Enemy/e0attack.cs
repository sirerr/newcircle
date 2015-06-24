using UnityEngine;
using System.Collections;

public class e0attack : MonoBehaviour {


	public GameObject ecenter;
	public GameObject attackemitter;
	public bool playerclose = false;

	// Use this for initialization
	void Start () {
		ecenter = transform.GetChild(1).gameObject;
		attackemitter = transform.GetChild(2).transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(playerclose)
		{


		}

	}

	public void FixedUpdate()
	{


	}
}
