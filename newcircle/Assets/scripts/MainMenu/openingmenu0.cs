using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class openingmenu0 : MonoBehaviour {

	//game objects
	public GameObject pogpic;
	public GameObject present;
	//icon
	public GameObject corleicon;
	//button
	public GameObject startbutton;

	//floats
	public float colorvalue = 0;
	public float wait1 = 0;
	public float wait2 = 0;
	public float wait3 = 0;


	// Use this for initialization
	void Start () {

		StartCoroutine(changevalue());

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator changevalue()
	{
		Color acolor;
		yield return new WaitForSeconds(wait1);
		print ("about to appear");
		for(float i =0; i<=colorvalue;i++)
		{
			acolor = pogpic.GetComponent<RawImage>().color;
			acolor.a = i/colorvalue;
			pogpic.GetComponent<RawImage>().color = acolor;
		 
			yield return new WaitForSeconds(wait2);
		}

		yield return new WaitForSeconds(wait1);

		for(float b = colorvalue; b>= 0;b--)
		{
			acolor = pogpic.GetComponent<RawImage>().color;
			acolor.a = b/colorvalue;
			pogpic.GetComponent<RawImage>().color = acolor;
			 
			yield return new WaitForSeconds(wait2);
		}

		yield return new WaitForSeconds(wait3);

		for(float i =0; i<=colorvalue;i++)
		{
			acolor = present.GetComponent<Text>().color;
			acolor.a = i/colorvalue;
			present.GetComponent<Text>().color = acolor;
		 
			yield return new WaitForSeconds(wait2);
		}
		
		yield return new WaitForSeconds(wait1);
		
		for(float b = colorvalue; b>= 0;b--)
		{
			acolor = present.GetComponent<Text>().color;
			acolor.a = b/colorvalue;
			present.GetComponent<Text>().color = acolor;
 
			yield return new WaitForSeconds(wait2);
		}

		yield return new WaitForSeconds(wait3);

		corleicon.SetActive(true);
		yield return new WaitForSeconds(1.5f);
		startbutton.SetActive(true);

	}

public void startgame()
	{

		Application.LoadLevel("testarea0");

	}
}
