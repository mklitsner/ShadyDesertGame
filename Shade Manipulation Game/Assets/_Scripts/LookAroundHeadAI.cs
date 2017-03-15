using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAroundHeadAI : MonoBehaviour {

	public Quaternion globalLookRotation;
	float lookangle=45;
	float newlookangle=45;
	int lookdirection=1;
	float lookspeed=1;
	float turnTime=2;
	float gazeTime=1;


	public float maxDistance = 10;

	// Use this for initialization
	void Start () {
		lookangle=45;
		newlookangle = -45;
		lookspeed=1;
	}
	
	// Update is called once per frame
	void Update () {
		string parentState = transform.parent.GetComponent<DesertWandererAI> ().state;



		if (parentState == "wandering") {
			globalLookRotation = transform.rotation;
				HeadLookAroundRandom ();
//			print ("rotation " + globalLookRotation);
//			print ("localrotation " + transform.localEulerAngles.y);

		}
			
		if (parentState == "sawSomething") {
			transform.rotation = globalLookRotation;

		}

//		print ("rotation " + transform.eulerAngles.y);
//		print ("localrotation " + transform.localEulerAngles.y);
	}





	void HeadLookAroundRandom(){
		if (turnTime < 1) {
			
			turnTime = turnTime + Time.deltaTime * lookspeed;
			transform.localRotation = Quaternion.Lerp (Quaternion.Euler (90, 0, lookangle), Quaternion.Euler (90, 0, newlookangle), turnTime);

		} else {
			if (gazeTime > 0) {
				gazeTime = gazeTime - Time.deltaTime;
			} else {

					lookdirection = lookdirection * -1;
					lookangle = newlookangle;
					newlookangle = Random.Range (20, 70) * lookdirection;
				gazeTime = Random.Range (1, 3);
					turnTime = 0;	

			}
		}
			

	}





}
