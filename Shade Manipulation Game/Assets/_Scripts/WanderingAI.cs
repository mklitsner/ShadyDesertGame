using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WanderingAI : MonoBehaviour {
	public float speed = 3.0f;
	public float obstacleRange = 5.0f;
	public float pVisionRange = 7.0f;
	public const float baseSpeed = 3.0f;
	private bool _alive;

	[SerializeField] private float askewAngle = 15.0f;

	[SerializeField] private GameObject fireballPrefab;
	private GameObject _fireball;

//	public  GameObject remainingObject;
//	private TextUpdater remainingTextUpdate;
	private int bluePolesLeft;

	public AudioSource _audio;
	public AudioClip walkingSound;
	public AudioClip eatingSound;
	public AudioClip dyingSound;

//	private string status;
	private string state;
//	public GameObject _StateMachineText;
	public GameObject _StateMachineIcon;
//	public GameObject _MainPanel;



//	void Awake() {
//		//Base speed that is adjusted by the speed setting
//		Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged); 
//	}

	void Start() {
		_alive = true;
//		remainingTextUpdate = remainingObject.GetComponent<TextUpdater> ();

		bluePolesLeft = GameObject.FindGameObjectsWithTag ("blueTag").Length;
		state = "wandering";
		_audio = GetComponent<AudioSource> ();
		SetState (state);
	}

	void Update(){

//		remainingTextUpdate.UpdateText(bluePolesLeft.ToString());
	

		if (_alive && (state != "alarmed")) {
			transform.Translate (0, 0, speed * Time.deltaTime);
			LookAhead ();
			LookAskew (askewAngle);
			LookAskew (-askewAngle);
		}

		if (_audio.isPlaying== false) {
			_audio.PlayOneShot (walkingSound);
		}
	}

	private void LookAhead(){
		

		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;

		//draw the ray for debugging
		Debug.DrawLine(transform.position, transform.position + (transform.forward * obstacleRange), Color.yellow);
		//Debug.DrawLine (transform.position + (transform.forward + obstacleRange), transform.position + (transform.forward + obstacleRange) + (transform.forward + 0.3f), Color.red);

		if (Physics.SphereCast (ray, 0.75f, out hit)) {
			GameObject hitObject = hit.transform.gameObject;


			if(hitObject.GetComponent<PlayerCharacter>()) {

				if (state!="alarmed") {
					SetState ("alarmed");

				}

				if(_fireball == null) {
					_fireball = Instantiate(fireballPrefab) as GameObject;
					_fireball.transform.position = transform.TransformPoint (Vector3.forward * 1.5f);
					_fireball.transform.rotation = transform.rotation;
				}
			}

			//eat the blue post
			if (hit.distance < 0.5) {
				if (hit.transform.gameObject.tag == "blueTag") {
					Destroy (hit.transform.gameObject);
					bluePolesLeft--;

//					GetComponent<AnimationState> ().UpdateAnimation (AnimationState.CurrentAnimation.Eaten);

				}
			}


			// sees something close that is untagged so run away and go red
			if (hit.distance < obstacleRange) {
				if(hit.transform.gameObject.tag != "blueTag"){
					float angle = Random.Range (-110, 110);
					transform.Rotate (0, angle, 0);
					gameObject.GetComponent<Renderer> ().material.color = Color.red;
		

				}
				// sees a blueTagged post don't do anything but change to cyan
				else {
//					Debug.Log ("Looks like a blue tag infront of me");

					GetComponent<AnimationState> ().UpdateAnimation (AnimationState.CurrentAnimation.Eating);
		

//					GetComponent<AnimationState>().UpdateAnimation (AnimationState.CurrentAnimation.Eating);
					gameObject.GetComponent<Renderer> ().material.color = Color.cyan;
				}
			}
			//doesn't see anything nearby so make it yellow
			else
				gameObject.GetComponent<Renderer> ().material.color = Color.yellow;
			GetComponent<AnimationState> ().UpdateAnimation (AnimationState.CurrentAnimation.Walking);

//			GetComponent<AnimationState> ().UpdateAnimation (AnimationState.CurrentAnimation.Eaten);

		}
	}

	private void LookAskew(float angle)
	{

		Vector3 rayAngle = Quaternion.Euler (0, angle, 0) * transform.forward;

		Ray ray = new Ray (transform.position, rayAngle);
		RaycastHit hit;

		Debug.DrawLine(transform.position, transform.position + (rayAngle * pVisionRange), Color.green);

		if(Physics.SphereCast( ray, 0.1f, out hit))
		{
			if((hit.transform.gameObject.tag == "blueTag") && (hit.distance < pVisionRange))
				{
				//change where it moves towards to be at the same y as itself
				Vector3 tmpTargetPosition;
				tmpTargetPosition = hit.transform.position;
				tmpTargetPosition.y = transform.position.y;
				transform.LookAt (tmpTargetPosition);

				if (state != "locked") {
					SetState ("locked");
				}

				//transform.Rotate(0, angle, 0);
				}
		}
	}


	public void SetState(string _state){
		state = _state;
		switch (_state) {
		case "wandering":
			Debug.Log ("case:" + _state);

//			_StateMachineText.GetComponent<Text>().text=_state;

			_StateMachineIcon.GetComponent<SpriteControl> ().SetIconState (2);

//			_MainPanel.GetComponent<Image> ().color = Color.white;

			break;

		case "locked":
			Debug.Log ("case:" + _state);

//			_StateMachineText.GetComponent<Text>().text=_state;

//			_MainPanel.GetComponent<Image> ().color = Color.white;

			_StateMachineIcon.GetComponent<SpriteControl> ().SetIconState (1);

			break;

		case "alarmed":
			Debug.Log ("case:" + _state);

//			_StateMachineText.GetComponent<Text> ().text = _state;

			_StateMachineIcon.GetComponent<SpriteControl> ().SetIconState (0);

//			_MainPanel.GetComponent<Image> ().color = Color.magenta;

			StartCoroutine (Stunned(3.0f));

			break;

		default:
		break;
		}
	}

	public void SetAlive(bool alive){
		_alive = alive;
	}

	void OnDestroy() {
		Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
	}
	private void OnSpeedChanged(float value) {
		speed = baseSpeed * value;
	}

	private IEnumerator Stunned(float _duration){
		yield return new WaitForSeconds (_duration);

		SetState ("wandering");
	}

}