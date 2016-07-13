using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;


public class PowerUp : MonoBehaviour {

	private float forwardForce = 3500f;
	private float blurTimeToEnd = 2f;
	private GameObject playerCar;
	private Rigidbody playerBody;
	private AudioSource powerUpaudio;
	private bool isColliding = false;
	private MeshRenderer mesh;
	private VignetteAndChromaticAberration vignette;


	// Use this for initialization
	void Start () {
		playerCar = GameObject.FindGameObjectWithTag ("Player");
		playerBody = playerCar.GetComponent<Rigidbody>();
		mesh = gameObject.GetComponent<MeshRenderer> ();
		powerUpaudio = GetComponent<AudioSource>();
		vignette = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<VignetteAndChromaticAberration> ();
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
			StopAllCoroutines ();
			isColliding = true;
			Debug.Log ("POWERUP");
			powerUpaudio.PlayOneShot(powerUpaudio.clip, 0.7F);
			Vector3 force = playerCar.transform.forward * forwardForce;
			playerBody.AddForce(force, ForceMode.Impulse);
			StartCoroutine (BlurScreen ());
			StartCoroutine (Disappear ());
		}
	}

	IEnumerator BlurScreen() {

		if (vignette == null)	
			vignette = Camera.main.GetComponent<VignetteAndChromaticAberration> ();
		if (vignette == null)
			yield break;

		float timeElapsed = 0f;
		float maxBlur = .7f;

		vignette.blur = maxBlur;

		while (timeElapsed < blurTimeToEnd) {
			float percentComplete = timeElapsed / blurTimeToEnd;
			float newBlur = Mathf.Lerp (maxBlur, 0f, percentComplete);
			vignette.blur = newBlur;
			timeElapsed += Time.deltaTime;
			yield return null;
		}

		if (vignette != null)			
			vignette.blur = 0f;

	}

	IEnumerator Disappear() {
		mesh.enabled = false;
		yield return new WaitForSeconds (blurTimeToEnd);
		gameObject.SetActive (false);
	}

}
