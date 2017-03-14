using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintScript : MonoBehaviour {

	public float lifespan;
	public float lifeleft;
	public Vector3 direction;
	public int footprintSide;

	// Use this for initialization
	void Start () {
		lifeleft = lifespan;
	}
	
	// Update is called once per frame
	void Update () {
		Timer ();

		if (lifeleft <= 0) {
			Destroy (gameObject);
		}

		direction = transform.eulerAngles;
		
	}


	void Timer(){
		lifeleft = lifeleft - Time.deltaTime;
	}
}
