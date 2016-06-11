using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

	public GameObject startingLine;
	public GameObject finishLine;
	public bool raceStarted = false;
	private float timeElapsed; // seconds
	public Text timeText;
	public Text countdownText;
	public GameObject finishPanel;
	public Text finishTimeText;
	private Vector3 carStartPosition;
	private Quaternion carStartRotation;
	private GameObject playerCar;
	private Rigidbody playerBody;
	public AudioClip countdownBeep;
	public AudioClip countdownBeepHigh;
	private AudioSource countdownAudioSource;
	private List<PowerUp> powerUps;

	void Start () {
		playerCar = GameObject.FindGameObjectWithTag ("Player");
		playerBody = playerCar.GetComponent<Rigidbody> ();
		carStartPosition = playerCar.transform.position;
		carStartRotation = playerCar.transform.rotation;
		countdownAudioSource = gameObject.AddComponent<AudioSource> ();
		countdownAudioSource.clip = countdownBeep;

		GameObject[] powerObjects = GameObject.FindGameObjectsWithTag ("PowerUp");
		powerUps = new List<PowerUp> ();
		foreach (GameObject p in powerObjects) {
			powerUps.Add(p.GetComponent<PowerUp>());
		}

		RestartLevel ();
	}

	void Update() {
//		if (Input.GetKeyDown (KeyCode.Escape)) {
//			RestartLevel ();
//		}
	}

	public void RestartLevel() {
		foreach (PowerUp p in powerUps) {
			p.Reset (); 
		}
		Time.timeScale = 1f;
		raceStarted = false;
		finishPanel.SetActive (false);
		playerCar.transform.position = carStartPosition;
		playerCar.transform.rotation = carStartRotation;
		playerBody.velocity = Vector3.zero;
		playerBody.angularVelocity = Vector3.zero;
		StartCoroutine (Countdown ());
	}

	IEnumerator Countdown() {
		yield return new WaitForSeconds (1f);
		timeText.enabled = false;
		countdownText.enabled = true;
		float secondsRemaining = 3f;
		while (secondsRemaining > 0f) {			
			countdownAudioSource.PlayOneShot(countdownAudioSource.clip, 1F);
			countdownText.text = string.Format ("{0}", secondsRemaining);
			yield return new WaitForSeconds (1f);
			secondsRemaining -= 1f;
		};
		countdownAudioSource.PlayOneShot(countdownBeepHigh, 1F);
		countdownText.enabled = false;
		StartRace ();
	}

	void StartRace() {	
		raceStarted = true;
		StartCoroutine (Timer());
	}
		
	IEnumerator Timer() {
		timeElapsed = 0f;
		timeText.enabled = true;
		while (raceStarted) {
			timeElapsed += Time.deltaTime;
			timeText.text = string.Format ("{0:0}:{1:00}", Mathf.Floor (timeElapsed / 60), timeElapsed % 60);
			yield return null;
		}
	}
		
	public void EndRace() {
		raceStarted = false; // halts coroutine automatically
		finishPanel.SetActive (true);
		finishTimeText.text = string.Format ("Final Time: {0:0}:{1:00}", Mathf.Floor (timeElapsed / 60), timeElapsed % 60);
		Time.timeScale = 0f;
	}



}
