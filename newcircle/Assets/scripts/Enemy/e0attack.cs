using UnityEngine;
using System.Collections;

public class e0attack : MonoBehaviour {

	public GameObject eattackobj;
	public GameObject ecenter;
	public GameObject attackemitter;
	public bool playerclose = false;

	public e0animation e0animref;

	//attack speed;
	public float attackspeedfloat = 0;
	//invoke float
	public float attackinvoker = 0;

	//game object
	public GameObject emmiterobj;

	//bool to do attack over and over
	public bool attackredo = false;

	public float firsttime =0;

	// Use this for initialization
	void Start () {

		ecenter = Instantiate(emmiterobj,transform.position,transform.rotation)as GameObject;
		e0animref = transform.GetComponent<e0animation>();
		attackemitter = ecenter.transform.GetChild(0).gameObject;

	}
	
	// Update is called once per frame
	void Update () {
	
		if(playerclose)
		{
			ecenter.transform.LookAt(e0animref.playerloc,worldUp:Vector3.up);
			if(firsttime == 0)
			{
				firsttime =1;
				attackredo = true;
			}

			if(attackredo)
			{
				StartCoroutine(redoattack());
			}
		}

	}

	IEnumerator redoattack()
	{
		attackredo = false;
		yield return new WaitForSeconds(attackinvoker);
		createattack();



	}
 

	public void createattack()
	{
		GameObject newattack;
		newattack = Instantiate(eattackobj,attackemitter.transform.position,attackemitter.transform.rotation) as GameObject;
		attackredo = true;
	}
}
