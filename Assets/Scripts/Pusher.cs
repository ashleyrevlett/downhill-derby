using UnityEngine;
using System.Collections;

public class Pusher : MonoBehaviour {

	[SerializeField]
	public float pushForce = 10f;

	[SerializeField]
	public float maxDistanceToPlayer = 4;

	[SerializeField]
	public float maxDistanceToAnimate = 1f;

	private Animator animator;
	private GameObject playerCar;
	private Rigidbody playerBody;
	private CarController car;
	private AudioSource audio;
	private PushMeter meter;
	private LevelController lc;
	private bool canPush = true;


	void Awake () {
		animator = gameObject.GetComponent<Animator> ();
		playerCar = GameObject.FindGameObjectWithTag ("Player");
		playerBody = playerCar.GetComponent<Rigidbody> ();
		car = playerCar.GetComponent<CarController> ();
		audio = GetComponent<AudioSource>();
		meter = GameObject.FindGameObjectWithTag ("LevelController").GetComponent<PushMeter> ();
		lc = GameObject.FindGameObjectWithTag ("LevelController").GetComponent<LevelController> ();
	}
		
	void Update() {
		
		if (!canPush)
			return;
		
		float dist = Vector3.Distance (gameObject.transform.position, playerCar.transform.position);

		if (Input.GetKeyDown (KeyCode.Space) && lc.raceStarted) 
			Push ();
		
		if (dist > maxDistanceToAnimate) 
			animator.SetBool ("isPushing", false);
		
		if (dist > maxDistanceToPlayer)
			canPush = false;

	}

	public void Push() {
		StopAllCoroutines ();
		animator.SetBool ("isPushing", true);					
		audio.PlayOneShot(audio.clip, 0.7F);
		Vector3 force = playerCar.transform.forward * pushForce;
		playerBody.AddForce(force, ForceMode.Impulse);
//		car.Move (0f, pushForce, 0f, 0f);

		meter.GoUp();
	}


}
