using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertWandererAI : MonoBehaviour {
	bool inshade;
	[Range(0, 1)]public float heat;//0-1
	[Range(0, 1)]public float tiredness;//0-1
	 public int state =wandering;
	const int asleep = 1;
	const int dead =2;
	const int wandering =3;
	const int sawSomething =4;

	float speed=1;
	float currentspeed;

	public Vector3 sunPosition;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		

		currentspeed = speed - (tiredness * speed);

		if (state == wandering) {
		}

		DetectShade ();

		if (!inshade) {
		SearchForShade ();
			if (state == sawSomething) {
				MoveForward (currentspeed);
			} else if(state == wandering){
				MoveRandomly(currentspeed);
			}
		}



		SetHeat (0.001f,0.02f);
		SetTiredness (0.001f, 0.02f);
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


	private void LookAhead(){
		

		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;

		//draw the ray for debugging
		//Debug.DrawLine(transform.position, transform.position + (transform.forward * obstacleRange), Color.yellow);
		//Debug.DrawLine (transform.position + (transform.forward + obstacleRange), transform.position + (transform.forward + obstacleRange) + (transform.forward + 0.3f), Color.red);

		if (Physics.SphereCast (ray, 0.75f, out hit)) {
			GameObject hitObject = hit.transform.gameObject;

		}
	}

	void MoveForward(float _speed){
		transform.Translate (0, 0, _speed * Time.deltaTime);
	}

	void MoveRandomly(float _speed){
		//float angle = Random.Range (-10, 10);
		//transform.Rotate (0, angle, 0);
		transform.Translate (0, 0, _speed * Time.deltaTime);
		print ("moving");
	}

	void SearchForShade(){
		bool seeShade =transform.GetChild (0).GetComponent<ShadeSearcher> ().inshade;

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
		Debug.Log (heat);
	}


	}


	


