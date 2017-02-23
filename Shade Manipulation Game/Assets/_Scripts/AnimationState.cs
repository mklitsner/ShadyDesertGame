using UnityEngine;
using System.Collections;

public class AnimationState : MonoBehaviour {

	//type of current animation
	public enum CurrentAnimation {Walking, Eating, Dying,isEating,Eaten};
	//current animation
	public CurrentAnimation curAnim;

	void Start(){
		curAnim = CurrentAnimation.Walking;
	}


	public void UpdateAnimation (CurrentAnimation _curanim){

		curAnim = _curanim;
		if (curAnim == CurrentAnimation.Walking) {
			GetComponent<Animator>().SetTrigger ("walking");
		}
		if (curAnim == CurrentAnimation.Eating) {
			GetComponent<Animator>().SetTrigger ("eating");
		}
		if (curAnim == CurrentAnimation.Dying) {
			GetComponent<Animator>().SetTrigger ("dying");
		}
		if (curAnim == CurrentAnimation.isEating) {
			GetComponent<Animator>().SetBool ("isEating",true);
		}
		if (curAnim == CurrentAnimation.Eaten) {
			GetComponent<Animator>().SetBool ("isEating",false);
		}
	}










//	public enum CurrentAnimation {Walking, Eating, Dying};
//	public CurrentAnimation curAnim;
//	
//	void Start () 
//	{
//		curAnim = CurrentAnimation.Walking;
//	}
//
//	public void UpdateAnimation(CurrentAnimation _curAnim)
//	{
//		curAnim = _curAnim;
//
//		if (curAnim == CurrentAnimation.Walking) {
//			GetComponent<Animator>().SetTrigger ("walking");
//		}
//		if (curAnim == CurrentAnimation.Eating) {
//			GetComponent<Animator>().SetTrigger ("eating");
//		}
//		if (curAnim == CurrentAnimation.Dying) {
//			GetComponent<Animator>().SetTrigger ("dying");
//		}
//	}
}
