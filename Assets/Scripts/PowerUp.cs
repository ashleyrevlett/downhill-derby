﻿using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;


public class PowerUp : MonoBehaviour {

	private float forwardForce = 3500f;
	private float blurTimeToEnd = 2f;
	private GameObject playerCar;
	private Rigidbody playerBody;
	private AudioSource audio;
	private bool isColliding = false;
	private MeshRenderer mesh;
	private VignetteAndChromaticAberration vignette;


	// Use this for initialization
	void Start () {
		playerCar = GameObject.FindGameObjectWithTag ("Player");
		playerBody = playerCar.GetComponent<Rigidbody>();
		mesh = gameObject.GetComponent<MeshRenderer> ();
		audio = GetComponent<AudioSource>();
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
			audio.PlayOneShot(audio.clip, 0.7F);
			Vector3 force = playerCar.transform.forward * forwardForce;
			playerBody.AddForce(force, ForceMode.Impulse);
			StartCoroutine (BlurScreen ());
			StartCoroutine (Disappear ());
		}
	}

	IEnumerator BlurScreen() {

		float timeElapsed = 0f;
		float maxBlur = .7f;
		if (vignette != null)			
			vignette.blur = maxBlur;

		while (timeElapsed < blurTimeToEnd) {
			float percentComplete = timeElapsed / blurTimeToEnd;
			float newBlur = Mathf.Lerp (maxBlur, 0f, percentComplete);
			vignette.blur = newBlur;
			timeElapsed += Time.deltaTime;
//
//			Debug.Log ("timeElapsed: " + timeElapsed);
//			Debug.Log ("percentComplete: " + percentComplete);
//			Debug.Log ("newBlur: " + newBlur);

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
