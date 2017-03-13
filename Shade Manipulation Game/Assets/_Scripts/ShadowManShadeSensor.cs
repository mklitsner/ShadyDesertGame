using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowManShadeSensor : MonoBehaviour {
	public bool inshade;
	public Vector3 sunPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		DetectShade ();


	}

	void DetectShade(){
		Vector3 sunPosition = transform.parent.GetComponent<ShadowManAI>().sunPosition;
		Ray ray = new Ray(transform.position, (sunPosition-transform.position));
		RaycastHit hit;
		//print (sunPosition - transform.position);
		Debug.DrawRay (transform.position, (sunPosition - transform.position));

		if (Physics.SphereCast (ray, 0.1f, out hit)) 
		{
			if (hit.transform.gameObject.name == "sunTarget") {
				inshade = false;
				GetComponent<MeshRenderer> ().enabled = false;
			} else if(hit.transform.gameObject.name == "shadowman"){
				inshade = false;
				GetComponent<MeshRenderer> ().enabled = false;
			}else{
				inshade = true;
				GetComponent<MeshRenderer> ().enabled = true;
			}
		}
	}
}
