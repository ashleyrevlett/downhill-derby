using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

	enum LevelState
	{
		Countdown,
		Racing,
		Finished // player is past finish line
	}

	private LevelState currentState;
	private GameObject startingLine;
	private GameObject finishLine;

	public GameObject menuUI;
	public GameObject levelUI;
	private Countdown countdown;
	private Timer timer;
	public ShowCurrentTime currentTime;
	private ShowPanels showPanels;

	void Awake() {
		countdown = levelUI.GetComponent<Countdown> ();
		timer = levelUI.GetComponent<Timer> ();
		showPanels = menuUI.GetComponent<ShowPanels> ();
	}
		
	void OnDisable() {
		EndLevel ();
	}
		
	public void DoCountdown() {
		currentState = LevelState.Countdown;
		levelUI.SetActive (true);
		countdown.enabled = true;
		showPanels.HidePanel ("FinishPanel");
		timer.StopTimer (); // don't show timer while doing countdown
		currentTime.HideTime();
	}

	// called by countdown when it's finished
	public void StartRace() {	
		currentState = LevelState.Racing;
		timer.gameObject.SetActive(true);
		timer.StartTimer ();
		currentTime.ShowTime();
	}
		
	// called from finish line when it collides with player
	public void EndRace() {
		currentState = LevelState.Finished;
		showPanels.ShowPanel ("FinishPanel");
		timer.StopTimer ();
		currentTime.HideTime ();
	}

	public void EndLevel() {
		countdown.enabled = false; // in case we quit before countdown finishes
		timer.StopTimer (); // in case we cut the race short
		if (currentTime.gameObject.activeInHierarchy)
			currentTime.HideTime (); // in case we cut the race short
		showPanels.HidePanel ("FinishPanel"); // in case we quit from finish
	}

}
