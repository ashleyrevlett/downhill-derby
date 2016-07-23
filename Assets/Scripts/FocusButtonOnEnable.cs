using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public class FocusButtonOnEnable : MonoBehaviour {

	public GameObject buttonToFocus;
	bool waitingToSelectFirst = false; // hackety hack

	void OnEnable() {
		buttonToFocus.SetActive (true);
		if (buttonToFocus != null && EventSystem.current != null) {
			EventSystem.current.SetSelectedGameObject (buttonToFocus);
		} else {
			waitingToSelectFirst = true;
		}
	}

	void OnGUI() {
		if(waitingToSelectFirst) {
			waitingToSelectFirst = false;   
			EventSystem.current.SetSelectedGameObject(buttonToFocus);
		}
	}

}
