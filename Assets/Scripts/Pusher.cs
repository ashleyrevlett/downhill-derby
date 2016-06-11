using UnityEngine;
using System.Collections;

public class Pusher : MonoBehaviour {

	public float pushForce = 10f;
	public float maxDistanceToPlayer = 4;
	private Animator animator;
	private GameObject playerCar;
	private Rigidbody playerBody;
	private AudioSource audio;

	void Awake () {
		animator = gameObject.GetComponent<Animator> ();
		playerCar = GameObject.FindGameObjectWithTag ("Player");
		playerBody = playerCar.GetComponent<Rigidbody> ();
		audio = GetComponent<AudioSource>();
	}
		
	void Update() {
		float dist = Vector3.Distance (gameObject.transform.position, playerCar.transform.position);
		if (dist <= maxDistanceToPlayer && Input.GetKeyDown (KeyCode.Space)) {
			Push (true);
		}
	}

	void Push(bool isPushing) {
		animator.SetBool ("isPushing", isPushing);
		if (isPushing) {			
			audio.PlayOneShot(audio.clip, 0.7F);
			Vector3 force = playerCar.transform.forward * pushForce;
			Debug.DrawLine(playerCar.transform.position, playerCar.transform.position + playerCar.transform.forward, Color.blue, 5f);
			playerBody.AddForce(force, ForceMode.Impulse);
			Debug.Log ("force: " + force);	
		}
	}

}
