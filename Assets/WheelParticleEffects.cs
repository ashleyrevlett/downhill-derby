using UnityEngine;
using System.Collections;

public class WheelParticleEffects : MonoBehaviour {

	public GameObject[] powerupParticles;

	void Start () {
		foreach (GameObject obj in powerupParticles) {
			if (obj.activeInHierarchy)
				obj.SetActive (false);
		}
	}
	
	public void ShowParticles() {
		print ("Showing particles!");
		StartCoroutine (ShowParticlesRoutine ());
	}

	IEnumerator ShowParticlesRoutine() {
		foreach (GameObject obj in powerupParticles) {
			if (!obj.activeInHierarchy)
				obj.SetActive (true);
		}
		yield return new WaitForSeconds(1f);
		foreach (GameObject obj in powerupParticles) {
			if (obj.activeInHierarchy)
				obj.SetActive (false);
		}
		yield return null;
	}


}
