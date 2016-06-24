using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour {

	public Text countdownText;
	public AudioClip countdownBeep;
	public AudioClip countdownBeepHigh;
	private AudioSource countdownAudioSource;
	private LevelController lc;

	void Start() {
		lc = GameObject.FindGameObjectWithTag ("LevelController").GetComponent<LevelController> ();
		countdownAudioSource = gameObject.AddComponent<AudioSource> ();
		countdownAudioSource.playOnAwake = false;
		countdownAudioSource.clip = countdownBeep;
	}

	void OnEnable() {
		countdownText.gameObject.SetActive (true); // hide text until countdown begins
		StartCoroutine (DoCountdown ());
	}

	void OnDisable() {
		countdownText.gameObject.SetActive (false);
//		StopAllCoroutines ();
	}
		
	IEnumerator DoCountdown() {
		Debug.Log ("DoCountdown");
		yield return new WaitForSeconds (1f); // give the camera time to move to position
		countdownText.gameObject.SetActive (true);
		float secondsRemaining = 3f;
		while (secondsRemaining > 0f && countdownText.gameObject.activeInHierarchy) {	
			countdownAudioSource.PlayOneShot(countdownAudioSource.clip, 1F);
			countdownText.text = string.Format ("{0}", secondsRemaining);
			secondsRemaining -= 1f;
			yield return new WaitForSeconds (1f);
		};
		if (countdownText.gameObject.activeInHierarchy) {
			countdownAudioSource.PlayOneShot (countdownBeepHigh, 1F);
			countdownText.gameObject.SetActive (false);
			lc.StartRace ();
		};
	}

}
