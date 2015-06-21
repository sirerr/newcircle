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

	// Use this for initialization
	void Start () {
		shipspecref = transform.GetComponent<shipspec>();
		uicoderef = GameObject.Find("areamanager").transform.GetComponent<UIcode>();

		for(int i =0; i<shipspecref.weaponpart.Count; i++)
		{ 
			uicoderef.toolLocs[i].transform.GetChild(0).gameObject.SetActive(true);
			uicoderef.toolLocs[i].transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(() => currentweapons[0].GetComponent<Partspec>().firetheweapon());
		}


	}
	
	// Update is called once per frame
	void Update () {
 
//		if(Input.GetMouseButtonDown(0))
//		{
//			if(currentweapons[0].GetComponent<Partspec>().defaultweaponamount>0)
//			{
//				if((shipspecref.shippower-currentweapons[0].GetComponent<Partspec>().ppowerneeded)>0)
//				{
//				
//				//	currentweapons[0].GetComponent<Partspec>().weaponaction();
//					currentweapons[0].GetComponent<Partspec>().fireweapon = true;
//				}
//			}
//			else
//			{
//				currentweapons[0].GetComponent<Partspec>().refreshweaponer();
//			}
//
//		}
 
	}

	IEnumerator quicksec()
	{
		yield return new WaitForSeconds(.5f);

	}
}
