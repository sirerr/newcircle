using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class weaponcontrol : MonoBehaviour {

	public List<GameObject> currentweapons = new List<GameObject>();

	public shipspec shipspecref;


	// Use this for initialization
	void Start () {
		shipspecref = transform.GetComponent<shipspec>();

	}
	
	// Update is called once per frame
	void Update () {
 
		if(Input.GetMouseButtonDown(0))
		{

			if(currentweapons[0].GetComponent<Partspec>().defaultweaponamount>0)
			{
			
				if((shipspecref.shippower-currentweapons[0].GetComponent<Partspec>().ppowerneeded)>0)
				{
				
				//	currentweapons[0].GetComponent<Partspec>().weaponaction();
					currentweapons[0].GetComponent<Partspec>().fireweapon = true;
				}
			}
			else
			{
				currentweapons[0].GetComponent<Partspec>().refreshweaponer();
			}

		}




	}

	IEnumerator quicksec()
	{
		yield return new WaitForSeconds(.5f);

	}
}
