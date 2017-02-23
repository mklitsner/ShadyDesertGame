using UnityEngine;
using System.Collections;

public class ReactiveTarget : MonoBehaviour {

	public void ReactToHit(){
		WanderingAI behavior = GetComponent<WanderingAI> ();
		if (behavior != null) {
			behavior.SetAlive (false);
		}
		StartCoroutine (Die ());
		behavior._audio.PlayOneShot (behavior.dyingSound);
	}

	private IEnumerator Die(){

		GetComponent<AnimationState> ().UpdateAnimation (AnimationState.CurrentAnimation.Dying);


		//GetComponent<AnimationState> ().UpdateAnimation (AnimationState.CurrentAnimation.Dying);

		//this.transform.Rotate (-75, 0, 0);

		//wait until animation is over
		yield return new WaitForSeconds(1.0f);

		Destroy (this.gameObject);
	}



}
