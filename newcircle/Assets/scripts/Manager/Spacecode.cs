using UnityEngine;
using System.Collections;

public class Spacecode : MonoBehaviour {

	// The idea of this scritp is to handle the creation of the arena 

	public GameObject arenacenter;
	//bool object will decide if player is out of the arean
	public bool playerout = false;

	//determine if in courotine
	public bool incorourtine = false;

	public GameObject gameplaycanvas;
	public GameObject followingcam;

	//player warning text;
	public GameObject rtbtext;
	public float rtbcourtime = 0;
	//

	// Use this for initialization
	void Start () {
		Instantiate(arenacenter,transform.transform.position,transform.rotation);
		gameplaycanvas = GameObject.Find("DefaultCanvas");
		followingcam = GameObject.FindGameObjectWithTag("MainCamera");
	
		gameplaycanvas.GetComponent<Canvas>().worldCamera = followingcam.transform.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

		//for exiting the area and needing to get back
		if(playerout)
		{
			if(!incorourtine){
			StartCoroutine(rtbtextpopup());
			}
		}



	}

IEnumerator rtbtextpopup()
	{
		incorourtine = true;
    	yield return new WaitForSeconds(rtbcourtime);
		rtbtext.SetActive(true);
		yield return new WaitForSeconds(rtbcourtime);
		rtbtext.SetActive(false);
		incorourtine =false;
	}
}
