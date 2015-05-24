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

	//speed inc
	public bool incspeed = false;
	public float addedspeed = 0;

	// for weapon systems
	public bool refreshweapon = false;
	public float tempweaponamount = 0;
	public float refreshtime =0;
	public float defaultweaponamount =0;
	public Rigidbody weaponobj;
	public float weaponspeed =0;
	public bool okaytorefresh = true;

	public bool fireweapon = false;

	//charge up and blast ps from cannon possible additions setup time for those and gameobjects

	// Use this for initialization
	void Start () {

		tempweaponamount = defaultweaponamount;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate() 
	{

		if(fireweapon)
		{
			fireweapon = false;

			Rigidbody projectile;
			projectile = Instantiate(weaponobj,transform.position,transform.rotation) as Rigidbody;
			projectile.velocity = transform.TransformDirection(Vector3.forward * weaponspeed);
			defaultweaponamount --;
		}
	}

//	public void weaponaction()
//	{
//		Rigidbody projectile;
//		projectile = Instantiate(weaponobj,transform.position,transform.rotation) as Rigidbody;
//		projectile.velocity = transform.TransformDirection(Vector3.forward * weaponspeed);
//		defaultweaponamount --;
//	}

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
