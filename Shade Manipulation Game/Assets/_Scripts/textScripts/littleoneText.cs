using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class littleoneText : MonoBehaviour {

	float colorTiming;
	Color mainColor;
	public Color activeColor;
	public Color deadColor;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		initialColor = transform.GetComponent<Text> ().color;


		if (GameObject.Find ("wanderer") != null) {
			mainColor=
			transform.GetComponent<Text> ().color = mainColor;
		} else {
			transform.GetComponent<Text> ().color = deadColor;
		}

	}

}
