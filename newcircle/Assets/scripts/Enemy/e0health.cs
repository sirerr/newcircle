using UnityEngine;
using System.Collections;

public class e0health : MonoBehaviour {

	//multiple health levels?
	public bool multihealth = false;

	//enemy health
	public float[] ehealth;

	//ref to other script
	public e0attack attackref;

	//shield?
	public bool shieldattached = false;

	//shield obj
	public GameObject shieldobj;
	//shield strength
	public float shieldpower =0;

	//explosion obj
	public GameObject explosionobj;

	// Use this for initialization
	void Start () {
	
		attackref = transform.GetComponent<e0attack>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if(!multihealth && ehealth[0]<=0)
		{
			Destroy(this.gameObject);
		}

	}

	void OnCollisionEnter(Collision ahit)
	{
		if(ahit.transform.tag == "playerattack")
		{
			if(multihealth)
			{}
			else
			{

				ehealth[0] -= ahit.transform.GetComponent<attacklogic>().attackpower;
			}

			if(shieldattached)
			{

			}
		
		}

	}

}
