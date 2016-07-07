using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CarUserControl : MonoBehaviour {
	
	private CarController m_Car; // the car controller we want to use
	private Recorder recorder;

    private void Awake() {
        // get the car controller
        m_Car = GetComponent<CarController>();
		recorder = GetComponent<Recorder> ();
    }
		
    private void FixedUpdate() {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
		m_Car.Move(h, 0f, 0f, 0f);
		if (recorder != null) {
			recorder.SetSteering (h);
		}
    }

}