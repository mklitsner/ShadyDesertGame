using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

public float sensitivity = 0.1f;


	float sunXrotation=90;
	//90=noon
	float maxXrot=90;
	//0=dusk
	float minXrot=0;

	float sunYrotation=0;

	float sunXrotate;
	float sunYrotate;
	//global
	//0=shade at 12oclock
	//180= shade at 6oclock

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		sunXrotation=transform.localEulerAngles.x;
		sunYrotation=transform.eulerAngles.y;




		ChangeSunCoordinates ();
		MoveSun ();
	}

	void MoveSun(){
		transform.Rotate (sunXrotate, 0, 0,Space.Self);
		transform.Rotate (0, sunYrotate, 0,Space.World);
	}




	void ChangeSunCoordinates(){
		if (Input.GetKey ("up")) {
			print ("up arrow key is held down");
			if (sunXrotation < maxXrot) {
				sunXrotate = sensitivity;
			}
		} else if (Input.GetKey ("down")) {
			print ("down arrow key is held down");
			if (sunXrotation > minXrot) {
				sunXrotate = -sensitivity;
			}
		} else {
			sunXrotate = 0;
		}

		if (Input.GetKey("left")) {
			print ("left arrow key is held down");
			sunYrotate = sensitivity;
		} else if (Input.GetKey ("right")) {
			print ("right arrow key is held down");
			sunYrotate = -sensitivity;
		} else {
			sunYrotate = 0;
		}
	}


}
