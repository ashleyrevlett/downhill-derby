using UnityEngine;
using System.Collections;

public class RaceController : MonoBehaviour {

	enum LevelState
	{
		Countdown,
		Racing,
		Finished // player is past finish line
	}

	private LevelState currentState;


	// Use this for initialization
	void OnEnable() {
		currentState = LevelState.Countdown;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
