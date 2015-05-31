using UnityEngine;
using System.Collections;

public class wallcode : MonoBehaviour {

	public Spacecode spaceref;

	public string areatext;

	// Use this for initialization
	void Start () {
		StartCoroutine(quickwait());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider intrig)
	{
	if(intrig.gameObject.tag == "Player")
		{
			spaceref.playerout = true;
			print("he's in the area");
		}

	}

	void OnTriggerExit(Collider exittrig)
	{

		if(exittrig.gameObject.tag =="Player")
		{
			spaceref.playerout = false;
			print("he's out of the area");
		}
	}

	IEnumerator quickwait()
	{
		yield return new WaitForSeconds(1f);
		spaceref = GameObject.Find(areatext).GetComponent<Spacecode>();
	}

}
