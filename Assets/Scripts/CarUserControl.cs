using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CarUserControl : MonoBehaviour
{
//		public float engineScaleFactor = .2f;
	private CarController m_Car; // the car controller we want to use


    private void Awake()
    {
        // get the car controller
        m_Car = GetComponent<CarController>();
    }


    private void FixedUpdate()
    {
        // pass the input to the car!
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
		m_Car.Move(h, 0f, 0f, 0f);
    }
}
