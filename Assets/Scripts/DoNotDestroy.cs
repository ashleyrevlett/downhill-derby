using UnityEngine;
using System.Collections;

public class DoNotDestroy : MonoBehaviour {

	public void Awake()
	{
		DontDestroyOnLoad(this.gameObject);

		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}
	}
		
}
