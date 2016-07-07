using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class Recorder : MonoBehaviour {
	public String fileName;
	Rigidbody body;
	float totalTimeElapsed;
	List<CarState> states;
	Recordings recordings;
	int counter;
	bool isRecording = false;
	GameController gc;
	GameObject ghost;

	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		body = player.GetComponent<Rigidbody> ();
		ghost = Instantiate (Resources.Load ("Ghost")) as GameObject;	
		print ("player.transform.position: " + player.transform.position);
		ghost.transform.position = player.transform.position;
		ghost.transform.rotation = player.transform.rotation;
//		ghost.SetActive (false);
		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		states = new List<CarState> ();
		recordings = new Recordings();
		totalTimeElapsed = 0f;
		counter = 0;
		Load ();
	}

	void FixedUpdate () {
		if (!isRecording)
			return;

		totalTimeElapsed += Time.fixedDeltaTime;
		CarState newState = new CarState ();
		newState.timeElapsed = totalTimeElapsed;
		newState.velocity = (SerializableVector3)body.velocity;
		newState.position = (SerializableVector3)body.gameObject.transform.position;
		newState.rotation = (SerializableQuaternion)body.gameObject.transform.rotation;
		states.Add (newState);

	}

	// set from car controller
	public void SetSteering(float steering) {
		if (states == null || states.Count == 0)
			return;
		CarState theState = states [states.Count - 1];
		theState.steering = steering;
	}

	public void StartRecording() {
		totalTimeElapsed = 0f;
		counter = 0;
		states.Clear ();
		isRecording = true;
		Debug.Log ("Recording");
	}

	public void StopRecording() {
		isRecording = false;
	}

	public void PlayRecording() {
		RecordingData recording = recordings.GetRecording (gc.GetCurrentLevelName ());
		if (recording.states != null) {
			ghost.SetActive (true);
			StartCoroutine (MoveGhost (recording));
		}
	}

	IEnumerator MoveGhost(RecordingData recording) {
 		List<CarState> reverseList = new List<CarState>(recording.states);
		reverseList.Reverse ();
		Stack stateStack = new Stack (reverseList);
		float timeElapsed = 0f;
		while (stateStack.Count > 0) {
			
			CarState currentState = (CarState)stateStack.Peek ();

			while (timeElapsed >= currentState.timeElapsed && stateStack.Count > 1) {
				ghost.transform.position = currentState.position;
				ghost.transform.rotation = currentState.rotation;
				stateStack.Pop ();
				currentState = (CarState)stateStack.Peek ();
			}

			timeElapsed += Time.deltaTime;

			yield return null;

		}

//		ghost.SetActive (false);
	}

	public void SaveRecording(string levelName, float timeElapsed) {
		StopRecording ();

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/" + fileName);
		Debug.Log ("Saved to " + file.Name);
		// store current run info
		RecordingData data = new RecordingData();
		data.states = states;
		data.levelName = levelName;
		data.totalTime = timeElapsed;

		// if we've already saved this level, replace the old one, or add it if not
		recordings.SetRecording(levelName, data);

		bf.Serialize(file, recordings);
		file.Close();
	}


	public void Load() {
		Debug.Log ("Loading " + Application.persistentDataPath + "/" + fileName);
		if (File.Exists(Application.persistentDataPath + "/" + fileName)) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);
			Recordings data = (Recordings)bf.Deserialize(file);
			file.Close();
			recordings = data;
		}
	}


}
	
[Serializable]
class Recordings {
	public List<RecordingData> recordings;

	public Recordings() {
		recordings = new List<RecordingData> ();
	}

	public RecordingData GetRecording(string levelName) {
		foreach (RecordingData r in recordings) {
			if (r.levelName == levelName)
				return r;
		}
		RecordingData data = new RecordingData ();
		return data;
	}

	public void SetRecording(string levelName, RecordingData data) {
		RecordingData oldRecording = GetRecording (levelName);
		if (oldRecording.states != null)
			oldRecording = data;
		else
			recordings.Add (data);
	}

}

[Serializable]
class RecordingData {
	public string levelName;
	public float totalTime;
	public List<CarState> states;
}
	
[Serializable]
public struct CarState {
	public float timeElapsed;
	public SerializableVector3 velocity;
	public SerializableVector3 position;
	public SerializableQuaternion rotation;
	public float steering;
}
	
[Serializable]
public struct SerializableVector3 {
	public float x;
	public float y;
	public float z;

	public SerializableVector3(float rX, float rY, float rZ) {
		x = rX;
		y = rY;
		z = rZ;
	}

	public override string ToString() {
		return String.Format("[{0}, {1}, {2}]", x, y, z);
	}

	public static implicit operator Vector3(SerializableVector3 rValue) {
		return new Vector3(rValue.x, rValue.y, rValue.z);
	}

	public static implicit operator SerializableVector3(Vector3 rValue) {
		return new SerializableVector3(rValue.x, rValue.y, rValue.z);
	}
}
	
[Serializable]
public struct SerializableQuaternion {
	public float x;
	public float y;
	public float z;
	public float w;

	public SerializableQuaternion(float rX, float rY, float rZ, float rW) {
		x = rX;
		y = rY;
		z = rZ;
		w = rW;
	}

	public override string ToString() {
		return String.Format("[{0}, {1}, {2}, {3}]", x, y, z, w);
	}

	public static implicit operator Quaternion(SerializableQuaternion rValue) {
		return new Quaternion(rValue.x, rValue.y, rValue.z, rValue.w);
	}

	public static implicit operator SerializableQuaternion(Quaternion rValue) {
		return new SerializableQuaternion(rValue.x, rValue.y, rValue.z, rValue.w);
	}

}