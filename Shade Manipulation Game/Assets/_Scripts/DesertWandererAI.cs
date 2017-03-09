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



	public Vector3 sunPosition;
	// Use this for initialization
	void Start () {
		state = wandering;
		rotationSpeed=1;
		rotationFrequency=1;
		SetState (wandering);
		footprintSide = 1;
	}






	// Update is called once per frame
	void Update () {
		

		currentspeed = speed - (tiredness * speed);


		DetectShade ();

		if (!inshade) {
			SearchForShade ();
			if (state == sawSomething) {
				MoveForward (currentspeed);
			} else if (state == wandering) {
				MoveRandomly (currentspeed);
			}
		} else {
			if (state == wandering) {
				MoveRandomly (currentspeed);
			}
		}



		SetHeat (0.001f,0.02f);
		SetTiredness (0.001f, 0.02f);
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
			StartCoroutine(FootPrintTiming (1));
			break;

		case sawSomething:
			//perks up at the shade he saw
			break;
		case moveTowardSomething:
			//perks up at the shade he saw
			break;
		}
	}




	void MoveForward(float _speed){
		transform.Translate (0, 0, _speed * Time.deltaTime);
	}

	void MoveRandomly(float _speed){
		float newrotationFrequency = (heat + 1) * rotationFrequency;
		float _angle = Mathf.Cos (Time.time*newrotationFrequency)*rotationSpeed;
		//float angle = Random.Range (-10, 10);
		//transform.Rotate (0, angle, 0);
		transform.Translate (0, 0, _speed * Time.deltaTime);
		transform.Rotate (0, _angle, 0);

	}

	void SearchForShade(){
		bool seeShade =transform.GetChild (0).GetChild(0).GetComponent<ShadeSearcher> ().inshade;

		if (seeShade) {
			SetState (sawSomething);

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
			Vector3 footprintposition = new Vector3 (transform.localPosition.x + 0.05f*footprintSide, transform.localPosition.y,transform.localPosition.z);
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


	


