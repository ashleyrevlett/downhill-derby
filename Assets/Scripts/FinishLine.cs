using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour {

	private GameController gc;

	// Use this for initialization
	void Start () {
		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Entered! " + other.name);
		if (other.gameObject.name == "ColliderBody" || other.gameObject.name == "ColliderBottom" || other.gameObject.name == "ColliderFront") {
			gc.CompleteRace ();
		}
	}

}
