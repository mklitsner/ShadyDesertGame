using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeSearcher : MonoBehaviour {
	public bool inshade;
	float Yposition;
	float Zposition;
	public bool testing=true;

	void Start () {
		Zposition = transform.localPosition.z;
	}
	
	// Update is called once per frame
	void Update () {
		ScanForShade();
		DetectShade();

	}

	void DetectShade(){
		Vector3 sunPosition = transform.parent.parent.GetComponent<DesertWandererAI>().sunPosition;
		Ray ray = new Ray(transform.position, (sunPosition-transform.position));
		RaycastHit hit;
		//print (sunPosition - transform.position);
		Debug.DrawRay (transform.position, (sunPosition - transform.position));

		if (Physics.SphereCast (ray, 0.1f, out hit)) 
		{
			if (hit.transform.gameObject.name == "sunTarget") {
				inshade = false;
				GetComponent<MeshRenderer> ().enabled = false;
			} else if(hit.transform.gameObject.name == "wanderer"||hit.transform.gameObject.name == "head"||hit.transform.gameObject.tag == "shadowman"){
				inshade = false;
				GetComponent<MeshRenderer> ().enabled = false;
			}else{
				inshade = true;
				if (testing) {
					GetComponent<MeshRenderer> ().enabled = true;
				}
			}
		}
	}


	void ScanForShade(){
		float maxdistance =30;

		Yposition= transform.localPosition.y ;



		if (Yposition < maxdistance) {
			transform.Translate(0,0,1);
		} else if(Yposition>=maxdistance){
			transform.localPosition = new Vector3 (0, 2, Zposition);
		}

			
	}


}
