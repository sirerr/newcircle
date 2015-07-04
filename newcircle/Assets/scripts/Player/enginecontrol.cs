using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class enginecontrol : MonoBehaviour {

	// will be adding the UI objects to increase speed

	//part ref
	public Partspec engpartref;
	//playercontrol ref
	public playercontrol playerconref;
	public shipspec shipspecsref;
 

	public bool extraspeedactive = false;

	//ui code ref
	public UIcode uicoderef;
	// Use this for initialization
	void Start () {
		engpartref = transform.GetComponent<Partspec>();
		playerconref = transform.root.GetComponent<playercontrol>();
		shipspecsref = transform.root.GetComponent<shipspec>();


		uicoderef = GameObject.Find("areamanager").transform.GetComponent<UIcode>();
		uicoderef.themovementbuttons[0].transform.GetComponent<Button>().onClick.AddListener(() => engpartref.gofaster());
	
	}

	// goal to create ui objects to enable increases in speed if needed.

	// Update is called once per frame
	void Update () {
 
	}
}
