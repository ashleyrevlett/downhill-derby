using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour {

	private LevelController level;

	// Use this for initialization
	void Start () {
		level = GameObject.FindGameObjectWithTag ("GameController").GetComponent<LevelController> ();
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Entered! " + other.name);
		if (other.gameObject.name == "ColliderBody" || other.gameObject.name == "ColliderBottom" || other.gameObject.name == "ColliderFront") {
			level.EndRace ();
		}
	}

}
