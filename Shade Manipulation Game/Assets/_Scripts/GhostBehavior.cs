using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehavior : MonoBehaviour {

	float speed;
	float scaleX;
	float scaleY;
	float offset;
	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler (50, 0, 0);
		speed = 0.3f;
		offset = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate ((Mathf.Cos((Time.time-offset)*10)/10), speed, 0);
		transform.localScale = new Vector3 (scaleX, scaleY,1);

	

		scaleY = scaleY + 0.05f;
		scaleX = scaleX + 0.01f;


		if (transform.position.y > 60) {
			Destroy (gameObject);
		}

	}
}
