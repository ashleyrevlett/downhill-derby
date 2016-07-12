using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	public bool timerRunning { get; set; }
	public float timeElapsed { get; set; } // seconds

	void Start() {
		StopTimer ();
	}

	public void ResetTimer() {
		timeElapsed = 0f;
	}

	public void StopTimer() {
//		Debug.Log ("Stopping timer");
		timerRunning = false;
	}

	public void StartTimer() {
		timeElapsed = 0f;
		timerRunning = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (timerRunning) {
			timeElapsed += Time.deltaTime;
		}
	}
		
}
