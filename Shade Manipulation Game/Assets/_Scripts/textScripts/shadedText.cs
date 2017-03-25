using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class shadedText : MonoBehaviour {
	Vector3 sunPosition;
	public bool inshade;
	public float offset;
	public float offsetX;
	float timeFactor;
	float colorTiming;
	Color initialColor;
	public Color activeColor;
	public Color selectColor;
	public string destination;
	// Use this for initialization
	void Start () {
		initialColor = transform.GetComponent<Text> ().color;
	}
	
	// Update is called once per frame
	void Update () {
		DetectShade ();

		if (inshade) {
			if (timeFactor == 0) {
				timeFactor = Time.time;
			}
			colorTiming = Mathf.PingPong (Time.time-timeFactor, 1);
			LoadSelection ();
		} else {
			if (colorTiming > 0.1f) {
				colorTiming = Mathf.PingPong (Time.time - timeFactor, 1);
			} else {
				colorTiming = 0;
				timeFactor = 0;
			}

		}

		transform.GetComponent<Text> ().color=Color.Lerp(initialColor, activeColor,colorTiming);
		
	}

	void DetectShade(){
		sunPosition = GameObject.Find ("sunTarget").transform.position;

		Vector3 position=new Vector3(transform.position.x+offsetX,transform.position.y,transform.position.z+offset);
		Ray ray = new Ray(position, (sunPosition-position));
		RaycastHit hit;
	
		//print (sunPosition - transform.position);


		if (Physics.SphereCast (ray, 0.1f, out hit)) 
		{
			Debug.DrawRay (position, sunPosition - position);
			if (hit.transform.gameObject.name == "sunTarget") {
				inshade = false;
			} else {
				inshade = true;
			}
		}
	}



	void LoadSelection(){
		if (Input.GetKey ("space")) {
			SceneManager.LoadSceneAsync (destination);
	}


	




}
}
