using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	[System.Serializable]
	public struct LevelInfo {
		public int buildSceneNumber; // scene number from build settings
		public float maxTime; // max time for level to be complete before reset
		public AudioClip levelMusic;
		public Button levelSelectButton; // from main UI
		public bool unlocked; // level may be raced
		bool complete; // level has been beat
		float bestTime; // best time so far for level
	}

	public bool inMainMenu { get; set; }
	public bool raceStarted { get; set; }
	public GameObject menuUI;
	private ShowPanels showPanels;
	public GameObject levelUI;
	private Countdown countdown;
	private ShowAtFinish showAtFinish;
	private Timer timer;
	public int currentLevel = 0; // offset by 1 like array
	public LevelInfo[] levels;
	AudioSource mainAudio;

	// Use this for initialization
	void Start () {

		raceStarted = false;
		inMainMenu = true;
		showPanels = menuUI.GetComponent<ShowPanels> ();
		mainAudio = gameObject.GetComponent<AudioSource> ();
		countdown = levelUI.GetComponent<Countdown> ();
		timer = levelUI.GetComponent<Timer> ();
		showAtFinish = levelUI.GetComponent<ShowAtFinish> ();

		for (int i = 0; i < levels.Length; i++) {

			Debug.Log ("LEVELS LOADING!!!" + i + " : " + currentLevel);

			Button levelButton = levels [i].levelSelectButton;
			if (i + 1 > currentLevel) {
				Debug.Log ("setting to not interactable " + i);
				levelButton.interactable = false;
				levels [i].unlocked = false;
			} else {
				Debug.Log ("setting to YES interactable " + i);
				levelButton.interactable = true;
				levels [i].unlocked = true;
			}
		}

	}


	// param == normal; not offset by 1
	public void StartLevel(int levelNumber) {

		if (levelNumber > levels.Length)
			return;

		StopAllCoroutines ();

		//Hide the main menu UI element
		inMainMenu = false;
		showPanels.HideMenu ();

		LevelInfo level = levels [levelNumber - 1];
		Debug.Log ("level.buildSceneNumber: " + level.buildSceneNumber);
		SceneManager.LoadScene ((int)level.buildSceneNumber);

		mainAudio.Stop ();
		mainAudio.clip = levels [levelNumber - 1].levelMusic;
		mainAudio.Play ();

		currentLevel = levelNumber;

	}
		
	void OnLevelWasLoaded(int level) {
		if (level != 0) {
			levelUI.SetActive (true);
			countdown.StartCountdown ();
		} else {
			levelUI.SetActive (false);
		}
	}
		
	// called when countdown ends, from countdown
	public void StartRace() {	
		raceStarted = true;
		timer.StartTimer ();
	}

	public void CompleteRace() {
		showAtFinish.ShowStats (timer.timeElapsed);
	}

	public void RestartLevel() {
		StartLevel (currentLevel);
	}

}
