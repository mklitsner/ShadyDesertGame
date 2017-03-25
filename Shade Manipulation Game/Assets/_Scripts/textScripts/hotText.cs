using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class hotText : MonoBehaviour {

	float colorTiming;
	Color initialColor;
	public Color activeColor;
	public Color deadColor;
	// Use this for initialization
	void Start () {
		initialColor = transform.GetComponent<Text> ().color;
	}
	
	// Update is called once per frame
	void Update () {

		if (GameObject.Find ("wanderer") != null) {

			DesertWandererAI wanderer= GameObject.Find ("wanderer").GetComponent<DesertWandererAI> ();


			colorTiming = Mathf.PingPong (Time.time, 1 - (wanderer.heat /2));

			transform.GetComponent<Text> ().color = Color.Lerp (activeColor, initialColor, colorTiming);
		} else {
			transform.GetComponent<Text> ().color = deadColor;
		}
		
	}




	




}

