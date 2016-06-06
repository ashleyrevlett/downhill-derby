using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public float forwardForce = 1500f;
	private GameObject playerCar;
	private Rigidbody playerBody;
	private AudioSource audio;
	private bool isColliding = false;
	private MeshRenderer mesh;

	// Use this for initialization
	void Start () {
		playerCar = GameObject.FindGameObjectWithTag ("Player");
		playerBody = playerCar.GetComponent<Rigidbody>();
		mesh = gameObject.GetComponent<MeshRenderer> ();
		audio = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Reset() {
		isColliding = false;
		mesh.enabled = true;
		gameObject.SetActive (true);
	}


	void OnTriggerEnter(Collider other) {
		if (isColliding)
			return;
		
		if (other.gameObject.name == "ColliderBody" || other.gameObject.name == "ColliderBottom" || other.gameObject.name == "ColliderFront") {
			isColliding = true;
			Debug.Log ("POWERUP");
			audio.PlayOneShot(audio.clip, 0.7F);
			Vector3 force = playerCar.transform.forward * forwardForce;
			playerBody.AddForce(force, ForceMode.Impulse);
			StartCoroutine (Disappear ());
		}
	}

	IEnumerator Disappear() {
		mesh.enabled = false;
		yield return new WaitForSeconds(1);
		gameObject.SetActive (false);
	}

}
