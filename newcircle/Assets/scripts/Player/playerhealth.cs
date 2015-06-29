using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerhealth : MonoBehaviour {

	//reference to the spec
	private shipspec shipspecref;

	//reference the manager
	public UIcode uicoderef;

	//the canvas obj
	public GameObject canvasobj;

	//healthbar obj
	public GameObject healthbarobj;

	//powerbar obj
	public GameObject powerbarobj;

	//playercontrol ref
	private playercontrol playerconref;

	// Use this for initialization
	void Start () {
	
		shipspecref = transform.GetComponent<shipspec>();
		uicoderef = GameObject.Find("areamanager").transform.GetComponent<UIcode>();
		canvasobj = GameObject.Find("DefaultCanvas");
		playerconref = transform.GetComponent<playercontrol>();

		for(int i=0; i<canvasobj.transform.childCount;i++)
		{
			//print("looking");
			if(canvasobj.transform.GetChild(i).name == "Healthbar")
			{
			//	print("found bar");
				healthbarobj = canvasobj.transform.GetChild(i).gameObject;
			}

			if(canvasobj.transform.GetChild(i).name == "Powerbar")
			{
				powerbarobj = canvasobj.transform.GetChild(i).gameObject;
			}
		}

		healthbarobj.GetComponent<Slider>().maxValue = shipspecref.shiphealth;
		healthbarobj.GetComponent<Slider>().value = shipspecref.shiphealth;

		powerbarobj.GetComponent<Slider>().maxValue = shipspecref.shippower;
		powerbarobj.GetComponent<Slider>().value = shipspecref.shippower;

	}
	
	// Update is called once per frame
	void Update () {

		if(shipspecref.shiphealth <=0)
		{
			shipspecref.shiphealth = shipspecref.defaultshiphealth;
		}
	
		powerbarobj.GetComponent<Slider>().value = shipspecref.shippower;
	}

	IEnumerator playerdeath()
	{
		yield return new WaitForSeconds(3f);

	}


	void OnCollisionEnter(Collision phit)
	{
		if(phit.transform.tag == "enemyattack")
		{
			shipspecref.shiphealth -= phit.transform.GetComponent<enemattacklogic>().eattackpower;
			healthbarobj.GetComponent<Slider>().value = shipspecref.shiphealth;

			playerconref.newdefaultrot();
		}

	}
}
