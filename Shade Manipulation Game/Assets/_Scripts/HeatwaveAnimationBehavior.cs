using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatwaveAnimationBehavior : MonoBehaviour {

	float heat;
	Vector3 MinScale;
	Vector3 MaxScale;
	Animator anim;

	public float initialScale;
	public float endScale;

	// Use this for initialization
	void Start () {


		MinScale = new Vector3 (initialScale, initialScale, initialScale);
		MaxScale = new Vector3 (endScale, endScale, endScale);
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		heat = transform.parent.transform.parent.GetComponent<DesertWandererAI> ().heat;

		float alpha = transform.GetComponent<SpriteRenderer> ().color.a;

		if (heat < 0.2) {
			transform.localScale = Vector3.Lerp (MinScale, MaxScale, heat);
			anim.speed = 0.5f;
		}else{
			//animate it moving faster
			anim.speed = heat/0.2f*0.5f;
			}
			
		

		//as heat increases, heatwave gets bigger and faster

		
	}
}
