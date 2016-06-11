using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour {

	private GameController gc;

	// Use this for initialization
	void Start () {
		GameObject gcObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gcObject != null)
			gc = gcObject.GetComponent<GameController> ();
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Entered! " + other.name);
		if (other.gameObject.name == "ColliderBody" || other.gameObject.name == "ColliderBottom" || other.gameObject.name == "ColliderFront") {
			if (gc != null) 
				gc.CompleteRace ();
		}
	}

}
