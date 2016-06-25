using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {


	private GameObject startingLine;
	private GameObject finishLine;

	private GameController gc;
	public GameObject levelUI;
	private Countdown countdown;
	private Timer timer;
	private ShowPanels showPanels;

	void Awake() {

		GameObject gcObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gcObject == null) 
			gcObject =   Instantiate (Resources.Load ("GameController")) as GameObject;
			gc = gcObject.GetComponent<GameController> ();

		countdown = GameObject.Find ("CountdownText").GetComponent<Countdown>();

		timer = gameObject.GetComponent<Timer> ();
		showPanels = gameObject.GetComponent<ShowPanels> ();
	}
		
	void OnDisable() {
		EndLevel ();
	}
		
	public void DoCountdown() {
		countdown.enabled = true;
		showPanels.HidePanel ("FinishPanel");
		timer.StopTimer (); // don't show timer while doing countdown
	}

	// called by countdown when it's finished
	public void StartRace() {	
		timer.StartTimer ();
	}
		
	// called from finish line when it collides with player
	public void EndRace() {
		showPanels.ShowPanel ("FinishPanel");
		timer.StopTimer ();
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

}
