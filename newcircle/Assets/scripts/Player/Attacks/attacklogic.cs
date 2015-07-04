using UnityEngine;
using System.Collections;

public class attacklogic : MonoBehaviour {

	//attack power
	public float attackpower = 0;

	//linger?
	public bool stayfortime = false;

	//time to stay
	public float staytime = 0;

	//expos game object
	public GameObject expPS;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision ehit)
	{

		if(ehit.gameObject.tag == "Enemy")
		{
			Instantiate(expPS,transform.position,transform.rotation);
		}

	}
}
