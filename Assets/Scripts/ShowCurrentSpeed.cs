using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowCurrentSpeed : MonoBehaviour {

	Text speedText;
	Rigidbody carBody;

	// Use this for initialization
	void Start () {	
		carBody = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody> ();
		speedText = gameObject.GetComponent<Text> ();
	}

	void Update () {
		int currentSpeed = Mathf.RoundToInt(Mathf.Abs (carBody.velocity.magnitude));
		speedText.text = string.Format("{0}", currentSpeed);
	}
}
