using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowBestTime : MonoBehaviour {

	public int level; // which level best time to show
	private Text theText; // assumes this script is attached to a text object
	private HighScores scores;

	// Use this for initialization
	void Start () {
		theText = gameObject.GetComponent<Text> ();
		scores = GameObject.FindGameObjectWithTag ("GameController").GetComponent<HighScores> ();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float theTime = scores.GetHighScore (level);
		if (theTime != 0f)
			theText.text = string.Format ("{0:0}:{1:00}", Mathf.Floor (theTime / 60), theTime % 60);
		
	}
}
