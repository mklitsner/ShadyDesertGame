using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertWandererAI : MonoBehaviour {
	bool inshade;
	[Range(0, 1)]float heat;//0-1
	[Range(0, 1)]float tiredness;//0-1
	int state =wandering;
	int asleep = 1;
	int dead =2;
	int wandering =3;
	int sawSomething =4;

	float speed;

	public Vector3 sunPosition;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float currentspeed;

		currentspeed = speed - (tiredness * speed);

		if (state == wandering) {
		}

		DetectShade ();
		if (!inshade) {
		SearchForShade ();
		}
		SetHeat (0.1f);
		StateIndicator ();

	}





	void SetState(int _state){

		switch (_state) {
		case asleep:
			//goes to sleep for a short amount of time
			break;

		case dead:
			//dies and becomes cactus man
			break;

		case wandering:
			//gets up and continues walking
			break;

		case sawSomething:
			//perks up at the shade he saw
			break;
		}
	}



	void MoveForward(){
		
	}

	void MoveRandomly(){
	}

	void SearchForShade(){
		bool seeShade =transform.GetChild ().GetComponent<ShadeSearcher> ().inshade;

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






	void SetHeat(float _increaseheat,float _decreaseheat){
		if(inshade){
			if(heat<0){
				heat = 0;
			}else{
				heat = heat-_decreaseheat;
				}
			}
		if(!inshade){
			if(heat>1){
				heat = 1;
			}else{
				heat = heat +_increaseheat;
			}
		}
	}
		

	void SetTiredness(float _increasetiredness,float _decreasetiredness){
		if (heat > 0.7 && state == wandering) {
			if (tiredness > 1) {
				tiredness = 1;
				if (inshade) {
					SetState (asleep);
				} else {
					tiredness = tiredness + _increasetiredness;
				}
			}
		}
		if (state = asleep) {
				if (inshade) {
					if (tiredness < 0) {
						tiredness = 0;
						state = wandering;
					} else {
						tiredness = tiredness - _decreasetiredness;
					}
				} else {
					if (heat => 1) {
						SetState (dead);
					}
				}
		}
	}
		



	void StateIndicator(){
		GetComponent<Renderer> ().material.color = new Color (heat, 0, 0);
		Debug.Log (heat);
	}


	}


	


