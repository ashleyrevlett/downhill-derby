using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowAtFinish : MonoBehaviour {

	public GameObject finishPanel;
	public Text finishTimeText;

	public void ShowStats( float timeElapsed ) {
		finishPanel.SetActive (true);
		finishTimeText.text = string.Format ("Final Time: {0:0}:{1:00}", Mathf.Floor (timeElapsed / 60), timeElapsed % 60);
	}

	public void HideStats() {
		finishPanel.SetActive (false);
	}

}
