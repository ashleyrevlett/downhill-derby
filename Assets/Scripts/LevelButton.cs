using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelButton : MonoBehaviour {

	public int level;
	GameController gc;
	Button levelButton;

	// Use this for initialization
	void Start () {
		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		levelButton = gameObject.GetComponent<Button>();
		levelButton.onClick.AddListener(() => gc.StartLevel(level));
		SetButtonState ();
	}

	void OnLevelWasLoaded(int level) {
		if (level == 0)
			SetButtonState ();
	}

	void SetButtonState () {
		if (gc == null)
			gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		if (levelButton == null)
			levelButton = gameObject.GetComponent<Button>();

		Debug.Log ("gc.farthestLevelReached: " + gc.farthestLevelReached);
		Debug.Log ("gc.level: " + level);

		if (gc.farthestLevelReached >= level)
			levelButton.interactable = true;
		else 
			levelButton.interactable = false;
		
	}
}
