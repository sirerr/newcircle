using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class weaponcontrol : MonoBehaviour {

	public List<GameObject> currentweapons = new List<GameObject>();

	public shipspec shipspecref;

	//ui button
	public GameObject uifirebutton;
	public UIcode uicoderef;

	private float weaponcount = 0;

	// Use this for initialization
	void Start () {
		shipspecref = transform.GetComponent<shipspec>();
		uicoderef = GameObject.Find("areamanager").transform.GetComponent<UIcode>();

		weaponcount = shipspecref.weaponpart.Count;

		for(int i =0; i<shipspecref.weaponpart.Count; i++)
		{ 
			uicoderef.toolLocs[i].transform.GetChild(0).gameObject.SetActive(true);
			uicoderef.toolLocs[i].transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(() => currentweapons[0].GetComponent<Partspec>().firetheweapon());
			uicoderef.toolLocs[i].transform.GetChild(1).gameObject.GetComponent<Text>().text = currentweapons[0].GetComponent<Partspec>().defaultweaponamount.ToString();
		}


	}
	
	// Update is called once per frame
	void Update () {
 
	switch(weaponcount.ToString())
		{
		case "1":
			uicoderef.toolLocs[0].transform.GetChild(1).gameObject.GetComponent<Text>().text = currentweapons[0].GetComponent<Partspec>().defaultweaponamount.ToString();
			break;
		case "2":
			uicoderef.toolLocs[0].transform.GetChild(1).gameObject.GetComponent<Text>().text = currentweapons[0].GetComponent<Partspec>().defaultweaponamount.ToString();
			uicoderef.toolLocs[1].transform.GetChild(1).gameObject.GetComponent<Text>().text = currentweapons[1].GetComponent<Partspec>().defaultweaponamount.ToString();
			break;
		}

	}

	IEnumerator quicksec()
	{
		yield return new WaitForSeconds(.5f);

	}
}
