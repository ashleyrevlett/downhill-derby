using UnityEngine;
using System.Collections;

public class CarAINavMeshControl : MonoBehaviour {

	public float acceleration = 20f;
	public float turnSpeed = 10f;
	public GameObject navLeader;
	public Transform target;
	private NavMeshAgent navAgent;
	private NavMeshPath path;
	private float elapsed = 0.0f;
	private CarController m_Car; // the car controller we want to use

	void Start () {
		m_Car = GetComponent<CarController>();
		navAgent = navLeader.GetComponent<NavMeshAgent>();
		navAgent.updateRotation = false;
		navAgent.updatePosition = false;
//		path = new NavMeshPath();
//		elapsed = 0.0f;
		navAgent.SetDestination(target.position);
	}

	void FixedUpdate() {
//		Vector3 relativePos = new Vector3(navAgent.steeringTarget.x, transform.position.y, navAgent.steeringTarget.z) - transform.position;
//		Quaternion rotation = Quaternion.LookRotation(relativePos);
//		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime*


		//				public void Move(float steering, float accel, float footbrake, float handbrake)

//		navAgent.Warp(m_Car.gameObject.transform.position);
		if (!navAgent.pathPending)
		{
			if (navAgent.remainingDistance <= navAgent.stoppingDistance) {
				if (!navAgent.hasPath || navAgent.desiredVelocity.sqrMagnitude == 0f) {
					// Done
					Debug.Log ("Doing nothing, remainingDistance:" + navAgent.remainingDistance + ", stoppingDist: " + navAgent.stoppingDistance);
				}
			} else {
				Vector3 carForward = m_Car.gameObject.transform.forward;
				Vector3 carToTarget = (navAgent.steeringTarget - m_Car.gameObject.transform.position).normalized;
				Quaternion desiredRotation = Quaternion.LookRotation(carToTarget);
				Quaternion currentRotation = m_Car.gameObject.transform.rotation;

				Debug.DrawLine (m_Car.gameObject.transform.position, m_Car.gameObject.transform.position + carForward, Color.blue);
				Debug.DrawLine (m_Car.gameObject.transform.position, carToTarget, Color.yellow);

				// get a numeric angle for each vector, on the X-Z plane (relative to world forward)
				float angleA = Mathf.Atan2(carForward.x, carForward.z) * Mathf.Rad2Deg;	
				float angleB = Mathf.Atan2 (carToTarget.x, carToTarget.z) * Mathf.Rad2Deg;
				float angleDiff = Mathf.DeltaAngle( angleA, angleB );
				angleDiff = Mathf.Clamp (angleDiff, -90f, 90f);

				float scaledAngle = angleDiff / 90f;	
				Debug.Log ("diff: " + angleDiff + ", scaled: " + scaledAngle + ", " + navAgent.steeringTarget);
				m_Car.Move(scaledAngle, Mathf.Clamp01(acceleration), 0f, 0f);
			}
		}

		Debug.Log ("navAgent.nextPosition: " + navAgent.nextPosition + ", pos: " + navAgent.gameObject.transform.position);
		navAgent.nextPosition = navAgent.gameObject.transform.position + navAgent.gameObject.transform.forward;


		for (int i = 0; i < navAgent.path.corners.Length-1; i++)
			Debug.DrawLine(navAgent.path.corners[i], navAgent.path.corners[i+1], Color.white);		


	}
}