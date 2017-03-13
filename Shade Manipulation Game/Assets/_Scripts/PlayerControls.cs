using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

public float sensitivity = 1f;


	float sunXrotation=90;
	//90=noon
	float maxXrot=90;
	//0=dusk
	float minXrot=0;

	float sunYrotation=0;

	float sunXrotate;
	float sunYrotate;

	public float deacceleration=0.1f;

	int sign=1;
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




		ChangeSunCoordinatesNatural();
		ChangeSunPitch ();
		MoveSun ();
	}

	void MoveSun(){
		transform.Rotate (sunXrotate, 0, 0,Space.Self);
		transform.Rotate (0, sunYrotate, 0,Space.World);

	}




	void ChangeSunCoordinatesNatural(){
		if (sunXrotation > 90) {
			sign = -1;
		} else {
			sign = 1;
		}


		if (Input.GetKey("up")) {

			sunYrotate = -sensitivity * Mathf.Sin ((sunYrotation* Mathf.PI)/180)*sign;
		} else if (Input.GetKey ("down")) {

			sunYrotate = sensitivity * Mathf.Sin ((sunYrotation* Mathf.PI)/180)*sign;
		} else if (Input.GetKey("left")) {

			sunYrotate = -sensitivity * Mathf.Cos ((sunYrotation* Mathf.PI)/180)*sign;
		} else if (Input.GetKey ("right")) {
			
			sunYrotate = sensitivity * Mathf.Cos ((sunYrotation* Mathf.PI)/180)*sign;
		} else {
			if(sunYrotate>0){
				sunYrotate = sunYrotate-deacceleration;
				if (sunYrotate <= 0) {
					sunYrotate = 0;
				}
			}else if (sunYrotate<0){
				sunYrotate = sunYrotate+deacceleration;
				if (sunYrotate >= 0) {
					sunYrotate = 0;
				}
			}
		}
	}


	void ChangeSunPitch(){

		if (Input.GetKey ("q")) {

			if (sunXrotation < maxXrot) {
				sunXrotate = sensitivity;
			}
		} else if (Input.GetKey ("a")) {

			if (sunXrotation > minXrot) {
				sunXrotate = -sensitivity;
			}
		} else {
			sunXrotate = 0;
		}
	}



	void ChangeSunCoordinates(){

		if (Input.GetKey("left")) {
			
			sunYrotate = sensitivity;
		} else if (Input.GetKey ("right")) {
			
			sunYrotate = -sensitivity;
		} else {
			sunYrotate = 0;
		}
	}


}
