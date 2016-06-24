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
		public bool unlocked { get; set; } // level may be raced
		bool complete; // level has been beat
		float bestTime; // best time so far for level
	}

	public GameObject levelControllerObject;
	LevelController lc;
	public AudioClip introMusic;
	public bool inMainMenu { get; set; }
	public bool raceStarted { get; set; }
	public GameObject menuUI;
	private ShowPanels showPanels;
	private Pause pause;
	public int currentLevel = 0; // offset by 1 like array
	private int farthestLevelReached = 1; // how far the player has progresssed
	public LevelInfo[] levels;
	AudioSource mainAudio;
	FadeScene fader;

	// start from title scene only
	void Start () {
		showPanels = menuUI.GetComponent<ShowPanels> ();
		fader = menuUI.GetComponent<FadeScene> ();
		pause = menuUI.GetComponent<Pause> ();
		mainAudio = gameObject.GetComponent<AudioSource> ();
		lc = levelControllerObject.GetComponent<LevelController> ();
		ShowMenuScene ();
	}

	// returning to title scene from level pause or finish
	private void ShowMenuScene() {

		currentLevel = 0;

		// if not in menu scene yet, load it
		Scene currentScene = SceneManager.GetActiveScene();
		if (currentScene.buildIndex != 0)
			SceneManager.LoadScene (0);

		pause.UnPause(); // may enter from pause, so unpause just in case
		showPanels.ShowPanel("MenuPanel"); // in case we are starting from level with disabled menu

		// disable level controller during menu scene
		lc.enabled = false;

		// setup music
		mainAudio.Stop ();
		mainAudio.clip = introMusic;
		mainAudio.Play ();

		// init menu level buttons
		for (int i = 0; i < levels.Length; i++) {
			Button levelButton = levels [i].levelSelectButton;
			if (farthestLevelReached >= i ) {
				levelButton.interactable = true;
				levels [i].unlocked = true;
			} else {
				levelButton.interactable = false;
				levels [i].unlocked = false;
			}
		}

	}


	// param == normal; not offset by 1
	public void StartLevel(int levelNumber) {
		
		currentLevel = levelNumber;

		if (levelNumber == 0) {
			ShowMenuScene ();
			return;
		} else {
			showPanels.HidePanel("MenuPanel"); // in case we are starting from main menu
			StartCoroutine (LoadDelayed (levels[levelNumber - 1]));
		}

	}
		

	private IEnumerator LoadDelayed(LevelInfo level) {

		pause.UnPause (); // in case we are restarting level from pause screen

		fader.FadeToBlack ();

		yield return new WaitForSeconds (fader.animTime);

		SceneManager.LoadScene (level.buildSceneNumber);

		lc.enabled = true;
		lc.DoCountdown ();

		mainAudio.Stop (); // in case we are restarting
		mainAudio.clip = levels [level.buildSceneNumber - 1].levelMusic;
		mainAudio.Play ();
	}
		

	public void RestartLevel() {
		StartLevel (currentLevel);
	}

}
