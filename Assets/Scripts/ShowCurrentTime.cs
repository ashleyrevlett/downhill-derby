using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowCurrentTime : MonoBehaviour {

	private Timer theTimer;
	private Text timeText;

	// Use this for initialization
	void Awake () {
		theTimer = GameObject.FindObjectOfType<Timer> ();
		timeText = gameObject.GetComponent<Text> ();
	}

	void Start() {
		UpdateText ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateText ();
	}

	void UpdateText() {
		if (timeText != null)
			timeText.text = string.Format ("{0:0}:{1:00}", Mathf.Floor (theTimer.timeElapsed / 60), theTimer.timeElapsed % 60);
	}

	public void HideTime() {
		timeText.gameObject.SetActive (false);
	}

	public void ShowTime() {
		timeText.gameObject.SetActive (true);
	}

}
