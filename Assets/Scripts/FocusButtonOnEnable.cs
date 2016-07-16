using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public class FocusButtonOnEnable : MonoBehaviour {

	public GameObject buttonToFocus;

	void OnEnable() {
		EventSystem.current.SetSelectedGameObject (buttonToFocus);
	}


}
