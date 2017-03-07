using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusScript : MonoBehaviour {
	DesertWandererAI AI;
	// Use this for initialization
	void Start () {
		AI = GameObject.Find ("wanderer").GetComponent<DesertWandererAI> ();
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text> ().text = ("heat:" + AI.heat+"\n"+"tiredness:" + AI.tiredness+"\n"+ "state:" + AI.state);

		
	}
}
