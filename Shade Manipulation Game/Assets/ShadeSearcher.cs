using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeSearcher : MonoBehaviour {
	public bool inshade;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		DetectShade();
	}

	void DetectShade(){
		Vector3 sunPosition = transform.parent.GetComponent<DesertWandererAI>().sunPosition;
		Ray ray = new Ray(transform.position, (sunPosition-transform.position));
		RaycastHit hit;
		//print (sunPosition - transform.position);
		Debug.DrawRay (transform.position, (sunPosition - transform.position));

		if (Physics.SphereCast (ray, 0.1f, out hit)) 
		{
			if (hit.transform.gameObject.name == "sunTarget") {
				inshade = false;
				GetComponent<MeshRenderer> ().enabled = false;
			} else {
				inshade = true;
				GetComponent<MeshRenderer> ().enabled = true;
			}
		}
	}
}
