using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour {

	private LevelController lc;

	// Use this for initialization
	void Start () {
		GameObject lcObject = GameObject.FindGameObjectWithTag ("LevelController");
		if (lcObject != null)
			lc = lcObject.GetComponent<LevelController> ();
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Entered! " + other.name);
		if (other.gameObject.name == "ColliderBody" || other.gameObject.name == "ColliderBottom" || other.gameObject.name == "ColliderFront") {
			if (lc != null) 
				lc.EndRace ();
		}
	}

}
