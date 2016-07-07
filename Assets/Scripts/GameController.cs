using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	[System.Serializable]
	public struct LevelInfo {
		public string levelName;
		public int buildSceneNumber; // scene number from build settings
		public float maxTime; // max time for level to be complete before reset
		public bool unlocked { get; set; } // level may be raced
		public bool complete; // level has been beat
		float bestTime; // best time so far for level
	}

	public bool startFromMenu = true; // so we can run the scene from a level for testing

	LevelController lc;
	public bool inMainMenu { get; set; }
	public bool raceStarted { get; set; }
	private ShowPanels showPanels;
	private Pause pause;
	public int currentLevel = 0; // offset by 1 like array
	public int farthestLevelReached = 1; // how far the player has progresssed
	public LevelInfo[] levels;
	FadeScene fader;

	// start from title scene only
	void Start () {
		showPanels = gameObject.GetComponent<ShowPanels> ();
		fader = gameObject.GetComponent<FadeScene> ();
		pause = gameObject.GetComponent<Pause> ();

		// in case we forget to change this in inspector
		if (currentLevel <= farthestLevelReached)
			farthestLevelReached = currentLevel + 1;

		if (startFromMenu || currentLevel == 0) {
			ShowMenuScene ();
		} else {
			StartLevel (currentLevel);
		}

	}

	// returning to title scene from level pause or finish
	public void ShowMenuScene() {

		currentLevel = 0;

		// if not in menu scene yet, load it; may be returning to menu from race
		Scene currentScene = SceneManager.GetActiveScene();
		if (currentScene.buildIndex != 0)
			SceneManager.LoadScene (0);

		pause.UnPause(); // may enter from pause, so unpause just in case
		showPanels.ShowPanel("MenuPanel"); // in case we are starting from level with disabled menu

	}

	public bool isLevelComplete (int level) {
		return levels [level - 1].complete;
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

	}


	void OnLevelWasLoaded(int level) {
		if (level != 0) {
			lc = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController> ();
			lc.StartLevel ();
		}

	}
		

	public void RestartLevel() {
		StartLevel (currentLevel);
	}


	public void NextLevel() {
		if (currentLevel + 1 <= levels.Length) {
			farthestLevelReached++;
			StartLevel (currentLevel + 1);
		} else {
			Debug.Log ("Last level complete!");
			// TODO game winning scene
		}
	}


	public string GetCurrentLevelName() {
		if (currentLevel > 0) {
			return levels [currentLevel].levelName;
		}
		return "Menu";
	}


}
