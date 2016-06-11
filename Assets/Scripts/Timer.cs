using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	public bool timerRunning { get; set; }
	public Text timeText;
	public float timeElapsed { get; set; } // seconds

	void Awake() {
		timerRunning = false;
	}

	public void StartTimer() {
		timeText.gameObject.SetActive (true);
		timeText.enabled = true;
		timerRunning = true;
		timeElapsed = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (timerRunning) {
			timeElapsed += Time.deltaTime;
			timeText.text = string.Format ("{0:0}:{1:00}", Mathf.Floor (timeElapsed / 60), timeElapsed % 60);
		}
	}
		
}
