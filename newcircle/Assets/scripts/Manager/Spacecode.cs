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

	//dust maker
	public GameObject duster;
	public GameObject[] dustarray;
	public float dustamount = 0;
	public float posdustloc = 0;
	public float negdustloc = 0;
	//

	// Use this for initialization
	void Start () {
		Instantiate(arenacenter,transform.transform.position,transform.rotation);
		gameplaycanvas = GameObject.Find("DefaultCanvas");
		followingcam = GameObject.FindGameObjectWithTag("MainCamera");
	
		gameplaycanvas.GetComponent<Canvas>().worldCamera = followingcam.transform.GetComponent<Camera>();

		for(int i =0; i<dustamount; i++)
		{
			float posx = Random.Range(negdustloc,posdustloc);
			float posy = Random.Range(negdustloc,posdustloc);
			float posz = Random.Range(negdustloc,posdustloc);

		dustarray[i] = Instantiate(duster,new Vector3(posx,posy,posz),Quaternion.identity) as GameObject;

		}
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
