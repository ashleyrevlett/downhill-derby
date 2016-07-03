using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour {

	public Text countdownText;
	public AudioClip countdownBeep;
	public AudioClip countdownBeepHigh;
	private AudioSource countdownAudioSource;
	private LevelController lc;
	private Pusher pusher; // so we can start pushing animation at last 2 seconds

	void Start() {
		lc = GameObject.FindGameObjectWithTag ("LevelController").GetComponent<LevelController> ();
		countdownAudioSource = gameObject.AddComponent<AudioSource> ();
		countdownAudioSource.playOnAwake = false;
		countdownAudioSource.clip = countdownBeep;
		pusher = GameObject.FindGameObjectWithTag ("Pusher").GetComponent<Pusher> ();
	}

	void OnEnable() {
		countdownText.gameObject.SetActive (true); // hide text until countdown begins
		StartCoroutine (DoCountdown ());
		Debug.Log ("Countdown Enabled");
	}

	void OnDisable() {
		countdownText.gameObject.SetActive (false);
		StopAllCoroutines ();
		Debug.Log ("Countdown DISabled");
	}
		
	IEnumerator DoCountdown() {
		Debug.Log ("DoCountdown");
		countdownText.gameObject.SetActive (true);
		float secondsRemaining = 3f;
		while (secondsRemaining > 0f && countdownText.gameObject.activeInHierarchy) {	
			yield return new WaitForSeconds (1f);
			countdownAudioSource.PlayOneShot(countdownAudioSource.clip, 1F);
			countdownText.text = string.Format ("{0}", secondsRemaining);
			secondsRemaining -= 1f;

			if (secondsRemaining == 0f)
				pusher.Push ();
			
		};
		yield return new WaitForSeconds (1f); 
		countdownAudioSource.PlayOneShot (countdownBeepHigh, 1F);
		lc.StartRace ();
		yield return new WaitForSeconds (1f); // wait for beep to end
		countdownText.gameObject.SetActive (false);

	}

}
