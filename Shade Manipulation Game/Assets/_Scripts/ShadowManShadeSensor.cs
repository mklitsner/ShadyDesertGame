using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowManShadeSensor : MonoBehaviour {
	public bool inshade;
	public Vector3 sunPosition;
	public bool inClosePosition;
	public bool testing=true;

	// Use this for initialization
	void Start () {
		transform.localPosition = new Vector3 (transform.localPosition.x * 0.5f, transform.localPosition.y, transform.localPosition.z * 0.5f);
		inClosePosition = true;
	}

	// Update is called once per frame
	void LateUpdate () {

		if (!inClosePosition) {
			transform.localPosition = new Vector3 (transform.localPosition.x * 2, transform.localPosition.y, transform.localPosition.z * 2);
			inClosePosition = true;
		} else {
			transform.localPosition = new Vector3 (transform.localPosition.x * 0.5f, transform.localPosition.y, transform.localPosition.z * 0.5f);
			inClosePosition = false;
		}
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
				
			} else if(hit.transform.gameObject.tag == "shadowman"||hit.transform.gameObject.name == "wanderer"){
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
}
