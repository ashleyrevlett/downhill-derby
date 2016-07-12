using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

	public Transform spawnPoint1;
	public Transform spawnPoint2;
	public GameObject levelUI;
	GameObject startingLine;
	GameObject finishLine;
	GameController gc;
	Countdown countdown;
	Timer timer;
	ShowPanels showPanels;
	Rigidbody carBody;
	HighScores scores;
	Recorder recorder;

	public bool raceStarted = false;

	void Awake() {

		GameObject gcObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gcObject == null) 
			gcObject = Instantiate (Resources.Load ("GameController")) as GameObject;

		gc = gcObject.GetComponent<GameController> ();

		recorder = GetComponent<Recorder>();

		scores = gcObject.GetComponent<HighScores> ();

		countdown = GameObject.Find ("CountdownText").GetComponent<Countdown>();

		timer = gameObject.GetComponent<Timer> ();
		showPanels = gameObject.GetComponent<ShowPanels> ();

		carBody = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody>();
		carBody.isKinematic = true;

		Debug.Log ("LC Awake");
	}


	void SetCarStartingPosition() {

		// get best time recording for this level
		// set car at spawnPoint not == recording first position
		if (recorder != null) {
			RecordingData recording = recorder.GetRecording ();
			if (recording.states != null) {
				Vector3 startingPos = recording.states [0].position;
				float maxDist = .5f;
				float dist1 = Vector3.Distance (startingPos, spawnPoint1.position);
				if (dist1 <= maxDist) {
					carBody.gameObject.transform.position = spawnPoint2.position;
					carBody.gameObject.transform.rotation = spawnPoint2.rotation;
				} else {
					carBody.gameObject.transform.position = spawnPoint1.position;
					carBody.gameObject.transform.rotation = spawnPoint1.rotation;
				}
			} else {
				carBody.gameObject.transform.position = spawnPoint1.position;
				carBody.gameObject.transform.rotation = spawnPoint1.rotation;
			}
		}
	}

		
	void OnDisable() {
		EndLevel ();
	}

	// called at init of each race
	public void StartLevel() {
		Debug.Log ("LC STARTING LEVEL!");

		if (recorder != null)
			recorder.Setup (); 
		SetCarStartingPosition ();
			
		carBody.isKinematic = true; // can't go until countdown ends!
		timer.StopTimer (); // don't show timer while doing countdown
		showPanels.HidePanel ("FinishPanel");
		raceStarted = false;

		DoCountdown ();

	}
		
	public void DoCountdown() {
		countdown.gameObject.SetActive (true);
		countdown.enabled = true;
		Debug.Log ("LC doing countdown");
	}

	// called by countdown when it's finished
	public void StartRace() {	
		carBody.isKinematic = false;
		timer.StartTimer ();
		if (recorder != null) {
			recorder.StartRecording ();
			recorder.PlayRecording ();
		}
		raceStarted = true;
	}
		
	// called from finish line when it collides with player
	public void EndRace() {
		showPanels.ShowPanel ("FinishPanel");
		timer.StopTimer ();

		// update high scores
		scores.SetHighScore (gc.currentLevel, timer.timeElapsed);

		// save ghost
		print("timer.timeElapsed: " + timer.timeElapsed);
		print("scores.highscore_level1: " + scores.highscore_level1);
		if (recorder == null)
			return;
		
		if (timer.timeElapsed <= scores.highscore_level1) {
			print ("saving recording for level: " + gc.GetCurrentLevelName ());
			recorder.SaveRecording (gc.GetCurrentLevelName (), timer.timeElapsed);
		} else {
			print ("not saving recording, didn't beat high score");
		}
	}

	public void EndLevel() {
		countdown.enabled = false; // in case we quit before countdown finishes
		timer.StopTimer (); // in case we cut the race short
		showPanels.HidePanel ("FinishPanel"); // in case we quit from finish
	}

	public void RestartRace() {
		int scene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
		Debug.Log ("Restarting race...");
	}

	public void NextLevel() {
		if (gc == null)
			return;
		gc.NextLevel ();
	}

	public void ReturnToMenu() {
		gc.ShowMenuScene ();
	}

}
