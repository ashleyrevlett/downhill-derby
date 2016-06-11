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
	void Start () {
		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		countdownAudioSource = gameObject.AddComponent<AudioSource> ();
		countdownAudioSource.clip = countdownBeep;
		countdownText.gameObject.SetActive (false);
	}

	void OnDestroy() {
		StopCountdown ();
	}

	public void StartCountdown() {
		countdownText.gameObject.SetActive (true);
		StartCoroutine (DoCountdown ());
	}

	public void StopCountdown() {
		StopCoroutine ("DoCountdown");
		countdownText.gameObject.SetActive (false);
	}

	IEnumerator DoCountdown() {
		float secondsRemaining = 3f;
		while (secondsRemaining > 0f) {	
			countdownAudioSource.PlayOneShot(countdownAudioSource.clip, 1F);
			countdownText.text = string.Format ("{0}", secondsRemaining);
			secondsRemaining -= 1f;
			yield return new WaitForSeconds (1f);
		};
		countdownAudioSource.PlayOneShot(countdownBeepHigh, 1F);
		countdownText.gameObject.SetActive (false);
		gc.StartRace();
	}

}
