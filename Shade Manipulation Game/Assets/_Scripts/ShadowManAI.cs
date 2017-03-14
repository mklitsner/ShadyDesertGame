using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowManAI : MonoBehaviour {

	public Vector3 sunPosition;
	public Vector3 currentDirection;
	Vector3 wandererDirection;
	bool hasDirection=false;


	bool shadowSensed;
	public bool inshade;

	string state;
	const string circumvent= "circumvent";
	const string evading= "evading";
	const string searching= "searching";
	const string tracking= "tracking";
	const string pursuing= "pursuing";
	const string engulfed= "engulfed";

	float speed;

	float alarm;

	float turnangle;
	float newturnangle;
	float turnTime;

	float rotationSpeed;
	float rotationFrequency;

	public float wanderingtime;
	float initialwanderingtime;


	// Use this for initialization
	void Start () {
		SetState (searching);
		speed = 1;
		rotationSpeed=1;
		rotationFrequency=1;
		initialwanderingtime = 5;
		wanderingtime = initialwanderingtime;
	}
	
	// Update is called once per frame
	void Update () {


		DetectShade ();

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




		
					//set current direction:
					//if footprint is found, set current direction to the footprint
					//if wanderer is in range, set current direction to wanderer
					//else, if no foot print or wanderer is found for x seconds, goes from tracking to searching


					//check if sensors are under shadows
					bool[] sensors=new bool[4];
					shadowSensed=false;
					CheckShadeSensors (sensors,shadowSensed);
					
						if (shadowSensed) {
						
						SetState (circumvent);
							}

				if (state == searching) {
			rotationSpeed = 1;
					MoveRandomly (speed+alarm);
					//if any children are inshade, go to circumvention
					
					//if a footprint is found, go to tracking(in collider script

					
					//if wanderer is in range, set current direction to wanderer and go to pursuing state



			if (alarm <= 0) {
				alarm = 0;
			} else {
				alarm=alarm-1.5f;
			}

				}else if(state == tracking){

			speed = 2;

			MoveForward (speed);
			if (turnTime < 1) {
				turnTime = turnTime + Time.deltaTime * rotationSpeed;
				//turn a certain amount
				transform.localRotation = Quaternion.Lerp (Quaternion.Euler (0, turnangle, 0), Quaternion.Euler (0, currentDirection.y, 0), turnTime);
			} else if (turnTime >= 1) {
					
				WanderingTimer ();
				turnTime = 1;

			}
			if (wanderingtime <= 0) {
				SetState (searching);
			
			}

					//move a certain amount, check for foot prints, 
//					if collide with footprints, changes current direction to footprint direction
				
					// if no footprints are seen for x seconds, go to searching

				}else if(state == pursuing){
					//move toward current direction

				}else if(state == circumvent){
			rotationSpeed = 10;
			speed = 2;

			if (sensors [0] || sensors [1]) {
				//sensed from the back
				//move forward slightly faster
				//rotate slightly less
				rotationSpeed = 5;
				speed = 5 + alarm;

			} else {
				rotationSpeed = 20;	
			}
				if (sensors [0] || sensors [3]) {
				//turn away from left
				transform.Rotate (0, rotationSpeed, 0);
				print ("rotating to right");
				} else if (sensors [1] || sensors [2]) {
					//turn away from right
				print ("rotating to left");
				transform.Rotate (0, -rotationSpeed, 0);
				//rotating;
				}

			if (!shadowSensed) {
					SetState (evading);
				
				//if there is no current direction to follow, he goes to searching

			}

			if(alarm<10){
				alarm = alarm + 2f;
			}
			MoveForward(speed);
						
			}else if(state == evading){
				MoveForward(speed);
			if (alarm <= 0) {
				alarm = 0;
				SetState (searching);
			} else {
				alarm=alarm-1.5f;
			}

				}
	}



	void SetState(string _state){
		print (_state);
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
			state = searching;
			hasDirection = false;

			break;

		case tracking:
			//if wanderer is out of range, shadowman tracks wanderers footsteps.
			hasDirection = true;
			wanderingtime=initialwanderingtime;
			turnTime = 0;
			turnangle = transform.eulerAngles.y;
			ResetWanderingTimer(1);
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


	void MoveForward(float _speed){
		transform.Translate (0, 0, _speed * Time.deltaTime);
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








	void CheckShadeSensors( bool[]_sensors,  bool _shadowSensed){
		for(int i=0; i < transform.childCount;i++){
			if (transform.GetChild (i).transform.GetComponent<ShadowManShadeSensor> ().inshade) {
				_sensors [i] = true;
				print (transform.GetChild (i).transform.name);
				shadowSensed = true;
			} else {
				_sensors [i] = false;
			}
	}
}



	void OnTriggerEnter(Collider col){
		
		if (col.transform.tag =="footprint"){
			print ("found footprint");
			StateIndicator ();
			SetState (tracking);
			if (col.transform.GetComponent<FootprintScript> ().footprintSide == 1) {
				currentDirection = new Vector3 (0, col.transform.eulerAngles.y-180, 0);
			} else {
				currentDirection = new Vector3 (0, col.transform.eulerAngles.y, 0);
			}
		}
	}

	void StateIndicator(){
		GetComponent<Renderer> ().material.color = new Color (1, 0, 0);
	}



}
