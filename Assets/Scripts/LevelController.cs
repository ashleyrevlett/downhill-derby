﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

	private GameObject startingLine;
	private GameObject finishLine;

	public GameObject levelUI;
	private GameController gc;
	private Countdown countdown;
	private Timer timer;
	private ShowPanels showPanels;
	private Rigidbody carBody;
	private HighScores scores;

	public bool raceStarted = false;

	void Awake() {

		GameObject gcObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gcObject == null) 
			gcObject = Instantiate (Resources.Load ("GameController")) as GameObject;

		gc = gcObject.GetComponent<GameController> ();

		scores = gcObject.GetComponent<HighScores> ();

		countdown = GameObject.Find ("CountdownText").GetComponent<Countdown>();
		countdown.enabled = false; 

		timer = gameObject.GetComponent<Timer> ();
		showPanels = gameObject.GetComponent<ShowPanels> ();

		carBody = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody>();

	}
		
	void OnDisable() {
		EndLevel ();
	}

	// called at init of each race
	public void StartLevel() {
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
		raceStarted = true;
	}
		
	// called from finish line when it collides with player
	public void EndRace() {
		showPanels.ShowPanel ("FinishPanel");
		timer.StopTimer ();

		// update high scores
		scores.SetHighScore (gc.currentLevel, timer.timeElapsed);
		
	}

	public void EndLevel() {
		countdown.enabled = false; // in case we quit before countdown finishes
		timer.StopTimer (); // in case we cut the race short
		showPanels.HidePanel ("FinishPanel"); // in case we quit from finish
	}

	public void RestartRace() {
		int scene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
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
