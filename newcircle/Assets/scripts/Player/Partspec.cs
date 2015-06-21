using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Partspec : MonoBehaviour {

	//weight of part
	public float pweight = 0;
	//does it take power from ship?
	public bool powerusage = false;
	public float ppowerneeded = 0;

	//specific class
	public bool classspecific = false;
	public string partclass = "universal";

	//for engine systems-------------------------
	public bool incspeed = false;
	public float addedspeed = 0;
	//will boost in speed last forever?
	public bool shortburst = false;
	//time limit of the speed boosst
	public float speedtimelimit = 0;
	//will control the boost in speed using a cooroutine
	public bool extraspeedactive = false;




	// for weapon systems----------------------
	public bool refreshweapon = false;
	public float tempweaponamount = 0;
	public float refreshtime =0;
	public float defaultweaponamount =0;
	public Rigidbody weaponobj;
	public float weaponspeed =0;
	public bool okaytorefresh = true;

	public bool fireweapon = false;
	//auto target
	public bool autotarget = false;
	public bool autofirsttime = true;
	public float firstautotimelimit = 0;
	public float nextautotimelimit = 0;

	public List<GameObject> enemylist  = new List<GameObject>();

	//special additions-----------------


	//ship reference
	public shipspec shipspecscriptref;
	public playercontrol playercontrolref;

	//charge up and blast ps from cannon possible additions setup time for those and gameobjects

	// Use this for initialization
	void Start () {

		shipspecscriptref = transform.root.transform.GetComponent<shipspec>();
		playercontrolref = transform.root.transform.GetComponent<playercontrol>();

		if(transform.tag == "weaponpart"){

			tempweaponamount = defaultweaponamount;
			if(autotarget)
			{
				StartCoroutine(autotarsystem());
			}
		}

		if(transform.tag =="enginepart")
		{

		}

	}

	public void gofaster()
	{

		print ("about to speed up now");
	 
		if(!extraspeedactive)
		{
			extraspeedactive = true;
			if((shipspecscriptref.shippower - ppowerneeded)> 0 && playercontrolref.shipmainspeed< playercontrolref.finalspeed)
			{
				playercontrolref.shipmainspeed += addedspeed;
				shipspecscriptref.shippower -= ppowerneeded;

			}
			if(shortburst)
			{
				StartCoroutine(waitandslowdown());
			}
		}

	}

	IEnumerator waitandslowdown()
	{
		yield return new WaitForSeconds(speedtimelimit);
		playercontrolref.shipmainspeed = playercontrolref.defaultspeed;
		shipspecscriptref.shippower = shipspecscriptref.defaultshippower;
		extraspeedactive = false;
	}


	IEnumerator autotarsystem()
	{
		float enemycount = 0;
		if(autofirsttime){
			yield return new WaitForSeconds(firstautotimelimit);
			autofirsttime = false;		
		}
		else
		{
			yield return new WaitForSeconds(nextautotimelimit);
		}

		enemylist.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

		// add cross hairs to nearest enemy and then check to see if still there with another script then go back here and check again.

	}

	// Update is called once per frame
	void Update () {
	
	}
//	void FixedUpdate() 
//	{
//		if(fireweapon)
//		{
//			fireweapon = false;
//			if(defaultweaponamount>0)
//			{
//				if((shipspecscriptref.shippower - ppowerneeded)>0)
//				{
//					Rigidbody projectile;
//					projectile = Instantiate(weaponobj,transform.position,transform.rotation) as Rigidbody;
//					projectile.velocity = transform.TransformDirection(Vector3.forward * weaponspeed);
//					defaultweaponamount --;
//				}
//			}
//			else
//			{
//				refreshweaponer();
//			}
//		}
//	}

 public void firetheweapon()
	{
		if(defaultweaponamount>0)
		{
			if((shipspecscriptref.shippower - ppowerneeded)>0)
			{
				Rigidbody projectile;
				projectile = Instantiate(weaponobj,transform.position,transform.rotation) as Rigidbody;
				projectile.velocity = transform.TransformDirection(Vector3.forward * weaponspeed);
				defaultweaponamount --;
			}
		}
		else
		{
			refreshweaponer();
		}

	}

	public void refreshweaponer()
	{
		if(okaytorefresh && refreshweapon)
		{
			StartCoroutine(refresher());
			okaytorefresh = false;
		}
	}

	IEnumerator refresher()
	{

		yield return new WaitForSeconds(refreshtime);
		defaultweaponamount = tempweaponamount;
		okaytorefresh =true;

	}

}
