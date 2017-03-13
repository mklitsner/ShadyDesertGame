using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowManAI : MonoBehaviour {

	public Vector3 sunPosition;
	Vector3 currentDirection;
	public bool inshade;

	string state;
	const string circumvent= "circumvent";
	const string evading= "evading";
	const string searching= "searching";
	const string tracking= "tracking";
	const string pursuing= "pursuing";
	const string engulfed= "engulfed";

	float speed;

	float turnangle;
	float newturnangle;
	float turnTime;

	float rotationSpeed;
	float rotationFrequency;

	public float wanderingtime;
	float initialwanderingtime;


	// Use this for initialization
	void Start () {
		state = searching;
		speed = 1;
		rotationSpeed=1;
		rotationFrequency=1;
		initialwanderingtime = 5;
		wanderingtime = initialwanderingtime;
	}
	
	// Update is called once per frame
	void Update () {
		DetectShade ();

		print ("shadowschildren"+transform.childCount);
		/* if his outer sensor gets touched by a shadow,
		 * he turns away from it while moving forward  until it is no longer touching that sensor
		 */

		/*
		 * if his inner sensor gets touched by a shadow,
		 * he reacts very quickly and runs away from the shadow, moving faster than normal for a short amount of time
		 */

		/*
		 *if no shadows are touching his sensors,
		 *he randomly searches until he finds footsteps of the wanderer(or the wanderer)
		 *he follows those footsteps by reading their direction and walking in that direction.(so we will need a current direction)
		 *if he encounters a shadow, he takes evasive measures until he is safe, then will continue tracking the wanderer
		 *if the wanderer comes into range at any point, the shadow will come after him, ignoring the footsteps. again, he will move arund shadows
		 */




				if (state == searching) {
					MoveRandomly (speed);
					//if any children are inshade, go to circumvention

				}else if(state == tracking){
					//move a certain amount,
				}else if(state == pursuing){

				}else if(state == circumvent){
					//if the sensor being touched is one of the front,
					//turn as you're moving forward

					//if the sensor being touched is one of the back,
					//turn slightly and move fast for a moment

				}else if(state == evading){

				}
	}



	void SetState(string _state){

		switch (_state) {
		case circumvent:
			//if he gets close to shadow, he turns until he is no longe moving toward it. this will be detected by four shadow sensors
			state=circumvent;
			break;

		case evading:
			//if he feels like a shadow is moving too close to him or toward him, he tries to run away from it.
			//this will be detected by four close sensors
			state=evading;
			break;

		case searching:
			//if wanderer is out of range, and shadowman sees no footsteps, shadowman randomly wanders in search of wanderer's footsteps or the wanderer
			state=searching;
			break;

		case tracking:
			//if wanderer is out of range, shadowman tracks wanderers footsteps.
			state=tracking;
			break;

		case pursuing:
			//shadowman sees wanderer and pursues him (float pursuit range, pursuit speed)
			state=pursuing;
			break;

		case engulfed:
			//shadowman is swallowed by a shadow
			state=engulfed;
			break;

		}
	}


	void MoveRandomly(float _speed){
		float newrotationFrequency = rotationFrequency;
		float _angle = Mathf.Cos (Time.time * newrotationFrequency) * rotationSpeed;
		//float angle = Random.Range (-10, 10);
		//transform.Rotate (0, angle, 0);
		transform.Translate (0, 0, _speed * Time.deltaTime);
		transform.Rotate (0, _angle, 0);


		if(wanderingtime>0){
			WanderingTimer ();
			turnangle = transform.localEulerAngles.y;
			turnTime = 0;
		}else if (wanderingtime <= 0) {
			turnTime = turnTime + Time.deltaTime * rotationSpeed;
			//turn a certain amount
			transform.localRotation = Quaternion.Lerp (Quaternion.Euler (0,turnangle,0), Quaternion.Euler (0, newturnangle, 0), turnTime);
			if (turnTime >= 1) {
				ResetWanderingTimer (initialwanderingtime - 0.5f*initialwanderingtime);
				newturnangle = newturnangle + Random.Range (90, 45) * RandomSign ();
			}
		}

	}


	void WanderingTimer(){
		//wanders in a direction for a certain amount of time, 
		//if its doesnt see anything or bump into its own foot prints, it turns;
		wanderingtime=wanderingtime-Time.deltaTime;
	}

	void ResetWanderingTimer(float _initialTime){
		if (wanderingtime <= 0) {
			wanderingtime = _initialTime;
		}

	}

	int RandomSign(){
		if (Random.Range (0, 2) == 0) {
			return -1;
		} 
		return 1;

	}

	void DetectShade(){
		Vector3 topPos= new Vector3(0,0.5f,0);
		Vector3 bottomPos = new Vector3(0,-0.5f,0);

		sunPosition = GameObject.Find ("sunTarget").transform.position;
		Ray bottomRay = new Ray(transform.position+bottomPos, (sunPosition-transform.position));
		RaycastHit bottomHit;
		Ray topRay = new Ray(transform.position+topPos, (sunPosition-transform.position));
		RaycastHit topHit;
		//print (sunPosition - transform.position);
		Debug.DrawRay (transform.position+bottomPos, (sunPosition - transform.position));
		Debug.DrawRay (transform.position+topPos, (sunPosition - transform.position));

		if (Physics.SphereCast (bottomRay, 0.1f, out bottomHit)&&Physics.SphereCast (topRay, 0.1f, out topHit)) 
		{
			if (topHit.transform.gameObject.name == "sunTarget"&& bottomHit.transform.gameObject.name == "sunTarget") {
				inshade = false;
			} else {
				inshade = true;
			}
		}
	}




	void CheckShadeSensors(){
		bool[] sensors= new bool[4];
		for(int i=0; i < transform.childCount;i++){
			if (transform.GetChild (i).transform.GetComponent<ShadowManShadeSensor> ().inshade) {
				sensors [i] = true;
			}
				
	}
}
}
