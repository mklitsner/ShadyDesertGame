using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertWandererAI : MonoBehaviour {
	bool inshade;
	[Range(0, 1)]public float heat;//0-1
	[Range(0, 1)]public float tiredness;//0-1

	public GameObject footprint;
	int footprintSide=1;

	const string asleep = "asleep";
	const string dead ="dead";
	const string wandering ="wandering";
	const string sawSomething ="sawSomething";
	const string moveTowardSomething ="moveTowardSomething";
	public string state =wandering;

	float speed=1;
	float currentspeed;

	float rotationSpeed;
	float rotationFrequency;

	Vector3 globalRotation;
	float turnangle;
	float newturnangle;
	float turnTime;

	public float wanderingtime;
	float initialwanderingtime;



	public Vector3 sunPosition;
	// Use this for initialization
	void Start () {
		state = "wandering";
		rotationSpeed=1;
		rotationFrequency=1;
		SetState (wandering);
		footprintSide = 1;
		initialwanderingtime = 5;
		wanderingtime = initialwanderingtime;
	}






	// Update is called once per frame
	void Update () {
		
		currentspeed = speed - (tiredness * speed);


		DetectShade ();


		//WHAT HAPPENS IF NOT IN THE SHADE
		if (!inshade) {
			SearchForShade ();
			if (state == sawSomething) {
				//if shade is seen, move toward it
				MoveForward(currentspeed);
				TurnToHead ();
			} else if (state == wandering) {
				MoveRandomly (currentspeed);
			}
		} 

		//WHAT HAPPENS IF IN THE SHADE
		else {
			if (state == wandering) {
				MoveRandomly (currentspeed);
			}
		}



		SetHeat (0.0001f,0.02f);
		SetTiredness (0.0001f, 0.02f);
		StateIndicator ();

	}





	void SetState(string _state){

		switch (_state) {
		case asleep:
			//goes to sleep for a short amount of time
			break;

		case dead:
			//dies and becomes cactus man
			break;

		case wandering:
			//gets up and continues walking
			StartCoroutine (FootPrintTiming (1));
			state = wandering;
			break;

		case sawSomething:
			//perks up at the shade he saw
			turnangle = transform.rotation.y;
			turnTime = 0;
			state = sawSomething;
			break;

		case moveTowardSomething:
			//perks up at the shade he saw
			break;
		}
	}




	void MoveForward(float _speed){
		transform.Translate (0, 0, _speed * Time.deltaTime);

	}

	void TurnToHead(){
		Vector3 _headDirection= transform.GetChild (0).transform.eulerAngles;
		if (turnTime >= 1) {
			turnTime = 1;
		} else {
			turnTime = turnTime + Time.deltaTime * rotationSpeed;
			//turn a certain amount
			Debug.DrawRay(transform.position,_headDirection);

			transform.rotation = Quaternion.Lerp (Quaternion.Euler (0, turnangle, 0), Quaternion.Euler (0, _headDirection.y, 0), turnTime);
		}
	}


	void MoveRandomly(float _speed){
		float newrotationFrequency = (heat + 1) * rotationFrequency;
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
				ResetWanderingTimer (initialwanderingtime - heat*0.5f*initialwanderingtime);
				newturnangle = newturnangle + Random.Range (90, 45) * RandomSign ();
			}
		}

	}

	int RandomSign(){
		if (Random.Range (0, 2) == 0) {
			return -1;
		} 
		return 1;
		
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




	void SearchForShade(){
		bool seeShade =transform.GetChild (0).GetChild(0).GetComponent<ShadeSearcher> ().inshade;

		if (seeShade) {
			if (state != sawSomething) {
				SetState (sawSomething);
			}
			print ("saw Shade");
		}
	}


	void DetectShade(){
		sunPosition = GameObject.Find ("sunTarget").transform.position;
		Ray ray = new Ray(transform.position, (sunPosition-transform.position));
		RaycastHit hit;
		//print (sunPosition - transform.position);
		Debug.DrawRay (transform.position, (sunPosition - transform.position));

		if (Physics.SphereCast (ray, 0.1f, out hit)) 
		{
			if (hit.transform.gameObject.name == "sunTarget") {
				inshade = false;
			} else {
				inshade = true;
			}
		}
	}








	private IEnumerator FootPrintTiming(float _duration){

		while (state==wandering)
		{
			footprintSide=-1*footprintSide;
			Vector3 footprintposition = new Vector3 (transform.localPosition.x + 0.05f*footprintSide, transform.localPosition.y-0.5f,transform.localPosition.z);
			GameObject footprintclone = (GameObject)Instantiate(footprint,footprintposition,Quaternion.Euler(180+footprintSide*90,transform.localEulerAngles.y,90+90*footprintSide));

			if(footprintSide==-1){
				
			}
			yield return new WaitForSeconds (_duration);
		}


	}




	void SetHeat(float _increaseheat,float _decreaseheat){
		if(inshade){
			if(heat<=0){
				heat = 0;
			}else{
				heat = heat-_decreaseheat;
				}
			}
		if(!inshade){
			if(heat>=1){
				heat = 1;
			}else{
				heat = heat +_increaseheat;
			}
		}
	}
		

	void SetTiredness(float _increasetiredness,float _decreasetiredness){
		if (heat > 0.7 && state == wandering) {
			if (tiredness >= 1) {
				tiredness = 1;
				if (inshade) {
					SetState (asleep);
				}
				} else {
					tiredness = tiredness + _increasetiredness;
				}

		}
		if (state == asleep) {
				if (inshade) {
					if (tiredness <= 0) {
						state = wandering;
					} else {
						tiredness = tiredness - _decreasetiredness;
					}
				} else {
					if (heat >= 1) {
						SetState (dead);
					}
				}
		}
	}
		



	void StateIndicator(){
		GetComponent<Renderer> ().material.color = new Color (heat, 0, 0);
	}





	}


	


