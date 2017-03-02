using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertWandererAI : MonoBehaviour {
	bool inshade;
	[Range(0, 1)]float heat;//0-1
	[Range(0, 1)]float tiredness;//0-1



	Vector3 sunPosition;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		DetectShade ();
		SetHeat (0.01f);
		StateIndicator ();

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


	void SetState(int _state){

		switch (_state) {
		case 1:

			break;

		case 2:
			
			break;

		case 3:
			
			break;

		default:
			break;
		}
		}

	void SetHeat(float _deltaheat){
		if(inshade){
			if(heat<0){
				heat = 0;
			}else{
				heat = heat-_deltaheat;
				}
			}
		if(!inshade){
			if(heat>1){
				heat = 1;
			}else{
				heat = heat +_deltaheat;
			}
		}
	}

	void SetTiredness(float _deltatiredness){
		if(heat>0.5){
			if(tiredness>1){
				tiredness = 1;
			}else{
				tiredness = tiredness+_deltatiredness;
			}
		}
	}


	void StateIndicator(){
		GetComponent<Renderer> ().material.color = new Color (heat, 0, 0);
		Debug.Log (heat);
	}


	}


	


