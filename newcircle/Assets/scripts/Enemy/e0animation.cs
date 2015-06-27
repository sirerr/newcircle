using UnityEngine;
using System.Collections;

public class e0animation : MonoBehaviour {

	[SerializeField]   private Animator animcon;
	//reference to attack
	public e0attack attackref;
	//spin speed
	public float spinspeed = 0;
	//player distance
	public float playerdistance = 0;
	//player required distance
	public float playerclosedistance = 0;
	//player position
	public Transform playerloc;
	public string trig1 = "";
	public string trig2 = "";

	// Use this for initialization
	void Start () {
	
		animcon = GetComponent<Animator>();
		attackref = GetComponent<e0attack>();
		playerloc = GameObject.FindGameObjectWithTag("Player").transform;

	}
	
	// Update is called once per frame
	void Update () {

		playerdistance = Vector3.Distance(playerloc.position, transform.position);

		if(playerdistance<= playerclosedistance)
		{
			animcon.ResetTrigger(trig2);
			animcon.SetTrigger(trig1);
			attackref.playerclose = true;
			transform.Rotate(new Vector3(0,Mathf.Lerp(0f,spinspeed,.5f),0));
			attackref.playerclose = true;	
		}
			else
		{
			animcon.ResetTrigger(trig1);
			animcon.SetTrigger(trig2);
			attackref.playerclose = false;
			transform.Rotate(new Vector3(0,0,0));
			attackref.playerclose = false;
		}
	

	}
}
