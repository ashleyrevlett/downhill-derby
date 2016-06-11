using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour {

	public Text countdownText;
	public AudioClip countdownBeep;
	public AudioClip countdownBeepHigh;
	private AudioSource countdownAudioSource;
	private GameController gc;

	// Use this for initialization
	void Awake () {
		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		countdownAudioSource = gameObject.AddComponent<AudioSource> ();
		countdownAudioSource.clip = countdownBeep;
	}

	public void StartCountdown() {
		StartCoroutine (DoCountdown ());
	}

	IEnumerator DoCountdown() {
		countdownText.gameObject.SetActive (true);
		yield return new WaitForSeconds (1f);
		float secondsRemaining = 3f;
		while (secondsRemaining > 0f) {			
			countdownAudioSource.PlayOneShot(countdownAudioSource.clip, 1F);
			countdownText.text = string.Format ("{0}", secondsRemaining);
			yield return new WaitForSeconds (1f);
			secondsRemaining -= 1f;
		};
		countdownAudioSource.PlayOneShot(countdownBeepHigh, 1F);
		countdownText.gameObject.SetActive (false);
		gc.StartRace();
	}


}
