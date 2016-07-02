using UnityEngine;
using System.Collections;

public class OffCourseCorrection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collisionInfo) {
		print("Collision with " + collisionInfo.transform.name);
	}


	void OnCollisionStay(Collision collisionInfo) {
		print("Staying in collision with " + collisionInfo.transform.name);
	}
		
	void OnCollisionExit(Collision collisionInfo) {
		print("No longer in contact with " + collisionInfo.transform.name);
	}


}
